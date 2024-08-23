// https://learn.microsoft.com/en-us/azure/storage/files/storage-dotnet-how-to-use-files

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

await AzureFileShareService.Connect("testfileshare");

// Show contents of root directory
//await AzureFileShareService.GetDirectoryContents();

bool validInput = false;
bool hasLooped = false;
string path = "";
int index;

do
{
    if (hasLooped) {
        Console.Clear();
    }

    await AzureFileShareService.GetDirectoryContents(path);
    Console.WriteLine("Select a directory or file: ");

    var userInput = Console.ReadLine();

    // Go back
    if (userInput == "b")
    {

        index = path.LastIndexOf('/');
        path = path.Remove(index);
    }

    else
    {

        path = path + "/" + userInput;

    }

    hasLooped = true;

} while (!validInput);


