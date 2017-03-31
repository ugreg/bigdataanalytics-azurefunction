#load "..\models\NewsModel.csx"
#load "..\models\TweetModel.csx"

using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

public class DataIngestService
{

    private static String newsApiKey = "3e99c92f3b244bc4ae9693eb2f5f97fb";
    private String newsApiEndpoint = $"https://newsapi.org/v1/articles?source=techcrunch&sortBy=top&apiKey={newsApiKey}";

    public DataIngestService()
    {

    }

    public void ingestAllTheThings()
    {

    }

    public string getNews()
    {
        try
        {
            HttpWebRequest request = WebRequest.Create(newsApiEndpoint) as HttpWebRequest;
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

    public void getTweets()
    {

    }
}
