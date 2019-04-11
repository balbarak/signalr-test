using Microsoft.AspNetCore.SignalR;
using SignalrTest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalrTest.Server.Protocol
{
    public class HubProtocol : Hub
    {
        public override async Task OnConnectedAsync()
        {
           await base.OnConnectedAsync();

            await Groups.AddToGroupAsync(Context.ConnectionId, AppClient.GROUP_ID);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task<bool> SendGroupMessage(ProtocolGroupMessage protoclMessage)
        {

            await Clients.OthersInGroup(protoclMessage.GroupId)
                .SendAsync("OnGroupMessage", protoclMessage);

            return true;
        }

    }
}
