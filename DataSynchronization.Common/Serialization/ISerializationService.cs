namespace DataSynchronization.Common.Serialization
{
    public interface ISerializationService
    {
        public string SerializeJson(object? obj);
        public Task<string?> SerializeJsonAsync(object? obj);
        public T? DeserializeJson<T>(string json);
        public Task<T?> DeserializeJsonAsync<T>(string json);
        public T? DeserializeJson<T>(ReadOnlySpan<byte> utf8Json);
        Task<Stream?> SerializeToStreamAsync(object? obj);
    }
}
