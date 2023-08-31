using Microsoft.Extensions.DependencyInjection;

namespace CVU.CONDICA.Application.Services.Infrastructure
{
    public class ServiceActivator
    {
        internal static IServiceProvider ServiceProvider;

        public static void Configure(IServiceProvider sp)
        {
            ServiceProvider = sp;
        }

        public static IServiceScope GetScope()
        {
            var sp1 = ServiceProvider.GetRequiredService<IServiceScopeFactory>();

            var sp2 = sp1.CreateScope();

            return sp2;
        }
    }
}
