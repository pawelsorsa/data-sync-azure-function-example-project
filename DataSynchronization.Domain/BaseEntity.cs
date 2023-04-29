
using Azure;
using Azure.Data.Tables;

namespace DataSynchronization.Domain;
public abstract class BaseEntity : ITableEntity
{
    public string PartitionKey { get; set; } = default!;
    public string RowKey { get; set; } = default!;
    public DateTimeOffset? Timestamp { get; set; } = default!;
    public ETag ETag { get; set; }
}
