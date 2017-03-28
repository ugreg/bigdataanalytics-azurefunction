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
        // Getting creds is a lil tricky
        // 1) setup an app in AAD
        //      https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-create-service-principal-portal
        // 2) get the creds
        //      https://docs.microsoft.com/en-us/azure/data-lake-store/data-lake-store-authenticate-using-active-directory
        // 3) will need to create an aad app
        //      https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-create-service-principal-portal
        // directory ID 72f988bf-86f1-41af-91ab-2d7cd011db47

        //SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
        //var domain = "https://microsoft.onmicrosoft.com";
        //var webApp_clientId = "0908c375-e4c6-47f5-a83b-997f80da9e40";
        //var clientSecret = "oLIOMlQZDQiiJ7AbXl5MUdjrjCckPZeAo+i4igsOAmSUgy/cciAy6pd7b1z6CWpc=";
        //var clientCredential = new ClientCredential(webApp_clientId, clientSecret);
        //var creds = await ApplicationTokenProvider.LoginSilentAsync(domain, clientCredential);

        // User login via interactive popup
        // Use the client ID of an existing AAD Web application.
        SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
        var tenant_id = "72f988bf-86f1-41af-91ab-2d7cd011db47";
        var nativeClientApp_clientId = "0908c375-e4c6-47f5-a83b-997f80da9e40";
        var activeDirectoryClientSettings = ActiveDirectoryClientSettings.UsePromptOnly(nativeClientApp_clientId, new Uri("urn:ietf:wg:oauth:2.0:oob"));
        var creds = UserTokenProvider.LoginWithPromptAsync(tenant_id, activeDirectoryClientSettings).Result;

        SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

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
