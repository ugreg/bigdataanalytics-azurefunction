using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class TweetModel
    {   
        [JsonProperty("trends")]
        public List<Tweet> trends { get; set; }

    }

    class Tweet { 
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("url")]
        public string url { get; set; }

        [JsonProperty("promoted_content")]
        public string promoted_content { get; set;}

        [JsonProperty("query")]
        public string query { get; set; }

        [JsonProperty("tweet_volume")]
        public string tweet_volume { get; set; }

    }

    class TweetAuthModel
    {
        [JsonProperty("token_type")]
        public string token_type { get; set; }

        [JsonProperty("access_token")]
        public string access_token { get; set; }
    }
}
