using Microsoft.AspNetCore.Builder;
using WebJetMovies.Api.Middleware;

namespace WebJetMovies.Api.Extensions
{
    public static class ConfigureCustomMiddleware
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }

        public static IApplicationBuilder UseRouteHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RouteHandlerMiddleware>();
        }
    }
}
