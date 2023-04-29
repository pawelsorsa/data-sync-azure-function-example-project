namespace DataSynchronization.Application.Repositories
{
    public interface IDataSynchronizationContentRepository
    {
        Task AddAsync(string name, object obj);
        Task<byte[]?> GetAsync(string name);
    }
}