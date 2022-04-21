
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
                bool x = await unitOfWork.RoleManager.RoleExistsAsync(model.RoleName);

                if (!x)
                {

                    var role = new ApplicationRole();

                    role.Name = model.RoleName;

                    var res = await unitOfWork.RoleManager.CreateAsync(role);

                    if (res.Succeeded)
                    {

                        var SystemAuthorizationsResult = await unitOfWork.AuthorizationSectionsManager.GetAsync(includeProperties:"SectionAuthorizations");

                        List<AuthorizationSection> AuthorizationSections = SystemAuthorizationsResult.ToList();

                        AuthorizationsModel roleAuthModel = new AuthorizationsModel();

                        List<AuthorizationSectionModel> roleDefaultSectionAuthorizations = new List<AuthorizationSectionModel>();

                        for(int i = 0; i < AuthorizationSections.Count; i++)
                        {

                            AuthorizationSectionModel authSectionModel = new AuthorizationSectionModel();

                            authSectionModel.ID = AuthorizationSections[i].ID;

                            authSectionModel.NameAR = AuthorizationSections[i].NameAR;

                            authSectionModel.NameEN = AuthorizationSections[i].NameEN;

                            authSectionModel.Code = AuthorizationSections[i].Code;

                            authSectionModel.SectionAuthorizations = new List<SectionAuthorizationModel>();

                            for(int j = 0; j < AuthorizationSections[i].SectionAuthorizations.Count; j++)
                            {

                                SectionAuthorizationModel sectionAuthModel = new SectionAuthorizationModel();

                                sectionAuthModel.ID = AuthorizationSections[i].SectionAuthorizations[j].ID;

                                sectionAuthModel.NameAR = AuthorizationSections[i].SectionAuthorizations[j].NameAR;

                                sectionAuthModel.NameEN = AuthorizationSections[i].SectionAuthorizations[j].NameEN;

                                sectionAuthModel.Code = AuthorizationSections[i].SectionAuthorizations[j].Code;

                                sectionAuthModel.AuthorizationSectionID = AuthorizationSections[i].ID;

                                sectionAuthModel.IsAuthorized = false;

                                authSectionModel.SectionAuthorizations.Add(sectionAuthModel);

                            }

                            roleDefaultSectionAuthorizations.Add(authSectionModel);

                        }

                        var roleResult = await unitOfWork.RoleManager.FindByNameAsync(model.RoleName);

                        roleAuthModel.AuthorizationSections = roleDefaultSectionAuthorizations;

                        roleAuthModel.RoleName = roleResult.Name;

                        roleAuthModel.RoleID = roleResult.Id;

                        roleResult.SystemAuthorizations = JsonConvert.SerializeObject(roleAuthModel);

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

        //public async Task<ApiResponse<bool>> UpdateUserRole(UpdateUserRoleModel model)
        //{
        //    ApiResponse<bool> result = new ApiResponse<bool>();
        //    try
        //    {





        //    }
        //    catch (Exception ex)
        //    {
        //        result.Succeeded = false;
        //        result.Errors.Add(ex.Message);
        //        return result;
        //    }

        //}


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


