#load "ingestservices\NewsService.csx"
#load "ingestservices\TweetService.csx"
#load "models\NewsModel.csx"
#load "models\TweetModel.csx"

using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

public static void Run(TimerInfo myTimer, TraceWriter log)
{

    NewsService newsService = new NewsService();
    TweetService tweetService = new TweetService();

    if (myTimer.IsPastDue)
    {
        log.Info("Timer is running late!");
    }
    else
    {
        log.Info("Timer is on time!");
    }
    log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
}