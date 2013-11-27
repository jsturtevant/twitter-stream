namespace twitter.tests.Twitter
{
    using System.Linq;

    using audit.twitter.DTO;
    using audit.twitter.DTO.Search;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using twitter.tests.Properties;

    [TestClass]
    public class TwitterDTOTests
    {
        private Status statusWithLocation;

        private Status statusWithoutLocation;

        public TwitterDTOTests()
        {
            SearchResponse locationData = TweetParser.ParseSearchResponse(Resources.twitter_json_location);
            SearchResponse noLocationData = TweetParser.ParseSearchResponse(Resources.twitter_json_nolocation);

            this.statusWithLocation = locationData.statuses.First();
            this.statusWithoutLocation = noLocationData.statuses.First();
        }


       [TestMethod]
       public void  TestLocationType()
       {
           Assert.AreEqual(TwitterLocationType.Point, this.statusWithLocation.LocationType);
       }

       [TestMethod]
       public void TestLocationType_NoLocation()
       {
           Assert.AreEqual(TwitterLocationType.NoLocation, this.statusWithoutLocation.LocationType);
       }

       [TestMethod]
       public void TestLocationType_Unkown()
       {
           this.statusWithLocation.coordinates.type = "";
           Assert.AreEqual(TwitterLocationType.Unknown, this.statusWithLocation.LocationType);
       }

        [TestMethod]
        public void TestParsingStream()
        {
            StreamingResponse streamingResponse = TweetParser.ParseStreamingResponse(Resources.twittemr_json_stream);

            Assert.AreEqual("tenerife_golf", streamingResponse.user.screen_name);
        }
    }
}
