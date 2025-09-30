using Hinet.Model.Entities;
using Hinet.Model.IdentityEntities;

namespace Hinet.Service.NotificationService.Dto
{
    public class NotificationDto : Notification
    {
        public AppUser FromUserInfo { get; set; }
        public string FromUserName { get; internal set; }
    }
}