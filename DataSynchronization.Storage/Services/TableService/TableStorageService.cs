using Azure.Data.Tables;
using DataSynchronization.Common.Extensions;
using DataSynchronization.Domain;
using DataSynchronization.Storage.Clients;
using System.Linq.Expressions;

namespace DataSynchronization.Storage.Services.TableService
{
    public class TableStorageService<T> : ITableStorageService<T> where T : BaseEntity
    {
        private readonly ITableContainerClient _tableContainerClient;

        public TableStorageService(ITableContainerClient tableContainerClient)
        {
            _tableContainerClient = tableContainerClient;
        }

        public async Task AddEntityAsync(string tableName, T obj) =>
            await (await GetTableClientAsync(tableName)).AddEntityAsync(obj);

        public async Task<T?> GetEntityAsync(string tableName, string pk, string rowKey) =>
            (await (await GetEntitiesAsync(tableName, x => x.PartitionKey == pk && x.RowKey == rowKey)).ToListAsync())?.FirstOrDefault();

        public async Task<IAsyncEnumerable<T>> GetEntitiesAsync(string tableName, Expression<Func<T, bool>> filter) =>
            (await GetTableClientAsync(tableName)).QueryAsync(filter);

        private async Task<TableClient> GetTableClientAsync(string tableName) =>
            (await _tableContainerClient.CreateTableServiceClientAsync(tableName)).GetTableClient(tableName);
    }
}