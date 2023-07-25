using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Commands.Hubs
{
    public class MessageHub : Hub
    {
        public Task JoinChat(string chatId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }

        public Task LeaveChat(string chatId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
        }
    }
}
