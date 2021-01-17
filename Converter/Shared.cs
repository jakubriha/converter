using System;
using System.Text.Json;

namespace Converter
{
    /// <summary>
    /// Shared class used across the application.
    /// </summary>
    internal class Shared
    {
        public static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        public static bool IsFileSystemPath(string path)
        {
            var isValidUri = Uri.TryCreate(path, UriKind.RelativeOrAbsolute, out var uri);

            return isValidUri && (!uri.IsAbsoluteUri || uri.Scheme == "file");
        }
    }
}
