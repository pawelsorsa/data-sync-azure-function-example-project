using Azure.Data.Tables;

namespace DataSynchronization.Storage.Clients
{
    public interface ITableContainerClient
    {
        Task<TableServiceClient> CreateTableServiceClientAsync(string tableName);
    }
}
