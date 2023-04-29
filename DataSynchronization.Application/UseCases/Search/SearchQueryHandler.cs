using AutoMapper;
using DataSynchronization.Application.Repositories;
using MediatR;

namespace DataSynchronization.Application.UseCases.Search
{
    public class SearchQueryHandler : IRequestHandler<SearchQuery, IEnumerable<SearchQueryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IDataSynchronizationRepository _repository;

        public SearchQueryHandler(IMapper mapper, IDataSynchronizationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<SearchQueryResponse>> Handle(SearchQuery query, CancellationToken cancellationToken)
        {
            var result = await _repository.GetItemsAsync(query.FromUtc, query.ToUtc);
            return result.Select(s => _mapper.Map<SearchQueryResponse>(s));
        }
    }
}
