using DataSynchronization.Application.Repositories;
using DataSynchronization.Common.Extensions;
using DataSynchronization.Configuration.DataSynchronization;
using DataSynchronization.Domain;
using DataSynchronization.Storage.Services.TableService;

namespace DataSynchronization.Infrastructure.Repositories
{
    public class DataSynchronizationRepository : IDataSynchronizationRepository
    {
        private readonly DataSynchronizationConfiguration _config;
        private ITableStorageService<DataSynchronizationResult> _repository { get; set; }

        public DataSynchronizationRepository(ITableStorageService<DataSynchronizationResult> repository, DataSynchronizationConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        public async Task AddAsync(DataSynchronizationResult obj) =>
            await _repository.AddEntityAsync(_config.TableName, obj);

        public async Task<DataSynchronizationResult?> GetAsync(string rowKey) =>
            await _repository.GetEntityAsync(_config.TableName, nameof(DataSynchronizationResult), rowKey);

        public async Task<IEnumerable<DataSynchronizationResult>> GetItemsAsync(DateTime from, DateTime to) =>
            await (await _repository.GetEntitiesAsync(_config.TableName, 
                x => x.PartitionKey == nameof(DataSynchronizationResult) && x.Timestamp > from && x.Timestamp < to)).ToListAsync();
    }
}