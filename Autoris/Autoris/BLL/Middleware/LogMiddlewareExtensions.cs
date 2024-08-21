using Microsoft.AspNetCore.Builder;

namespace Autoris.BLL.Middleware
{
    public static class LogMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogMiddleware>();
        }
    }

}
