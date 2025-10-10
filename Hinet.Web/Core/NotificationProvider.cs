using Hinet.Model.Entities;
using Hinet.Service.Constant;
using Hinet.Web.HubControl;
using Microsoft.AspNet.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace Hinet.Web.Core
{
    public class NotificationProvider
    {
        public static async Task SendMessage(Notification tb)
        {
            var tbHub = GlobalHost.ConnectionManager.GetHubContext<ThongBaoHub>();
            if (tb.Type == NotificationTypeConstant.Global)
            {
                var userConnnect = RepositoryConnectUser.AllChuyenvien();
                tbHub.Clients.Clients(userConnnect.ToArray()).thongbaoglobal(tb.Message, tb.Link);
            }
            else
            {
                var userConnnect = RepositoryConnectUser.Find(tb.ToUser);
                if (userConnnect != null && userConnnect.LstConnection != null && userConnnect.LstConnection.Any())
                {
                    //if (userConnnect.TypeAccount == AccountTypeConstant.BussinessUser)
                    //{
                    tbHub.Clients.Clients(userConnnect.LstConnection.ToArray()).thongbao(tb.Message, tb.Link, false);
                    //}
                    //else
                    //{
                    //    tbHub.Clients.Clients(userConnnect.LstConnection.ToArray()).thongbao(tb.Message, tb.Link, true);

                    //}
                }
            }
        }
    }
}