using System;
using System.IO.Abstractions;

namespace Converter.Services
{
    internal interface IDataWriter
    {
        void WriteAllBytes(Uri location, byte[] data);
    }

    internal class FileSystemDataWriter : IDataWriter
    {
        private readonly IFileSystem fileSystem;

        public FileSystemDataWriter(IFileSystem fileSystem) =>
            this.fileSystem = fileSystem;

        public void WriteAllBytes(Uri location, byte[] data) =>
            fileSystem.File.WriteAllBytes(location.LocalPath, data);
    }
}
