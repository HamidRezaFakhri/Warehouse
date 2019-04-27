namespace Warehouse.MiddleWares
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    //public class AuthorizationMiddleware
    //{
    //    private readonly RequestDelegate _next;

    //    public AuthorizationMiddleware(RequestDelegate next)
    //    {
    //        _next = next;
    //    }

    //    public Task Invoke(HttpContext httpContext)
    //    {
    //        httpContext.Response.Headers.Add("X-Xss-Protection", "1");
    //        httpContext.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
    //        httpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");

    //        return _next(httpContext);
    //    }
    //}
}