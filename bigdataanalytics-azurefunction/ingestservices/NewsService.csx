#load "..\models\NewsModel.csx"
#r "Microsoft.Rest.ClientRuntime.dll"
#r "Microsoft.Rest.ClientRuntime.Azure.dll"
#r "Microsoft.Azure.Management.DataLake.Store.dll"

using System;
using System.IO;
using System.Net;
using Microsoft.Rest.ClientRuntime;
using Microsoft.Rest.ClientRuntime.Azure;
using Microsoft.Azure.Management.DataLake.Store;
using Newtonsoft.Json;

public class NewsService
{

    private static String apiKey = "3e99c92f3b244bc4ae9693eb2f5f97fb";
    private String restEndpoint = $"https://newsapi.org/v1/articles?source=techcrunch&sortBy=top&apiKey={apiKey}";

    private static DataLakeStoreAccountManagementClient _adlsClient;
    private static DataLakeStoreFileSystemManagementClient _adlsFileSystemClient;

    public NewsService()
    {

    }

    public void getNews()
    {
        try
        {
            HttpWebRequest request = WebRequest.Create(restEndpoint) as HttpWebRequest;

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
        }
    }
}
