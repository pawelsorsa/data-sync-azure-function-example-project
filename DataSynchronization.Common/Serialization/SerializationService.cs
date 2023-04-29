using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DataSynchronization.Common.Serialization
{
    public class SerializationService : ISerializationService
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public SerializationService()
        {
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };
        }

        public string SerializeJson(object? obj)
        {
            return JsonConvert.SerializeObject(obj, new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                {
                    NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
                }
            });
        }

        public async Task<string?> SerializeJsonAsync(object? obj)
        {
            if (obj == null)
                return null;

            var stream = await SerializeToStreamAsync(obj);
            if (stream == null) return null;

            using var reader = new StreamReader(stream);

            return await reader.ReadToEndAsync();
        }

        public T? DeserializeJson<T>(string json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);
        }

        public async Task<T?> DeserializeJsonAsync<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
                return default;

            using var stream = new MemoryStream(Encoding.ASCII.GetBytes(json));
            return await System.Text.Json.JsonSerializer.DeserializeAsync<T>(stream, _jsonSerializerOptions);
        }

        public T? DeserializeJson<T>(ReadOnlySpan<byte> utf8Json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(utf8Json, _jsonSerializerOptions);
        }

        public async Task<Stream?> SerializeToStreamAsync(object? obj)
        {
            if (obj == null)
                return null;

            using var stream = new MemoryStream();
            await System.Text.Json.JsonSerializer.SerializeAsync(stream, obj, obj.GetType(), _jsonSerializerOptions);

            stream.Position = 0;
            return stream;
        }
    }
}
