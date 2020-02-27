using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalrCoreDemoWithSqlTableDependency.Hubs
{
    public class ChatHub : Hub
    {
        public void SendToAll(string name, string message)
        {
            Clients.All.InvokeAsync("sendToAll", name, message);
            //Clients.User(name + "2").InvokeAsync("sendToAll", name, message);
        }
    }
}
