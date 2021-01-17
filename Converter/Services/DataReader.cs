using System.IO.Abstractions;

namespace Converter.Services
{
    public interface IDataReader : IProcessingService<string>
    {
        byte[] ReadAllBytes(string path);
    }

    internal class FileSystemDataReader : IDataReader
    {
        private readonly IFileSystem fileSystem;

        public FileSystemDataReader(IFileSystem fileSystem) =>
            this.fileSystem = fileSystem;

        public bool IsValidService(string parameter) =>
            Shared.IsFileSystemPath(parameter);

        public byte[] ReadAllBytes(string path) =>
            fileSystem.File.ReadAllBytes(path);
    }
}
