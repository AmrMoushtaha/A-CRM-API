using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Stack.Core;
using Stack.DTOs.Requests.Shared;
using Stack.Entities.Models.Modules.Chat;
using Stack.ServiceLayer.Modules.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stack.API.Hubs
{
    public class ChatHub : Hub    
    {

        private readonly UnitOfWork unitOfWork;
        private readonly ChatService ChatService;
        private readonly IMapper mapper;

        public ChatHub(UnitOfWork unitOfWork, IMapper mapper, ChatService ChatService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.ChatService = ChatService;
        }

        public async Task SendMessage(HubAddMsg msg)  
        {
            if (msg.ConversationID != 0)
            {

                AddMsg msgTocreate = new AddMsg()
                {
                    Content = msg.Content,
                    ConversationID = msg.ConversationID
                };

                var result = await ChatService.Add_Message(msgTocreate);

                var conversationUsers = await ChatService.Get_UsersConversation(msg.ConversationID);

                if (result.Succeeded)

                    for (var i = 0; i < conversationUsers.Data.Count; i++)
                    {
                        await Clients.User(conversationUsers.Data[i].ApplicationUserID).SendAsync("Receive", msg);
                    }

            }
            else
            {
                ConversationToCreate msgTocreate = new ConversationToCreate()
                {
                    Content = msg.Content,
                    ReceiverID = msg.ReceiverID
                };

                var result = await ChatService.Create_Conversation(msgTocreate);
                if (result.Succeeded)
                    await Clients.User(msg.ReceiverID).SendAsync("Receive", msg);

            }
            
        }
    }
}
