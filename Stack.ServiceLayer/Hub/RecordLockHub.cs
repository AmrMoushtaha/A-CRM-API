using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Stack.Core;
using Stack.DTOs;
using Stack.DTOs.Models;
using Stack.DTOs.Requests;
using Stack.Entities.Models;
using Stack.Entities.Models.Modules.Common;
using Stack.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stack.API.Hubs
{
    [Authorize]
    public class RecordLockHub : Hub
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;
        protected IHubContext<RecordLockHub> _context;

        public RecordLockHub(UnitOfWork unitOfWork, IMapper mapper, IHubContext<RecordLockHub> context)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this._context = context;
        }

        public async override Task OnConnectedAsync()
        {
            var username = Context.User.Identity.Name;
            var user = await unitOfWork.UserManager.FindByNameAsync(username);
            ConnectionID conId = new ConnectionID
            {
                ID = Context.ConnectionId,
                UserID = user.Id
            };
            await unitOfWork.ConnectionIDsManager.CreateAsync(conId);
            await unitOfWork.SaveChangesAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            var conId = await unitOfWork.ConnectionIDsManager.GetByIdAsync(Context.ConnectionId);
            await unitOfWork.ConnectionIDsManager.RemoveAsync(conId);
            await unitOfWork.SaveChangesAsync();
        }

        //Send pool update request for pool connected users
        public async Task<ApiResponse<bool>> UpdatePool(long poolID)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var connectedPoolUsersQuery = await unitOfWork.ConnectionIDsManager.GetAsync(a => a.PoolID == poolID);
                var connectedPoolUsers = connectedPoolUsersQuery.ToList();
                if (connectedPoolUsers != null && connectedPoolUsers.Count > 0)
                {
                    var List = connectedPoolUsers.Select(c => c.ID).ToList();
                    await _context.Clients.Clients(List).SendAsync("updatePool");

                    result.Succeeded = true;
                    result.Data = true;
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No connected users");
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }

        }

        //Log current active pool for user
        public async Task<ApiResponse<bool>> LogUsersCurrentPool(string userID, long poolID)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var connectedPoolUserQuery = await unitOfWork.ConnectionIDsManager.GetAsync(a => a.UserID == userID);
                var connectedPoolUser = connectedPoolUserQuery.FirstOrDefault();
                if (connectedPoolUser != null)
                {
                    //Update current connection with Pool ID
                    connectedPoolUser.PoolID = poolID;

                    var updateRes = await unitOfWork.ConnectionIDsManager.UpdateAsync(connectedPoolUser);

                    if (updateRes)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Unable to log current pool for user's connection");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("User disconnected");
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }

        }

    }
}
