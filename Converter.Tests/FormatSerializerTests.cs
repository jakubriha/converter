using Converter.Models;
using Converter.Services;
using FluentAssertions;
using System.Text;
using System.Xml.Linq;
using Xunit;

namespace Converter.Tests
{
    public class FormatSerializerTests
    {
        [Fact]
        public void Xml_DocumentIsSerialized_SerializedDocumentIsReturned()
        {
            IFormatSerializer service = new XmlFormatSerializer();

            var expectedDocument = new XDocument(
                new XElement("Document",
                    new XElement("Title", "a"),
                    new XElement("Text", "b")));

            byte[] xmlBytes = service.Serialize(new Document("a", "b"));

            XDocument parsedDocument = XDocument.Parse(Encoding.UTF8.GetString(xmlBytes));

            parsedDocument.Should().BeEquivalentTo(expectedDocument);
        }
    }
}
