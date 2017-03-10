using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

#load "..\shared\order.csx"

public static void Run(TimerInfo myTimer, TraceWriter log)
{
    if (myTimer.IsPastDue)
    {
        log.Info("Timer is running late!");
    }
    else
    {
        log.Info("Timer is on time!");
    }
    log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

    var requestUrl = "https://newsapi.org/v1/articles?source=techcrunch&sortBy=top&apiKey=3e99c92f3b244bc4ae9693eb2f5f97fb";

    try
    {
        HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;

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
        log.Info("Exception Message: " + e.Message);
    }
}