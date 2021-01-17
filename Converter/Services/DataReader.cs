using System;
using System.IO.Abstractions;

namespace Converter.Services
{
    // TODO: Do not use Uri as location type. Uri doesn't support relative paths for file system files.

    public interface IDataReader
    {
        bool IsValidLocation(Uri location);

        byte[] ReadAllBytes(Uri location);
    }

    internal class FileSystemDataReader : IDataReader
    {
        private readonly IFileSystem fileSystem;

        public FileSystemDataReader(IFileSystem fileSystem) =>
            this.fileSystem = fileSystem;

        public bool IsValidLocation(Uri location)
        {
            // TODO: Implement valid location check.
            throw new NotImplementedException();
        }

        public byte[] ReadAllBytes(Uri location) =>
            fileSystem.File.ReadAllBytes(location.LocalPath);
    }
}
