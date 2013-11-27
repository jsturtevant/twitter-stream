namespace twitter.tests.Twitter
{
    using audit.twitter;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TwitterClientTests
    {
        [TestMethod]
        [ExpectedException(typeof(TwitterClientException), "Tweet cannot be longer than 140 characters.")]
        public void Tweet_NotMoreThan140_ThrowsException()
        {
            var twitterClient = new TwitterClient(Twitter.API_V1_1);
            string messsage = new string('t', 145);
            twitterClient.Tweet(messsage);
        }

        [TestMethod]
        [ExpectedException(typeof(TwitterClientException), "Tweet cannot be empty.")]
        public void Tweet_Null_ThrowsException()
        {
            var twitterClient = new TwitterClient(Twitter.API_V1_1);
            twitterClient.Tweet(null);
        }

        [TestMethod]
        [ExpectedException(typeof(TwitterClientException), "Tweet cannot be empty.")]
        public void Tweet_Emtpy_ThrowsException()
        {
            var twitterClient = new TwitterClient(Twitter.API_V1_1);
            twitterClient.Tweet(string.Empty);
        }
    }
}
