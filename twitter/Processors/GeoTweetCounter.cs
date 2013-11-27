namespace audit.twitter.Processors
{
    using System;

    using NLog;

    using audit.twitter.DTO;

    public class GeoTweetCounter : ProcessorBase, IProcessor
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public int TotalTweets = 0;
        public int GeoTweets = 0;


        public override void Process(string jsonText)
        {
            try
            {
                StreamingResponse streamingResponse = TweetParser.ParseStreamingResponse(jsonText);
                if (streamingResponse == null)
                {
                    logger.Error("Tweet recieved from twitter could not be proccess");
                    return;
                }

                this.TotalTweets++;

                logger.Info("tweet: " + streamingResponse.text);

                if (streamingResponse.LocationType != TwitterLocationType.NoLocation)
                {
                    this.GeoTweets++;

                    logger.Info("****************************************************");
                    logger.Info("GEO tweet: " + streamingResponse.text);
                    logger.Info("Lat:" + streamingResponse.Latitude);
                    logger.Info("Long:" + streamingResponse.Longitude);
                    logger.Info("****************************************************");
                }
            }
            catch (Exception ex)
            {
                logger.ErrorException("Error Processing tweet: ", ex);
                // Dont error out.  keep processing
            }
        }
        
        public decimal PercentageGeo
        {
            get
            {
                if (this.TotalTweets == 0) return 0;

                return (GeoTweets / TotalTweets) * 100;
            }
        }

        public override void Report()
        {
            logger.Info("****************************");
            logger.Info("Total Tweets: " + this.TotalTweets);
            logger.Info("Total Geo Tweets: " + this.GeoTweets);
            logger.Info("Percentage Geo: " + this.PercentageGeo + "%");
            logger.Info("Time: " + this.TotalTime);
            logger.Info("****************************");
        }
    }
}