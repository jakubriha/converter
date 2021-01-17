using Converter.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Converter.Tests
{
    public class ValidServiceSelectorTests
    {
        [Fact]
        public void SingleValidServiceIsProvided_TheServiceIsReturned()
        {
            var serviceMocks = GenerateServiceMocks(true);

            IValidServiceSelector validServiceSelector = new ValidServiceSelector(null);

            IProcessingService<string> validService = validServiceSelector.SelectValidService(serviceMocks, "", null);

            validService.Should().BeSameAs(serviceMocks[0]);
        }

        [Fact]
        public void SingleInvalidServiceIsProvided_NullIsReturned()
        {
            var serviceMocks = GenerateServiceMocks(false);

            IValidServiceSelector validServiceSelector = new ValidServiceSelector(Substitute.For<ILogger<ValidServiceSelector>>());

            IProcessingService<string> validService = validServiceSelector.SelectValidService(serviceMocks, "", null);

            validService.Should().BeNull();
        }

        [Fact]
        public void TwoServicesWithTheSecondValidAreProvided_TheSecondIsReturned()
        {
            var serviceMocks = GenerateServiceMocks(false, true);

            IValidServiceSelector validServiceSelector = new ValidServiceSelector(null);

            IProcessingService<string> validService = validServiceSelector.SelectValidService(serviceMocks, "", null);

            validService.Should().BeSameAs(serviceMocks[1]);
        }

        private static IList<IProcessingService<string>> GenerateServiceMocks(params bool[] servicesValidity) =>
            servicesValidity.Select(validity =>
            {
                var service = Substitute.For<IProcessingService<string>>();
                service.IsValidService(Arg.Any<string>()).Returns(validity);

                return service;
            }).ToList();
    }
}
