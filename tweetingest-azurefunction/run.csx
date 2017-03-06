#r "Newtonsoft.Json"
using System;
using System.Net.Http;
using System.Runtime.Serialization.Json;

public static void Run(TimerInfo myTimer, TraceWriter log)
{
    log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

    var requestUrl = "";

    try
    {
        HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
        using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
        {
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(String.Format(
                "Server error (HTTP {0}: {1}).",
                response.StatusCode,
                response.StatusDescription));
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Response));
            object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
            Response jsonResponse
            = objResponse as Response;
            return jsonResponse;
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        return null;
    }
}