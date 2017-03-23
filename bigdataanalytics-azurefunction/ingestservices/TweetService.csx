#load "..\models\TweetModel.csx"

using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

public class TweetService
{
    private static String apiKey = "3e99c92f3b244bc4ae9693eb2f5f97fb";
    private String restEndpoint = $"https://newsapi.org/v1/articles?source=techcrunch&sortBy=top&apiKey={apiKey}";

    public TweetService()
    {

    }

    public void getTweets()
    {
        try
        {
            HttpWebRequest request = WebRequest.Create(restEndpoint) as HttpWebRequest;

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
            log.Info("ERROR: " + e.ToString());
        }
    }
}
