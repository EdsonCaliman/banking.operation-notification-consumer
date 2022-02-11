using Banking.Operation.Notification.Consumer.Domain.Notification.Dtos;
using Banking.Operation.Notification.Consumer.Domain.Notification.Parameters;
using Banking.Operation.Notification.Consumer.Domain.Notification.Services;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace Banking.Operation.Notification.Consumer.Infra.Data.ExternalServices
{
    public class NotificationService : INotificationService
    {
        private readonly MailParameters _mailParameters;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(MailParameters mailParameters, ILogger<NotificationService> logger)
        {
            _mailParameters = mailParameters;
            _logger = logger;
        }

        public Task Notify(Message message)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress(_mailParameters.Name, _mailParameters.Email));
            mailMessage.To.Add(new MailboxAddress(message.Name, message.Email));
            mailMessage.Subject = message.Subject;

            mailMessage.Body = new TextPart("plain")
            {
                Text = message.Body
            };

            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect(_mailParameters.Host, _mailParameters.Port, _mailParameters.UseSSL);

                    client.Authenticate(_mailParameters.UserName, _mailParameters.Password);

                    client.Send(mailMessage);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on send message to smtp client.");
                throw;
            }

            return Task.CompletedTask;
        }
    }
}