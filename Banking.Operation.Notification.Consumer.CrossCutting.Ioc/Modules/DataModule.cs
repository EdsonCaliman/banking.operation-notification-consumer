using Banking.Operation.Notification.Consumer.Domain.Notification.Parameters;
using Banking.Operation.Notification.Consumer.Domain.Notification.Services;
using Banking.Operation.Notification.Consumer.Infra.Data.ExternalServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Banking.Operation.Notification.Consumer.CrossCutting.Ioc.Modules
{
    public static class DataModule
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            var rabbitParameters = configuration.GetSection("RabbitParameters").Get<RabbitParameters>();
            services.AddSingleton(rabbitParameters);
            var mailParameters = configuration.GetSection("MailParameters").Get<MailParameters>();
            services.AddSingleton(mailParameters);
            services.AddScoped<INotificationService, NotificationService>();
        }
    }
}