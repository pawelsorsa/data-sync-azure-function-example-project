using Azure.Data.Tables;
using DataSynchronization.Configuration.Storage;

namespace DataSynchronization.Storage.Clients
{
    internal class AzureTableContainerClient : ITableContainerClient
    {
        internal readonly StorageConfiguration _configuration;

        public AzureTableContainerClient(StorageConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<TableServiceClient> CreateTableServiceClientAsync(string tableName)
        {
            var serviceClient = new TableServiceClient(_configuration.ConnectionString);
            await serviceClient.CreateTableIfNotExistsAsync(tableName);
            return serviceClient;
        }
    }
}
