namespace audit.twitter.DTO.Search
{
    using System.Collections.Generic;

    public class SearchResponse
    {
        public List<Status> statuses { get; set; }
        public SearchMetadata search_metadata { get; set; }
    }
}