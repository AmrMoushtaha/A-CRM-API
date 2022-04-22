using AutoMapper;
using Hangfire;
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
            PoolConnectionID conId = new PoolConnectionID
            {
                ID = Context.ConnectionId,
                UserID = user.Id
            };
            await unitOfWork.PoolConnectionIDsManager.CreateAsync(conId);
            await unitOfWork.SaveChangesAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            var username = Context.User.Identity.Name;
            var user = await unitOfWork.UserManager.FindByNameAsync(username);
            if (user != null)
            {
                var conIdQuery = await unitOfWork.PoolConnectionIDsManager.GetAsync(t => t.UserID == user.Id); //User Connection exists
                var connection = conIdQuery.FirstOrDefault();
                if (connection != null) //Unlock record destroy active connection
                {
                    if (connection.RecordID != 0)
                    {
                        if (connection.RecordType == 0) //Contact
                        {
                            var recordQ = await unitOfWork.ContactManager.GetAsync(t => t.ID == connection.RecordID);
                            var record = recordQ.FirstOrDefault();
                            if (record != null)
                            {
                                //Remove scheduled job
                                var jobDeletionRes = BackgroundJob.Delete(record.ForceUnlock_JobID);

                                record.IsLocked = false;
                                record.ForceUnlock_JobID = null;
                                var updateResult = await unitOfWork.ContactManager.UpdateAsync(record);

                            }
                        }
                        else if (connection.RecordType == 1)//Lead
                        {
                            throw new NotImplementedException();
                        }



                        //Update current pool for users
                        await UpdatePool(connection.PoolID);
                    }
                    //Destroy connection
                    await unitOfWork.PoolConnectionIDsManager.RemoveAsync(connection);
                    await unitOfWork.SaveChangesAsync();
                }

            }
        }

        //Send pool update request for pool connected users
        public async Task<ApiResponse<bool>> UpdatePool(long poolID, long? recordID = null, int? customerStage = null)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var connectedPoolUsersQuery = await unitOfWork.PoolConnectionIDsManager.GetAsync(a => a.PoolID == poolID);
                var connectedPoolUsers = connectedPoolUsersQuery.ToList();
                if (connectedPoolUsers != null && connectedPoolUsers.Count > 0)
                {
                    var List = connectedPoolUsers.Select(c => c.ID).ToList();
                    await _context.Clients.Clients(List).SendAsync("updatePool", new
                    {
                        RecordID = recordID,
                        PoolID = poolID,
                        CustomerStage = customerStage
                    });

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

                var connectedPoolUserQuery = await unitOfWork.PoolConnectionIDsManager.GetAsync(a => a.UserID == userID);
                var connectedPoolUser = connectedPoolUserQuery.FirstOrDefault();
                if (connectedPoolUser != null)
                {
                    //Update current connection with Pool ID
                    connectedPoolUser.PoolID = poolID;

                    var updateRes = await unitOfWork.PoolConnectionIDsManager.UpdateAsync(connectedPoolUser);

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
