using Converter.Models;
using Converter.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.IO.Abstractions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Converter.Tests")]

namespace Converter
{
    public class Program
    {
        static async Task<int> Main(string[] args) =>
            await BuildCommandLine()
                .UseHost(_ => Host.CreateDefaultBuilder(),
                    host =>
                    {
                        host.ConfigureServices((hostContext, services) =>
                        {
                            // Data reading/writing
                            services.AddTransient<IDataReader, FileSystemDataReader>();
                            services.AddTransient<IDataWriter, FileSystemDataWriter>();

                            // Format deserializers
                            services.AddTransient<IFormatDeserializer, JsonFormatDeserializer>();
                            services.AddTransient<IFormatDeserializer, XmlFormatDeserializer>();

                            // Format serializers
                            services.AddTransient<IFormatSerializer, JsonFormatSerializer>();
                            services.AddTransient<IFormatSerializer, XmlFormatSerializer>();
                            
                            // Program flow
                            services.AddTransient<IProcessingServicesAggregator, ProcessingServicesAggregator>();
                            services.AddTransient<IValidServiceSelector, ValidServiceSelector>();
                            services.AddTransient<IProgramPipeline, ProgramPipeline>();

                            // Others
                            services.AddTransient<IFileSystem, FileSystem>();
                        });
                    })
                .UseDefaults()
                .Build()
                .InvokeAsync(args);

        private static CommandLineBuilder BuildCommandLine()
        {
            var root = new RootCommand("Converter is a utility program to convert data between multiple formats.")
            {
                new Option<string>("--input")
                {
                    IsRequired = true,
                    Description = "Relative or absolute path from where to load the data"
                },
                new Option<FormatType>("--input-format")
                {
                    IsRequired = true,
                    Description = "Input data format"
                },
                new Option<string>("--output")
                {
                    IsRequired = true,
                    Description = "Relative or absolute path to where to save the data"
                },
                new Option<FormatType>("--output-format")
                {
                    IsRequired = true,
                    Description = "Output data format"
                }
            };

            root.Handler = CommandHandler.Create<ProgramOptions, IHost>(Run);

            return new CommandLineBuilder(root);
        }

        private static Task<int> Run(ProgramOptions programOptions, IHost host)
        {
            var programPipeline = host.Services.GetService<IProgramPipeline>();

            var exitCode = programPipeline.ExecutePipeline(programOptions);

            return Task.FromResult(exitCode);
        }
    }
}
