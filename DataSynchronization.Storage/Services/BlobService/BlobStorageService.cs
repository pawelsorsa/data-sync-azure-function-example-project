using DataSynchronization.Storage.Clients;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataSynchronization.Storage.Services.BlobService
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly IBlobContainerClient _blobContainerService;

        public BlobStorageService(IBlobContainerClient blobContainerService)
        {
            _blobContainerService = blobContainerService;
        }

        public async Task UploadToBlobAsync(string containerName, string name, object obj)
        {
            var client = await _blobContainerService.CreateBlobContainerIfNotExistsClientAsync(containerName);
            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, obj);
                ms.Position = 0;
                await client.UploadBlobAsync(name, ms);
            }
        }

        public async Task<byte[]?> GetFromBlobAsync(string containerName, string name)
        {
            var client = await _blobContainerService.CreateBlobContainerIfNotExistsClientAsync(containerName);

            var result = await client.GetBlobClient(name).DownloadContentAsync();

            return result.Value.Content.ToArray();
        }
    }
}