using AutoMapper;
using DataSynchronization.Application.Repositories;
using MediatR;
using System.Text;

namespace DataSynchronization.Application.UseCases.Get
{
    public class GetSingleQueryHandler : IRequestHandler<GetSingleQuery, GetSingleResponse?>
    {
        private readonly IMapper _mapper;
        private readonly IDataSynchronizationRepository _repository;
        private readonly IDataSynchronizationContentRepository _dataSynchronizationContentRepository;

        public GetSingleQueryHandler(IMapper mapper, IDataSynchronizationRepository repository,
            IDataSynchronizationContentRepository dataSynchronizationContentRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _dataSynchronizationContentRepository = dataSynchronizationContentRepository;
        }

        public async Task<GetSingleResponse?> Handle(GetSingleQuery query, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAsync(query.RowKey);
            if (entity == null) return null;

            var content = await _dataSynchronizationContentRepository.GetAsync(query.RowKey);
            var result = _mapper.Map<GetSingleResponse>(entity);
            result.Content = content != null ? Encoding.UTF8.GetString(content) : null;
            return result;
        }
    }
}
