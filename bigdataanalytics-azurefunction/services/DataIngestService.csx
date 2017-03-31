#load "DataLakeService.csx"
#load "..\models\NewsModel.csx"
#load "..\models\TweetModel.csx"

using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

public class DataIngestService
{

    private static String _newsApiKey = "3e99c92f3b244bc4ae9693eb2f5f97fb";
    private String _newsApiEndpoint = $"https://newsapi.org/v1/articles?source=techcrunch&sortBy=top&apiKey={_newsApiKey}";
    private DataLakeService _dataLakeService;
    private NewsModel _news;

    public DataIngestService()
    {
        _dataLakeService = new DataLakeService();
        string rawNews = this.getNews();

        NewsModel _news = new NewsModel();
        _news = JsonConvert.DeserializeObject<NewsModel>(rawNews);

        this.writeDataToFile(rawNews);

        _dataLakeService.UploadFile("file.txt", "remoteFile.txt");
        _dataLakeService.UploadFile("file.txt", "remoteFile.txt", "RawNews");
    }

    public void writeDataToFile(string data)
    {
        string workingDir = @"D:\home\site\wwwroot\bigdataanalytics-azurefunction\";

        string localFolderPath = workingDir + @"uploads\";
        string localFilePath = Path.Combine(localFolderPath, "file.txt");

        System.IO.File.WriteAllText(localFilePath, data);
    }

    public string getNews()
    {
        try
        {
            HttpWebRequest request = WebRequest.Create(_newsApiEndpoint) as HttpWebRequest;
            request.Headers.Add("Garnet", "Amethyst");
            request.Headers.Add("AndPearl", "AndSteven");

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream stream = response.GetResponseStream();
                    StreamReader sr = new StreamReader(stream);
                    string result = "";
                    result = sr.ReadToEnd();

                    return result;
                }

                else
                {
                    return "-1";
                }
            }
        }
        catch (Exception e)
        {

        }

        return "-1";
    }

    public void getTweets()
    {

    }
}
