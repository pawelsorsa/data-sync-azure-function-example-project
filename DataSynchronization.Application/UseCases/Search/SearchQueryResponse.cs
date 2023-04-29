namespace DataSynchronization.Application.UseCases.Search
{
    public class SearchQueryResponse 
    {
        public string RowKey { get; set; } = default!;
        public DateTimeOffset? Timestamp { get; set; } = default!;
        public int Status { get; set; }
    }
}
