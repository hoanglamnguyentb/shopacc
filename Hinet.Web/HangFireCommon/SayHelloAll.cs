using Hinet.Model.Entities;
using Hinet.Service.Constant;
using Hinet.Web.Core;

namespace Hinet.Web.HangFireCommon
{
    public class SayHelloAll
    {
        public void hello(string mes)
        {
            var notifycation = new Notification()
            {
                FromUser = 1,
                Message = mes,
                Type = NotificationTypeConstant.Global,
            };

            NotificationProvider.SendMessage(notifycation);
        }
    }
}