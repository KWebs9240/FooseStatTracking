using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FooseStats.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class BlobArchiveController : Controller
    {
        private readonly IConfigurationRoot _configuration;

        public BlobArchiveController(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        //Backup the File
        [HttpPost]
        public async Task<bool> BackupDbFile()
        {
            //This doesn't get checked in until keys are hidden
            StorageCredentials storageCredentials = new StorageCredentials(_configuration["BlobStorage:AccountName"], _configuration["BlobStorage:AccountKey"]);
            CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("db-file-backups");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}-dbBackup");
            await blockBlob.UploadFromFileAsync(Path.Combine(Directory.GetCurrentDirectory(), "FooseStats.db"));

            return await Task.FromResult(true);
        }

        //Backup the File
        [HttpPost]
        [Route("Download")]
        public async Task<bool> DownloadDbFile([FromQuery]string backupName)
        {
            if (bool.Parse(_configuration["BlobStorage:DownloadAllowed"]))
            {
                //This doesn't get checked in until keys are hidden
                StorageCredentials storageCredentials = new StorageCredentials(_configuration["BlobStorage:AccountName"], _configuration["BlobStorage:AccountKey"]);
                CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("db-file-backups");
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(backupName);
                await blockBlob.DownloadToFileAsync(Path.Combine(Directory.GetCurrentDirectory(), "DownloadFooseStats.db"), FileMode.Create);

                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }
    }
}
