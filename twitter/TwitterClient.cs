
namespace audit.twitter
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.IO.Compression;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System;
    using System.Threading.Tasks;
    using NLog;

    using Newtonsoft.Json;

    using audit.twitter.DTO;
    using audit.twitter.DTO.Errors;
    using audit.twitter.DTO.Search;
    using audit.twitter.OAuth;

    public static class Twitter
    {
        public const string STREAMING_API_V1_1 = "https://stream.twitter.com/1.1/";

        public const string API_V1_1 = "https://api.twitter.com/1.1/";
    }


    public class TwitterClient : ITwitterClient
    {
        readonly HttpClient _client = new HttpClient();
        private static Logger logger = LogManager.GetCurrentClassLogger();
 
        private OauthAuthorization authorization;
        private AccessToken _accessToken;

        public TwitterClient(string baseAddress)
        {
            logger.Debug("Initializing twitter client");
            this._client.BaseAddress = new Uri(baseAddress);
            _client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);

        }

        public TwitterClient()
        {
            this._client.BaseAddress = new Uri(Twitter.API_V1_1);
        }

        public void Authenticate( OauthAuthorization authorization)
        {
            logger.Debug("Authenticating twitter client");
            this.authorization = authorization;

            string encodedKey = EncodeConsumerKeyAndSecret(authorization.ConsumerKey,authorization.ConsumerSecret);

            // create request
            var keyValuePair = new KeyValuePair<string, string>("grant_type", "client_credentials");
            HttpContent httpContent = new FormUrlEncodedContent(new Collection<KeyValuePair<string, string>>(){keyValuePair});
            httpContent.Headers.ContentType.CharSet = "UTF-8";
            this._client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
            this._client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedKey);

            // post request
            HttpResponseMessage postAsync = _client.PostAsync("/oauth2/token", httpContent).Result;
            HandleErrors(postAsync);

            // Get access token
            Stream readAsStringAsync = postAsync.Content.ReadAsStreamAsync().Result;
            string json = Decompress(readAsStringAsync);
            this._accessToken = JsonConvert.DeserializeObject<AccessToken>(json);

            this._client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(this._accessToken.token_type, this._accessToken.access_token);

            logger.Debug("Authenticated");
        }

        public async Task<SearchResponse> GetHashTag(string hashtag)
        {
            HttpResponseMessage response = await _client.GetAsync("search/tweets.json?q=%23" + hashtag);                
            HandleErrors(response);

            // decompress
            Stream responseBody = await response.Content.ReadAsStreamAsync();
            string json = Decompress(responseBody);

            SearchResponse searchResponse = TweetParser.ParseSearchResponse(json);
            return searchResponse;
        }
     
        public void Tweet(string message)
        {
            string resourceUrl = Twitter.API_V1_1 + "statuses/update.json";
            if (string.IsNullOrEmpty(message))
            {
                throw new TwitterClientException("Tweet cannot be empty.");
            }

            if (message.Length > 140)
            {
                throw new TwitterClientException("Tweet cannot be longer than 140 characters.");
            }

            // This is the content
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("status",  message));

            // Load content
            HttpContent content = new FormUrlEncodedContent(postData);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            // create a new Request Message
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, resourceUrl);
            requestMessage.Content = content;

            // Sign Request
            requestMessage.SignRequest(postData, this.authorization);

            var response = _client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead).Result;
            HandleErrors(response);
        }

        private static void HandleErrors(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.Forbidden)
            {
                Stream readAsStringAsync = response.Content.ReadAsStreamAsync().Result;
                string accesstoken = Decompress(readAsStringAsync);
                var errors = JsonConvert.DeserializeObject<TwitterErrors>(accesstoken);

                logger.Error(string.Format("REQUEST ERROR: {0}", HttpStatusCode.BadRequest));
                foreach (var twitterError in errors.errors)
                {
                    logger.Error(string.Format("Error {0}: {1}", twitterError.code, twitterError.message));
                }

                throw new TwitterClientException("Unable to process request.", errors);
            }
        }

        public static string EncodeConsumerKeyAndSecret(string key, string secret)
        {
            string Consumerkey = WebUtility.UrlEncode(key);
            string Consumersecret = WebUtility.UrlEncode(secret);

            byte[] toEncodeAsBytes = Encoding.ASCII.GetBytes(Consumerkey + ":" + secret);
            string returnValue = Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        static string Decompress(Stream streamToDecompress)
        {
            string res;
            using (var decompress = new GZipStream(streamToDecompress, CompressionMode.Decompress))
            using (var sr = new StreamReader(decompress))
            {
                res = sr.ReadToEnd();
            }
            return res;
        }

        public string TweetUser(string username, string message)
        {
            if (username == null || message == null)
            {
                throw new ArgumentNullException("Please supply a user name and message.");
            }

            if (!username.StartsWith("@"))
            {
                username = username.Insert(0, "@");
            }
            string tweet = string.Format("{0} {1}", username, message);
            this.Tweet(tweet);
            return tweet;
        }
    }
}
