using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Stack.API.Hubs;
using Stack.DTOs.Requests.Modules.Chat;
using Stack.DTOs.Requests.Shared;
using Stack.Repository.Common;
using Stack.ServiceLayer.Modules.Chat;

namespace Public_Chat.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChatHubController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ChatService _chatService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChatHubController(IHubContext<ChatHub> hubContext, ChatService chatService, IHttpContextAccessor httpContextAccessor)
        {
            _hubContext = hubContext;
            _chatService = chatService;
             _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> Send(HubAddMsg msg)
        {
            var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

            if (userID != null)
            {
                if (msg.ConversationID != 0)
                {

                    AddMsg msgTocreate = new AddMsg()
                    {
                        Content = msg.Content,
                        ConversationID = msg.ConversationID
                    };

                    var result = await _chatService.Add_Message(msgTocreate);

                    var conversationUsers = await _chatService.Get_UsersConversation(msg.ConversationID);

                    if (result.Succeeded)
                    {
                        conversationUsers.Data = conversationUsers.Data.FindAll(a => a.ApplicationUserID != userID);
                        for (var i = 0; i < conversationUsers.Data.Count; i++)
                        {
                            await _hubContext.Clients.User(conversationUsers.Data[i].ApplicationUserID).SendAsync("Receive", msg);
                        }
                    }

                }
                else
                {
                    ConversationToCreate msgTocreate = new ConversationToCreate()
                    {
                        Content = msg.Content,
                        ReceiverID = msg.ReceiverID
                    };

                    var result = await _chatService.Create_Conversation(msgTocreate);
                    if (result.Succeeded)
                        await _hubContext.Clients.User(msg.ReceiverID).SendAsync("Receive", msg);

                }
            }
          //  _hubContext.Clients.All.SendAsync("Receive", msg);
            return Ok();
        }
    }
}
