using Converter.Services;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Converter.Tests
{
    public class ProgramPipelineTests
    {
        [Fact]
        public void SingleValidReaderIsProvided_TheReaderIsReturned()
        {
            var dataReaders = GenerateDataReaderMocks(true);

            IProgramPipeline pipeline = new ProgramPipeline(dataReaders);

            IDataReader correctReader = pipeline.SelectCorrectDataReader(new Uri(@"C:\test.txt"));

            correctReader.Should().BeSameAs(dataReaders[0]);
        }

        [Fact]
        public void SingleInvalidReaderIsProvided_NullIsReturned()
        {
            var dataReaders = GenerateDataReaderMocks(false);

            IProgramPipeline pipeline = new ProgramPipeline(dataReaders);

            IDataReader nullReader = pipeline.SelectCorrectDataReader(null);

            nullReader.Should().BeNull();
        }

        [Fact]
        public void TwoReadersWithTheSecondValidAreProvided_TheSecondIsReturned()
        {
            var dataReaders = GenerateDataReaderMocks(false, true);

            IProgramPipeline pipeline = new ProgramPipeline(dataReaders);

            IDataReader correctReader = pipeline.SelectCorrectDataReader(new Uri(@"C:\test.txt"));

            correctReader.Should().BeSameAs(dataReaders[1]);
        }

        [Fact]
        public void NoReaderIsProvided_ExceptionIsThrown() =>
            Assert.ThrowsAny<Exception>(() => new ProgramPipeline(Array.Empty<IDataReader>()));

        private static IList<IDataReader> GenerateDataReaderMocks(params bool[] dataReadersValidity) =>
            dataReadersValidity.Select(validity =>
            {
                var dataReader = Substitute.For<IDataReader>();
                dataReader.IsValidLocation(new Uri(@"C:\test.txt")).Returns(validity);

                return dataReader;
            }).ToList();
    }
}
