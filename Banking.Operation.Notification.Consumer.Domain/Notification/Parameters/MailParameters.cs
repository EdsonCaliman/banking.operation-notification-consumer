using System.Diagnostics.CodeAnalysis;

namespace Banking.Operation.Notification.Consumer.Domain.Notification.Parameters
{
    [ExcludeFromCodeCoverage]
    public class MailParameters
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
    }
}