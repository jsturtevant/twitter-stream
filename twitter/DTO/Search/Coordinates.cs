namespace audit.twitter.DTO.Search
{
    using System.Collections.Generic;

    public class Coordinates
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }
}