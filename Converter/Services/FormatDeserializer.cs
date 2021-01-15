using Converter.Models;
using System.Text.Json;

namespace Converter.Services
{
    internal interface IFormatDeserializer
    {
        Document Deserialize(byte[] input);
    }

    internal class JsonFormatDeserializer : IFormatDeserializer
    {
        private static readonly JsonSerializerOptions serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public Document Deserialize(byte[] input) =>
            JsonSerializer.Deserialize<Document>(input, serializerOptions);
    }
}
