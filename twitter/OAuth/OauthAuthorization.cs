namespace audit.twitter.OAuth
{
    public class OauthAuthorization
    {
        public OauthAuthorization(string consumerkey, string consumersecret, string accessToken, string accessTokenSecret)
        {
            this.ConsumerKey = consumerkey;
            this.ConsumerSecret = consumersecret;
            this.AccessToken = accessToken;
            this.AccessTokenSecret = accessTokenSecret;
        }

        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }

        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
    }
}