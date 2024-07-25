using Ordering.APIs.Errors;
using System.Net;
using System.Text.Json;

namespace Ordering.APIs.Middelwares
{
    public class ExceptionMiddelware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddelware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddelware(RequestDelegate next, ILogger<ExceptionMiddelware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                // Log Exception in Database [Production]
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment() ? 
                               new ApiExceptionResponse(500, ex.Message, ex.StackTrace.ToString()) 
                               : new ApiExceptionResponse(500);

                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);
            }
        }

    }
}
