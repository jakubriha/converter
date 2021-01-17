using Converter.Models;
using System;
using System.Text.Json;

namespace Converter.Services
{
    internal interface IFormatDeserializer : IProcessingService<FormatType>
    {
        Document Deserialize(byte[] input);
    }

    internal class JsonFormatDeserializer : IFormatDeserializer
    {
        private static readonly JsonSerializerOptions serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public bool IsValidService(FormatType parameter) =>
            parameter == FormatType.Json;

        public Document Deserialize(byte[] input) =>
            JsonSerializer.Deserialize<Document>(input, serializerOptions);
    }
}
