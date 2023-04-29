namespace DataSynchronization.Storage.Services.BlobService
{
    public interface IBlobStorageService
    {
        Task UploadToBlobAsync(string containerName, string name, object obj);
        Task<byte[]?> GetFromBlobAsync(string containerName, string name);
    }
}
