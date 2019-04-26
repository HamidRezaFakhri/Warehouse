using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Warehouse.Extentions;
using Warehouse.Models;
using Warehouse.Options;
using Warehouse.StartUp;

namespace Warehouse
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //public Startup(IHostingEnvironment env)
        //{
        //    var configPath = Path.Combine(env.ContentRootPath, "_config");

        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(configPath)
        //        .AddJsonFile("dataaccess.json", optional: true, reloadOnChange: true)
        //        .AddEnvironmentVariables();
        //    Configuration = builder.Build();
        //}

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            ///????????
            services.AddWebEncoders();
            services.AddOptions();
            services.AddLogging();

            services.AddDataAccess<WarehouseContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<WarehouseContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("WarehouseContext"),
                    opts => opts.CommandTimeout(120))
                    //options.UseLoggerFactory(MyConsoleLoggerFactory)
                    //options.EnableSensitiveDataLogging(true)
                    );
        }

        private ConnectionString BuildConnectionString(IServiceCollection services)
        {
            var section = Configuration.GetSection("ConnectionString");
            services.Configure<ConnectionString>(section);

            var configureOptions = services.BuildServiceProvider().GetRequiredService<IConfigureOptions<ConnectionString>>();
            var connectionString = new ConnectionString();
            configureOptions.Configure(connectionString);
            return connectionString;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddSerilog();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseWebApiExceptionHandler();
                //app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
