using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Stack.API.Controllers.Common;
using Stack.API.Hubs;
using Stack.DTOs.Requests.Modules.Chat;
using Stack.DTOs.Requests.Shared;
using Stack.ServiceLayer.Modules.Chat;

namespace Public_Chat.Controllers
{
    [Route("api/chat")]
    [ApiController]


    public class ChatController : BaseResultHandlerController<ChatService>
    {

        public ChatController(ChatService _service) : base(_service)
        {
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddMessage(AddMsg AddMsg)
        {
            return await AddItemResponseHandler(async () => await service.Add_Message(AddMsg));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateConversation(ConversationToCreate model)
        {
            return await AddItemResponseHandler(async () => await service.Create_Conversation(model));
        }

        [AllowAnonymous]
        [HttpGet("{ID}")]
        public async Task<IActionResult> DeleteConversation(long ID)
        {
            return await GetResponseHandler(async () => await service.Delete_Conversation(ID));
        }

        [AllowAnonymous]
        [HttpGet("{ID}")]
        public async Task<IActionResult> GetConversationMessages(long ID)
        {
            return await GetResponseHandler(async () => await service.Get_ConversationMessages(ID));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetUserConversations()
        {
            return await GetResponseHandler(async () => await service.Get_UserConversations());
        }
        [AllowAnonymous]
        [HttpGet("{ID}")]
        public async Task<IActionResult> GetUsersConversation(long ID)
        {
            return await GetResponseHandler(async () => await service.Get_UsersConversation(ID));
        }


        [AllowAnonymous]
        [HttpGet("{ID}")]
        public async Task<IActionResult> DeleteMessage(long ID)
        {
            return await GetResponseHandler(async () => await service.Delete_Message(ID));
        }

    }
}
