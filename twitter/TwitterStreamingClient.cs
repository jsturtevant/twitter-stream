namespace audit.twitter
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading;


    using NLog;

    using audit.twitter.OAuth;
    using audit.twitter.Processors;

    public class TwitterStreamingClient
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly OauthAuthorization authorization;

        private IProcessor processor;

        private bool cancel = false;

        public TwitterStreamingClient(OauthAuthorization authorization, IProcessor processor)
        {
            this.authorization = authorization;
            this.processor = processor;
        }

        public void StartStreaming(string hashTag)
        {
            new Thread(() => this.Stream(hashTag)).Start();
        }

        private void Stream(string hashTag)
        {
            logger.Info("Start Streaming Hash: {0}", hashTag);
            string resourceUrl = Twitter.STREAMING_API_V1_1 + "statuses/filter.json";

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(resourceUrl);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Timeout = -1;

            // This is the content
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("track", hashTag));

            // sign Request
            OAuthParameters oAuthParameters = new OAuthParameters(this.authorization,
                resourceUrl,
                postData,
                new List<KeyValuePair<string, string>>());
            OauthHeader oauthHeader = new OauthHeader(oAuthParameters);
            webRequest.Headers.Add("Authorization", oauthHeader.ToString());

            webRequest.AddPostData(postData);

            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            var webResponse = (HttpWebResponse)webRequest.GetResponse();
            var responseStream = new StreamReader(webResponse.GetResponseStream(), encode);

            logger.Info("Stream Initialized. Entering listening mode...");

            this.processor.SetStartTime();

            //Read the stream.
            while (!this.cancel)
            {
                string jsonText = responseStream.ReadLine();
                logger.Debug("Recieved from twitter:" + jsonText);

                this.processor.Process(jsonText);
            }

            this.processor.SetEndTime();
           
            //Abort is needed or responseStream.Close() will hang.
            webRequest.Abort();
            responseStream.Close();
            webResponse.Close();

            this.Canceled = true;
        }

        public void StopStreaming()
        {
            this.cancel = true;

            while (this.Canceled == false)
            {
                // wait for the system to shut down.
            }
        }

        protected bool Canceled { get; set; }
    }
}