using System;
using System.IO.Abstractions;

namespace Converter.Services
{
    // TODO: Do not use Uri as location type. Uri doesn't support relative paths for file system files.

    public interface IDataReader : IProcessingService<string>
    {
        byte[] ReadAllBytes(string path);
    }

    internal class FileSystemDataReader : IDataReader
    {
        private readonly IFileSystem fileSystem;

        public FileSystemDataReader(IFileSystem fileSystem) =>
            this.fileSystem = fileSystem;

        public bool IsValidService(string input)
        {
            // TODO: Implement valid location check.
            throw new NotImplementedException();
        }

        public byte[] ReadAllBytes(string path) =>
            fileSystem.File.ReadAllBytes(path);
    }
}
