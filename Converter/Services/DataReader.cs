using System.IO.Abstractions;

namespace Converter.Services
{
    /// <summary>
    /// Provides method to read data from data source.
    /// </summary>
    public interface IDataReader : IProcessingService<string>
    {
        /// <summary>
        /// Reads all data from a data source to byte array.
        /// </summary>
        /// <param name="path">The data source to read from.</param>
        /// <returns>A byte array containing the contents of the data source.</returns>
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
