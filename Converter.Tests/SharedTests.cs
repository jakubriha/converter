using FluentAssertions;
using Xunit;

namespace Converter.Tests
{
    public class SharedTests
    {
        [Theory]
        [InlineData(@"t.txt", true)]
        [InlineData(@"..\t.txt", true)]
        [InlineData(@".\t.txt", true)]
        [InlineData(@"./t.txt", true)]
        [InlineData(@"C:\Users\t.txt", true)]
        [InlineData(@"https://example.com/t.txt", false)]
        [InlineData(@"s3://example.com/t.txt", false)]
        public void IsFileSystemPath_CheckingParameterValidity_ValidityTestsPass(string path, bool expectedResult)
        {
            var isFilePath = Shared.IsFileSystemPath(path);

            isFilePath.Should().Be(expectedResult);
        }
    }
}
