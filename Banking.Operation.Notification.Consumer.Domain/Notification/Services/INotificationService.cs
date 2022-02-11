using Banking.Operation.Notification.Consumer.Domain.Notification.Dtos;
using System.Threading.Tasks;

namespace Banking.Operation.Notification.Consumer.Domain.Notification.Services
{
    public interface INotificationService
    {
        Task Notify(Message message);
    }
}
