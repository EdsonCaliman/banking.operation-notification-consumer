using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Banking.Operation.Notification.Consumer.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Worker> _logger;

        public Worker(IHostEnvironment env, ILogger<Worker> logger)
        {
            _logger = logger;

            var environmentName = env.EnvironmentName;
            var builder = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{(string.IsNullOrEmpty(environmentName) ? "Development" : environmentName)}.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Aguardando mensagens...");

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "admin",
                Port = 5672,
                VirtualHost = "banking"
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "banking.operation-notification",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            channel.BasicConsume(queue: "banking.operation-notification",
                autoAck: true,
                consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation(
                    $"Worker ativo em: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                await Task.Delay(1000, stoppingToken);
            }
        }

        private void Consumer_Received(
            object sender, BasicDeliverEventArgs e)
        {
            _logger.LogInformation(
                $"[Nova mensagem | {DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
                Encoding.UTF8.GetString(e.Body.ToArray()));
        }
    }
}
