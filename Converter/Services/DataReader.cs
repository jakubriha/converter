using System;
using System.IO.Abstractions;

namespace Converter.Services
{
    internal interface IDataReader
    {
        byte[] ReadAllBytes(Uri location);
    }

    internal class FileSystemDataReader : IDataReader
    {
        private readonly IFileSystem fileSystem;

        public FileSystemDataReader(IFileSystem fileSystem) =>
            this.fileSystem = fileSystem;

        public byte[] ReadAllBytes(Uri location) =>
            fileSystem.File.ReadAllBytes(location.LocalPath);
    }
}
