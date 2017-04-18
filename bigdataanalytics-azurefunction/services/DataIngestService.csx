#load "DataLakeService.csx"
#load "..\models\NewsModel.csx"
#load "..\models\TweetModel.csx"

using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

public class DataIngestService
{

    private static String _newsApiKey = "3e99c92f3b244bc4ae9693eb2f5f97fb";
    private String _newsApiEndpoint = $"https://newsapi.org/v1/articles?source=techcrunch&sortBy=top&apiKey={_newsApiKey}";
    private DataLakeService _dataLakeService;
    private NewsModel _news;
    private String twitterApiEndpoint = "https://api.twitter.com";

    public DataIngestService()
    {
        _dataLakeService = new DataLakeService();
        string rawNews = this.getNews();

        NewsModel _news = JsonConvert.DeserializeObject<NewsModel>(rawNews);
        
        //to authenticate against the twitter API
        // TweetAuthModel bearer = JsonConvert.DeserializeObject<TweetAuthModel>(twitterAuth());
            
        // var tweetLine = getTweets(bearer.access_token);
        //Console.WriteLine(tweetLine);
        //TweetModel listOfTweets = JsonConvert.DeserializeObject<TweetModel>(tweetLine);

        this.writeDataToFile(rawNews, "newsFile.txt");
        // this.writeDataToFile(tweetLine, "tweetFile.txt");

        _dataLakeService.UploadFile("newsFile.txt", "remoteNewsFileV2.txt");
        // _dataLakeService.UploadFile("tweetFile.txt", "remoteTweetFileV2.txt");
    }

    public void writeDataToFile(string data, string fileName)
    {
        string workingDir = @"D:\home\site\wwwroot\bigdataanalytics-azurefunction\";

        string localFolderPath = workingDir + @"uploads\";
        string localFilePath = Path.Combine(localFolderPath, fileName);

        System.IO.File.WriteAllText(localFilePath, data);
    }

    public string getNews()
    {
        try
        {
            HttpWebRequest request = WebRequest.Create(_newsApiEndpoint) as HttpWebRequest;
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

        public string getTweets(String token)
        {
            // try
            // {
            //     HttpWebRequest request = WebRequest.Create(twitterApiEndpoint + "/1.1/trends/place.json?id=1") as HttpWebRequest;
            //     request.UserAgent = "USDX Twitter App v1.0.23";
            //     request.Headers.Add("Authorization", "Bearer " + token);

            //     using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            //     {
            //         if (response.StatusCode == HttpStatusCode.OK)
            //         {
            //             System.IO.Stream stream = response.GetResponseStream();
            //             StreamReader sr = new StreamReader(stream);
            //             string result = "";
            //             result = sr.ReadToEnd();

            //             return result;
            //         }

            //         else
            //         {
            //             return "-1";
            //         }
            //     }
            // }
            // catch (Exception e)
            // {
            //     Console.WriteLine(e.ToString());
            // }

            // return "-1";
            return "";
        }

        public string twitterAuth()
        {
            // string CONSUMER_KEY = "ShSD7YHGltyKUneMJXD0qLpP7";
            // string CONSUMER_SECRET = "Tr4o0mlxdKCFkt2FibQzzJVnLrGw8UsOBEuYZfta6WWv4MnTLI";
            // string ACCESS_TOKEN = "2835606410-SZu9XIKfUEv9c0s3Rm1CApvdEJSGeM2QELKlhgG";
            // string ACCESS_TOKEN_SECRET = "tTJFL4N8uDPwFRHEUx4eH2QtjnT4ZNKE63UrGKSLZTAoS";

            // try
            // {
            //     // app only auth https://dev.twitter.com/oauth/application-only
            //     HttpWebRequest request = WebRequest.Create(twitterApiEndpoint + "/oauth2/token") as HttpWebRequest;
            //     request.Method = "POST";
            //     request.UserAgent = "USDX Twitter App v1.0.23";

            //     var urlConsumerKey = WebUtility.UrlEncode(CONSUMER_KEY);
            //     var urlConsumerSecret = WebUtility.UrlEncode(CONSUMER_SECRET);

            //     var keySecret = urlConsumerKey + ':' + urlConsumerSecret.Replace("=","");

            //     var urlKeySecret = System.Text.Encoding.UTF8.GetBytes(keySecret);
            //     var base64Auth = System.Convert.ToBase64String(urlKeySecret);

            //     request.Headers.Add("Authorization", "Basic " + base64Auth);
            //     // request.Headers.Add("Garnet", "Amethyst");
            //     // request.Headers.Add("AndPearl", "AndSteven");
            //     request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            //     request.ContentLength = 29;
            //     request.AutomaticDecompression = DecompressionMethods.GZip;

            //     //  * BearerToken = "ConsumerKey" + ":" + "ConsumerSecretKey"
            //     //  request.Headers.Add("Authorization", "Basic " + CONSUMER_KEY + ":" + CONSUMER_SECRET);


            //     byte[] buf = Encoding.UTF8.GetBytes("grant_type=client_credentials");
            //     request.GetRequestStream().Write(buf, 0, buf.Length);

            //     using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            //     {
            //         if (response.StatusCode == HttpStatusCode.OK)
            //         {
            //             System.IO.Stream stream = response.GetResponseStream();
            //             StreamReader sr = new StreamReader(stream);
            //             string result = "";
            //             result = sr.ReadToEnd();

            //             return result;
            //         }

            //         else
            //         {
            //             return "-1";
            //         }
            //     }
            // }
            // catch (Exception e)
            // {
            //     Console.WriteLine(e.ToString());
            // }

            // return "-1";
            return "";
        }
    }
}
