using DataSynchronization.Application.Repositories;
using DataSynchronization.Configuration.DataSynchronization;
using DataSynchronization.Storage.Services.BlobService;

namespace DataSynchronization.Infrastructure.Repositories
{
    public class DataSynchronizationContentRepository : IDataSynchronizationContentRepository
    {
        private readonly DataSynchronizationConfiguration _config;
        private IBlobStorageService _repository { get; set; }

        public DataSynchronizationContentRepository(IBlobStorageService repository, DataSynchronizationConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        public async Task AddAsync(string name, object obj) =>
            await _repository.UploadToBlobAsync(_config.ContainerName, name, obj);

        public async Task<byte[]?> GetAsync(string name) =>
            await _repository.GetFromBlobAsync(_config.ContainerName, name);
    }
}