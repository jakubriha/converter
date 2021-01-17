using Converter.Models;
using Microsoft.Extensions.Logging;
using System;

namespace Converter.Services
{
    /// <summary>
    /// Represents main program pipeline.
    /// </summary>
    internal interface IProgramPipeline
    {
        /// <summary>
        /// Executes main program pipeline.
        /// </summary>
        /// <param name="programOptions">Program command-line options.</param>
        /// <returns>Program exit code.</returns>
        int ExecutePipeline(ProgramOptions programOptions);
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

        public int ExecutePipeline(ProgramOptions programOptions)
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

                return -1;
            }

            return 0;
        }
    }
}
