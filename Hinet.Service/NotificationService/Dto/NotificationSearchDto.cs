using Hinet.Service.Common;

namespace Hinet.Service.NotificationService.Dto
{
    public class NotificationSearchDto : SearchBase
    {
        public bool IsReadFilter { get; set; }
        public long? FromUserFilter { get; set; }
        public long? ToUserFilter { get; set; }
        public string MessageFilter { get; set; }
        public string TypeFilter { get; set; }
    }
}