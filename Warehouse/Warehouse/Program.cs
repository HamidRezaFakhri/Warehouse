using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace Warehouse
{
    public class Program
    {
        public static IConfiguration Configuration { get; }
            = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                        .Build();
        
        public static void Main(string[] args)
        {
            //var columnOption = new ColumnOptions();
            //columnOption.Store.Remove(StandardColumn.MessageTemplate);
            //columnOptions.Store.Remove(StandardColumn.Properties);
            //columnOption.AdditionalDataColumns = new Collection<DataColumn>  
            //{
            //    new DataColumn { DataType = typeof(string), ColumnName = "OtherData" },  
            //};

            Log.Logger = new LoggerConfiguration()
                                .MinimumLevel.Information()
                                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                                .Enrich.FromLogContext()
                                .ReadFrom.Configuration(Configuration)
                                //.WriteTo.File("Logs/Example.txt", fileSizeLimitBytes: null,
                                //rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, shared: true,
                                //outputTemplate:
                                //        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                                .CreateLogger();

            try
            {
                Log.Information("Getting the motors running...");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseConfiguration(Configuration)
                //.ConfigureAppConfiguration(builder => { /* App configuration */ })
                //.ConfigureServices(services => { /* Service configuration */})
                .UseSerilog()
                .UseDefaultServiceProvider(options =>
                    options.ValidateScopes = false);
    }
}