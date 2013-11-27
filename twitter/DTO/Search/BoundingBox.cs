namespace audit.twitter.DTO.Search
{
    using System.Collections.Generic;

    public class BoundingBox
    {
        public string type { get; set; }
        public List<List<List<double>>> coordinates { get; set; }
    }
}