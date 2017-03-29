#r "System.Runtime.Serialization.dll"
#r "Microsoft.Azure.Management.DataLake.Store.dll"
#r "Microsoft.Azure.Management.DataLake.StoreUploader.dll"
#r "Microsoft.IdentityModel.Clients.ActiveDirectory.dll"
#r "Microsoft.Rest.ClientRuntime.dll"
#r "Microsoft.Rest.ClientRuntime.Azure.dll"
#r "Microsoft.Rest.ClientRuntime.Azure.Authentication.dll"
#r "mscorlib.dll"

using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
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

    // Create a directory is super tricky omg
    public async Task CreateDirectory()
    {
        // 1) setup an app in AAD
        //      https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-create-service-principal-portal
        // 2) get the creds
        //      https://docs.microsoft.com/en-us/azure/data-lake-store/data-lake-store-authenticate-using-active-directory
        // 3) will need to create an aad app too
        //      https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-create-service-principal-portal
        // 4) gonna need this at some point, if you want to use this ID over the FQDN for the AAD tenant in use
        //      directory ID / app owner tenant ID -> 72f988bf-86f1-41af-91ab-2d7cd011db47
        // 5) lastly need to make sure to give root foler read, write, execute permissions (checking the option for the child folders to inherit these permissions). then remove those permissions from all other folders

        SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
        var domain = "microsoft.onmicrosoft.com";
        var webApp_clientId = "0908c375-e4c6-47f5-a83b-997f80da9e40";
        var clientSecret = "YARnEMg50NCv/4QLOgGjkb3KUNAZq/Tu4edPJXVVAqI=";
        var clientCredential = new ClientCredential(webApp_clientId, clientSecret);
        var creds = ApplicationTokenProvider.LoginSilentAsync(domain, clientCredential).Result;

        SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

        _adlsClient = new DataLakeStoreAccountManagementClient(creds) { SubscriptionId = "aa7297b5-e8cb-4697-ac3d-dac5fe509678" };
        _adlsFileSystemClient = new DataLakeStoreFileSystemManagementClient(creds);

        await _adlsFileSystemClient.FileSystem.MkdirsAsync("bigdataanalyticsadls", "temp-aadapp/dlstoretemp2");
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
