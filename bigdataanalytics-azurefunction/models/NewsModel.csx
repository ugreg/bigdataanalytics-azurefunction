using System.Collections.Generic;
using Newtonsoft.Json;

public class NewsModel
{
    [JsonProperty("status")]
    public string status { get; set; }
    [JsonProperty("source")]
    public string source { get; set; }
    [JsonProperty("sortBy")]
    public string sortBy { get; set; }
    [JsonProperty("articles")]
    public List<Article> articles { get; set; }

    public class Article
    {
        [JsonProperty("author")]
        public string author { get; set; }
        [JsonProperty("title")]
        public string title { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
        [JsonProperty("url")]
        public string url { get; set; }
        [JsonProperty("urlToImage")]
        public string urlToImage { get; set; }
        [JsonProperty("publishedAt")]
        public string publishedAt { get; set; }
    }
}