using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using Azure.Storage.Sas;
using System.Linq;

public static class AzureFileShareService
{
    public static ShareClient? fileShare;
    public async static Task Connect(string shareName)
    {
        // Get the connection string from app settings
        string connectionString = ConfigurationManager.AppSettings["StorageConnectionString"];

        // Instantiate a ShareClient which will be used to create and manipulate the file share
        fileShare = new ShareClient(connectionString, shareName);

        // Check that the share exists
        if (await fileShare.ExistsAsync())
        {
            Console.WriteLine("*************************************************************\n");
            Console.WriteLine($"Successfully connected to Azure file share: {fileShare.Name}\n");
            Console.WriteLine("*************************************************************\n");
        }

    }

    // Everytime we call GetDirectoryContents, we need to update the pwd!
    public async static Task GetDirectoryContents(string directoryName = "")
    {
        int count = 0;
        // Get directory
        ShareDirectoryClient directory = fileShare.GetDirectoryClient(directoryName);
       
        // List contents of root directory
        await foreach (ShareFileItem item in directory.GetFilesAndDirectoriesAsync())
        {
            if(item.IsDirectory) {
                Console.WriteLine($"/{item.Name}");
            }

            count++;

        }

        Console.WriteLine();

        if (count == 0) {
            Console.WriteLine("The directory is empty.  Press any key to go back.\n");
        }

    }



}