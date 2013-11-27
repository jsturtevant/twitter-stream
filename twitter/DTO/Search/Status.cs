namespace audit.twitter.DTO.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    public abstract class TweetBase
    {
        public string created_at { get; set; }
        public long id { get; set; }
        public string id_str { get; set; }
        public string text { get; set; }
        public string source { get; set; }
        public bool truncated { get; set; }
        public object in_reply_to_status_id { get; set; }
        public object in_reply_to_status_id_str { get; set; }
        public object in_reply_to_user_id { get; set; }
        public object in_reply_to_user_id_str { get; set; }
        public object in_reply_to_screen_name { get; set; }
        public User user { get; set; }
        public Geo geo { get; set; }
        public Coordinates coordinates { get; set; }
        public Place place { get; set; }
        public object contributors { get; set; }
        public int retweet_count { get; set; }
        public int favorite_count { get; set; }
        public Entities2 entities { get; set; }
        public bool favorited { get; set; }
        public bool retweeted { get; set; }
        public string lang { get; set; }

        // Below are custom functions.
        [JsonIgnore]
        public TwitterLocationType LocationType
        {
            get
            {
                if (this.coordinates == null || this.coordinates.type == null)
                {
                    return TwitterLocationType.NoLocation;
                }

                switch (this.coordinates.type.ToLower())
                {
                    case "point":
                        return TwitterLocationType.Point;

                    default:
                        return TwitterLocationType.Unknown;
                }
            }
        }

        [JsonIgnore]
        public double Latitude
        {
            get
            {
                if (this.coordinates == null || this.coordinates.coordinates.Count != 2) return -1;
                return this.coordinates.coordinates.Last();
            }
        }

        [JsonIgnore]
        public double Longitude
        {
            get
            {
                if (this.coordinates == null || this.coordinates.coordinates.Count != 2) return -1;
                return this.coordinates.coordinates.First();
            }
        }

        [JsonIgnore]
        public bool HasPicture
        {
            get
            {
                if (this.entities == null) return false;
                if (this.entities.media == null || !this.entities.media.Any()) return false;

                if (this.entities.media.Any(m => m.MediaType == MediaType.Photo)) return true;

                return false;
            }

        }

        [JsonIgnore]
        public IEnumerable<Medium> Pictures
        {
            get
            {
                return this.entities.media.Where(m => m.MediaType == MediaType.Photo);
            }
        }

        [JsonIgnore]
        public DateTime CreatedAt
        {
            get
            {
                var datetime = DateTime.MinValue;
                DateTime.TryParse(this.created_at, out datetime);

                return datetime;
            }

        }
    }

    public class Status : TweetBase
    {
        public Metadata metadata { get; set; }
       
    }
}