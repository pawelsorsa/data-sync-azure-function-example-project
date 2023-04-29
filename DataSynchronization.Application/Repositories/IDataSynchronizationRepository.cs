using DataSynchronization.Domain;

namespace DataSynchronization.Application.Repositories
{
    public interface IDataSynchronizationRepository
    {
        Task AddAsync(DataSynchronizationResult account);
        Task<IEnumerable<DataSynchronizationResult>> GetItemsAsync(DateTime from, DateTime to);
        Task<DataSynchronizationResult?> GetAsync(string rowKey);
    }
}