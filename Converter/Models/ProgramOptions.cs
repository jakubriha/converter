namespace Converter.Models
{
    /// <summary>
    /// Stores program command-line options.
    /// </summary>
    internal class ProgramOptions
    {
        public string Input { get; set; }

        public string Output { get; set; }

        public FormatType InputFormat { get; set; }

        public FormatType OutputFormat { get; set; }
    }

    /// <summary>
    /// Represents data format.
    /// </summary>
    internal enum FormatType
    {
        Xml,
        Json
    }
}
