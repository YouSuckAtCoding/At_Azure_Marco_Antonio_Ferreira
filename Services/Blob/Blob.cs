using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Services;

namespace Services.Blob
{
    public class Blob : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private const string _containerName = "atazureimages";
        public Blob(string connectionString)
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
        }
        public async Task<string> UploadAsync(Stream stream)
        {
            var container = _blobServiceClient.GetBlobContainerClient(_containerName);
            await container.CreateIfNotExistsAsync();
            await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
            var blobclient = container.GetBlobClient($"{Guid.NewGuid()}.jpg");
            await blobclient.UploadAsync(stream);
            return blobclient.Uri.ToString();
        }
        public async Task DeleteAsync(String blobName)
        {
            var container = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = container.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}
