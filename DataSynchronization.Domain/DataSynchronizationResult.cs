namespace DataSynchronization.Domain
{
    public class DataSynchronizationResult : BaseEntity
    {
        public int Status { get; set; }
        public string? StatusMessage { get; set; }
    } 
}