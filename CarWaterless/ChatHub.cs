using Microsoft.AspNet.SignalR;
using CarWaterless.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CarWaterless
{
    public class ChatHub:Hub
    {
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();


        private readonly static ConnectionMappingHelper<string> _connections = new ConnectionMappingHelper<string>();
        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name;
            _connections.Add(name, Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string name = Context.User.Identity.Name;
            _connections.Remove(name, Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }
        public override Task OnReconnected()
        {
            string name = Context.User.Identity.Name;

            if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                _connections.Add(name, Context.ConnectionId);
            }
            return base.OnReconnected();
        }

        public void sendAllS(string obj)
        {
            hubContext.Clients.All.sendAllC(obj);
        }

        public void JoinGroup(string name)
        {
            this.Groups.Add(this.Context.ConnectionId, name);
        }

        public void test()
        {
            Clients.All.fuck();
        }
    }
}