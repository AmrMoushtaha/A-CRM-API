
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Stack.DTOs.Requests.Modules.Activities;
using Stack.DTOs;
using System.Threading.Tasks;
using System;
using Stack.DTOs.Enums;
using Stack.Entities.Models.Modules.Activities;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Stack.DTOs.Requests.Modules.Pool;
using Stack.DTOs.Requests.Modules.Chat;
using Stack.Repository.Common;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using Stack.Entities.Models.Modules.Chat;
using Stack.DTOs.Requests.Shared;
using Stack.API.Hubs;

namespace Stack.ServiceLayer.Modules.Chat
{
    public class ChatService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        private readonly ChatHub ChatHub;
        public ChatService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor, ChatHub ChatHub)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;
            this.ChatHub = ChatHub;


        }

    
        public async Task<ApiResponse<List<ConversationDto>>> Get_UserConversations()
        {
            ApiResponse<List<ConversationDto>> result = new ApiResponse<List<ConversationDto>>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    Expression<Func<Conversation, bool>> filter = x => !x.IsDeleted
                    && x.UsersConversations.Any(a => a.ApplicationUserID == userID);
 
                    var ConversationsResult =( await unitOfWork.ConversationManager.GetAsync(filter, includeProperties: "Messages")).ToList();
                    if (ConversationsResult.Count != 0)
                    {
                        result.Succeeded = true;
                        result.Data = mapper.Map<List<ConversationDto>>(ConversationsResult);
                        return result;
                    }
                    result.Errors.Add("Failed to find Conversations!");
                    result.Succeeded = false;
                    return result;
                }
                else
                {
                    result.Errors.Add("Failed to find Conversations!");
                    result.Succeeded = false;
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

        public async Task<ApiResponse<List<MessageDto>>> Get_ConversationMessages(long ConversationID)
        {
            ApiResponse<List<MessageDto>> result = new ApiResponse<List<MessageDto>>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    Expression<Func<Conversation, bool>> filter = x => !x.IsDeleted
                    && x.ID==ConversationID;

                    var ConversationsResult = (await unitOfWork.ConversationManager.GetAsync(filter, includeProperties: "Messages")).ToList();

                    result.Succeeded = true;
                    result.Data = mapper.Map<List<MessageDto>>(ConversationsResult.Take(50));
                    return result;

                }
                else
                {
                    result.Errors.Add("Failed to find Messages!");
                    result.Succeeded = false;
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

        public async Task<ApiResponse<List<UsersConversationsDto>>> Get_UsersConversation(long ConversationID)
        {
            ApiResponse<List<UsersConversationsDto>> result = new ApiResponse<List<UsersConversationsDto>>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    Expression<Func<UsersConversations, bool>> filter = x => !x.IsDeleted
                    && x.ConversationID == ConversationID;

                    var ConversationsResult = (await unitOfWork.UsersConversationsManager.GetAsync(filter)).ToList();

                    result.Succeeded = true;
                    result.Data = mapper.Map<List<UsersConversationsDto>>(ConversationsResult);
                    return result;

                }
                else
                {
                    result.Errors.Add("Failed to find users!");
                    result.Succeeded = false;
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }
        public async Task<ApiResponse<bool>> Add_Message(AddMsg msg)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    Message MessageToCreate = mapper.Map<Message>(msg);
                    MessageToCreate.SenderID = userID;
                    MessageToCreate.CreatedAt = DateTime.Now;

                    var createLevelResult = await unitOfWork.MessageManager.CreateAsync(MessageToCreate);
                    var SaveResult = await unitOfWork.SaveChangesAsync();

                    if (SaveResult)
                    {
                        result.Data = true;
                        result.Succeeded = true;
                        return result;
                    }
                    else
                    {
                        result.Errors.Add("Failed to create Interest");
                        result.Succeeded = false;
                        return result;
                    }
                }
                else
                {
                    result.Errors.Add("Failed to create message");
                    result.Succeeded = false;
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

        public async Task<ApiResponse<bool>> Create_Conversation(ConversationToCreate msg)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    Conversation ConversationToCreate = mapper.Map<Conversation>(msg);
                    ConversationToCreate.CreatedAt = DateTime.Now;

                    var createLevelResult = await unitOfWork.ConversationManager.CreateAsync(ConversationToCreate);
                    var SaveResult = await unitOfWork.SaveChangesAsync();


                    if (SaveResult)
                    {
                        UsersConversations UsersConversationToCreate = new UsersConversations()
                        {
                            ApplicationUserID = msg.ReceiverID,
                            ConversationID = ConversationToCreate.ID
                        };

                        var UsersConversationResult = await unitOfWork.UsersConversationsManager.CreateAsync(UsersConversationToCreate);
                        var SaveUsersConversationResult = await unitOfWork.SaveChangesAsync();

                        AddMsg msgTocreate = new AddMsg()
                        {
                            Content = msg.Content,
                            ConversationID = createLevelResult.ID
                        };

                        return await Add_Message(msgTocreate);
                    }
                    else
                    {
                        result.Errors.Add("Failed to create conversation");
                        result.Succeeded = false;
                        return result;
                    }
                }
                else
                {
                    result.Errors.Add("Failed to create conversation");
                    result.Succeeded = false;
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

        public async Task<ApiResponse<bool>> Delete_Conversation(long ConversationID)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var ConversationResult = await unitOfWork.ConversationManager.GetByIdAsync(ConversationID);

                if (ConversationResult != null)
                {
                    ConversationResult.IsDeleted = true;
                    var UpdateResult = await unitOfWork.ConversationManager.UpdateAsync(ConversationResult);
                    var SaveResult = await unitOfWork.SaveChangesAsync();

                    if (SaveResult)
                    {
                        result.Succeeded = true;
                        return result;
                    }

                }

                result.Errors.Add("Failed to delete Conversation!");
                result.Succeeded = false;
                return result;

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

        public async Task<ApiResponse<bool>> Delete_Message(long MsgID)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var ConversationResult = await unitOfWork.MessageManager.GetByIdAsync(MsgID);

                if (ConversationResult != null)
                {
                    ConversationResult.IsDeleted = true;
                    var UpdateResult = await unitOfWork.MessageManager.UpdateAsync(ConversationResult);
                    var SaveResult = await unitOfWork.SaveChangesAsync();

                    if (SaveResult)
                    {
                        result.Succeeded = true;
                        return result;
                    }

                }

                result.Errors.Add("Failed to delete Msg!");
                result.Succeeded = false;
                return result;

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

    }

}


