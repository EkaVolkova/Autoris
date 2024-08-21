using Autoris.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autoris.Middleware
{
    public class LogMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var path = httpContext.Request.Path;
            var ip = httpContext.Connection.RemoteIpAddress.ToString();
            _logger.WriteEvent(new Event($"IP: {ip}. Функция {path} запущена"));
            await _next(httpContext);
        }
    }

}
