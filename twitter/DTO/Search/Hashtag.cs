namespace audit.twitter.DTO.Search
{
    using System.Collections.Generic;

    public class Hashtag
    {
        public string text { get; set; }
        public List<int> indices { get; set; }
    }
}