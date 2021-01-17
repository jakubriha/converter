using Converter.Models;
using Microsoft.Extensions.Logging;
using System;

namespace Converter.Services
{
    internal interface IProgramPipeline
    {
        void ExecutePipeline(ProgramOptions programOptions);
    }

    internal class ProgramPipeline : IProgramPipeline
    {
        private readonly ILogger<ProgramPipeline> logger;
        private readonly IProcessingServicesAggregator processingServicesAggregator;

        public ProgramPipeline(ILogger<ProgramPipeline> logger, IProcessingServicesAggregator processingServicesAggregator)
        {
            this.logger = logger;
            this.processingServicesAggregator = processingServicesAggregator;
        }

        public void ExecutePipeline(ProgramOptions programOptions)
        {
            try
            {
                var (dataReader, formatDeserializer, formatSerializer, dataWriter) = processingServicesAggregator.SelectValidProcessingServices(programOptions);

                var dataToBeDeserialized = dataReader.ReadAllBytes(programOptions.Input);

                var document = formatDeserializer.Deserialize(dataToBeDeserialized);

                var dataToBeSaved = formatSerializer.Serialize(document);

                dataWriter.WriteAllBytes(programOptions.Output, dataToBeSaved);

                logger.LogInformation("Conversion was successful.");
            }
            catch (Exception e)
            {
                logger.LogError(e, "There was an error in the application");
            }
        }
    }
}
