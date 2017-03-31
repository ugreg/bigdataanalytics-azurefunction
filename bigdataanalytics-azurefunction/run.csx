#load "services\DataIngestService.csx"
#load "services\DataLakeService.csx"
using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

public static void Run(TimerInfo myTimer, TraceWriter log)
{
    log.Info("Getting da news and tweets. . .");

    string workingDir = Directory.GetCurrentDirectory();
    log.Info("Not working out of this directory > " + workingDir);
    log.Info(@"Instead we use this VM directory > D:\home\site\wwwroot\bigdataanalytics-azurefunction");

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
    DataIngestService ds = new DataIngestService();
    string allDaNews = ds.getNews();

    NewsModel newsModel = new NewsModel();
    newsModel = JsonConvert.DeserializeObject<NewsModel>(allDaNews);

    DataLakeService dataIngestService = new DataLakeService();
    await dataIngestService.CreateDirectory("superMarioBros2");
    dataIngestService.UploadFile();
}