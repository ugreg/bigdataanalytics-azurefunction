using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class NewsModel
    {
        [JsonProperty("status")]
        public string status { get; }
        [JsonProperty("source")]
        public string source { get; }
        [JsonProperty("sortBy")]
        public string sortBy { get; }
        [JsonProperty("articles")]
        public List<Article> articles { get; }

        public class Article
        {
            [JsonProperty("author")]
            public string author { get; }
            [JsonProperty("title")]
            public string title { get; }
            [JsonProperty("description")]
            public string description { get; }
            [JsonProperty("url")]
            public string url { get; }
            [JsonProperty("urlToImage")]
            public string urlToImage { get; }
            [JsonProperty("publishedAt")]
            public string publishedAt { get; }
        }
    }    
}
