namespace audit.twitter.DTO.Search
{
    using System.Collections.Generic;

    public class Medium
    {
        public long id { get; set; }
        public string id_str { get; set; }
        public List<int> indices { get; set; }
        public string media_url { get; set; }
        public string media_url_https { get; set; }
        public string url { get; set; }
        public string display_url { get; set; }
        public string expanded_url { get; set; }
        public string type { get; set; }
        public Sizes sizes { get; set; }

        public MediaType MediaType
        {
            get
            {
                switch (this.type)
                {
                    case "photo":
                        return MediaType.Photo;
                    default:
                        return MediaType.Unkown;
                }
            }
        }
    }

    public enum MediaType
    {
        Photo,

        Unkown
    }
}