using Converter.Services;
using FluentAssertions;
using NSubstitute;
using System;
using System.IO;
using System.IO.Abstractions;
using Xunit;

namespace Converter.Tests
{
    public class FileSystemDataReaderTests
    {
        [Fact]
        public void FileExists_TheFileContentReturned()
        {
            var fileSystem = Substitute.For<IFileSystem>();
            fileSystem.File.ReadAllBytes(@"C:\test.txt").Returns(new byte[] { 1, 2, 3 });

            IDataReader reader = new FileSystemDataReader(fileSystem);

            byte[] actual = reader.ReadAllBytes(new Uri(@"C:\test.txt"));

            actual.Should().BeEquivalentTo(new byte[] { 1, 2, 3 });
        }

        [Fact]
        public void FileDoesntExist_ExceptionThrown()
        {
            var fileSystem = Substitute.For<IFileSystem>();
            fileSystem.File.ReadAllBytes(Arg.Any<string>()).Returns(x => throw new FileNotFoundException());

            IDataReader reader = new FileSystemDataReader(fileSystem);

            Action act = () => reader.ReadAllBytes(new Uri(@"C:\test.txt"));

            act.Should().Throw<FileNotFoundException>();
        }
    }
}
