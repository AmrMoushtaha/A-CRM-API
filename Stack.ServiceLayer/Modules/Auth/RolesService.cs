
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Stack.Core;
using Stack.DTOs;
using AutoMapper;
using Stack.Entities.Models.Modules.Auth;

namespace Stack.ServiceLayer.Modules.Auth
{
    public class RolesService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public RolesService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
            this.mapper = mapper;
        }

        //Function for creating a user role . 
        public async Task<ApiResponse<bool>> CreateRole(string roleName)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                bool x = await unitOfWork.RoleManager.RoleExistsAsync(roleName);
                if (!x)
                {
                    var role = new ApplicationRole();
                    role.Name = roleName;

                    var res = await unitOfWork.RoleManager.CreateAsync(role);

                    if (res.Succeeded)
                    {
                        result.Data = true;
                        result.Succeeded = true;
                        return result;
                    }
                    result.Succeeded = false;
                    foreach (var error in res.Errors)
                    {
                        result.Errors.Add(error.Description);
                    }
                    return result;
                }
                result.Succeeded = false;
                result.Errors.Add("Unable to create role !");
                return result;
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }

        }

        public async Task<ApiResponse<bool>> InstantiateSystemRoles()
        {
            ApiResponse<bool> result = new ApiResponse<bool>();

            //PreDefined System Roles
            string[] systemRoles = new string[5] { "Purchasing Employee", "Purchasing Manager", "Department Employee", "Admin", "Vendor" }; 

            foreach (string roleName in systemRoles)
            {
                try
                {
                    bool isRoleFound = await unitOfWork.RoleManager.RoleExistsAsync(roleName);
                    if (!isRoleFound)
                    {
                        var role = new ApplicationRole();
                        role.Name = roleName;

                        var roleCreationResult = await unitOfWork.RoleManager.CreateAsync(role);

                        if (roleCreationResult.Succeeded)
                        {
                            result.Data = true;
                            result.Succeeded = true;
                            continue;
                        }
                        else
                        {
                            result.Succeeded = false;
                            foreach (var error in roleCreationResult.Errors)
                            {
                                result.Errors.Add(error.Description);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Succeeded = false;
                    result.Errors.Add(ex.Message);
                    return result;
                }
            }
            return result;
        }


    }

}


