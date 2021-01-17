using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Converter.Services
{
    /// <summary>
    /// Provides ability to select a single valid service from a collection of services.
    /// </summary>
    internal interface IValidServiceSelector
    {
        /// <summary>
        /// Selects a single valid service from a collection of services.
        /// </summary>
        /// <typeparam name="ServiceT">Type of service.</typeparam>
        /// <typeparam name="ValidityT">Type of validation data.</typeparam>
        /// <param name="services">Services to select from.</param>
        /// <param name="input">Validation parameter used to determine valid service.</param>
        /// <param name="noValidServiceErrorMessage">Error message if no valid service is available.</param>
        /// <returns>The valid service.</returns>
        ServiceT SelectValidService<ServiceT, ValidityT>(IEnumerable<ServiceT> services, ValidityT input, string noValidServiceErrorMessage)
            where ServiceT : IProcessingService<ValidityT>;
    }

    public class ValidServiceSelector : IValidServiceSelector
    {
        private readonly ILogger<ValidServiceSelector> logger;

        public ValidServiceSelector(ILogger<ValidServiceSelector> logger) =>
            this.logger = logger;

        public ServiceT SelectValidService<ServiceT, ValidityT>(IEnumerable<ServiceT> services, ValidityT input, string noValidServiceErrorMessage)
            where ServiceT : IProcessingService<ValidityT>
        {
            Debug.Assert(services.Any());

            var validService = services.FirstOrDefault(service => service.IsValidService(input));

            if (validService == null)
            {
                logger.LogError(noValidServiceErrorMessage);
            }

            return validService;
        }
    }
}
