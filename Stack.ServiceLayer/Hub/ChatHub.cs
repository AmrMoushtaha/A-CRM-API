using Microsoft.AspNetCore.SignalR;
using Stack.Entities.Models.Modules.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stack.API.Hubs
{
    public class ChatHub : Hub    
    {
        public Task SendMessage(Message msg)  
        {
            return Clients.All.SendAsync("Receive",msg);
        }
    }
}
