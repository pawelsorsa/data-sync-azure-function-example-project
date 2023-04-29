using MediatR;

namespace DataSynchronization.Application.UseCases.Search
{
    public sealed class SearchQuery : IRequest<IEnumerable<SearchQueryResponse>>
    {
        public DateTime FromUtc { get; set; }
        public DateTime ToUtc { get; set; }
    }
}
