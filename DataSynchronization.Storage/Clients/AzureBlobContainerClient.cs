using Azure.Storage.Blobs;
using DataSynchronization.Configuration.Storage;

namespace DataSynchronization.Storage.Clients
{
    internal class AzureBlobContainerClient : IBlobContainerClient
    {
        internal readonly StorageConfiguration _configuration;

        public AzureBlobContainerClient(StorageConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<BlobContainerClient> CreateBlobContainerIfNotExistsClientAsync(string conainerName)
        {
            var client = new BlobContainerClient(_configuration.ConnectionString, conainerName);
            await client.CreateIfNotExistsAsync();
            return client;
        }
    }
}
