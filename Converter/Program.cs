using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Threading.Tasks;

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

    public class RootService
    {
        public void Run(ProgramOptions programOptions)
        {
            //throw new NotImplementedException();
        }
    }

    public class ProgramOptions
    {
        public string Input { get; set; }

        public string Output { get; set; }
    }
}
