using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace audit.twitter.DTO
{
    using audit.twitter.DTO.Search;

    public class User
        {
            public int id { get; set; }
            public string id_str { get; set; }
            public string name { get; set; }
            public string screen_name { get; set; }
            public string location { get; set; }
            public object url { get; set; }
            public object description { get; set; }
            public bool @protected { get; set; }
            public int followers_count { get; set; }
            public int friends_count { get; set; }
            public int listed_count { get; set; }
            public string created_at { get; set; }
            public int favourites_count { get; set; }
            public int? utc_offset { get; set; }
            public string time_zone { get; set; }
            public bool geo_enabled { get; set; }
            public bool verified { get; set; }
            public int statuses_count { get; set; }
            public string lang { get; set; }
            public bool contributors_enabled { get; set; }
            public bool is_translator { get; set; }
            public string profile_background_color { get; set; }
            public string profile_background_image_url { get; set; }
            public string profile_background_image_url_https { get; set; }
            public bool profile_background_tile { get; set; }
            public string profile_image_url { get; set; }
            public string profile_image_url_https { get; set; }
            public string profile_banner_url { get; set; }
            public string profile_link_color { get; set; }
            public string profile_sidebar_border_color { get; set; }
            public string profile_sidebar_fill_color { get; set; }
            public string profile_text_color { get; set; }
            public bool profile_use_background_image { get; set; }
            public bool default_profile { get; set; }
            public bool default_profile_image { get; set; }
            public object following { get; set; }
            public object follow_request_sent { get; set; }
            public object notifications { get; set; }
        }

        public class User2
        {
            public int id { get; set; }
            public string id_str { get; set; }
            public string name { get; set; }
            public string screen_name { get; set; }
            public string location { get; set; }
            public object url { get; set; }
            public string description { get; set; }
            public bool @protected { get; set; }
            public int followers_count { get; set; }
            public int friends_count { get; set; }
            public int listed_count { get; set; }
            public string created_at { get; set; }
            public int favourites_count { get; set; }
            public string utc_offset { get; set; }
            public string time_zone { get; set; }
            public bool geo_enabled { get; set; }
            public bool verified { get; set; }
            public int statuses_count { get; set; }
            public string lang { get; set; }
            public bool contributors_enabled { get; set; }
            public bool is_translator { get; set; }
            public string profile_background_color { get; set; }
            public string profile_background_image_url { get; set; }
            public string profile_background_image_url_https { get; set; }
            public bool profile_background_tile { get; set; }
            public string profile_image_url { get; set; }
            public string profile_image_url_https { get; set; }
            public string profile_banner_url { get; set; }
            public string profile_link_color { get; set; }
            public string profile_sidebar_border_color { get; set; }
            public string profile_sidebar_fill_color { get; set; }
            public string profile_text_color { get; set; }
            public bool profile_use_background_image { get; set; }
            public bool default_profile { get; set; }
            public bool default_profile_image { get; set; }
            public string following { get; set; }
            public string follow_request_sent { get; set; }
            public string notifications { get; set; }
        }

        public class UserMention
        {
            public string screen_name { get; set; }
            public string name { get; set; }
            public int id { get; set; }
            public string id_str { get; set; }
            public List<int> indices { get; set; }
        }

        public class Medium2
        {
            public int w { get; set; }
            public int h { get; set; }
            public string resize { get; set; }
        }

        public class Large
        {
            public int w { get; set; }
            public int h { get; set; }
            public string resize { get; set; }
        }

        public class Small
        {
            public int w { get; set; }
            public int h { get; set; }
            public string resize { get; set; }
        }

        public class Thumb
        {
            public int w { get; set; }
            public int h { get; set; }
            public string resize { get; set; }
        }

        public class Sizes
        {
            public Medium2 medium { get; set; }
            public Large large { get; set; }
            public Small small { get; set; }
            public Thumb thumb { get; set; }
        }

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
        }

        public class Entities
        {
            public List<object> hashtags { get; set; }
            public List<object> symbols { get; set; }
            public List<object> urls { get; set; }
            public List<UserMention> user_mentions { get; set; }
            public List<Medium> media { get; set; }
        }

        public class RetweetedStatus
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
            public User2 user { get; set; }
            public object geo { get; set; }
            public object coordinates { get; set; }
            public object place { get; set; }
            public object contributors { get; set; }
            public int retweet_count { get; set; }
            public int favorite_count { get; set; }
            public Entities entities { get; set; }
            public bool favorited { get; set; }
            public bool retweeted { get; set; }
            public bool possibly_sensitive { get; set; }
            public string lang { get; set; }
        }

        public class UserMention2
        {
            public string screen_name { get; set; }
            public string name { get; set; }
            public int id { get; set; }
            public string id_str { get; set; }
            public List<int> indices { get; set; }
        }

        public class Medium4
        {
            public int w { get; set; }
            public int h { get; set; }
            public string resize { get; set; }
        }

        public class Large2
        {
            public int w { get; set; }
            public int h { get; set; }
            public string resize { get; set; }
        }

        public class Small2
        {
            public int w { get; set; }
            public int h { get; set; }
            public string resize { get; set; }
        }

        public class Thumb2
        {
            public int w { get; set; }
            public int h { get; set; }
            public string resize { get; set; }
        }

        public class Sizes2
        {
            public Medium4 medium { get; set; }
            public Large2 large { get; set; }
            public Small2 small { get; set; }
            public Thumb2 thumb { get; set; }
        }

        public class Medium3
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
            public Sizes2 sizes { get; set; }
            public long source_status_id { get; set; }
            public string source_status_id_str { get; set; }
        }

        public class Entities2
        {
            public List<object> hashtags { get; set; }
            public List<object> symbols { get; set; }
            public List<object> urls { get; set; }
            public List<UserMention2> user_mentions { get; set; }
            public List<Medium3> media { get; set; }
        }

        public class StreamingResponse : TweetBase
        {
            public bool possibly_sensitive { get; set; }
            public string filter_level { get; set; }
            public RetweetedStatus retweeted_status { get; set; }
        }
    }
