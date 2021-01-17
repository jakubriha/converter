using System.IO.Abstractions;

namespace Converter.Services
{
    /// <summary>
    /// Provides method to write data to data source.
    /// </summary>
    internal interface IDataWriter : IProcessingService<string>
    {
        /// <summary>
        /// Writes whole byte array to a data source.
        /// </summary>
        /// <param name="path">The data source to write to.</param>
        /// <param name="data">A byte array to be written to data source.</param>
        void WriteAllBytes(string path, byte[] data);
    }

    internal class FileSystemDataWriter : IDataWriter
    {
        private readonly IFileSystem fileSystem;

        public FileSystemDataWriter(IFileSystem fileSystem) =>
            this.fileSystem = fileSystem;

        public bool IsValidService(string parameter) =>
            Shared.IsFileSystemPath(parameter);

        public void WriteAllBytes(string path, byte[] data) =>
            fileSystem.File.WriteAllBytes(path, data);
    }
}
