namespace audit.twitter.Processors
{
    using System;

    using audit.twitter.DTO;
    using audit.twitter.OAuth;

    using NLog;

    public class Retweeter : ProcessorBase, IProcessor
    {
        public Retweeter(OauthAuthorization authorization)
        {
            this.client = new TwitterClient();
            this.client.Authenticate(authorization);
        }

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly TwitterClient client;

        public override void Process(string jsonText)
        {
            try
            {
                StreamingResponse streamingResponse = TweetParser.ParseStreamingResponse(jsonText);
                if (streamingResponse == null)
                {
                    logger.Error("Tweet recieved from twitter could not be proccess. raw data \n\t{0}", jsonText);
                    return;
                }

                logger.Info("tweet: " + streamingResponse.text);
                if (streamingResponse.LocationType == TwitterLocationType.NoLocation)
                {
                    var userTag = streamingResponse.user.screen_name;

                    string tweetUser = this.client.TweetUser(userTag, "Thanks for tweeting.");
                    logger.Info("tweet sent back: " + tweetUser);
                }
            }
            catch (Exception ex)
            {
                logger.ErrorException("Error Processing tweet: ", ex);
                // Dont error out.  keep processing
            }
        }

        public override void Report()
        {
            // could add something here
        }
    }
}