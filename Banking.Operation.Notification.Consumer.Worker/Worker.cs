using Banking.Operation.Notification.Consumer.Domain.Notification.Dtos;
using Banking.Operation.Notification.Consumer.Domain.Notification.Parameters;
using Banking.Operation.Notification.Consumer.Domain.Notification.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Banking.Operation.Notification.Consumer.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly INotificationService _notificationService;
        private readonly RabbitParameters _rabbitParameters;

        public Worker(
            ILogger<Worker> logger,
            INotificationService notificationService,
            RabbitParameters rabbitParameters)
        {
            _logger = logger;
            _notificationService = notificationService;
            _rabbitParameters = rabbitParameters;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Waiting for messages...");

            var factory = new ConnectionFactory()
            {
                HostName = _rabbitParameters.HostName,
                UserName = _rabbitParameters.UserName,
                Password = _rabbitParameters.Password,
                Port = _rabbitParameters.Port
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _rabbitParameters.Queue,
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            channel.BasicConsume(queue: _rabbitParameters.Queue,
                autoAck: true,
                consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

        private void Consumer_Received(
            object sender, BasicDeliverEventArgs e)
        {
            var body = Encoding.UTF8.GetString(e.Body.ToArray());

            _logger.LogInformation(
                $"[New message | {DateTime.Now:yyyy-MM-dd HH:mm:ss}] " + body);

            var message = JsonSerializer.Deserialize<Message>(body);

            _notificationService.Notify(message);
        }
    }
}