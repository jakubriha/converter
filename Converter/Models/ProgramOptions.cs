namespace Converter.Models
{
    internal class ProgramOptions
    {
        public string Input { get; set; }

        public string Output { get; set; }

        public FormatType InputFormat { get; set; }

        public FormatType OutputFormat { get; set; }
    }

    internal enum FormatType
    {
        Xml,
        Json
    }
}
