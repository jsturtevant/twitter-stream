namespace audit.twitter.OAuth
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public class OAuthParameters
    {
        public OAuthParameters(OauthAuthorization authorization, string resourceUrl, List<KeyValuePair<string, string>> postBody, List<KeyValuePair<string, string>> urlParameters)
        {
            this.Authorization = authorization;
            this.PostBody = postBody;
            this.ResourceUrl = resourceUrl;
            this.UrlParameters = urlParameters;

            // Defualt values
            this.OAuthVersion = "1.0";
            this.SignatureMethod = "HMAC-SHA1";
            this.Nonce = Convert.ToBase64String(
                new ASCIIEncoding().GetBytes(
                    DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture)));
            this.TimeStamp = CreateOauthTimeStap();
        }

        public OAuthParameters(OauthAuthorization authorization, string resourceUrl, List<KeyValuePair<string, string>> postBody)
            : this(authorization, resourceUrl, postBody, new List<KeyValuePair<string, string>>())
        {

        }

        protected string ResourceUrl { get; set; }

        public OauthAuthorization Authorization { get; set; }

        public string Nonce { get; set; }

        public string SignatureMethod { get; set; }

        public string TimeStamp { get; set; }

        public string OAuthVersion { get; set; }

        public List<KeyValuePair<string, string>> PostBody { get; set; }

        public List<KeyValuePair<string, string>> UrlParameters { get; set; }

        public override string ToString()
        {
            // Signature format
            var parameterString = string.Format("{7}oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}"
                                                + "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}{6}",
                this.Authorization.ConsumerKey,
                this.Nonce,
                this.SignatureMethod,
                this.TimeStamp,
                this.Authorization.AccessToken,
                this.OAuthVersion,
                this.GetPostBody(),
                this.GetUrlParameters());

            return parameterString;
        }

        private string GetUrlParameters()
        {
            if (!this.UrlParameters.Any())
            {
                return string.Empty;

            }
            else
            {
                IEnumerable<string> urlParmetersList = 
                    this.UrlParameters.Select(k => Rfc3986.EscapeUriDataString(k.Key) 
                                    + "=" + Rfc3986.EscapeUriDataString(k.Value));
                string parameters = string.Join("&", urlParmetersList);

                return parameters + "&";
            }
        }

        private string GetPostBody()
        {
            if (!this.PostBody.Any())
            {
                return string.Empty;

            }
            else
            {
                IEnumerable<string> postBodyList = this.PostBody.Select(k => Rfc3986.EscapeUriDataString(k.Key) + "=" + Rfc3986.EscapeUriDataString(k.Value));
                string postBody = string.Join("&", postBodyList);

                return "&" + postBody;
            }
        }

        public string GetSignatureBaseString()
        {
            string signatureBaseString = string.Concat("POST&", Rfc3986.EscapeUriDataString(this.ResourceUrl), "&", Rfc3986.EscapeUriDataString(this.ToString()));
            return signatureBaseString;
        }

        public string CreateOauthSignature()
        {
            var signingKey = string.Concat(Rfc3986.EscapeUriDataString(this.Authorization.ConsumerSecret), "&", Rfc3986.EscapeUriDataString(this.Authorization.AccessTokenSecret));

            var signatureBaseString = this.GetSignatureBaseString();
            using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(signingKey)))
            {
                return Convert.ToBase64String(hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(signatureBaseString)));
            }
        }

        private static string CreateOauthTimeStap()
        {
            var timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64(timeSpan.TotalSeconds).ToString(CultureInfo.InvariantCulture);
        }
    }
}