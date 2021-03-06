
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
using Stack.DTOs.Requests.CustomerStage;

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

        #region Creation
        //Create pool with default configuration
        public async Task<ApiResponse<bool>> CreatePool(PoolCreationModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                //var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

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
                            ConfigurationType = model.ConfigurationType
                        };

                        if (creationModel.ConfigurationType == (int)PoolConfigurationTypes.Capacity ||
                            (creationModel.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignmentCapacity))
                        {
                            creationModel.Capacity = model.Capacity;
                        }

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

                            if (creationModel.ConfigurationType == (int)PoolConfigurationTypes.Capacity ||
                                (creationModel.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignmentCapacity))
                            {
                                poolAdmin.Capacity = model.Capacity;
                            }


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
        #endregion

        #region Configuration
        public async Task<ApiResponse<PoolConfigurationModel>> GetPoolConfiguration(long poolID)
        {
            ApiResponse<PoolConfigurationModel> result = new ApiResponse<PoolConfigurationModel>();
            try
            {
                var poolQuery = await unitOfWork.PoolManager.GetAsync(t => t.ID == poolID);
                var pool = poolQuery.FirstOrDefault();

                if (pool != null)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<PoolConfigurationModel>(pool);
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

        public async Task<ApiResponse<bool>> SetPoolConfiguration(PoolConfigurationModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

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

        public async Task<ApiResponse<bool>> UpdatePool(UpdatePoolModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var poolQuery = await unitOfWork.PoolManager.GetAsync(t => t.ID == model.PoolID);
                var pool = poolQuery.FirstOrDefault();

                if (pool != null)
                {
                    pool.NameEN = model.NameEN;
                    pool.NameAR = model.NameAR;
                    pool.DescriptionAR = model.DescriptionAR;
                    pool.DescriptionEN = model.DescriptionEN;
                    int poolConfigurationType = pool.ConfigurationType;
                    pool.ConfigurationType = model.ConfigurationType;
                    pool.Capacity = model.Capacity;

                    var updateRes = await unitOfWork.PoolManager.UpdateAsync(pool);

                    if (updateRes)
                    {
                        //Update users capacity
                        if (poolConfigurationType == (int)PoolConfigurationTypes.AutoAssignment || poolConfigurationType == (int)PoolConfigurationTypes.Default
                            && (model.ConfigurationType == (int)PoolConfigurationTypes.Capacity || model.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignmentCapacity))
                        {
                            var poolUsersQ = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == pool.ID);
                            var poolUsers = poolUsersQ.ToList();
                            if (poolUsers != null && poolUsers.Count > 0)
                            {
                                for (int i = 0; i < poolUsers.Count; i++)
                                {
                                    var poolUser = poolUsers[i];
                                    poolUser.Capacity = model.Capacity;

                                    var updateCapacityRes = await unitOfWork.PoolUserManager.UpdateAsync(poolUser);
                                }
                            }

                        }

                        await unitOfWork.SaveChangesAsync();

                        result.Succeeded = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Error updating pool");
                        result.Errors.Add("Error updating pool");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("pool not found");
                    result.Errors.Add("pool not found");
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

        public async Task<ApiResponse<bool>> UpdateUsersCapacity(UpatePoolUsersCapacityModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    //Verify user pool permissions
                    var poolUsersQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == model.PoolID, includeProperties: "Pool");
                    var poolUsers = poolUsersQuery.ToList();

                    if (poolUsers != null)
                    {
                        for (int i = 0; i < model.Users.Count; i++)
                        {
                            var updatedUser = model.Users[i];
                            var user = poolUsers.Where(t => t.UserID == updatedUser.UserID).FirstOrDefault();

                            if (user != null)
                            {
                                user.Capacity = updatedUser.Capacity;

                                var updateRes = await unitOfWork.PoolUserManager.UpdateAsync(user);
                            }
                        }

                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {

                        result.Succeeded = false;
                        result.Errors.Add("Error fetching space users");
                        result.Errors.Add("Error fetching space users");
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
        #endregion

        #region Assignment
        public async Task<ApiResponse<bool>> AssignUsersToPool(PoolAssignmentModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

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

        //Suspend Pool Users
        public async Task<ApiResponse<bool>> SuspendPoolUsers(PoolAssignmentModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    //Verify admin priviliges
                    var poolQ = await unitOfWork.PoolManager.GetAsync(t => t.ID == model.PoolID, includeProperties: "Pool_Users");
                    var pool = poolQ.FirstOrDefault();

                    if (pool != null)
                    {

                        for (int i = 0; i < model.UserIDs.Count; i++)
                        {
                            var currentUserID = model.UserIDs[i];
                            var currentUser = pool.Pool_Users.Where(t => t.UserID == currentUserID).FirstOrDefault();

                            currentUser.Status = (int)PoolUserStatuses.Suspended;

                            var removalRes = await unitOfWork.PoolUserManager.UpdateAsync(currentUser);
                            if (!removalRes)
                            {
                                result.Errors.Add("Error suspending user");
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
                            int errorsCount = result.Errors.Count;
                            result.Errors = new List<string>();
                            result.Succeeded = false;
                            result.Errors.Add(errorsCount + " Users were not removed");
                            result.Errors.Add(errorsCount + " Users were not removed");
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
                    result.Errors.Add("Unauthorized");
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

        //UnSuspend Pool Users
        public async Task<ApiResponse<bool>> UnSuspendPoolUsers(PoolAssignmentModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    //Verify admin priviliges
                    var poolQ = await unitOfWork.PoolManager.GetAsync(t => t.ID == model.PoolID, includeProperties: "Pool_Users");
                    var pool = poolQ.FirstOrDefault();

                    if (pool != null)
                    {

                        for (int i = 0; i < model.UserIDs.Count; i++)
                        {
                            var currentUserID = model.UserIDs[i];
                            var currentUser = pool.Pool_Users.Where(t => t.UserID == currentUserID).FirstOrDefault();

                            currentUser.Status = (int)PoolUserStatuses.Active;

                            var removalRes = await unitOfWork.PoolUserManager.UpdateAsync(currentUser);
                            if (!removalRes)
                            {
                                result.Errors.Add("Error suspending user");
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
                            int errorsCount = result.Errors.Count;
                            result.Errors = new List<string>();
                            result.Succeeded = false;
                            result.Errors.Add(errorsCount + " Users were not removed");
                            result.Errors.Add(errorsCount + " Users were not removed");
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
                    result.Errors.Add("Unauthorized");
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


        public async Task<ApiResponse<bool>> GrantPoolAdminPermissions(PoolAssignmentModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    //Verify admin priviliges
                    var poolQ = await unitOfWork.PoolManager.GetAsync(t => t.ID == model.PoolID, includeProperties: "Pool_Users");
                    var pool = poolQ.FirstOrDefault();

                    if (pool != null)
                    {

                        for (int i = 0; i < model.UserIDs.Count; i++)
                        {
                            var currentUserID = model.UserIDs[i];
                            var currentUser = pool.Pool_Users.Where(t => t.UserID == currentUserID).FirstOrDefault();

                            currentUser.IsAdmin = true;

                            var removalRes = await unitOfWork.PoolUserManager.UpdateAsync(currentUser);
                            if (!removalRes)
                            {
                                result.Errors.Add("Error granting admin permissions");
                            }
                        }

                        await unitOfWork.SaveChangesAsync();

                        if (result.Errors.Count == 0)
                        {
                            result.Succeeded = true;
                            result.Data = true;
                            return result;
                        }
                        else
                        {
                            int errorsCount = result.Errors.Count;
                            result.Errors = new List<string>();
                            result.Succeeded = false;
                            result.Errors.Add(errorsCount + " Users were not removed");
                            result.Errors.Add(errorsCount + " Users were not removed");
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
                    result.Errors.Add("Unauthorized");
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


        public async Task<ApiResponse<List<PoolSidebarViewModel>>> GetUserAssignedPools()
        {
            ApiResponse<List<PoolSidebarViewModel>> result = new ApiResponse<List<PoolSidebarViewModel>>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

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

        //Get user assigned pools via user ID
        public async Task<ApiResponse<List<PoolSidebarViewModel>>> GetUserAssignedPoolsByUserID(string userID)
        {
            ApiResponse<List<PoolSidebarViewModel>> result = new ApiResponse<List<PoolSidebarViewModel>>();
            try
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
                    result.Errors.Add("No spaces found");
                    result.Errors.Add("لا يوجد مساحات");
                    result.ErrorType = ErrorType.NotFound;
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

        //Get pool users
        public async Task<ApiResponse<List<PoolAssignedUsersModel>>> GetPoolAssignedUsers(long poolID)
        {
            ApiResponse<List<PoolAssignedUsersModel>> result = new ApiResponse<List<PoolAssignedUsersModel>>();
            try
            {
                var poolQ = await unitOfWork.PoolManager.GetAsync(t => t.ID == poolID, includeProperties: "Pool_Users,Pool_Users.User");
                var pool = poolQ.FirstOrDefault();

                if (pool != null && pool.Pool_Users != null && pool.Pool_Users.Count > 0)
                {
                    List<Pool_User> poolUsers = pool.Pool_Users;

                    if (pool.ConfigurationType == (int)PoolConfigurationTypes.Capacity
                        || pool.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignmentCapacity)
                    {
                        List<PoolAssignedUsersModel> usersList = new List<PoolAssignedUsersModel>();
                        //Iterate pool users and get reserved slots
                        for (int i = 0; i < poolUsers.Count; i++)
                        {
                            var poolUser = poolUsers[i];
                            var poolUserRecordsQ = await unitOfWork.ContactManager.GetAsync(t => t.PoolID == poolID && t.AssignedUserID == poolUser.UserID && t.CapacityCalculated == true);
                            var poolUserRecordsCount = poolUserRecordsQ.Count();

                            PoolAssignedUsersModel user = new PoolAssignedUsersModel();
                            user = mapper.Map<PoolAssignedUsersModel>(poolUser);
                            user.ReservedSlots = poolUserRecordsCount;

                            usersList.Add(user);
                        }

                        result.Succeeded = true;
                        result.Data = usersList;
                        return result;

                    }

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

        public async Task<ApiResponse<List<PoolAssignedUsersModel>>> GetPoolAssignedUsers_ExcludeAdmins(long poolID)
        {
            ApiResponse<List<PoolAssignedUsersModel>> result = new ApiResponse<List<PoolAssignedUsersModel>>();
            try
            {
                var poolUsersQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == poolID && !t.IsAdmin, includeProperties: "User");
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

        //Get pool admins
        public async Task<ApiResponse<List<PoolAssignedUsersModel>>> GetPoolAssignedAdmins(long poolID)
        {
            ApiResponse<List<PoolAssignedUsersModel>> result = new ApiResponse<List<PoolAssignedUsersModel>>();
            try
            {
                var poolUsersQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == poolID && t.IsAdmin == true, includeProperties: "User");
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
        #endregion

        //Pool view - Get main pool details
        public async Task<ApiResponse<PoolSidebarViewModel>> GetPoolDetails(long poolID)
        {
            ApiResponse<PoolSidebarViewModel> result = new ApiResponse<PoolSidebarViewModel>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

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

        public async Task<ApiResponse<List<PoolConfigurationModel>>> GetSystemPools()
        {
            ApiResponse<List<PoolConfigurationModel>> result = new ApiResponse<List<PoolConfigurationModel>>();
            try
            {
                var poolQuery = await unitOfWork.PoolManager.GetAsync(includeProperties: "Pool_Users,Requests");
                var pools = poolQuery.ToList();

                if (pools != null && pools.Count > 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<PoolConfigurationModel>>(pools);
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

        //Get pool specified pool users / all pool users with their capacity
        public async Task<ApiResponse<List<PoolAssignedUserCapacityModel>>> GetPoolAssignedUsersCapacity(GetPoolAssignedUsersCapacityModel model)
        {
            ApiResponse<List<PoolAssignedUserCapacityModel>> result = new ApiResponse<List<PoolAssignedUserCapacityModel>>();
            try
            {
                var poolUsersQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == model.PoolID, includeProperties: "User");
                var poolUsers = poolUsersQuery.ToList();

                if (poolUsers != null && poolUsers.Count > 0)
                {
                    List<PoolAssignedUserCapacityModel> assignedUsers = new List<PoolAssignedUserCapacityModel>();

                    if (model.AssignedUserIDs == null || model.AssignedUserIDs.Count == 0) //Pool users list
                    {
                        for (int i = 0; i < poolUsers.Count; i++)
                        {
                            string userID = poolUsers[i].UserID;

                            var matchedUser = poolUsers.Where(t => t.UserID == userID).FirstOrDefault();

                            if (matchedUser != null)
                            {
                                var occupiedSlotsQuery = await unitOfWork.ContactManager.GetAsync(t => t.PoolID == model.PoolID && t.AssignedUserID == userID
                                && t.IsFinalized == false);
                                var occupiedSlots = occupiedSlotsQuery.Count();
                                assignedUsers.Add(new PoolAssignedUserCapacityModel
                                {
                                    UserID = userID,
                                    Capacity = matchedUser.Capacity.Value,
                                    FullName = matchedUser.User.FirstName + " " + matchedUser.User.LastName,
                                    OccupiedSlots = occupiedSlots
                                });
                            }
                        }
                        result.Succeeded = true;
                        result.Data = assignedUsers;
                        return result;
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Pool not found");
                    result.Errors.Add("Pool not found");
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

        #region Pool Records Listing

        public async Task<ApiResponse<ContactListMenuView>> GetPoolContacts(GetPoolContactsModel model)
        {
            ApiResponse<ContactListMenuView> result = new ApiResponse<ContactListMenuView>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    //Verify user pool permissions
                    var userPoolQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == model.PoolID && t.UserID == userID, includeProperties: "Pool");
                    var userPool = userPoolQuery.FirstOrDefault();

                    if (userPool != null)
                    {
                        var poolContactsMenu = await unitOfWork.PoolUserManager.GetPoolContacts(model.PoolID, userID, model.PageNumber);

                        if (poolContactsMenu != null)
                        {
                            result.Succeeded = true;
                            result.Data = poolContactsMenu;
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

        public async Task<ApiResponse<List<ContactListViewModel>>> GetPoolFreshContacts(long poolID)
        {
            ApiResponse<List<ContactListViewModel>> result = new ApiResponse<List<ContactListViewModel>>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    //Verify user pool permissions
                    var userPoolQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == poolID && t.UserID == userID, includeProperties: "Pool");
                    var userPool = userPoolQuery.FirstOrDefault();

                    if (userPool != null)
                    {
                        var poolContacts = await unitOfWork.PoolUserManager.GetPoolFreshContacts(poolID, userID);

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

        public async Task<ApiResponse<List<ContactListViewModel>>> GetPoolRecords(GetPoolRecordsModel model)
        {
            ApiResponse<List<ContactListViewModel>> result = new ApiResponse<List<ContactListViewModel>>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    //Verify user pool permissions
                    var userPoolQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == model.PoolID && t.UserID == userID, includeProperties: "Pool");
                    var userPool = userPoolQuery.FirstOrDefault();

                    if (userPool != null)
                    {
                        //var poolRecords = await unitOfWork.PoolUserManager.GetPoolRecords(poolID, userID);

                        if (model.RecordType == (int)CustomerStageIndicator.Prospect)
                        {
                            var recordsQ = await unitOfWork.ProspectManager.GetAsync(t => t.Deal.Customer.PoolID == model.PoolID
                            && t.State == (int)CustomerStageState.Unassigned, includeProperties: "Deal,Deal.Customer");
                            var records = recordsQ.ToList();

                            if (records != null && records.Count > 0)
                            {
                                result.Succeeded = true;
                                result.Data = mapper.Map<List<ContactListViewModel>>(records);
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("No records found");
                                result.Errors.Add("No records found");
                                return result;
                            }
                        }
                        else if (model.RecordType == (int)CustomerStageIndicator.Lead)
                        {
                            var recordsQ = await unitOfWork.LeadManager.GetAsync(t => t.Deal.Customer.PoolID == model.PoolID
                            && t.State == (int)CustomerStageState.Unassigned, includeProperties: "Deal,Deal.Customer");
                            var records = recordsQ.ToList();

                            if (records != null && records.Count > 0)
                            {
                                result.Succeeded = true;
                                result.Data = mapper.Map<List<ContactListViewModel>>(records);
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("No records found");
                                result.Errors.Add("No records found");
                                return result;
                            }
                        }
                        else if (model.RecordType == (int)CustomerStageIndicator.Opportunity)
                        {
                            var recordsQ = await unitOfWork.OpportunityManager.GetAsync(t => t.Deal.Customer.PoolID == model.PoolID
                            && t.State == (int)CustomerStageState.Unassigned, includeProperties: "Deal,Deal.Customer");
                            var records = recordsQ.ToList();

                            if (records != null && records.Count > 0)
                            {
                                result.Succeeded = true;
                                result.Data = mapper.Map<List<ContactListViewModel>>(records);
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("No records found");
                                result.Errors.Add("No records found");
                                return result;
                            }
                        }
                        else if (model.RecordType == (int)CustomerStageIndicator.DoneDeal)
                        {
                            var recordsQ = await unitOfWork.DoneDealManager.GetAsync(t => t.Deal.Customer.PoolID == model.PoolID
                            && t.State == (int)CustomerStageState.Unassigned, includeProperties: "Deal,Deal.Customer");
                            var records = recordsQ.ToList();

                            if (records != null && records.Count > 0)
                            {
                                result.Succeeded = true;
                                result.Data = mapper.Map<List<ContactListViewModel>>(records);
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("No records found");
                                result.Errors.Add("No records found");
                                return result;
                            }
                        }
                        else
                        {
                            throw new NotImplementedException();
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

        public async Task<ApiResponse<List<ContactListViewModel>>> GetPoolFreshRecords(GetPoolRecordsModel model)
        {
            ApiResponse<List<ContactListViewModel>> result = new ApiResponse<List<ContactListViewModel>>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    //Verify user pool permissions
                    var userPoolQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == model.PoolID && t.UserID == userID, includeProperties: "Pool");
                    var userPool = userPoolQuery.FirstOrDefault();

                    if (userPool != null)
                    {
                        //var poolRecords = await unitOfWork.PoolUserManager.GetPoolRecords(poolID, userID);

                        if (model.RecordType == (int)CustomerStageIndicator.Prospect)
                        {
                            var recordsQ = await unitOfWork.ProspectManager.GetAsync(t => t.Deal.Customer.PoolID == model.PoolID
                            && t.State == (int)CustomerStageState.Unassigned && t.IsFresh == true, includeProperties: "Deal,Deal.Customer");
                            var records = recordsQ.ToList();

                            if (records != null && records.Count > 0)
                            {
                                result.Succeeded = true;
                                result.Data = mapper.Map<List<ContactListViewModel>>(records);
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("No records found");
                                result.Errors.Add("No records found");
                                return result;
                            }
                        }
                        else if (model.RecordType == (int)CustomerStageIndicator.Lead)
                        {
                            var recordsQ = await unitOfWork.LeadManager.GetAsync(t => t.Deal.Customer.PoolID == model.PoolID
                            && t.State == (int)CustomerStageState.Unassigned && t.IsFresh == true, includeProperties: "Deal,Deal.Customer");
                            var records = recordsQ.ToList();

                            if (records != null && records.Count > 0)
                            {
                                result.Succeeded = true;
                                result.Data = mapper.Map<List<ContactListViewModel>>(records);
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("No records found");
                                result.Errors.Add("No records found");
                                return result;
                            }
                        }
                        else if (model.RecordType == (int)CustomerStageIndicator.Opportunity)
                        {
                            var recordsQ = await unitOfWork.OpportunityManager.GetAsync(t => t.Deal.Customer.PoolID == model.PoolID
                            && t.State == (int)CustomerStageState.Unassigned && t.IsFresh == true, includeProperties: "Deal,Deal.Customer");
                            var records = recordsQ.ToList();

                            if (records != null && records.Count > 0)
                            {
                                result.Succeeded = true;
                                result.Data = mapper.Map<List<ContactListViewModel>>(records);
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("No records found");
                                result.Errors.Add("No records found");
                                return result;
                            }
                        }
                        else
                        {
                            throw new NotImplementedException();
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

        public async Task<ApiResponse<List<ContactListViewModel>>> GetUserAssignedRecords(GetPoolRecordsModel model)
        {
            ApiResponse<List<ContactListViewModel>> result = new ApiResponse<List<ContactListViewModel>>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    //var poolRecords = await unitOfWork.PoolUserManager.GetPoolRecords(poolID, userID);

                    if (model.RecordType == (int)CustomerStageIndicator.Prospect)
                    {
                        var recordsQ = await unitOfWork.ProspectManager.GetAsync(t => t.AssignedUserID == userID &&
                        (t.State == (int)CustomerStageState.Initial), includeProperties: "Deal,Deal.Customer,Deal.Customer.Contact");
                        var records = recordsQ.ToList();

                        if (records != null && records.Count > 0)
                        {
                            result.Succeeded = true;
                            result.Data = mapper.Map<List<ContactListViewModel>>(records);
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("No records found");
                            result.Errors.Add("No records found");
                            return result;
                        }
                    }
                    else if (model.RecordType == (int)CustomerStageIndicator.Lead)
                    {
                        var recordsQ = await unitOfWork.LeadManager.GetAsync(t => t.AssignedUserID == userID &&
                        (t.State == (int)CustomerStageState.Initial), includeProperties: "Deal,Deal.Customer,Deal.Customer.Contact");
                        var records = recordsQ.ToList();

                        if (records != null && records.Count > 0)
                        {
                            result.Succeeded = true;
                            result.Data = mapper.Map<List<ContactListViewModel>>(records);
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("No records found");
                            result.Errors.Add("No records found");
                            return result;
                        }
                    }
                    else if (model.RecordType == (int)CustomerStageIndicator.Opportunity)
                    {
                        var recordsQ = await unitOfWork.OpportunityManager.GetAsync(t => t.AssignedUserID == userID &&
                        (t.State == (int)CustomerStageState.Initial), includeProperties: "Deal,Deal.Customer,Deal.Customer.Contact");
                        var records = recordsQ.ToList();

                        if (records != null && records.Count > 0)
                        {
                            result.Succeeded = true;
                            result.Data = mapper.Map<List<ContactListViewModel>>(records);
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("No records found");
                            result.Errors.Add("No records found");
                            return result;
                        }
                    }
                    else if (model.RecordType == (int)CustomerStageIndicator.DoneDeal)
                    {
                        var recordsQ = await unitOfWork.DoneDealManager.GetAsync(t => t.AssignedUserID == userID &&
                        (t.State == (int)CustomerStageState.Initial), includeProperties: "Deal,Deal.Customer,Deal.Customer.Contact");
                        var records = recordsQ.ToList();

                        if (records != null && records.Count > 0)
                        {
                            result.Succeeded = true;
                            result.Data = mapper.Map<List<ContactListViewModel>>(records);
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("No records found");
                            result.Errors.Add("No records found");
                            return result;
                        }
                    }
                    else
                    {
                        throw new NotImplementedException();
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

        public async Task<ApiResponse<List<ContactListViewModel>>> GetUserAssignedFreshRecords(GetPoolRecordsModel model)
        {
            ApiResponse<List<ContactListViewModel>> result = new ApiResponse<List<ContactListViewModel>>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    //var poolRecords = await unitOfWork.PoolUserManager.GetPoolRecords(poolID, userID);

                    if (model.RecordType == (int)CustomerStageIndicator.Prospect)
                    {
                        var recordsQ = await unitOfWork.ProspectManager.GetAsync(t => t.AssignedUserID == userID &&
                        (t.State == (int)CustomerStageState.Initial) && t.IsFresh == true, includeProperties: "Deal,Deal.Customer,Deal.Customer.Contact");
                        var records = recordsQ.ToList();

                        if (records != null && records.Count > 0)
                        {
                            result.Succeeded = true;
                            result.Data = mapper.Map<List<ContactListViewModel>>(records);
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("No records found");
                            result.Errors.Add("No records found");
                            return result;
                        }
                    }
                    else if (model.RecordType == (int)CustomerStageIndicator.Lead)
                    {
                        var recordsQ = await unitOfWork.LeadManager.GetAsync(t => t.AssignedUserID == userID &&
                        (t.State == (int)CustomerStageState.Initial) && t.IsFresh == true, includeProperties: "Deal,Deal.Customer,Deal.Customer.Contact");
                        var records = recordsQ.ToList();

                        if (records != null && records.Count > 0)
                        {
                            result.Succeeded = true;
                            result.Data = mapper.Map<List<ContactListViewModel>>(records);
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("No records found");
                            result.Errors.Add("No records found");
                            return result;
                        }
                    }
                    else if (model.RecordType == (int)CustomerStageIndicator.Opportunity)
                    {
                        var recordsQ = await unitOfWork.OpportunityManager.GetAsync(t => t.AssignedUserID == userID &&
                        (t.State == (int)CustomerStageState.Initial) && t.IsFresh == true, includeProperties: "Deal,Deal.Customer,Deal.Customer.Contact");
                        var records = recordsQ.ToList();

                        if (records != null && records.Count > 0)
                        {
                            result.Succeeded = true;
                            result.Data = mapper.Map<List<ContactListViewModel>>(records);
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("No records found");
                            result.Errors.Add("No records found");
                            return result;
                        }
                    }
                    else
                    {
                        throw new NotImplementedException();
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

        #endregion

        #region Pool Record Verification & Lock

        //Verify record before viewing for capacity related pool configuration / authorization
        public async Task<ApiResponse<bool>> ViewRecord_VerifyUser(VerifyRecordModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    var poolQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == model.PoolID && t.UserID == userID, includeProperties: "Pool");
                    var poolUser = poolQuery.FirstOrDefault();

                    if (poolUser != null)
                    {
                        //Existing locked record check
                        //Un-lock/schedule existing record if found
                        var currentPoolConnectionQuery = await unitOfWork.PoolConnectionIDsManager.GetAsync(t => t.UserID == userID &&
                        t.PoolID == model.PoolID);
                        var currentPoolConnection = currentPoolConnectionQuery.FirstOrDefault();
                        if (currentPoolConnection != null && currentPoolConnection.RecordID != 0) //Unlock record
                        {
                            if (currentPoolConnection.RecordType == 0) //Contact
                            {
                                var recordQuery = await unitOfWork.ContactManager.GetAsync(t => t.ID == currentPoolConnection.RecordID);
                                var record = recordQuery.FirstOrDefault();
                                if (record != null)
                                {
                                    record.IsLocked = false;
                                    //Remove scheduled unlock
                                    var jobDeletionRes = BackgroundJob.Delete(record.ForceUnlock_JobID);
                                    record.ForceUnlock_JobID = null;
                                    var updateRecordRes = await unitOfWork.ContactManager.UpdateAsync(record);
                                    if (jobDeletionRes && updateRecordRes)
                                    {
                                        await unitOfWork.SaveChangesAsync();
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
                        //End of existing record lock check

                        var pool = poolUser.Pool;

                        if (pool.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignmentCapacity
                            || pool.ConfigurationType == (int)PoolConfigurationTypes.Capacity)
                        {
                            //Contacts
                            //Verify user capacity
                            var assignedPoolContactsQuery = await unitOfWork.ContactManager.GetAsync(t => t.PoolID == pool.ID && t.AssignedUserID == userID
                            && t.CapacityCalculated == true && t.IsFinalized == false);
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

                            if (model.CustomerStage == (int)CustomerStageIndicator.Contact)
                            {
                                //Verify Lock
                                var recordQuery = await unitOfWork.ContactManager.GetAsync(t => t.ID == model.RecordID);
                                var record = recordQuery.FirstOrDefault();
                                if (record != null)
                                {
                                    //Verify locked record's assignee
                                    if (record.IsLocked == true)
                                    {
                                        result.Succeeded = false;
                                        result.Errors.Add("Record is locked");
                                        result.Errors.Add("Record is locked");
                                        return result;

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
                            else if (model.CustomerStage == (int)CustomerStageIndicator.Prospect)
                            {
                                //Verify Lock
                                var recordQuery = await unitOfWork.ProspectManager.GetAsync(t => t.ID == model.RecordID);
                                var record = recordQuery.FirstOrDefault();
                                if (record != null)
                                {
                                    //Verify locked record's assignee
                                    if (record.IsLocked == true)
                                    {
                                        result.Succeeded = false;
                                        result.Errors.Add("Record is locked");
                                        result.Errors.Add("Record is locked");
                                        return result;

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
                            else if (model.CustomerStage == (int)CustomerStageIndicator.Lead)
                            {
                                //Verify Lock
                                var recordQuery = await unitOfWork.LeadManager.GetAsync(t => t.ID == model.RecordID);
                                var record = recordQuery.FirstOrDefault();
                                if (record != null)
                                {
                                    //Verify locked record's assignee
                                    if (record.IsLocked == true)
                                    {
                                        result.Succeeded = false;
                                        result.Errors.Add("Record is locked");
                                        result.Errors.Add("Record is locked");
                                        return result;

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
                            else if (model.CustomerStage == (int)CustomerStageIndicator.Opportunity)
                            {
                                //Verify Lock
                                var recordQuery = await unitOfWork.OpportunityManager.GetAsync(t => t.ID == model.RecordID);
                                var record = recordQuery.FirstOrDefault();
                                if (record != null)
                                {
                                    //Verify locked record's assignee
                                    if (record.IsLocked == true)
                                    {
                                        result.Succeeded = false;
                                        result.Errors.Add("Record is locked");
                                        result.Errors.Add("Record is locked");
                                        return result;

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
                            else if (model.CustomerStage == (int)CustomerStageIndicator.DoneDeal)
                            {
                                result.Succeeded = true;
                                result.Data = true;
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("Invalid Stage");
                                result.Errors.Add("غير مصرح");
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
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

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
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

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
                            //Schedule record unlock
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
                    else if (model.CustomerStage == (int)CustomerStageIndicator.Prospect)
                    {
                        var recordQuery = await unitOfWork.ProspectManager.GetAsync(t => t.ID == model.RecordID);
                        var record = recordQuery.FirstOrDefault();

                        record.IsLocked = true;

                        var updateRes = await unitOfWork.ProspectManager.UpdateAsync(record);
                        if (updateRes)
                        {
                            //Schedule record unlock
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

                                    var recordScheduleUpdateResult = await unitOfWork.ProspectManager.UpdateAsync(record);
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
                        var recordQuery = await unitOfWork.LeadManager.GetAsync(t => t.ID == model.RecordID);
                        var record = recordQuery.FirstOrDefault();

                        record.IsLocked = true;

                        var updateRes = await unitOfWork.LeadManager.UpdateAsync(record);
                        if (updateRes)
                        {
                            //Schedule record unlock
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

                                    var recordScheduleUpdateResult = await unitOfWork.LeadManager.UpdateAsync(record);
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
                    else if (model.CustomerStage == (int)CustomerStageIndicator.Opportunity)
                    {
                        var recordQuery = await unitOfWork.OpportunityManager.GetAsync(t => t.ID == model.RecordID);
                        var record = recordQuery.FirstOrDefault();

                        record.IsLocked = true;

                        var updateRes = await unitOfWork.OpportunityManager.UpdateAsync(record);
                        if (updateRes)
                        {
                            //Schedule record unlock
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

                                    var recordScheduleUpdateResult = await unitOfWork.OpportunityManager.UpdateAsync(record);
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
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Invalid Stage");
                        result.Errors.Add("Invalid Stage");
                        return result;
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
                        else if (model.CustomerStage == (int)CustomerStageIndicator.Prospect)
                        {
                            var recordQuery = await unitOfWork.ProspectManager.GetAsync(t => t.ID == model.RecordID);
                            var record = recordQuery.FirstOrDefault();

                            record.IsLocked = false;
                            record.ForceUnlock_JobID = null;

                            var updateRes = await unitOfWork.ProspectManager.UpdateAsync(record);
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
                            var recordQuery = await unitOfWork.LeadManager.GetAsync(t => t.ID == model.RecordID);
                            var record = recordQuery.FirstOrDefault();

                            record.IsLocked = false;
                            record.ForceUnlock_JobID = null;

                            var updateRes = await unitOfWork.LeadManager.UpdateAsync(record);
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
                        else if (model.CustomerStage == (int)CustomerStageIndicator.Opportunity)
                        {
                            var recordQuery = await unitOfWork.OpportunityManager.GetAsync(t => t.ID == model.RecordID);
                            var record = recordQuery.FirstOrDefault();

                            record.IsLocked = false;
                            record.ForceUnlock_JobID = null;

                            var updateRes = await unitOfWork.OpportunityManager.UpdateAsync(record);
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
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Invalid Stage");
                            result.Errors.Add("Invalid Stage");
                            return result;
                        }

                        //Emit lock update response
                        RecordLockHub recordLockHub = new RecordLockHub(unitOfWork, mapper, _recordLockContext);

                        var lockResult = await recordLockHub.UpdatePool(model.PoolID, model.RecordID, model.CustomerStage);

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

        #endregion

        #region Pool Transfers

        public async Task<ApiResponse<bool>> RequestTransfer(RequestTransferModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var assigneeName = await unitOfWork.UserManager.GetUserById(model.AssigneeID);

                //Transfer contact from current pool
                if (model.PrimaryPhoneNumber != null)
                {
                    //Get record's pool
                    var contactQ = await unitOfWork.ContactManager.GetAsync(t => t.PrimaryPhoneNumber == model.PrimaryPhoneNumber);
                    var contact = contactQ.FirstOrDefault();

                    if (contact != null && model.RequestedPoolID == null)
                    {
                        //Get current pool 
                        var currentPoolQ = await unitOfWork.PoolManager.GetAsync(t => t.ID == model.CurrentPoolID);
                        var currentPool = currentPoolQ.FirstOrDefault();

                        //Create transfer request
                        PoolRequest newRequest = new PoolRequest
                        {
                            PoolID = contact.PoolID,
                            RecordID = contact.ID,
                            RecordType = model.RecordType,
                            RequestType = (int)PoolRequestTypes.RecordFromPool,
                            Status = (int)PoolRequestStatuses.Pending,
                            RequesteeID = model.AssigneeID,
                            Requestee_PoolID = currentPool.ID,
                            RecordStatusID = model.RecordStatusID,
                            DescriptionEN = assigneeName.FirstName + " " + assigneeName.LastName + " is requesting to transfer record " +
                            contact.PrimaryPhoneNumber + " to space " + currentPool.NameEN,
                            RequestDate = await HelperFunctions.GetEgyptsCurrentLocalTime()
                        };

                        var requestCreationResult = await unitOfWork.PoolRequestManager.CreateAsync(newRequest);
                        if (requestCreationResult != null)
                        {
                            await unitOfWork.SaveChangesAsync();
                            result.Succeeded = true;
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Unable to process request");
                            result.Errors.Add("Unable to process request");
                            return result;
                        }
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Unable to find record");
                        result.Errors.Add("Unable to find record");
                        return result;
                    }
                }
                //Request to join pool
                else if (model.PrimaryPhoneNumber == null && model.RequestedPoolID != null)
                {
                    PoolRequest newRequest = new PoolRequest
                    {
                        PoolID = model.RequestedPoolID.Value,
                        RequesteeID = model.AssigneeID,
                        RequestType = (int)PoolRequestTypes.AddUserToPool,
                        Status = (int)PoolRequestStatuses.Pending,
                        DescriptionEN = assigneeName.FirstName + " " + assigneeName.LastName + " is requesting to join",
                    };

                    var requestCreationResult = await unitOfWork.PoolRequestManager.CreateAsync(newRequest);
                    if (requestCreationResult != null)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Unable to process request");
                        result.Errors.Add("Unable to process request");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Unable to process request");
                    result.Errors.Add("Unable to process request");
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

        public async Task<ApiResponse<List<PoolRequestModel>>> GetPoolPendingRequests(long poolID)
        {
            ApiResponse<List<PoolRequestModel>> result = new ApiResponse<List<PoolRequestModel>>();

            try
            {
                var pendingRequestsQ = await unitOfWork.PoolRequestManager.GetAsync(t => t.PoolID == poolID &&
                t.Status == (int)PoolRequestStatuses.Pending);
                var pendingRequests = pendingRequestsQ.ToList();

                if (pendingRequests != null && pendingRequests.Count > 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<PoolRequestModel>>(pendingRequests);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No pending requests");
                    result.Errors.Add("No pending requests");
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

        public async Task<ApiResponse<List<PoolRequestModel>>> GetUserPoolsPendingRequests()
        {
            ApiResponse<List<PoolRequestModel>> result = new ApiResponse<List<PoolRequestModel>>();

            try
            {
                //Get user pools
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    var userPoolsQ = await unitOfWork.PoolUserManager.GetAsync(t => t.UserID == userID && t.IsAdmin == true);
                    var userPools = userPoolsQ.ToList();
                    if (userPools != null && userPools.Count > 0)
                    {
                        List<PoolRequestModel> poolRequests = new List<PoolRequestModel>();

                        for (int i = 0; i < userPools.Count; i++)
                        {
                            var userPool = userPools[i];

                            var pendingRequestsQ = await unitOfWork.PoolRequestManager.GetAsync(t => t.PoolID == userPool.PoolID &&
                               t.Status == (int)PoolRequestStatuses.Pending, includeProperties: "Pool");
                            var pendingRequests = pendingRequestsQ.ToList();

                            if (pendingRequests != null && pendingRequests.Count > 0)
                            {
                                poolRequests.AddRange(mapper.Map<List<PoolRequestModel>>(pendingRequests));
                            }
                        }

                        //Filter requests via date
                        if (poolRequests.Count > 0)
                        {
                            poolRequests.OrderBy(t => t.RequestDate);
                            result.Succeeded = true;
                            result.Data = poolRequests;
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("No requests found");
                            result.Errors.Add("No requests found");
                            return result;
                        }
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("No pools for such user");
                        result.Errors.Add("No pools for such user");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Unauthorized");
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

        public async Task<ApiResponse<bool>> ApproveRequest(long requestID)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();

            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    var requestQ = await unitOfWork.PoolRequestManager.GetAsync(t => t.ID == requestID && t.Status == (int)PoolRequestStatuses.Pending);
                    var request = requestQ.FirstOrDefault();

                    if (request != null)
                    {
                        request.Status = (int)PoolRequestStatuses.Accepted;
                        request.AppliedActionDate = await HelperFunctions.GetEgyptsCurrentLocalTime();

                        var updateRes = await unitOfWork.PoolRequestManager.UpdateAsync(request);

                        if (updateRes)
                        {
                            //Set Request action
                            var actionResult = await ApplyRequestAction(request, request.RequestType);
                            return actionResult;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Unable to update request");
                            result.Errors.Add("Unable to update request");
                            return result;
                        }
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Request does not exist");
                        result.Errors.Add("Request does not exist");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Unauthoried");
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

        public async Task<ApiResponse<bool>> RejectRequest(long requestID)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();

            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    var requestQ = await unitOfWork.PoolRequestManager.GetAsync(t => t.ID == requestID && t.Status == (int)PoolRequestStatuses.Pending);
                    var request = requestQ.FirstOrDefault();

                    if (request != null)
                    {
                        request.Status = (int)PoolRequestStatuses.Rejected;
                        request.AppliedActionDate = await HelperFunctions.GetEgyptsCurrentLocalTime();

                        var updateRes = await unitOfWork.PoolRequestManager.UpdateAsync(request);

                        if (updateRes)
                        {
                            await unitOfWork.SaveChangesAsync();
                            result.Succeeded = true;
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Unable to update request");
                            result.Errors.Add("Unable to update request");
                            return result;
                        }
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Request does not exist");
                        result.Errors.Add("Request does not exist");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Unauthoried");
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

        public async Task<ApiResponse<bool>> ApplyRequestAction(PoolRequest request, int requestType)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                //Transfer record ownership to requestee and transfer from pool
                if (requestType == (int)PoolRequestTypes.RecordFromPool)
                {
                    //Get record
                    var contactQ = await unitOfWork.ContactManager.GetAsync(t => t.ID == request.RecordID.Value, includeProperties: "Customer");
                    var contact = contactQ.FirstOrDefault();

                    if (contact != null)
                    {
                        if (contact.CustomerID != null)
                        {
                            var customerDealsQ = await unitOfWork.DealManager.GetAsync(t => t.CustomerID == contact.CustomerID);
                            contact.Customer.Deals = customerDealsQ.ToList();
                        }
                        //Update contact status
                        var transferResult = await TransferRecordOwnership(request, contact);
                        return transferResult;

                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Record not found");
                        result.Errors.Add("Record not found");
                        return result;
                    }
                }
                //Transfer user to this pool and assign record to them
                else if (requestType == (int)PoolRequestTypes.RecordToPool)
                {
                    //Get pool details
                    var poolQ = await unitOfWork.PoolManager.GetAsync(t => t.ID == request.PoolID);
                    var pool = poolQ.FirstOrDefault();


                    //Add user to pool
                    Pool_User assignmentModel = new Pool_User
                    {
                        Capacity = pool.Capacity.HasValue ? pool.Capacity.Value : 0,
                        PoolID = request.PoolID,
                        UserID = request.RequesteeID,
                        IsAdmin = false
                    };

                    var assignmentResult = await unitOfWork.PoolUserManager.CreateAsync(assignmentModel);

                    if (assignmentResult != null)
                    {
                        await unitOfWork.SaveChangesAsync();

                        //Get record
                        var contactQ = await unitOfWork.ContactManager.GetAsync(t => t.ID == request.RecordID.Value, includeProperties: "Customer");
                        var contact = contactQ.FirstOrDefault();

                        if (contact != null)
                        {
                            if (contact.CustomerID != null)
                            {
                                var customerDealsQ = await unitOfWork.DealManager.GetAsync(t => t.CustomerID == contact.CustomerID);
                                contact.Customer.Deals = customerDealsQ.ToList();
                            }
                            //Update contact status
                            var transferResult = await TransferRecordOwnership(request, contact);
                            return transferResult;

                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Record not found");
                            result.Errors.Add("Record not found");
                            return result;
                        }
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Unable to add user to space");
                        result.Errors.Add("Unable to add user to space");
                        return result;
                    }

                }
                //Add user to pool
                else if (requestType == (int)PoolRequestTypes.AddUserToPool)
                {
                    //Get pool details
                    var poolQ = await unitOfWork.PoolManager.GetAsync(t => t.ID == request.PoolID);
                    var pool = poolQ.FirstOrDefault();


                    //Add user to pool
                    Pool_User assignmentModel = new Pool_User
                    {
                        Capacity = pool.Capacity.HasValue ? pool.Capacity.Value : 0,
                        PoolID = request.PoolID,
                        UserID = request.RequesteeID,
                        IsAdmin = false
                    };

                    var assignmentResult = await unitOfWork.PoolUserManager.CreateAsync(assignmentModel);

                    if (assignmentResult != null)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Unable to add user to space");
                        result.Errors.Add("Unable to add user to space");
                        return result;
                    }
                }
                else
                {
                    throw new NotImplementedException();
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

        public async Task<ApiResponse<bool>> TransferRecordOwnership(PoolRequest request, Contact contact)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                contact.PoolID = request.Requestee_PoolID.Value;
                contact.AssignedUserID = request.RequesteeID;
                contact.State = (int)CustomerStageState.Converted;
                //record has flows
                if (contact.CustomerID != null)
                {
                    //Iterate all customer deals and overtake their assignee ID with requestee ID
                    for (int i = 0; i < contact.Customer.Deals.Count; i++)
                    {
                        var currentDeal = contact.Customer.Deals[i];

                        currentDeal.PoolID = request.Requestee_PoolID.Value;

                        //get active stage for deal
                        if (currentDeal.ActiveStageType == (int)CustomerStageIndicator.Prospect)
                        {
                            var activeStageQ = await unitOfWork.ProspectManager.GetAsync(t => t.ID == currentDeal.ActiveStageID);
                            var activeStage = activeStageQ.FirstOrDefault();

                            if (activeStage != null)
                            {
                                activeStage.AssignedUserID = request.RequesteeID;

                                await unitOfWork.ProspectManager.UpdateAsync(activeStage);
                            }
                        }
                        else if (currentDeal.ActiveStageType == (int)CustomerStageIndicator.Lead)
                        {
                            var activeStageQ = await unitOfWork.LeadManager.GetAsync(t => t.ID == currentDeal.ActiveStageID);
                            var activeStage = activeStageQ.FirstOrDefault();

                            if (activeStage != null)
                            {
                                activeStage.AssignedUserID = request.RequesteeID;

                                await unitOfWork.LeadManager.UpdateAsync(activeStage);
                            }
                        }
                        else if (currentDeal.ActiveStageType == (int)CustomerStageIndicator.Opportunity)
                        {
                            var activeStageQ = await unitOfWork.OpportunityManager.GetAsync(t => t.ID == currentDeal.ActiveStageID);
                            var activeStage = activeStageQ.FirstOrDefault();

                            if (activeStage != null)
                            {
                                activeStage.AssignedUserID = request.RequesteeID;

                                await unitOfWork.OpportunityManager.UpdateAsync(activeStage);
                            }
                        }
                        else if (currentDeal.ActiveStageType == (int)CustomerStageIndicator.DoneDeal)
                        {
                            var activeStageQ = await unitOfWork.DoneDealManager.GetAsync(t => t.ID == currentDeal.ActiveStageID);
                            var activeStage = activeStageQ.FirstOrDefault();

                            if (activeStage != null)
                            {
                                activeStage.AssignedUserID = request.RequesteeID;

                                await unitOfWork.DoneDealManager.UpdateAsync(activeStage);
                            }
                        }
                    }

                    //Create new deal with requested flow type

                    Deal deal = new Deal
                    {
                        CustomerID = contact.CustomerID.Value,
                        PoolID = request.Requestee_PoolID.Value,
                        ActiveStageType = request.RecordType.Value,
                    };

                    var dealCreationRes = await unitOfWork.DealManager.CreateAsync(deal);

                    //Create requested type for deal
                    if (dealCreationRes != null)
                    {
                        await unitOfWork.SaveChangesAsync();

                        //Create related record
                        if (request.RecordType == (int)CustomerStageIndicator.Lead)
                        {
                            Lead record = new Lead
                            {
                                AssignedUserID = request.RequesteeID,
                                DealID = dealCreationRes.ID,
                                IsFresh = true,
                                State = (int)CustomerStageState.Initial,
                                StatusID = request.RecordStatusID,
                            };

                            var recordCreationRes = await unitOfWork.LeadManager.CreateAsync(record);
                            if (recordCreationRes != null)
                            {
                                //Finalize
                                await unitOfWork.SaveChangesAsync();

                                //Update Deal Record ID
                                dealCreationRes.ActiveStageID = recordCreationRes.ID;
                                var dealUpdateRes = await unitOfWork.DealManager.UpdateAsync(deal);
                                await unitOfWork.SaveChangesAsync();
                                result.Succeeded = true;
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("Error creating record, please try again");
                                result.Errors.Add("Error creating record, please try again");
                                return result;
                            }
                        }
                        else if (request.RecordType == (int)CustomerStageIndicator.Prospect)
                        {
                            Prospect record = new Prospect
                            {
                                AssignedUserID = request.RequesteeID,
                                DealID = dealCreationRes.ID,
                                IsFresh = true,
                                State = (int)CustomerStageState.Initial,
                                StatusID = request.RecordStatusID,
                            };
                            var recordCreationRes = await unitOfWork.ProspectManager.CreateAsync(record);
                            if (recordCreationRes != null)
                            {
                                //Finalize
                                await unitOfWork.SaveChangesAsync();
                                //Update Deal Record ID
                                dealCreationRes.ActiveStageID = recordCreationRes.ID;
                                var dealUpdateRes = await unitOfWork.DealManager.UpdateAsync(deal);
                                result.Succeeded = true;
                                await unitOfWork.SaveChangesAsync();
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("Error creating record, please try again");
                                result.Errors.Add("Error creating record, please try again");
                                return result;
                            }
                        }
                        else if (request.RecordType == (int)CustomerStageIndicator.Opportunity)
                        {
                            Opportunity record = new Opportunity
                            {
                                AssignedUserID = request.RequesteeID,
                                DealID = dealCreationRes.ID,
                                IsFresh = true,
                                State = (int)CustomerStageState.Initial,
                                StatusID = request.RecordStatusID,
                            };
                            var recordCreationRes = await unitOfWork.OpportunityManager.CreateAsync(record);
                            if (recordCreationRes != null)
                            {
                                //Finalize
                                await unitOfWork.SaveChangesAsync();
                                //Update Deal Record ID
                                dealCreationRes.ActiveStageID = recordCreationRes.ID;
                                var dealUpdateRes = await unitOfWork.DealManager.UpdateAsync(deal);
                                await unitOfWork.SaveChangesAsync();
                                result.Succeeded = true;
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("Error creating record, please try again");
                                result.Errors.Add("Error creating record, please try again");
                                return result;
                            }
                        }
                        else if (request.RecordType == (int)CustomerStageIndicator.DoneDeal)
                        {
                            DoneDeal record = new DoneDeal
                            {
                                AssignedUserID = request.RequesteeID,
                                DealID = dealCreationRes.ID,
                                State = (int)CustomerStageState.Initial,

                            };
                            var recordCreationRes = await unitOfWork.DoneDealManager.CreateAsync(record);
                            if (recordCreationRes != null)
                            {

                                //Finalize
                                await unitOfWork.SaveChangesAsync();
                                //Update Deal Record ID
                                contact.IsFinalized = true;
                                var finalizeContactRes = await unitOfWork.ContactManager.UpdateAsync(contact);
                                dealCreationRes.ActiveStageID = recordCreationRes.ID;
                                var dealUpdateRes = await unitOfWork.DealManager.UpdateAsync(deal);
                                await unitOfWork.SaveChangesAsync();
                                await unitOfWork.SaveChangesAsync();
                                result.Succeeded = true;
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("Error creating record, please try again");
                                result.Errors.Add("Error creating record, please try again");
                                return result;
                            }
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Invalid Customer Stage");
                            result.Errors.Add("Invalid Customer Stage");
                            return result;
                        }
                    }
                    //Deal creation error
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Error creating record, please try again");
                        result.Errors.Add("Error creating record, please try again");
                        return result;
                    }

                }
                //record has no flows, Create flow with desired record type
                else
                {
                    //Create customer
                    Customer customer = new Customer
                    {
                        FullNameEN = contact.FullNameEN,
                        FullNameAR = contact.FullNameAR,
                        Address = contact.Address,
                        AssignedUserID = request.RequesteeID,
                        Email = contact.Email,
                        PrimaryPhoneNumber = contact.PrimaryPhoneNumber,
                        Occupation = contact.Occupation,
                        ChannelID = contact.ChannelID,
                        LSTID = contact.LSTID,
                        LSNID = contact.LSNID,
                        PoolID = contact.PoolID
                    };

                    var customerCreationRes = await unitOfWork.CustomerManager.CreateAsync(customer);

                    if (customerCreationRes != null)
                    {
                        await unitOfWork.SaveChangesAsync();

                        contact.CustomerID = customerCreationRes.ID;
                        var updateContactRes = await unitOfWork.ContactManager.UpdateAsync(contact);


                        //Create deal

                        Deal deal = new Deal
                        {
                            CustomerID = customerCreationRes.ID,
                            PoolID = request.Requestee_PoolID.Value,
                            ActiveStageType = request.RecordType.Value
                        };

                        var dealCreationRes = await unitOfWork.DealManager.CreateAsync(deal);

                        if (dealCreationRes != null)
                        {
                            await unitOfWork.SaveChangesAsync();

                            //Create related record
                            if (request.RecordType == (int)CustomerStageIndicator.Lead)
                            {
                                Lead record = new Lead
                                {
                                    AssignedUserID = request.RequesteeID,
                                    DealID = dealCreationRes.ID,
                                    IsFresh = true,
                                    State = (int)CustomerStageState.Initial,
                                    StatusID = request.RecordStatusID,
                                };

                                var recordCreationRes = await unitOfWork.LeadManager.CreateAsync(record);
                                if (recordCreationRes != null)
                                {

                                    //Finalize
                                    await unitOfWork.SaveChangesAsync();

                                    deal.ActiveStageID = recordCreationRes.ID;
                                    var dealUpdate = await unitOfWork.SaveChangesAsync();

                                    await unitOfWork.SaveChangesAsync();
                                    result.Succeeded = true;
                                    return result;
                                }
                                else
                                {
                                    result.Succeeded = false;
                                    result.Errors.Add("Error creating record, please try again");
                                    result.Errors.Add("Error creating record, please try again");
                                    return result;
                                }
                            }
                            else if (request.RecordType == (int)CustomerStageIndicator.Prospect)
                            {
                                Prospect record = new Prospect
                                {
                                    AssignedUserID = request.RequesteeID,
                                    DealID = dealCreationRes.ID,
                                    IsFresh = true,
                                    State = (int)CustomerStageState.Initial,
                                    StatusID = request.RecordStatusID,
                                };
                                var recordCreationRes = await unitOfWork.ProspectManager.CreateAsync(record);
                                if (recordCreationRes != null)
                                {
                                    //Finalize
                                    await unitOfWork.SaveChangesAsync();

                                    deal.ActiveStageID = recordCreationRes.ID;
                                    var dealUpdate = await unitOfWork.SaveChangesAsync();

                                    await unitOfWork.SaveChangesAsync();
                                    result.Succeeded = true;
                                    return result;
                                }
                                else
                                {
                                    result.Succeeded = false;
                                    result.Errors.Add("Error creating record, please try again");
                                    result.Errors.Add("Error creating record, please try again");
                                    return result;
                                }
                            }
                            else if (request.RecordType == (int)CustomerStageIndicator.Opportunity)
                            {
                                Opportunity record = new Opportunity
                                {
                                    AssignedUserID = request.RequesteeID,
                                    DealID = dealCreationRes.ID,
                                    IsFresh = true,
                                    State = (int)CustomerStageState.Initial,
                                    StatusID = request.RecordStatusID,
                                };
                                var recordCreationRes = await unitOfWork.OpportunityManager.CreateAsync(record);
                                if (recordCreationRes != null)
                                {
                                    //Finalize
                                    await unitOfWork.SaveChangesAsync();

                                    deal.ActiveStageID = recordCreationRes.ID;
                                    var dealUpdate = await unitOfWork.SaveChangesAsync();

                                    await unitOfWork.SaveChangesAsync();
                                    result.Succeeded = true;
                                    return result;
                                }
                                else
                                {
                                    result.Succeeded = false;
                                    result.Errors.Add("Error creating record, please try again");
                                    result.Errors.Add("Error creating record, please try again");
                                    return result;
                                }
                            }
                            else if (request.RecordType == (int)CustomerStageIndicator.DoneDeal)
                            {
                                DoneDeal record = new DoneDeal
                                {
                                    AssignedUserID = request.RequesteeID,
                                    DealID = dealCreationRes.ID,
                                    State = (int)CustomerStageState.Initial,

                                };
                                var recordCreationRes = await unitOfWork.DoneDealManager.CreateAsync(record);
                                if (recordCreationRes != null)
                                {

                                    //Finalize
                                    await unitOfWork.SaveChangesAsync();

                                    contact.IsFinalized = true;
                                    var finalizeContactRes = await unitOfWork.ContactManager.UpdateAsync(contact);

                                    deal.ActiveStageID = recordCreationRes.ID;
                                    var dealUpdate = await unitOfWork.SaveChangesAsync();

                                    await unitOfWork.SaveChangesAsync();
                                    result.Succeeded = true;
                                    return result;
                                }
                                else
                                {
                                    result.Succeeded = false;
                                    result.Errors.Add("Error creating record, please try again");
                                    result.Errors.Add("Error creating record, please try again");
                                    return result;
                                }
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("Invalid Customer Stage");
                                result.Errors.Add("Invalid Customer Stage");
                                return result;
                            }
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Error creating record, please try again");
                            result.Errors.Add("Error creating record, please try again");
                            return result;
                        }
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Error creating record, please try again");
                        result.Errors.Add("Error creating record, please try again");
                        return result;
                    }
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

        #endregion

        #region Pool Records Filter
        public async Task<ApiResponse<List<ContactListViewModel>>> FilterPoolRecords(FilterPoolRecordsModel model)
        {
            ApiResponse<List<ContactListViewModel>> result = new ApiResponse<List<ContactListViewModel>>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    if (model.PoolID != null)
                    {
                        if (model.CustomerStage == (int)CustomerStageIndicator.Contact)
                        {
                            var recordQuery = await unitOfWork.ContactManager.GetAsync(t => t.PoolID == model.PoolID && t.State == (int)CustomerStageState.Unassigned
                            && (t.Address.Contains(model.Address) || t.Email.Contains(model.Email) || t.FullNameEN.Contains(model.FullNameEN) || t.FullNameAR.Contains(model.FullNameAR)
                            || t.Occupation.Contains(model.Occupation) || t.ChannelID == model.ChannelID || t.LSTID == model.LSTID || t.LSNID == model.LSNID));

                            var records = recordQuery.ToList();

                            if (records != null)
                            {
                                result.Succeeded = true;
                                result.Data = mapper.Map<List<ContactListViewModel>>(records);
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("No records found");
                                result.Errors.Add("No records found");
                                return result;
                            }
                        }
                        else if (model.CustomerStage == (int)CustomerStageIndicator.Prospect)
                        {
                            var recordQuery = await unitOfWork.ProspectManager.GetAsync(t => t.Deal.Customer.PoolID == model.PoolID && t.State == (int)CustomerStageState.Unassigned
                            && (t.Deal.Customer.Address.Contains(model.Address) || t.Deal.Customer.Email.Contains(model.Email) || t.Deal.Customer.FullNameEN.Contains(model.FullNameEN)
                            || t.Deal.Customer.FullNameAR.Contains(model.FullNameAR) || t.Deal.Customer.Occupation.Contains(model.Occupation) || t.Deal.Customer.ChannelID == model.ChannelID
                            || t.Deal.Customer.LSTID == model.LSTID || t.Deal.Customer.LSNID == model.LSNID));
                            var records = recordQuery.ToList();

                            if (records != null)
                            {
                                result.Succeeded = true;
                                result.Data = mapper.Map<List<ContactListViewModel>>(records);
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("No records found");
                                result.Errors.Add("No records found");
                                return result;
                            }

                        }
                        else if (model.CustomerStage == (int)CustomerStageIndicator.Lead)
                        {
                            var recordQuery = await unitOfWork.LeadManager.GetAsync(t => t.Deal.Customer.PoolID == model.PoolID && t.State == (int)CustomerStageState.Unassigned
                            && (t.Deal.Customer.Address.Contains(model.Address) || t.Deal.Customer.Email.Contains(model.Email) || t.Deal.Customer.FullNameEN.Contains(model.FullNameEN)
                            || t.Deal.Customer.FullNameAR.Contains(model.FullNameAR) || t.Deal.Customer.Occupation.Contains(model.Occupation) || t.Deal.Customer.ChannelID == model.ChannelID
                            || t.Deal.Customer.LSTID == model.LSTID || t.Deal.Customer.LSNID == model.LSNID));
                            var records = recordQuery.ToList();

                            if (records != null)
                            {
                                result.Succeeded = true;
                                result.Data = mapper.Map<List<ContactListViewModel>>(records);
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("No records found");
                                result.Errors.Add("No records found");
                                return result;
                            }

                        }
                        else if (model.CustomerStage == (int)CustomerStageIndicator.Opportunity)
                        {
                            var recordQuery = await unitOfWork.OpportunityManager.GetAsync(t => t.Deal.Customer.PoolID == model.PoolID && t.State == (int)CustomerStageState.Unassigned
                            && (t.Deal.Customer.Address.Contains(model.Address) || t.Deal.Customer.Email.Contains(model.Email) || t.Deal.Customer.FullNameEN.Contains(model.FullNameEN)
                            || t.Deal.Customer.FullNameAR.Contains(model.FullNameAR) || t.Deal.Customer.Occupation.Contains(model.Occupation) || t.Deal.Customer.ChannelID == model.ChannelID
                            || t.Deal.Customer.LSTID == model.LSTID || t.Deal.Customer.LSNID == model.LSNID));
                            var records = recordQuery.ToList();

                            if (records != null)
                            {
                                result.Succeeded = true;
                                result.Data = mapper.Map<List<ContactListViewModel>>(records);
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("No records found");
                                result.Errors.Add("No records found");
                                return result;
                            }
                        }
                        else if (model.CustomerStage == (int)CustomerStageIndicator.DoneDeal)
                        {
                            var recordQuery = await unitOfWork.DoneDealManager.GetAsync(t => t.Deal.Customer.PoolID == model.PoolID && t.State == (int)CustomerStageState.Unassigned
                            && (t.Deal.Customer.Address.Contains(model.Address) || t.Deal.Customer.Email.Contains(model.Email) || t.Deal.Customer.FullNameEN.Contains(model.FullNameEN)
                            || t.Deal.Customer.FullNameAR.Contains(model.FullNameAR) || t.Deal.Customer.Occupation.Contains(model.Occupation) || t.Deal.Customer.ChannelID == model.ChannelID
                            || t.Deal.Customer.LSTID == model.LSTID || t.Deal.Customer.LSNID == model.LSNID));
                            var records = recordQuery.ToList();

                            if (records != null)
                            {
                                result.Succeeded = true;
                                result.Data = mapper.Map<List<ContactListViewModel>>(records);
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("No records found");
                                result.Errors.Add("No records found");
                                return result;
                            }

                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Invalid Stage");
                            result.Errors.Add("Invalid Stage");
                            return result;
                        }
                    }
                    //Filter user assigned records
                    else
                    {
                        if (model.CustomerStage == (int)CustomerStageIndicator.Contact)
                        {
                            var recordQuery = await unitOfWork.ContactManager.GetAsync(t => t.AssignedUserID == userID && t.State == (int)CustomerStageState.Initial
                            && (t.Address.Contains(model.Address) || t.Email.Contains(model.Email) || t.FullNameEN.Contains(model.FullNameEN) || t.FullNameAR.Contains(model.FullNameAR)
                            || t.Occupation.Contains(model.Occupation) || t.ChannelID == model.ChannelID || t.LSTID == model.LSTID || t.LSNID == model.LSNID));
                            var records = recordQuery.ToList();

                            if (records != null)
                            {
                                result.Succeeded = true;
                                result.Data = mapper.Map<List<ContactListViewModel>>(records);
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("No records found");
                                result.Errors.Add("No records found");
                                return result;
                            }


                        }
                        else if (model.CustomerStage == (int)CustomerStageIndicator.Prospect)
                        {
                            var recordQuery = await unitOfWork.ProspectManager.GetAsync(t => t.AssignedUserID == userID && t.State == (int)CustomerStageState.Initial
                            && (t.Deal.Customer.Address.Contains(model.Address) || t.Deal.Customer.Email.Contains(model.Email) || t.Deal.Customer.FullNameEN.Contains(model.FullNameEN)
                            || t.Deal.Customer.FullNameAR.Contains(model.FullNameAR) || t.Deal.Customer.Occupation.Contains(model.Occupation) || t.Deal.Customer.ChannelID == model.ChannelID
                            || t.Deal.Customer.LSTID == model.LSTID || t.Deal.Customer.LSNID == model.LSNID));
                            var records = recordQuery.ToList();

                            if (records != null)
                            {
                                result.Succeeded = true;
                                result.Data = mapper.Map<List<ContactListViewModel>>(records);
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("No records found");
                                result.Errors.Add("No records found");
                                return result;
                            }



                        }
                        else if (model.CustomerStage == (int)CustomerStageIndicator.Lead)
                        {
                            var recordQuery = await unitOfWork.LeadManager.GetAsync(t => t.AssignedUserID == userID && t.State == (int)CustomerStageState.Initial
                            && (t.Deal.Customer.Address.Contains(model.Address) || t.Deal.Customer.Email.Contains(model.Email) || t.Deal.Customer.FullNameEN.Contains(model.FullNameEN)
                            || t.Deal.Customer.FullNameAR.Contains(model.FullNameAR) || t.Deal.Customer.Occupation.Contains(model.Occupation) || t.Deal.Customer.ChannelID == model.ChannelID
                            || t.Deal.Customer.LSTID == model.LSTID || t.Deal.Customer.LSNID == model.LSNID));
                            var records = recordQuery.ToList();

                            if (records != null)
                            {
                                result.Succeeded = true;
                                result.Data = mapper.Map<List<ContactListViewModel>>(records);
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("No records found");
                                result.Errors.Add("No records found");
                                return result;
                            }


                        }
                        else if (model.CustomerStage == (int)CustomerStageIndicator.Opportunity)
                        {
                            var recordQuery = await unitOfWork.OpportunityManager.GetAsync(t => t.AssignedUserID == userID && t.State == (int)CustomerStageState.Initial
                            && (t.Deal.Customer.Address.Contains(model.Address) || t.Deal.Customer.Email.Contains(model.Email) || t.Deal.Customer.FullNameEN.Contains(model.FullNameEN)
                            || t.Deal.Customer.FullNameAR.Contains(model.FullNameAR) || t.Deal.Customer.Occupation.Contains(model.Occupation) || t.Deal.Customer.ChannelID == model.ChannelID
                            || t.Deal.Customer.LSTID == model.LSTID || t.Deal.Customer.LSNID == model.LSNID));
                            var records = recordQuery.ToList();

                            if (records != null)
                            {
                                result.Succeeded = true;
                                result.Data = mapper.Map<List<ContactListViewModel>>(records);
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("No records found");
                                result.Errors.Add("No records found");
                                return result;
                            }
                        }
                        else if (model.CustomerStage == (int)CustomerStageIndicator.DoneDeal)
                        {
                            var recordQuery = await unitOfWork.DoneDealManager.GetAsync(t => t.AssignedUserID == userID && t.State == (int)CustomerStageState.Initial
                            && (t.Deal.Customer.Address.Contains(model.Address) || t.Deal.Customer.Email.Contains(model.Email) || t.Deal.Customer.FullNameEN.Contains(model.FullNameEN)
                            || t.Deal.Customer.FullNameAR.Contains(model.FullNameAR) || t.Deal.Customer.Occupation.Contains(model.Occupation) || t.Deal.Customer.ChannelID == model.ChannelID
                            || t.Deal.Customer.LSTID == model.LSTID || t.Deal.Customer.LSNID == model.LSNID));
                            var records = recordQuery.ToList();

                            if (records != null)
                            {
                                result.Succeeded = true;
                                result.Data = mapper.Map<List<ContactListViewModel>>(records);
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("No records found");
                                result.Errors.Add("No records found");
                                return result;
                            }

                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Invalid Stage");
                            result.Errors.Add("Invalid Stage");
                            return result;
                        }
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
        #endregion
    }

}


