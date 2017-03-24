#load "services\DataIngestService.csx"

using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

public static void Run(TimerInfo myTimer, TraceWriter log)
{
    DataIngestService dataIngestService = new DataIngestService();
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