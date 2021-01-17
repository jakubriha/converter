using System;
using System.Collections.Generic;
using System.Linq;

namespace Converter.Services
{
    internal interface IProgramPipeline
    {
        IDataReader SelectCorrectDataReader(Uri location);
    }

    internal class ProgramPipeline : IProgramPipeline
    {
        private readonly IEnumerable<IDataReader> dataReaders;

        public ProgramPipeline(IEnumerable<IDataReader> dataReaders)
        {
            if (!dataReaders.Any())
            {
                throw new ArgumentOutOfRangeException(nameof(dataReaders), "No data readers provided.");
            }

            this.dataReaders = dataReaders;
        }

        public IDataReader SelectCorrectDataReader(Uri location) =>
            dataReaders.FirstOrDefault(dataReader => dataReader.IsValidLocation(location));
    }
}
