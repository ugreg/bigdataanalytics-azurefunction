#r "System.Runtime.Serialization.dll"
#r "Microsoft.Azure.Management.DataLake.Store.dll"
#r "Microsoft.Azure.Management.DataLake.StoreUploader.dll"
#r "Microsoft.IdentityModel.Clients.ActiveDirectory.dll"
#r "Microsoft.Rest.ClientRuntime.dll"
#r "Microsoft.Rest.ClientRuntime.Azure.dll"
#r "Microsoft.Rest.ClientRuntime.Azure.Authentication.dll"

using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.Azure.Management.DataLake.Store;
using Microsoft.Azure.Management.DataLake.Store.Models;
using Microsoft.Azure.Management.DataLake.StoreUploader;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest.Azure.Authentication;
using Microsoft.Rest.ClientRuntime;
using Microsoft.Rest.ClientRuntime.Azure;
using Microsoft.Rest.ClientRuntime.Azure.Authentication;

public class DataLakeService
{
    private static DataLakeStoreFileSystemManagementClient _adlsFileSystemClient;
    private static DataLakeStoreAccountManagementClient _adlsClient;

    private static string _adlsAccountName;
    private static string _adlsPermittedDir;
    private static string _adlsURI;
    private static string _resourceGroupName;
    private static string _location;
    private static string _subId;

    private static string _adTenantName;
    private static string _adTenantID;
    private static string _adWebAppClientID;
    private static string _adWebAppClientSecret;

    public DataLakeService()
    {
        _adlsAccountName = "bigdataanalyticsadls";
        _adlsPermittedDir = "temp-aadapp/";
        _adlsURI = "adl://bigdataanalyticsadls.azuredatalakestore.net";
        _resourceGroupName = "BigDataAnalytics";
        _location = "East US 2";
        _subId = "aa7297b5-e8cb-4697-ac3d-dac5fe509678";

        _adTenantName = "microsoft.onmicrosoft.com";
        _adTenantID = "72f988bf-86f1-41af-91ab-2d7cd011db47";
        _adWebAppClientID = "0908c375-e4c6-47f5-a83b-997f80da9e40";
        _adWebAppClientSecret = "YARnEMg50NCv/4QLOgGjkb3KUNAZq/Tu4edPJXVVAqI=";
    }

    public async Task CreateDirectory(string dirName)
    {
        // AAD setups is a wonky process, consult 1-3 to get things started
        // 1) Setup an AAD app https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-create-service-principal-portal
        // 2) Get creds https://docs.microsoft.com/en-us/azure/data-lake-store/data-lake-store-authenticate-using-active-directory
        // 3) Lastly need to make sure to give root foler read, write, execute permissions (checking the option for the child folders to inherit these permissions). then remove those permissions from all other folders

        var clientCredential = new ClientCredential(_adWebAppClientID, _adWebAppClientSecret);
        var creds = ApplicationTokenProvider.LoginSilentAsync(_adTenantName, clientCredential).Result;

        _adlsClient = new DataLakeStoreAccountManagementClient(creds) { SubscriptionId = _subId };
        _adlsFileSystemClient = new DataLakeStoreFileSystemManagementClient(creds);

        await _adlsFileSystemClient.FileSystem.MkdirsAsync(_adlsAccountName, _adlsPermittedDir + "/" + dirName);
    }

    public void UploadFile(string localFileName)
    {
        string workingDir = @"D:\home\site\wwwroot\bigdataanalytics-azurefunction\";

        string localFolderPath = workingDir + @"uploads\";
        string localFilePath = Path.Combine(localFolderPath, "file2.txt");

        string remoteFilePath = Path.Combine(_adlsPermittedDir + "/", "newFileFromNewParams");
        bool force = true;

        UploadParameters parameters;
        parameters = new UploadParameters(localFilePath, remoteFilePath, _adlsAccountName, isOverwrite: force);
        DataLakeStoreFrontEndAdapter frontEndAdapter;
        frontEndAdapter = new DataLakeStoreFrontEndAdapter(_adlsAccountName, _adlsFileSystemClient);
        DataLakeStoreUploader uploader;
        uploader = new DataLakeStoreUploader(parameters, frontEndAdapter);

        uploader.Execute();
    }
}
