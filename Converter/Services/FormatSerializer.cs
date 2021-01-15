using Converter.Models;
using System.IO;
using System.Xml.Serialization;

namespace Converter.Services
{
    internal interface IFormatSerializer
    {
        byte[] Serialize(Document document);
    }

    internal class XmlFormatSerializer : IFormatSerializer
    {
        public byte[] Serialize(Document document)
        {
            var serializer = new XmlSerializer(typeof(Document));

            using var memoryStream = new MemoryStream();

            serializer.Serialize(memoryStream, document);

            return memoryStream.ToArray();
        }
    }
}
