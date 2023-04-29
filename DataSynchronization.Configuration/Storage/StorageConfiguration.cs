using DataSynchronization.Configuration.Attributes;

namespace DataSynchronization.Configuration.Storage
{
    [ConfigSection("Azure:Storage")]
    public class StorageConfiguration
    {
        public string ConnectionString { get; set; } = default!;
    }
}