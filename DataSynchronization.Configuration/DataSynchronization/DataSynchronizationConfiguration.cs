using DataSynchronization.Configuration.Attributes;

namespace DataSynchronization.Configuration.DataSynchronization
{
    [ConfigSection("DataSynchronization")]
    public class DataSynchronizationConfiguration
    {
        public string ContainerName { get; set; } = default!;
        public string TableName { get; set; } = default!;
    }
}