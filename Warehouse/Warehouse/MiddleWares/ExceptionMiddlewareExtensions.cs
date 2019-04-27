namespace Warehouse.MiddleWares
{
    using System.Net;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.Extensions.Logging;
    using Serilog;

    public static class ExceptionMiddlewareExtensions
    {
        //public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        //{
        //    app.UseExceptionHandler(appError =>
        //    {
        //        appError.Run(async context =>
        //        {
        //            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //            context.Response.ContentType = "application/json";

        //            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

        //            if (contextFeature != null)
        //            {
        //                Log
        //                    .Logger
        //                    .ForContext("OtherData", "Test Data")
        //                    .Information("Middle Ware Error with status: " + context.Response.StatusCode);
        //            }
        //        });
        //    });
        //}
    }
}