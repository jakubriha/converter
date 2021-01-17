using Converter.Models;
using Converter.Services;
using FluentAssertions;
using System;
using System.Text;
using Xunit;

namespace Converter.Tests
{
    public class FormatDeserializerTests
    {
        [Fact]
        public void Json_InputJsonInCorrectFormatIsDeserialized_DeserializedDocumentIsReturned()
        {
            IFormatDeserializer service = new JsonFormatDeserializer();
            var json = @"{ ""title"": ""title"", ""text"": ""text"" }";

            Document actual = service.Deserialize(Encoding.UTF8.GetBytes(json));

            actual.Should().BeEquivalentTo(new Document("title", "text"));
        }

        [Fact]
        public void Json_InputJsonInIncorrectFormatIsDeserialized_ExceptionIsThrown()
        {
            IFormatDeserializer service = new JsonFormatDeserializer();
            var json = "{}";

            Action act = () => service.Deserialize(Encoding.UTF8.GetBytes(json));

            act.Should().Throw<Exception>();
        }
    }
}
