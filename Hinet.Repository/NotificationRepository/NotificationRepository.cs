using Hinet.Model.Entities;
using System.Data.Entity;
using System.Linq;

namespace Hinet.Repository.NotificationRepository
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(DbContext context)
            : base(context)
        {
        }

        public Notification GetById(long id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }
    }
}