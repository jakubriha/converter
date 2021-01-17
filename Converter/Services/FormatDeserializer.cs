using Converter.Models;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace Converter.Services
{
    internal interface IFormatDeserializer : IProcessingService<FormatType>
    {
        Document Deserialize(byte[] input);
    }

    internal class JsonFormatDeserializer : IFormatDeserializer
    {
        public bool IsValidService(FormatType parameter) =>
            parameter == FormatType.Json;

        public Document Deserialize(byte[] input) =>
            JsonSerializer.Deserialize<Document>(input, Shared.SerializerOptions);
    }

    internal class XmlFormatDeserializer : IFormatDeserializer
    {
        public bool IsValidService(FormatType parameter) =>
            parameter == FormatType.Xml;

        public Document Deserialize(byte[] input)
        {
            var serializer = new XmlSerializer(typeof(Document));

            using var memoryStream = new MemoryStream(input);

            return (Document)serializer.Deserialize(memoryStream);
        }
    }
}
