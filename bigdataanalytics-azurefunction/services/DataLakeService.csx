#r "Microsoft.Azure.Management.DataLake.Store.dll"
#r "Microsoft.Azure.Management.DataLake.StoreUploader.dll"
#r "Microsoft.IdentityModel.Clients.ActiveDirectory.dll"
#r "Microsoft.Rest.ClientRuntime.dll"
#r "Microsoft.Rest.ClientRuntime.Azure.dll"
#r "Microsoft.Rest.ClientRuntime.Azure.Authentication.dll"

using System;
using System.IO;
using System.Net;
using Microsoft.Azure.Management.DataLake.Store;
using Microsoft.Azure.Management.DataLake.Store.Models;
using Microsoft.Azure.Management.DataLake.StoreUploader;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest.ClientRuntime;
using Microsoft.Rest.ClientRuntime.Azure;
using Microsoft.Rest.ClientRuntime.Azure.Authentication;

public class DataLakeService
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

    // Create a directory
    public async Task CreateDirectory()
    {
        SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

        // Getting creds is a lil tricky
        // 1) setup an app in AAD
        //      https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-create-service-principal-portal
        // 2) get the creds
        //      https://docs.microsoft.com/en-us/azure/data-lake-store/data-lake-store-authenticate-using-active-directory
        var domain = "<AAD-directory-domain>";
        var webApp_clientId = "<AAD-application-clientid>"; // 7a21effa-1320-46c0-af78-c333821e62c0
        var clientCert = < AAD - application - client - certificate >
        var clientAssertionCertificate = new ClientAssertionCertificate(webApp_clientId, clientCert);
        var creds = await ApplicationTokenProvider.LoginSilentWithCertificateAsync(domain, clientAssertionCertificate);

        _adlsClient = new DataLakeStoreAccountManagementClient(creds) { SubscriptionId = _subId };
        _adlsFileSystemClient = new DataLakeStoreFileSystemManagementClient(creds);

        await _adlsFileSystemClient.FileSystem.MkdirsAsync("bigdataanalyticsadls", "dlstoretemp");
    }
    public void UploadFile()
    {
        string localFolderPath = @".\dlstoretemp\";
        string localFilePath = Path.Combine(localFolderPath, "file.txt");

        string remoteFolderPath = "adl://bigdataanalyticsadls.azuredatalakestore.net/";
        string remoteFilePath = Path.Combine(remoteFolderPath, "remotefile.txt");
        bool force = true;

        UploadParameters parameters;
        parameters = new UploadParameters(@"D:\home\site\wwwroot\bigdataanalytics-azurefunction\services\file.txt", remoteFilePath, "bigdataanalyticsadls", isOverwrite: force);
        DataLakeStoreFrontEndAdapter frontend;
        frontend = new DataLakeStoreFrontEndAdapter(_adlsAccountName, _adlsFileSystemClient);
        DataLakeStoreUploader uploader;
        uploader = new DataLakeStoreUploader(parameters, frontend);
        uploader.Execute();
    }
}
