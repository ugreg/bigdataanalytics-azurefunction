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
            ns.getNews();

            /*
             string responseAsString = null;
            responseAsString = await response.Content.ReadAsStringAsync();
            //OutputField.Text = responseAsString;

            //TODO: Add check to see if API Call recieved a response

            RootObject rootObject = null;

            rootObject = JsonConvert.DeserializeObject<RootObject>(responseAsString);
             */

            NewsModel model = new NewsModel();
        }

        public class NewsService
        {
            private static String apiKey = "3e99c92f3b244bc4ae9693eb2f5f97fb";
            private String enpoint = $"https://newsapi.org/v1/articles?source=cnn&sortBy=top&apiKey=3e99c92f3b244bc4ae9693eb2f5f97fb";

            public NewsService()
            {

            }

            public void getNews()
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
                            var result = sr.ReadToEnd();
                        }
                    }
                }
                catch (Exception e)
                {
                }
            }
        }
    }
}
