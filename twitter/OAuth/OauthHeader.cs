namespace audit.twitter.OAuth
{
    using System;

    public class OauthHeader
    {
        public OauthHeader(OAuthParameters oAuthParameters)
        {
            this.parameters = oAuthParameters;
        }

        private OAuthParameters parameters { get; set; }

        public override string ToString()
        {
            //Return auth header
            return string.Format("OAuth " +
                                 "oauth_consumer_key=\"{0}\","
                                 + " oauth_nonce=\"{1}\","
                                 + " oauth_signature=\"{2}\", "
                                 + "oauth_signature_method=\"{3}\", "
                                 + "oauth_timestamp=\"{4}\","
                                 + " oauth_token=\"{5}\", "
                                 + "oauth_version=\"{6}\"",
                Rfc3986.EscapeUriDataString(this.parameters.Authorization.ConsumerKey),
                Rfc3986.EscapeUriDataString(this.parameters.Nonce),
                Rfc3986.EscapeUriDataString(this.parameters.CreateOauthSignature()),
                Rfc3986.EscapeUriDataString(this.parameters.SignatureMethod),
                Rfc3986.EscapeUriDataString(this.parameters.TimeStamp),
                Rfc3986.EscapeUriDataString(this.parameters.Authorization.AccessToken),
                Rfc3986.EscapeUriDataString(this.parameters.OAuthVersion)
                );
        }
    }
}