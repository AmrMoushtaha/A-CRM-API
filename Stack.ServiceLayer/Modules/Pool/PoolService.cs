
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


namespace Stack.ServiceLayer.Modules.pool
{
    public class PoolService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private static readonly HttpClient client = new HttpClient();

        public PoolService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;

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
                        };

                        var creationResult = await unitOfWork.PoolManager.CreateAsync(creationModel);
                        if (creationResult != null)
                        {
                            await unitOfWork.SaveChangesAsync();

                            //Assign pool creator as admin
                            Pool_Admin poolAdmin = new Pool_Admin
                            {
                                PoolID = creationResult.ID,
                                UserID = userID
                            };

                            var adminAssignmentResult = await unitOfWork.PoolAdminManager.CreateAsync(poolAdmin);
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

        public async Task<ApiResponse<bool>> AssignUsersToPool(PoolAssignmentModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (userID != null)
                {
                    //Verify admin priviliges
                    var adminVerificationQuery = await unitOfWork.PoolAdminManager.GetAsync(t => t.UserID == userID);
                    var adminVerification = adminVerificationQuery.FirstOrDefault();

                    if (adminVerification != null)
                    {
                        for (int i = 0; i < model.UserIDs.Count; i++)
                        {
                            var currentUserID = model.UserIDs[i];

                            Pool_Users pool_Users = new Pool_Users
                            {
                                PoolID = model.PoolID,
                                UserID = currentUserID,
                            };

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

        //Sidebar view
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


                    var adminPools = await unitOfWork.PoolAdminManager.GetUserPools(userID);

                    if (adminPools != null && adminPools.Count > 0)
                    {
                        poolSidebarViewModel.AddRange(adminPools);
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
                        var poolContacts = await unitOfWork.PoolAdminManager.GetPoolContacts(poolID, userID);

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
                    else //Attempt admin verification check
                    {
                        //Verify user pool permissions
                        var adminPoolQuery = await unitOfWork.PoolAdminManager.GetAsync(t => t.PoolID == poolID && t.UserID == userID, includeProperties: "Pool");
                        var adminPool = adminPoolQuery.FirstOrDefault();

                        if (adminPool != null) //Get administrator contacts
                        {
                            var poolContacts = await unitOfWork.PoolAdminManager.GetPoolContacts(poolID, userID);

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
                        else //Unauthorized
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Unauthorized");
                            result.Errors.Add("غير مصرح");
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
    }

}


