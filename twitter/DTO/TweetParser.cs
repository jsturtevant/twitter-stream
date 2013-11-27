namespace audit.twitter.DTO
{
    using NLog;

    using Newtonsoft.Json;

    using audit.twitter.DTO.Search;

    public class TweetParser
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        public static SearchResponse ParseSearchResponse(string json)
        {
            logger.Debug("Parsing Search Response: {0}",json);
            SearchResponse searchResponse = JsonConvert.DeserializeObject<SearchResponse>(json);
            return searchResponse;
        }

        public static StreamingResponse ParseStreamingResponse(string json)
        {
            logger.Debug("Parsing Streaming Response: {0}", json);

            StreamingResponse streamingResponse = JsonConvert.DeserializeObject<StreamingResponse>(json);
            return streamingResponse;
        } 
    }
}
