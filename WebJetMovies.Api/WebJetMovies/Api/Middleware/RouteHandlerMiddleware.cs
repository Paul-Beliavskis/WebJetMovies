using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebJetMovies.Api.Constants;

namespace WebJetMovies.Api.Middleware
{
    public class RouteHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public RouteHandlerMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await _next.Invoke(httpContext);

            if (httpContext.Response.StatusCode == 404 &&
                !Path.HasExtension(httpContext.Request.Path.Value) &&
                !httpContext.Request.Path.Value.StartsWith("/api"))
            {
                httpContext.Request.Path = "/" + ApiConstants.Defaults.DefaultPage;
                httpContext.Response.StatusCode = 200;
                await _next.Invoke(httpContext);
            }
        }
    }
}
