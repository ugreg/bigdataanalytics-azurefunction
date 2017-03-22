using Newtonsoft.Json;
using System;
using System.Net;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting news json. . .");
            NewsService ns = new NewsService();
            string allDaNews = ns.getNews();

            NewsModel newsModel = new NewsModel();
            newsModel = JsonConvert.DeserializeObject<NewsModel>(allDaNews);
        }

        public class NewsService
        {
            private static String apiKey = "3e99c92f3b244bc4ae9693eb2f5f97fb";
            private String enpoint = $"https://newsapi.org/v1/articles?source=cnn&sortBy=top&apiKey={apiKey}";

            public NewsService()
            {

            }

            public string getNews()
            {
                try
                {
                    HttpWebRequest request = WebRequest.Create(enpoint) as HttpWebRequest;
                    request.Headers.Add("Garnet", "Amethyst");
                    request.Headers.Add("AndPearl", "AndSteven");

                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            Stream stream = response.GetResponseStream();
                            StreamReader sr = new StreamReader(stream);
                            string result = "";
                            result = sr.ReadToEnd();

                            return result;
                        }

                        else
                        {
                            return "-1";
                        }
                    }
                }
                catch (Exception e)
                {
                }

                return "-1";
            }
        }
    }
}
