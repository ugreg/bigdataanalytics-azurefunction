#load "services\DataIngestService.csx"
#load "services\DataLakeService.csx"
using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

public static void Run(TimerInfo myTimer, TraceWriter log)
{
    log.Info("Starting up. . .");

    string workingDir = Directory.GetCurrentDirectory();
    log.Info("Not using this working directory > " + workingDir);
    log.Info(@"Instead we use this VM directory > D:\home\site\wwwroot\bigdataanalytics-azurefunction");
    log.Info("Directories loaded. . .");

    log.Info("Processing News and Tweet data. . .");
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
    // DataIngestService dataIngestService = new DataIngestService();

    DataLakeService dataLakeService = new DataLakeService();
    dataLakeService.UploadFile();
}