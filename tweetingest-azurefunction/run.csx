using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Runtime.Serialization;

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
}