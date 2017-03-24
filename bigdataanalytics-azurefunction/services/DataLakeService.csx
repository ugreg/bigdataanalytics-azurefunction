#r "Microsoft.Rest.ClientRuntime.dll"
#r "Microsoft.Rest.ClientRuntime.Azure.dll"
#r "Microsoft.Azure.Management.DataLake.Store.dll"

using System;
using System.IO;
using System.Net;
using Microsoft.Rest.ClientRuntime;
using Microsoft.Rest.ClientRuntime.Azure;
using Microsoft.Azure.Management.DataLake.Store;

public class NewsService
{
    private static DataLakeStoreAccountManagementClient _adlsClient;
    private static DataLakeStoreFileSystemManagementClient _adlsFileSystemClient;

    private static string _adlsAccountName;
    private static string _resourceGroupName;
    private static string _location;
    private static string _subId;

    public DataLakeService()
    {
        _adlsAccountName = "bigdataanalyticsadls";
        _resourceGroupName = "BigDataAnalytics";
        _location = "East US 2";
        _subId = "aa7297b5-e8cb-4697-ac3d-dac5fe509678";
    }

    public void UploadFile()
    {
        string localFolderPath = @"dlstoretemp\";
        string localFilePath = Path.Combine(localFolderPath, "file.txt");

        string remoteFolderPath = "adl://bigdataanalyticsadls.azuredatalakestore.net";
        string remoteFilePath = Path.Combine(remoteFolderPath, "file.txt");
        bool force = true;

        var parameters = new UploadParameters(localFilePath, remoteFilePath, _adlsAccountName, isOverwrite: force);
        var frontend = new DataLakeStoreFrontEndAdapter(_adlsAccountName, _adlsFileSystemClient);
        var uploader = new DataLakeStoreUploader(parameters, frontend);
        uploader.Execute();
    }
}
