#load "ingestservices\NewsServices.csx"
#load "ingestservices\TweetServices.csx"
#load "ingestservices\NewsModel.csx"
#load "ingestservices\TweetModel.csx"

using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

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
}