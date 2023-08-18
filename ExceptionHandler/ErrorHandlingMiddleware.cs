using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ExceptionHandler.Handler;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using FluentValidation;

namespace ExceptionHandler
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly IExceptionHandlerClass _exceptionHandler;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger, IExceptionHandlerClass exceptionHandler)
        {
            _logger = logger;
            this.next = next;
            _exceptionHandler = exceptionHandler;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var error = _exceptionHandler.HandleException(ex);
                context.Response.StatusCode = (int)error.Status;
                var toBeLogged = new ToLog
                {
                    Endpoint = context.Request.Path,
                    ErrorDetails = error,
                    Method = context.Request.Method,
                    Origin = ex.Source
                };
                // ILogger in loc de Logger
                Logger.LogExceptionToJson(toBeLogged);
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                await context.Response.WriteAsync(JsonConvert.SerializeObject(error, serializerSettings));
            }
        }
    }
    public class ToLog
    {
        public string Method { get; set; }
        public string Endpoint { get; set; }
        public string Origin { get; set; }
        public ErrorDetails ErrorDetails { get; set; }
    }
}
