
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
using Stack.Entities.Enums.Modules.Auth;

namespace Stack.ServiceLayer.Modules.Auth
{
    public class AuthService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private static readonly HttpClient client = new HttpClient();

        public AuthService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;

        }


        public async Task<ApiResponse<bool>> InitializeSystem(string Password)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                //Initialize system roles
                if (!await unitOfWork.RoleManager.RoleExistsAsync(UserRoles.Administrator.ToString()))
                {
                    var role = new ApplicationRole();
                    role.Name = UserRoles.Administrator.ToString();
                    var res = await unitOfWork.RoleManager.CreateAsync(role);
                    await unitOfWork.SaveChangesAsync();
                }

                ApplicationUser user = new ApplicationUser
                {

                    UserName = "SysAdmin",
                    FirstName = "Administrator",
                    LastName = "Administrator",
                    PhoneNumber = "01000000000",
                    LockoutEnabled = true
                };

                var createUserResult = await unitOfWork.UserManager.CreateAsync(user, Password);

                if (createUserResult.Succeeded)
                {
                    var roleAssignmentRes = await unitOfWork.UserManager.AddToRoleAsync(user, UserRoles.Administrator.ToString());
                    if (roleAssignmentRes.Succeeded)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Data = false;
                        result.Errors.Add("Administrator creation failed!");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Data = false;
                    result.Errors.Add("Administrator creation failed!");
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

        public async Task<ApiResponse<bool>> CreateAdministrator(CreateDummyUserModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.UserName,
                };

                var createUserResult = await unitOfWork.UserManager.CreateAsync(user, model.Password);

                await unitOfWork.SaveChangesAsync();

                if (createUserResult.Succeeded)
                {
                    var roleAssignmentRes = await unitOfWork.UserManager.AddToRoleAsync(user, UserRoles.Administrator.ToString());
                    if (roleAssignmentRes.Succeeded)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Data = false;
                        result.Errors.Add("Administrator creation failed!");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    foreach (var error in createUserResult.Errors)
                    {
                        result.Errors.Add(error.Description);
                    }
                    result.ErrorType = ErrorType.LogicalError;
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


