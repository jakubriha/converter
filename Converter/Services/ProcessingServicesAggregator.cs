using Converter.Models;
using System.Collections.Generic;

namespace Converter.Services
{
    internal interface IProcessingServicesAggregator
    {
        (IDataReader dataReader, IFormatDeserializer formatDeserializer, IFormatSerializer formatSerializer, IDataWriter dataWriter) SelectValidProcessingServices(ProgramOptions programOptions);
    }

    internal class ProcessingServicesAggregator : IProcessingServicesAggregator
    {
        private readonly IEnumerable<IDataReader> dataReaders;
        private readonly IEnumerable<IFormatDeserializer> formatDeserializers;
        private readonly IEnumerable<IFormatSerializer> formatSerializers;
        private readonly IEnumerable<IDataWriter> dataWriters;
        private readonly IValidServiceSelector validServiceSelector;

        public ProcessingServicesAggregator(
            IEnumerable<IDataReader> dataReaders,
            IEnumerable<IFormatDeserializer> formatDeserializers,
            IEnumerable<IFormatSerializer> formatSerializers,
            IEnumerable<IDataWriter> dataWriters,
            IValidServiceSelector validServiceSelector)
        {
            this.dataReaders = dataReaders;
            this.formatDeserializers = formatDeserializers;
            this.formatSerializers = formatSerializers;
            this.dataWriters = dataWriters;
            this.validServiceSelector = validServiceSelector;
        }

        public (IDataReader dataReader, IFormatDeserializer formatDeserializer, IFormatSerializer formatSerializer, IDataWriter dataWriter) SelectValidProcessingServices(ProgramOptions programOptions) =>
        (
            validServiceSelector.SelectValidService(dataReaders, programOptions.Input, "Invalid input parameter."),
            validServiceSelector.SelectValidService(formatDeserializers, programOptions.InputFormat, "Invalid format input parameter."),
            validServiceSelector.SelectValidService(formatSerializers, programOptions.OutputFormat, "Invalid format output parameter."),
            validServiceSelector.SelectValidService(dataWriters, programOptions.Output, "Invalid output parameter.")
        );
    }
}
