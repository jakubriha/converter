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
        private readonly ProcessingServicesAggregator validProcessingServicesSelector;

        public ProgramPipeline(ILogger<ProgramPipeline> logger, ProcessingServicesAggregator validProcessingServicesSelector)
        {
            this.logger = logger;
            this.validProcessingServicesSelector = validProcessingServicesSelector;
        }

        public void ExecutePipeline(ProgramOptions programOptions)
        {
            try
            {
                var (dataReader, formatDeserializer, formatSerializer, dataWriter) = validProcessingServicesSelector.SelectValidProcessingServices(programOptions);

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
