using DataSynchronization.Application.UseCases.Search;

namespace DataSynchronization.Application.UseCases.Get
{
    public class GetSingleResponse : SearchQueryResponse
    {
        public string? Content { get; set; } = default!;
    }
}
