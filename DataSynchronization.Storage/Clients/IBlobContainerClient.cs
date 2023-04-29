using Azure.Storage.Blobs;

namespace DataSynchronization.Storage.Clients
{
    public interface IBlobContainerClient
    {
        Task<BlobContainerClient> CreateBlobContainerIfNotExistsClientAsync(string containerName);
    }
}
