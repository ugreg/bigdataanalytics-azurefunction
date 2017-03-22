using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class NewsModel
    {
        private class Article
        {
            public string author { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public string url { get; set; }
            public string urlToImage { get; set; }
            public string publishedAt { get; set; }
        }

        private class RootObject
        {
            public string status { get; set; }
            public string source { get; set; }
            public string sortBy { get; set; }
            public List<Article> articles { get; set; }
        }
    }    
}
