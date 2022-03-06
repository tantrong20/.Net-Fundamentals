using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                /*await HandleExceptionAsync(httpContext, ex);*/
            }
        }

        /*private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                return;
            }
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var baseEx = ex.GetBaseException();

            //
            // Convert to model
            var message = !_env.IsProduction() || ex is DomainException ? baseEx.GetErrorMessages() : "InternalServerError";
            var error = new ErrorViewModel(message, !_env.IsProduction() ? baseEx.StackTrace : null);
            ExceptionLogger.LogToFile(message + "\n" + baseEx.StackTrace);
            _logger.LogError(baseEx, baseEx.Message);
            //
            // Return as json
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(error));
        }*/
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
