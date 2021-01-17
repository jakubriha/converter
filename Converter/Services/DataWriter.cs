﻿using System.IO.Abstractions;

namespace Converter.Services
{
    internal interface IDataWriter : IProcessingService<string>
    {
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
