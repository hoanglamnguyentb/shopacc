using Hinet.Model.Entities;

namespace Hinet.Repository.NotificationRepository
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        Notification GetById(long id);
    }
}