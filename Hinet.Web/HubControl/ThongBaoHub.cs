using Hinet.Web.Core;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace Hinet.Web.HubControl
{
    public class ThongBaoHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void SendCanhBao(long? userId, string tram, string message)
        {
            Clients.All.CanhBao(userId, tram, message);
        }

        public void init(long idUser, string connectId, string type, bool isToaDam)
        {
            RepositoryConnectUser.Save(idUser, type, connectId, isToaDam);
        }

        public void initByType(long idUser, string connectId, int idToaDam, string type)
        {
            RepositoryConnectUserToaDam.Save(idUser, connectId, idToaDam, type);
        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            //Clients.Others.userConnected(Context.ConnectionId);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool ak)
            {
            //Clients.Others.userLeft(Context.ConnectionId);
            RepositoryConnectUser.Remove(Context.ConnectionId);
            return base.OnDisconnected(false);
        }
    }
}