using Hinet.Service.Common;
using Hinet.Service.NotificationService.Dto;

namespace Hinet.Web.Models
{
    public class EndUserNotificationViewModel
    {
        public PageListResultBO<NotificationDto> NotiList { get; set; }
    }
}