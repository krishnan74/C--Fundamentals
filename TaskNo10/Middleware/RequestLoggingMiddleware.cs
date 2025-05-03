using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace TaskNo10.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // Called for each HTTP request
        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation($"Handling request: {context.Request.Method} {context.Request.Path}");

            await _next(context);
            
            _logger.LogInformation($"Finished handling request.");
        }
    }

}
