using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Converter.Services
{
    internal interface IValidServiceSelector
    {
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
