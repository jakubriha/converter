using Converter.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
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
                            services.AddTransient<RootService>();
                        });
                    })
                .UseDefaults()
                .Build()
                .InvokeAsync(args);

        // TODO: Implement other parameters.
        private static CommandLineBuilder BuildCommandLine()
        {
            var root = new RootCommand()
            {
                new Option<string>("--input")
                {
                    IsRequired = true,
                    Description = "Input path"
                },
                new Option<string>("--output")
                {
                    IsRequired = true,
                    Description = "Output path"
                },
                new Option<FormatType>("--input-format")
                {
                    IsRequired = true,
                    Description = "Input format"
                },
                new Option<FormatType>("--output-format")
                {
                    IsRequired = true,
                    Description = "Output format"
                }
            };

            root.Handler = CommandHandler.Create<ProgramOptions, IHost>(Run);

            return new CommandLineBuilder(root);
        }

        private static void Run(ProgramOptions programOptions, IHost host)
        {
            var srv = host.Services.GetService<RootService>();

            srv.Run(programOptions);
        }
    }

    internal class RootService
    {
        public void Run(ProgramOptions programOptions)
        {
            //throw new NotImplementedException();
        }
    }
}
