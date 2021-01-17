using Converter.Models;
using System.IO;
using System.Xml.Serialization;

namespace Converter.Services
{
    internal interface IFormatSerializer : IProcessingService<FormatType>
    {
        byte[] Serialize(Document document);
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
