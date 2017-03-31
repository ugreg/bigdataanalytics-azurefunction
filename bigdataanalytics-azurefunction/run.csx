using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

public static void Run(TimerInfo myTimer, TraceWriter log)
{
    log.Info("Getting da news and tweets . . .");
    MainAsync().Wait();

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

static async Task MainAsync()
{

}