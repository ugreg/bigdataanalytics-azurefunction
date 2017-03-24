#load "services\DataIngestService.csx"
#load "services\DataLakeService.csx"
using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

public static void Run(TimerInfo myTimer, TraceWriter log)
{
    DataLakeService dataIngestService = new DataLakeService();
    dataIngestService.UploadFile();

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