using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hinet.Web.HubControl
{
    public class NotificationHub : Hub
    {
        public override System.Threading.Tasks.Task OnConnected()
        {
            var userId = Context.QueryString["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
                Groups.Add(Context.ConnectionId, userId);
            }
            return base.OnConnected();
        }
    }
}