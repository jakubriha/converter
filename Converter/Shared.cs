using System;

namespace Converter
{
    internal class Shared
    {
        public static bool IsFileSystemPath(string path)
        {
            var isValidUri = Uri.TryCreate(path, UriKind.RelativeOrAbsolute, out var uri);

            return isValidUri && (!uri.IsAbsoluteUri || uri.Scheme == "file");
        }
    }
}
