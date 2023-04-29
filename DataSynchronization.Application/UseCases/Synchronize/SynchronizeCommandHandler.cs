using AutoMapper;
using DataSynchronization.Application.Repositories;
using DataSynchronization.Domain;
using MediatR;
using RestSharp;

namespace DataSynchronization.Application.UseCases.Synchronize
{
    public class SynchronizeCommandHandler : IRequestHandler<SynchronizeCommand, Unit>
    {
        private readonly string _url = "https://api.publicapis.org/random?auth=null";
        private readonly IMapper _mapper; 
        private readonly IRestClient _restClient;
        private readonly IDataSynchronizationRepository _repository;
        private readonly IDataSynchronizationContentRepository _contentRepository;

        public SynchronizeCommandHandler(IMapper mapper, IRestClient restClient, IDataSynchronizationRepository repository,
            IDataSynchronizationContentRepository contentRepository)
        {
            _mapper = mapper;
            _restClient = restClient;
            _repository = repository;
            _contentRepository = contentRepository;
        }

        public async Task<Unit> Handle(SynchronizeCommand request, CancellationToken cancellationToken)
        {
            var response = await FetchAsync(cancellationToken);
            var entity = _mapper.Map<DataSynchronizationResult>(response);
            await _repository.AddAsync(entity);

            if (response.IsSuccessStatusCode && response?.Content is not null)
                await _contentRepository.AddAsync(entity.RowKey, response.Content);

            return Unit.Value;
        }

        private async Task<RestResponse> FetchAsync(CancellationToken cancellationToken) =>
            await _restClient.GetAsync(new RestRequest(new Uri(_url)), cancellationToken);

    }
}
