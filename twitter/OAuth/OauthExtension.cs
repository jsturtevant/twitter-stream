namespace audit.twitter.OAuth
{
    using System.Collections.Generic;
    using System.Net.Http;

    public static class OauthExtension
    {
        public static void SignRequest(this HttpRequestMessage message,List<KeyValuePair<string, string>> postData, OauthAuthorization authorization)
        {
            //message.Content.
            string oAuthHeader = BuildOAuthHeader(message.RequestUri.ToString(), postData, authorization);
            message.Headers.Add("Authorization", oAuthHeader);
        }

        private static string BuildOAuthHeader(string resourceUrl, List<KeyValuePair<string, string>> postData, OauthAuthorization authorization)
        {
            OAuthParameters oAuthParameters = new OAuthParameters(authorization, resourceUrl, postData);
            var header = new OauthHeader(oAuthParameters);
        
            return header.ToString();
        }
    }
}