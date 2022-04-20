
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using Stack.DTOs;
using Stack.DTOs.Enums;
using Stack.DTOs.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Stack.Repository.Common;
using System.IdentityModel.Tokens.Jwt;
using Stack.DTOs.Requests.Modules.Auth;
using Stack.Entities.Models.Modules.Auth;
using Stack.DTOs.Models.Modules.Auth;
using Stack.DTOs.Requests.Modules.Pool;
using Stack.Entities.Models.Modules.CustomerStage;
using Stack.DTOs.Models.Modules.Pool;
using Stack.Entities.Enums.Modules.Pool;
using Microsoft.AspNetCore.SignalR;
using Stack.API.Hubs;
using Stack.Entities.Enums.Modules.CustomerStage;
using Hangfire;

namespace Stack.ServiceLayer.Modules.pool
{
    public class PoolService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        protected IHubContext<RecordLockHub> _recordLockContext;
        private static readonly HttpClient client = new HttpClient();

        public PoolService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor, IHubContext<RecordLockHub> recordLockContext)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;
            this._recordLockContext = recordLockContext;

        }


        //Create pool with default configuration
        public async Task<ApiResponse<bool>> CreatePool(PoolCreationModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (userID != null)
                {
                    //Verify name duplication
                    var duplicateNameQuery = await unitOfWork.PoolManager.GetAsync(t => t.NameEN == model.NameEN || t.NameAR == model.NameAR);
                    var duplicateName = duplicateNameQuery.FirstOrDefault();

                    if (duplicateName == null)
                    {
                        Pool creationModel = new Pool
                        {
                            NameEN = model.NameEN,
                            NameAR = model.NameAR,
                            DescriptionEN = model.DescriptionEN,
                            DescriptionAR = model.DescriptionAR,
                            ConfigurationType = (int)PoolConfigurationTypes.Default
                        };

                        var creationResult = await unitOfWork.PoolManager.CreateAsync(creationModel);
                        if (creationResult != null)
                        {
                            await unitOfWork.SaveChangesAsync();

                            //Assign pool creator as admin
                            Pool_User poolAdmin = new Pool_User
                            {
                                PoolID = creationResult.ID,
                                UserID = userID,
                                IsAdmin = true
                            };

                            var adminAssignmentResult = await unitOfWork.PoolUserManager.CreateAsync(poolAdmin);
                            if (adminAssignmentResult != null)
                            {
                                await unitOfWork.SaveChangesAsync();

                                result.Succeeded = true;
                                result.Data = true;
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("Unable to create pool");
                                result.Errors.Add("غير قادر على إنشاء قوائم");
                                return result;
                            }
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Unable to create pool");
                            result.Errors.Add("غير قادر على إنشاء قوائم");
                            return result;
                        }
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("A Pool with the same name already exists");
                        result.Errors.Add("يوجد بالفعل قائمة يحمل نفس الاسم");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Not authorized");
                    result.Errors.Add("غير مصرح");
                    result.ErrorCode = ErrorCode.A500;
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

        //Set Pool Configuration: 'Capacity'/'Auto-Assignment'/ 'Auto-Assignment W/Capacity'
        public async Task<ApiResponse<bool>> SetPoolConfiguration(PoolConfigurationModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (userID != null)
                {
                    //Verify admin priviliges
                    var adminVerificationQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.UserID == userID && t.PoolID == model.PoolID && t.IsAdmin == true, includeProperties: "Pool");
                    var adminVerification = adminVerificationQuery.FirstOrDefault();

                    if (adminVerification != null)
                    {
                        Pool pool = adminVerification.Pool;


                        //Capacity Configuration Type
                        if (model.ConfigurationType == (int)PoolConfigurationTypes.Capacity)
                        {
                            pool.ConfigurationType = (int)PoolConfigurationTypes.Capacity;
                            pool.Capacity = model.Capacity.Value;

                            var updateRes = await unitOfWork.PoolManager.UpdateAsync(pool);
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
                                result.Errors.Add("Error updating configuration");
                                result.Errors.Add("خطأ في تحديث الإعداد");
                                return result;
                            }
                        }
                        //Auto Assignment Configuration Type

                        else if (model.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignment)
                        {
                            pool.ConfigurationType = (int)PoolConfigurationTypes.AutoAssignment;

                            var updateRes = await unitOfWork.PoolManager.UpdateAsync(pool);
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
                                result.Errors.Add("Error updating configuration");
                                result.Errors.Add("خطأ في تحديث الإعداد");
                                return result;
                            }
                        }
                        //Auto Assignment W/ Capacity Configuration Type

                        else if (model.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignmentCapacity)
                        {
                            pool.ConfigurationType = (int)PoolConfigurationTypes.AutoAssignmentCapacity;
                            pool.Capacity = model.Capacity.Value;

                            var updateRes = await unitOfWork.PoolManager.UpdateAsync(pool);
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
                                result.Errors.Add("Error updating configuration");
                                result.Errors.Add("خطأ في تحديث الإعداد");
                                return result;
                            }
                        }
                        else if (model.ConfigurationType == (int)PoolConfigurationTypes.Default)
                        {
                            if (pool.Capacity.HasValue)
                            {
                                pool.Capacity = null;
                            }

                            pool.ConfigurationType = (int)PoolConfigurationTypes.Default;

                            var updateRes = await unitOfWork.PoolManager.UpdateAsync(pool);
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
                                result.Errors.Add("Error updating configuration");
                                result.Errors.Add("خطأ في تحديث الإعداد");
                                return result;
                            }
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Invalid Configuration Type");
                            result.Errors.Add("نوع الإعداد غير صالح");
                            result.ErrorCode = ErrorCode.A500;
                            return result;
                        }
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Not authorized");
                        result.Errors.Add("غير مصرح");
                        result.ErrorCode = ErrorCode.A500;
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Not authorized");
                    result.Errors.Add("غير مصرح");
                    result.ErrorCode = ErrorCode.A500;
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

        //Pool Assignment
        public async Task<ApiResponse<bool>> AssignUsersToPool(PoolAssignmentModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (userID != null)
                {
                    //Verify admin priviliges
                    var adminVerificationQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.UserID == userID && t.PoolID == model.PoolID && t.IsAdmin == true, includeProperties: "Pool");
                    var adminVerification = adminVerificationQuery.FirstOrDefault();

                    if (adminVerification != null)
                    {
                        Pool pool = adminVerification.Pool;

                        for (int i = 0; i < model.UserIDs.Count; i++)
                        {
                            var currentUserID = model.UserIDs[i];

                            Pool_User pool_Users = new Pool_User
                            {
                                PoolID = model.PoolID,
                                UserID = currentUserID,
                            };

                            //Set user capacity for capacity related pool configuration types
                            if (pool.ConfigurationType == (int)PoolConfigurationTypes.Capacity || pool.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignmentCapacity)
                            {
                                pool_Users.Capacity = pool.Capacity;
                            }

                            var assignmentRes = await unitOfWork.PoolUserManager.CreateAsync(pool_Users);
                            if (assignmentRes == null)
                            {
                                result.Errors.Add("Error assigning user");
                            }
                        }

                        await unitOfWork.SaveChangesAsync();

                        if (result.Errors.Count == 0)
                        {
                            await unitOfWork.SaveChangesAsync();
                            result.Succeeded = true;
                            result.Data = true;
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add(result.Errors.Count + " Users were not assigned");
                            result.Errors.Add(result.Errors.Count + " Users were not assigned");
                            return result;
                        }
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Not authorized");
                        result.Errors.Add("غير مصرح");
                        result.ErrorCode = ErrorCode.A500;
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Not authorized");
                    result.Errors.Add("غير مصرح");
                    result.ErrorCode = ErrorCode.A500;
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



        //Get
        public async Task<ApiResponse<List<PoolAssignedUsersModel>>> GetPoolAssignedUsers(long poolID)
        {
            ApiResponse<List<PoolAssignedUsersModel>> result = new ApiResponse<List<PoolAssignedUsersModel>>();
            try
            {
                var poolUsersQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == poolID, includeProperties: "User");
                var poolUsers = poolUsersQuery.ToList();

                if (poolUsers != null && poolUsers.Count > 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<PoolAssignedUsersModel>>(poolUsers);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No users found");
                    result.Errors.Add("لم يتم العثور على مستخدمين");
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

        //Sidebar view
        //Includes count of available contacts/leads/prospects/opportunities/donedeals within user assignesd pools
        public async Task<ApiResponse<List<PoolSidebarViewModel>>> GetUserAssignedPools()
        {
            ApiResponse<List<PoolSidebarViewModel>> result = new ApiResponse<List<PoolSidebarViewModel>>();
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (userID != null)
                {
                    List<PoolSidebarViewModel> poolSidebarViewModel = new List<PoolSidebarViewModel>();

                    var userPools = await unitOfWork.PoolUserManager.GetUserPools(userID);

                    if (userPools != null && userPools.Count > 0)
                    {
                        poolSidebarViewModel.AddRange(userPools);
                    }


                    if (poolSidebarViewModel.Count > 0)
                    {
                        result.Succeeded = true;
                        result.Data = poolSidebarViewModel;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("No pools found");
                        result.Errors.Add("لا يوجد قوائم");
                        result.ErrorType = ErrorType.NotFound;
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Not authorized");
                    result.Errors.Add("غير مصرح");
                    result.ErrorCode = ErrorCode.A500;
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

        //Get Assigned Pool Stages count (Sidebar view)
        public async Task<ApiResponse<AssignedPoolSidebarViewModel>> GetUserAssignedPoolStages()
        {
            ApiResponse<AssignedPoolSidebarViewModel> result = new ApiResponse<AssignedPoolSidebarViewModel>();
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (userID != null)
                {
                    List<PoolSidebarViewModel> poolSidebarViewModel = new List<PoolSidebarViewModel>();

                    var assignedPool = await unitOfWork.PoolManager.GetAssignedPoolStages(userID);

                    if (assignedPool != null)
                    {
                        result.Succeeded = true;
                        result.Data = assignedPool;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("An error occured while retreiving assigned pool data");
                        result.Errors.Add("حدث خطأ أثناء استرداد بيانات التجمع المعينة");
                        result.ErrorType = ErrorType.NotFound;
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Not authorized");
                    result.Errors.Add("غير مصرح");
                    result.ErrorCode = ErrorCode.A500;
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

        //Get main pool details
        public async Task<ApiResponse<PoolSidebarViewModel>> GetPoolDetails(long poolID)
        {
            ApiResponse<PoolSidebarViewModel> result = new ApiResponse<PoolSidebarViewModel>();
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (userID != null)
                {
                    var userPools = await unitOfWork.PoolManager.GetPoolDetails(poolID);

                    if (userPools != null)
                    {
                        result.Succeeded = true;
                        result.Data = userPools;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("No pools found");
                        result.Errors.Add("لا يوجد قوائم");
                        result.ErrorType = ErrorType.NotFound;
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Not authorized");
                    result.Errors.Add("غير مصرح");
                    result.ErrorCode = ErrorCode.A500;
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

        //Pool Contacts
        public async Task<ApiResponse<List<ContactListViewModel>>> GetPoolContacts(long poolID)
        {
            ApiResponse<List<ContactListViewModel>> result = new ApiResponse<List<ContactListViewModel>>();
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (userID != null)
                {
                    //Verify user pool permissions
                    var userPoolQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == poolID && t.UserID == userID, includeProperties: "Pool");
                    var userPool = userPoolQuery.FirstOrDefault();

                    if (userPool != null)
                    {
                        var poolContacts = await unitOfWork.PoolUserManager.GetPoolContacts(poolID, userID);

                        if (poolContacts != null)
                        {
                            result.Succeeded = true;
                            result.Data = poolContacts;
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Data = null;
                            result.Errors.Add("No contacts found");
                            result.Errors.Add("No contacts found");
                            return result;
                        }
                    }
                    else
                    {

                        result.Succeeded = false;
                        result.Errors.Add("Unauthorized");
                        result.Errors.Add("غير مصرح");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Unauthorized");
                    result.Errors.Add("غير مصرح");
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


        //Verify record before viewing for capacity related pool configuration / authorization
        //Verify locked records for assigned users
        public async Task<ApiResponse<bool>> ViewRecord_VerifyUser(VerifyRecordModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (userID != null)
                {
                    var poolQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == model.PoolID && t.UserID == userID, includeProperties: "Pool");
                    var poolUser = poolQuery.FirstOrDefault();

                    if (poolUser != null)
                    {
                        var pool = poolUser.Pool;

                        if (pool.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignmentCapacity
                            || pool.ConfigurationType == (int)PoolConfigurationTypes.Capacity)
                        {
                            //Contacts
                            //Verify user capacity
                            var assignedPoolContactsQuery = await unitOfWork.ContactManager.GetAsync(t => t.PoolID == pool.ID && t.AssignedUserID == userID
                                               && t.IsFinalized == false);
                            var assignedPoolContactsCount = assignedPoolContactsQuery.Count();

                            if (assignedPoolContactsCount < poolUser.Capacity.Value)
                            {
                                result.Succeeded = true;
                                result.Data = true;
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("Space Capacity limit reached");
                                result.Errors.Add("تم الوصول إلى حد السعة");
                                result.ErrorType = ErrorType.CapacityReached;
                                return result;
                            }
                        }
                        else
                        {
                            //Contact
                            //Verify Lock
                            var recordQuery = await unitOfWork.ContactManager.GetAsync(t => t.ID == model.RecordID);
                            var record = recordQuery.FirstOrDefault();
                            if (record != null)
                            {
                                //Verify locked record's assignee
                                if (record.IsLocked == true)
                                {
                                    var connectionQuery = await unitOfWork.PoolConnectionIDsManager.GetAsync(t => t.UserID == userID && t.PoolID == pool.ID && t.RecordID == record.ID);
                                    var connection = connectionQuery.FirstOrDefault();

                                    if (connection != null)
                                    {
                                        result.Succeeded = true;
                                        result.Data = true;
                                        return result;
                                    }
                                    else
                                    {
                                        result.Succeeded = false;
                                        result.Data = false;
                                        result.Errors.Add("Connection Record not found");
                                        result.Errors.Add("Connection Record not found");
                                        return result;
                                    }
                                }
                                else
                                {
                                    result.Succeeded = true;
                                    result.Data = true;
                                    return result;
                                }
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("Record not found");
                                result.Errors.Add("Record not found");
                                return result;
                            }

                        }
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("User not enrolled or Space does not exist");
                        result.Errors.Add("غير مصرح");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Unauthorized");
                    result.Errors.Add("غير مصرح");
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


        //Update user's pool connection with current active pool
        public async Task<ApiResponse<bool>> LogUsersActivePool(long poolID)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (userID != null)
                {
                    var poolQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == poolID && t.UserID == userID);
                    var poolUser = poolQuery.FirstOrDefault();

                    if (poolUser != null)
                    {
                        RecordLockHub recordLockHub = new RecordLockHub(unitOfWork, mapper, _recordLockContext);

                        //Update current user's connection

                        var connectionLogResult = await recordLockHub.LogUsersCurrentPool(userID, poolID);
                        return connectionLogResult;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("User not enrolled or Space does not exist");
                        result.Errors.Add("غير مصرح");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Unauthorized");
                    result.Errors.Add("غير مصرح");
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

        //Lock unassigned pool record for a specific user .. for a configured duration
        public async Task<ApiResponse<bool>> LockRecord(LockRecordModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (userID != null)
                {
                    bool recordLockedSuccessfully = false;
                    //Identify and lock current record via type
                    //Contact
                    if (model.CustomerStage == (int)CustomerStageIndicator.Contact)
                    {
                        var recordQuery = await unitOfWork.ContactManager.GetAsync(t => t.ID == model.RecordID);
                        var record = recordQuery.FirstOrDefault();

                        record.IsLocked = true;

                        var updateRes = await unitOfWork.ContactManager.UpdateAsync(record);
                        if (updateRes)
                        {
                            //Schedule record unlock - TODO
                            //Get system configuration's lock duration
                            var systemConfigQuery = await unitOfWork.SystemConfigurationManager.GetAsync();
                            var systemConfig = systemConfigQuery.FirstOrDefault();
                            if (systemConfig != null && systemConfig.LockDuration > 0)
                            {
                                //Get current time and add lock duration
                                var currentTime = await HelperFunctions.GetEgyptsCurrentLocalTime();
                                var extendedTime = currentTime.AddMinutes(systemConfig.LockDuration);

                                var scheduledTimespan = extendedTime - currentTime;

                                //Schedule force unlock
                                if (scheduledTimespan.TotalMinutes > 0)
                                {
                                    var jobID = BackgroundJob.Schedule(() => UnlockRecord(model), scheduledTimespan);

                                    //Update contact with current schedule ID
                                    record.ForceUnlock_JobID = jobID;

                                    var recordScheduleUpdateResult = await unitOfWork.ContactManager.UpdateAsync(record);
                                    if (recordScheduleUpdateResult)
                                    {
                                        await unitOfWork.SaveChangesAsync();
                                        recordLockedSuccessfully = true;
                                    }
                                    else
                                    {
                                        result.Succeeded = false;
                                        result.Errors.Add("Error locking record");
                                        result.Errors.Add("Error locking record");
                                        return result;
                                    }
                                }
                            }
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Error locking record");
                            result.Errors.Add("Error locking record");
                            return result;
                        }
                    }
                    else if (model.CustomerStage == (int)CustomerStageIndicator.Lead)
                    {
                        throw new NotImplementedException();

                    }
                    else
                    {
                        throw new NotImplementedException();
                    }

                    if (recordLockedSuccessfully)
                    {
                        //Update lock connection record
                        var connectionQuery = await unitOfWork.PoolConnectionIDsManager.GetAsync(t => t.UserID == userID && t.PoolID == model.PoolID);
                        var connection = connectionQuery.FirstOrDefault();

                        if (connection != null)
                        {
                            connection.RecordID = model.RecordID;
                            connection.RecordType = model.CustomerStage;

                            var connectionUpdateRes = await unitOfWork.PoolConnectionIDsManager.UpdateAsync(connection);
                            if (connectionUpdateRes)
                            {
                                await unitOfWork.SaveChangesAsync();
                                //Emit lock update response
                                RecordLockHub recordLockHub = new RecordLockHub(unitOfWork, mapper, _recordLockContext);

                                var lockResult = await recordLockHub.UpdatePool(model.PoolID);

                                return lockResult;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("Error updating connection status");
                                return result;
                            }

                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Error updating connection status");
                            return result;
                        }

                    }
                    else
                    {
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Unauthorized");
                    result.Errors.Add("غير مصرح");
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

        //Unlock record on record leave or lock duration end
        public async Task<ApiResponse<bool>> UnlockRecord(LockRecordModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {


                //Update lock connection record
                var connectionQuery = await unitOfWork.PoolConnectionIDsManager.GetAsync(t => t.PoolID == model.PoolID && t.RecordID == model.RecordID);
                var connection = connectionQuery.FirstOrDefault();

                if (connection != null)
                {
                    //Remove connection ID
                    connection.RecordID = 0;
                    connection.RecordType = 0;
                    var connectionUpdateRes = await unitOfWork.PoolConnectionIDsManager.UpdateAsync(connection);
                    if (connectionUpdateRes)
                    {

                        if (model.CustomerStage == (int)CustomerStageIndicator.Contact)
                        {
                            var recordQuery = await unitOfWork.ContactManager.GetAsync(t => t.ID == model.RecordID);
                            var record = recordQuery.FirstOrDefault();

                            record.IsLocked = false;
                            record.ForceUnlock_JobID = null;

                            var updateRes = await unitOfWork.ContactManager.UpdateAsync(record);
                            if (updateRes)
                            {
                                await unitOfWork.SaveChangesAsync();
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("Error locking record");
                                result.Errors.Add("Error locking record");
                                return result;
                            }
                        }
                        else if (model.CustomerStage == (int)CustomerStageIndicator.Lead)
                        {
                            throw new NotImplementedException();

                        }
                        else
                        {
                            throw new NotImplementedException();
                        }

                        //Emit lock update response
                        RecordLockHub recordLockHub = new RecordLockHub(unitOfWork, mapper, _recordLockContext);

                        var lockResult = await recordLockHub.UpdatePool(model.PoolID);

                        return lockResult;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Error updating connection status");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Error updating connection status");
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
    }

}


