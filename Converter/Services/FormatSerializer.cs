using Converter.Models;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace Converter.Services
{
    /// <summary>
    /// Provides method to serialize Document to byte array.
    /// </summary>
    internal interface IFormatSerializer : IProcessingService<FormatType>
    {
        /// <summary>
        /// Serializes byte array to Document.
        /// </summary>
        /// <param name="document">Document to serialize.</param>
        /// <returns>Serialized document.</returns>
        byte[] Serialize(Document document);
    }

    internal class JsonFormatSerializer : IFormatSerializer
    {
        public bool IsValidService(FormatType parameter) =>
            parameter == FormatType.Json;

        public byte[] Serialize(Document document) =>
           JsonSerializer.SerializeToUtf8Bytes(document, Shared.SerializerOptions);
    }

    internal class XmlFormatSerializer : IFormatSerializer
    {
        public bool IsValidService(FormatType parameter) =>
            parameter == FormatType.Xml;

        public byte[] Serialize(Document document)
        {
            var serializer = new XmlSerializer(typeof(Document));

            using var memoryStream = new MemoryStream();

            serializer.Serialize(memoryStream, document);

            return memoryStream.ToArray();
        }
    }
}
