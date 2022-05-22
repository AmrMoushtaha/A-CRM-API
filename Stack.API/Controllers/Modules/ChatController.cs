using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Stack.API.Hubs;
using Stack.DTOs.Requests.Modules.Chat;

namespace Public_Chat.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [Route("send")]                                          
        [HttpPost]
        public IActionResult SendRequest( MessageDto msg)
        {
            _hubContext.Clients.All.SendAsync("Receive", msg);
            return Ok();
        }
    }
}
