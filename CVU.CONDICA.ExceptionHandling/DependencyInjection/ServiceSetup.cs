using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace CVU.CONDICA.ExceptionHandling.DependencyInjection
{
    public static class ServiceSetup
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMiddleware<GlobalErrorHandlingMiddleware>();


        public static void AddExceptionFilter(this IServiceCollection services)
        {
        }
    }
}
