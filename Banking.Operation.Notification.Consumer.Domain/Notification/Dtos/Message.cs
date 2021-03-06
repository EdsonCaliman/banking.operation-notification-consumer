using System.Diagnostics.CodeAnalysis;

namespace Banking.Operation.Notification.Consumer.Domain.Notification.Dtos
{
    [ExcludeFromCodeCoverage]
    public class Message
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}