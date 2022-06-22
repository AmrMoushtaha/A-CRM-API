
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Stack.Core;
using Stack.DTOs;
using AutoMapper;
using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.Linq;
using Stack.DTOs.Models.Modules.Auth;
using Newtonsoft.Json;
using Stack.DTOs.Requests.Modules.Auth;

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

        /// <summary>
        /// Create a new user role and set all system authorizations to false.
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<ApiResponse<bool>> CreateNewRole(RoleCreationModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                bool x = await unitOfWork.RoleManager.RoleExistsAsync(model.NameEN);

                if (!x)
                {

                    var role = new ApplicationRole();

                    role.Name = model.NameEN;

                    role.NameAR = model.NameAR;

                    role.DescriptionEN = model.DescriptionEN;

                    role.DescriptionAR = model.DescriptionAR;

                    

                    if(model.ParentRoleID != null || model.ParentRoleID != "")
                    {
                        role.HasParent = true;
                        role.ParentRoleID = model.ParentRoleID;
                    }
                    else
                    {
                        role.ParentRoleID = null;
                        role.HasParent = false;
                    }

                    var res = await unitOfWork.RoleManager.CreateAsync(role);

                    if (res.Succeeded)
                    {

                        var roleResult = await unitOfWork.RoleManager.FindByNameAsync(model.NameEN);

                        model.AuthModel.RoleID = roleResult.Id;

                        model.AuthModel.RoleNameEN = roleResult.Name;

                        model.AuthModel.RoleNameAR = roleResult.NameAR;

                        roleResult.SystemAuthorizations = JsonConvert.SerializeObject(model.AuthModel);

                        var updateRoleResult  = await unitOfWork.RoleManager.UpdateAsync(roleResult);

                        await unitOfWork.SaveChangesAsync();

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
                result.Errors.Add("Failed to create a new role !");
                return result;

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }

        }


        /// <summary>
        /// Update an existing use role .
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<ApiResponse<bool>> UpdateRole(EditRoleModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var roleResult = await unitOfWork.RoleManager.FindByIdAsync(model.Id);

                if (roleResult != null)
                {

                    var duplicateRoleNameResult = await unitOfWork.RoleManager.CheckDuplicateName(model.Id, model.NameEN, model.NameAR);

                    if (duplicateRoleNameResult != null)
                    {

                        roleResult.Name = model.NameEN;

                        roleResult.NameAR = model.NameAR;

                        roleResult.DescriptionEN = model.DescriptionEN;

                        roleResult.DescriptionAR = model.DescriptionAR;

                        roleResult.SystemAuthorizations = JsonConvert.SerializeObject(model.AuthModel);

                        if (model.ParentRoleID != null || model.ParentRoleID != "")
                        {
                            roleResult.HasParent = true;
                            roleResult.ParentRoleID = model.ParentRoleID;
                        }
                        else
                        {
                            roleResult.ParentRoleID = null;
                            roleResult.HasParent = false;
                        }


                        var updateRoleResult = await unitOfWork.RoleManager.UpdateAsync(roleResult);

                        if(updateRoleResult.Succeeded == true)
                        {

                            await unitOfWork.SaveChangesAsync();

                            result.Succeeded = true;
                            result.Data = true;
                            return result;

                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Failed to update role !");
                            return result;
                        }

                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to update user type, a type with a similar name already exists !");
                        return result;
                    }
                  

                }

                result.Succeeded = false;
                result.Errors.Add("Failed to update role !");
                return result;

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }

        }


        /// <summary>
        /// Fetch the list of system roles . 
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse<List<ApplicationRoleDTO>>> GetSystemRoles()
        {
            ApiResponse<List<ApplicationRoleDTO>> result = new ApiResponse<List<ApplicationRoleDTO>>();
            try
            {

                List<ApplicationRole> rolesResult = await unitOfWork.RoleManager.GetAllSystemRoles();

                result.Data = mapper.Map<List<ApplicationRoleDTO>>(rolesResult);

                result.Succeeded = true;

                return result;

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }

        }

        //public async Task<ApiResponse<bool>> InstantiateSystemRoles()
        //{
        //    ApiResponse<bool> result = new ApiResponse<bool>();

        //    //PreDefined System Roles
        //    string[] systemRoles = new string[5] { "Purchasing Employee", "Purchasing Manager", "Department Employee", "Admin", "Vendor" }; 

        //    foreach (string roleName in systemRoles)
        //    {
        //        try
        //        {
        //            bool isRoleFound = await unitOfWork.RoleManager.RoleExistsAsync(roleName);
        //            if (!isRoleFound)
        //            {
        //                var role = new ApplicationRole();
        //                role.Name = roleName;

        //                var roleCreationResult = await unitOfWork.RoleManager.CreateAsync(role);

        //                if (roleCreationResult.Succeeded)
        //                {
        //                    result.Data = true;
        //                    result.Succeeded = true;
        //                    continue;
        //                }
        //                else
        //                {
        //                    result.Succeeded = false;
        //                    foreach (var error in roleCreationResult.Errors)
        //                    {
        //                        result.Errors.Add(error.Description);
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            result.Succeeded = false;
        //            result.Errors.Add(ex.Message);
        //            return result;
        //        }
        //    }
        //    return result;
        //}

    }

}


