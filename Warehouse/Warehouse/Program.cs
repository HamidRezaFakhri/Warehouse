using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Warehouse
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                //.ConfigureLogging((hostingContext, logging) =>
                //{
                //logging.AddConsole()
                //       .AddFilter<ConsoleLoggerProvider>
                //           (category: null, level: LogLevel.Information)
                //       // or
                //       .AddFilter<ConsoleLoggerProvider>
                //           ((category, level) => category == "A" ||
                //               level == LogLevel.Critical));
                //    })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.ClearProviders(); // removes all providers from LoggerFactory
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole(options => options.IncludeScopes = true);
                    logging.AddDebug();
                    logging.AddEventSourceLogger();
                    logging.AddTraceSource("Information, ActivityTracing"); // Add Trace listener provider
                })
                .UseStartup<Startup>()
                .UseDefaultServiceProvider(options =>
                    options.ValidateScopes = false)
            ;
    }
}