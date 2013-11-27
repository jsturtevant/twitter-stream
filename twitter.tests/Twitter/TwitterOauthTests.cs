namespace twitter.tests.Twitter
{
    using System.Collections.Generic;

    using audit.twitter;
    using audit.twitter.OAuth;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TwitterOauthTests
    {
        [TestMethod]
        public void EncodeKeyAndSecret()
        {
            // sample from twitter
            // https://dev.twitter.com/docs/auth/application-only-auth
            var key = "xvz1evFS4wEEPTGEFPHBog";
            var secret = "L8qq9PZyRg6ieKGEKhZolGC0vJWLw8iEJ88DRdyOg";
            string encodeConsumerKeyAndSecret = TwitterClient.EncodeConsumerKeyAndSecret(key, secret);
            Assert.AreEqual("eHZ6MWV2RlM0d0VFUFRHRUZQSEJvZzpMOHFxOVBaeVJnNmllS0dFS2hab2xHQzB2SldMdzhpRUo4OERSZHlPZw==",
                encodeConsumerKeyAndSecret);
        }

        [TestMethod]
        public void PercentEncoding()
        {
            Assert.AreEqual("%21", Rfc3986.EscapeUriDataString("!"));
        }

        [TestMethod]
        public void ParameterString()
        {
            var oAuthParameters = TestOAuthParameters();

            Assert.AreEqual("include_entities=true&oauth_consumer_key=xvz1evFS4wEEPTGEFPHBog&oauth_nonce=kYjzVBB8Y0ZFabxSWbWovY3uYSQ2pTgmZeNu2VS4cg&oauth_signature_method=HMAC-SHA1&oauth_timestamp=1318622958&oauth_token=370773112-GmHxMAgYyLbNEtIKZeRNFsMKPR9EyMZeS9weJAEb&oauth_version=1.0&status=Hello%20Ladies%20%2B%20Gentlemen%2C%20a%20signed%20OAuth%20request%21", 
                oAuthParameters.ToString());
        }

        private static OAuthParameters TestOAuthParameters()
        {
            KeyValuePair<string, string> body = new KeyValuePair<string, string>(
                "status", "Hello Ladies + Gentlemen, a signed OAuth request!");
            KeyValuePair<string, string> urlParam = new KeyValuePair<string, string>("include_entities", "true");

            // sample from twitter
            OauthAuthorization oauthAuthorization = new OauthAuthorization(
                "xvz1evFS4wEEPTGEFPHBog", "kAcSOqF21Fu85e7zjz7ZN2U4ZRhfV3WpwPAoE3Z7kBw", "370773112-GmHxMAgYyLbNEtIKZeRNFsMKPR9EyMZeS9weJAEb", "LswwdoUaIvS8ltyTt5jkRh4J50vUPVVHtR2YPi5kE");

            OAuthParameters oAuthParameters = new OAuthParameters(
                oauthAuthorization,
                "https://api.twitter.com/1/statuses/update.json",
                new List<KeyValuePair<string, string>>() { body },
                new List<KeyValuePair<string, string>>() { urlParam });

            // override with sample data.
            oAuthParameters.TimeStamp = "1318622958";
            oAuthParameters.Nonce = "kYjzVBB8Y0ZFabxSWbWovY3uYSQ2pTgmZeNu2VS4cg";
            return oAuthParameters;
        }

        [TestMethod]
        public void SignatureBaseString()
        {
            OAuthParameters testOAuthParameters = TestOAuthParameters();

            Assert.AreEqual("POST&https%3A%2F%2Fapi.twitter.com%2F1%2Fstatuses%2Fupdate.json&include_entities%3Dtrue%26oauth_consumer_key%3Dxvz1evFS4wEEPTGEFPHBog%26oauth_nonce%3DkYjzVBB8Y0ZFabxSWbWovY3uYSQ2pTgmZeNu2VS4cg%26oauth_signature_method%3DHMAC-SHA1%26oauth_timestamp%3D1318622958%26oauth_token%3D370773112-GmHxMAgYyLbNEtIKZeRNFsMKPR9EyMZeS9weJAEb%26oauth_version%3D1.0%26status%3DHello%2520Ladies%2520%252B%2520Gentlemen%252C%2520a%2520signed%2520OAuth%2520request%2521", 
                testOAuthParameters.GetSignatureBaseString());
        }

        [TestMethod]
        public void Signature()
        {
            OAuthParameters testOAuthParameters = TestOAuthParameters();

            Assert.AreEqual("tnnArxj06cWHq44gCs1OSKk/jLY=", testOAuthParameters.CreateOauthSignature());
        }


        [TestMethod]
        public void OAuthHeader()
        {
            OAuthParameters testOAuthParameters = TestOAuthParameters();

            OauthHeader oauthHeader = new OauthHeader(testOAuthParameters);

            Assert.AreEqual("OAuth oauth_consumer_key=\"xvz1evFS4wEEPTGEFPHBog\", oauth_nonce=\"kYjzVBB8Y0ZFabxSWbWovY3uYSQ2pTgmZeNu2VS4cg\", oauth_signature=\"tnnArxj06cWHq44gCs1OSKk%2FjLY%3D\", oauth_signature_method=\"HMAC-SHA1\", oauth_timestamp=\"1318622958\", oauth_token=\"370773112-GmHxMAgYyLbNEtIKZeRNFsMKPR9EyMZeS9weJAEb\", oauth_version=\"1.0\"" ,
                oauthHeader.ToString());
        }
    }
}
