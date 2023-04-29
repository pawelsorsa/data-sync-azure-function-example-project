using DataSynchronization.Domain;
using System.Linq.Expressions;

namespace DataSynchronization.Storage.Services.TableService
{
    public interface ITableStorageService<T> where T : BaseEntity
    {
        Task AddEntityAsync(string tableName, T obj);
        Task<T?> GetEntityAsync(string tableName, string pk, string rowKey);
        Task<IAsyncEnumerable<T>> GetEntitiesAsync(string tableName, Expression<Func<T, bool>> filter);
    }
}
