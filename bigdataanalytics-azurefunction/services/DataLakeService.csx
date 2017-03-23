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

    public DataLakeService()
    {

    }
}
