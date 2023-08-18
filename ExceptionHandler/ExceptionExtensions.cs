using ExceptionHandler.Handler;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ExceptionHandler
{
    public static class ExceptionExtensions
    {
        public static IServiceCollection AddErrorHandler(this IServiceCollection services)
        {
            services.AddSingleton<IExceptionHandlerClass, ExceptionHandlerClass>();
            return services;
        }

        public static IApplicationBuilder UseErrorHandler( this IApplicationBuilder application)
        {
            application.UseMiddleware<ErrorHandlingMiddleware>();
            return application;
        }
    }
}
