using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Debug.Assert(dataReaders.Any());

            this.dataReaders = dataReaders;
        }

        public IDataReader SelectCorrectDataReader(Uri location) =>
            dataReaders.FirstOrDefault(dataReader => dataReader.IsValidLocation(location));
    }
}
