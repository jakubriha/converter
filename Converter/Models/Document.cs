using System;
using System.Text.Json.Serialization;

namespace Converter.Models
{
    internal readonly struct Document
    {
        [JsonConstructor]
        public Document(string title, string text)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public string Title { get; init; }

        public string Text { get; init; }
    }
}
