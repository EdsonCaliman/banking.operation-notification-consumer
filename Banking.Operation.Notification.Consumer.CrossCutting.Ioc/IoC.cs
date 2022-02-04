using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Banking.Operation.Notification.Consumer.CrossCutting.Ioc.Modules;

namespace Banking.Operation.Notification.Consumer.CrossCutting.Ioc
{
    public static class IoC
    {
        public static IServiceCollection ConfigureContainer(this IServiceCollection services, IConfiguration configuration)
        {
            DataModule.Register(services, configuration);
            services.Register(configuration);
            return services;
        }
    }
}
