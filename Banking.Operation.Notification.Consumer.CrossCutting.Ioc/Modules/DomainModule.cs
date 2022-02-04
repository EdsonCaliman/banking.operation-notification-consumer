using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Banking.Operation.Notification.Consumer.CrossCutting.Ioc.Modules
{
    public static class DomainModule
    {
        public static void Register(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IService, Service>();
        }
    }
}
