
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using Stack.DTOs;
using Stack.DTOs.Enums;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Stack.DTOs.Requests.Modules.Auth;
using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Enums.Modules.Auth;
using Stack.DTOs.Requests.Modules.System;
using System.Collections.Generic;
using Stack.DTOs.Models.Modules.Auth;
using System.Linq;
using Newtonsoft.Json;

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

        public async Task<ApiResponse<bool>> CreateAdministratorAccount(UserCreationModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                ApplicationUser user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.FirstName+model.LastName,
                };


                var SystemAuthorizationsResult = await unitOfWork.AuthorizationSectionsManager.GetAsync(includeProperties: "SectionAuthorizations");

                List<Entities.Models.Modules.Auth.AuthorizationSection> AuthorizationSections = SystemAuthorizationsResult.ToList();

                AuthorizationsModel roleAuthModel = new AuthorizationsModel();

                List<AuthorizationSectionModel> roleDefaultSectionAuthorizations = new List<AuthorizationSectionModel>();

                for (int i = 0; i < AuthorizationSections.Count; i++)
                {

                    AuthorizationSectionModel authSectionModel = new AuthorizationSectionModel();

                    authSectionModel.ID = AuthorizationSections[i].ID;

                    authSectionModel.NameAR = AuthorizationSections[i].NameAR;

                    authSectionModel.NameEN = AuthorizationSections[i].NameEN;

                    authSectionModel.Code = AuthorizationSections[i].Code;

                    authSectionModel.SectionAuthorizations = new List<SectionAuthorizationModel>();

                    for (int j = 0; j < AuthorizationSections[i].SectionAuthorizations.Count; j++)
                    {

                        SectionAuthorizationModel sectionAuthModel = new SectionAuthorizationModel();

                        sectionAuthModel.ID = AuthorizationSections[i].SectionAuthorizations[j].ID;

                        sectionAuthModel.NameAR = AuthorizationSections[i].SectionAuthorizations[j].NameAR;

                        sectionAuthModel.NameEN = AuthorizationSections[i].SectionAuthorizations[j].NameEN;

                        sectionAuthModel.Code = AuthorizationSections[i].SectionAuthorizations[j].Code;

                        sectionAuthModel.AuthorizationSectionID = AuthorizationSections[i].ID;

                        sectionAuthModel.IsAuthorized = true;

                        authSectionModel.SectionAuthorizations.Add(sectionAuthModel);

                    }

                    roleDefaultSectionAuthorizations.Add(authSectionModel);

                }

                var roleResult = await unitOfWork.RoleManager.FindByNameAsync(UserRoles.Administrator.ToString());

                roleAuthModel.AuthorizationSections = roleDefaultSectionAuthorizations;

                roleAuthModel.RoleNameEN = roleResult.Name;

                roleAuthModel.RoleNameAR = roleResult.NameAR;

                roleAuthModel.RoleID = roleResult.Id;

                user.SystemAuthorizations = JsonConvert.SerializeObject(roleAuthModel);

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

        public async Task<ApiResponse<AuthorizationsModel>> GetAuthModelByRoleID(string RoleID)
        {
            ApiResponse<AuthorizationsModel> result = new ApiResponse<AuthorizationsModel>();
            try
            {

                ApplicationRole roleResult = await unitOfWork.RoleManager.FindByIdAsync(RoleID);

                if(roleResult != null)
                {

                    result.Data = JsonConvert.DeserializeObject<AuthorizationsModel>(roleResult.SystemAuthorizations);
                    result.Succeeded = true;
                    return result;

                }
                else
                {

                    result.Succeeded=false;
                    result.Errors.Add("Failed to fetch the role's authorization model please try again !");
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

        public async Task<ApiResponse<AuthorizationsModel>> GetAuthModelByUserID(string UserID)
        {
            ApiResponse<AuthorizationsModel> result = new ApiResponse<AuthorizationsModel>();
            try
            {

                ApplicationUser userResult = await unitOfWork.UserManager.FindByIdAsync(UserID);

                if (userResult != null)
                {

                    result.Data = JsonConvert.DeserializeObject<AuthorizationsModel>(userResult.SystemAuthorizations);
                    result.Succeeded = true;
                    return result;

                }
                else
                {

                    result.Succeeded = false;
                    result.Errors.Add("Failed to fetch the users's authorization model please try again !");
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

        public async Task<ApiResponse<AuthorizationsModel>> UpdateRoleAuthorizations(AuthorizationsModel model)
        {
            ApiResponse<AuthorizationsModel> result = new ApiResponse<AuthorizationsModel>();
            try
            {

                ApplicationRole roleResult = await unitOfWork.RoleManager.FindByIdAsync(model.RoleID);

                if (roleResult != null)
                {

                    roleResult.SystemAuthorizations = JsonConvert.SerializeObject(model);

                    var updateRoleResult = await unitOfWork.RoleManager.UpdateAsync(roleResult);

                    await unitOfWork.SaveChangesAsync();

                    if(updateRoleResult.Succeeded == true)
                    {

                        result.Succeeded = true;
                        result.Data = model;
                        return result;

                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to update role authorizations !");
                        return result;
                    }

                }
                else
                {

                    result.Succeeded = false;
                    result.Errors.Add("Failed to fetch the role's authorization model please try again !");
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

        public async Task<ApiResponse<AuthorizationsModel>> UpdateUserAuthorizations(UpdateUserAuthModel model)
        {
            ApiResponse<AuthorizationsModel> result = new ApiResponse<AuthorizationsModel>();
            try
            {

                ApplicationUser usersResult = await unitOfWork.UserManager.FindByIdAsync(model.UserID);

                if (usersResult != null)
                {

                    usersResult.SystemAuthorizations = JsonConvert.SerializeObject(model.AuthModel);

                    var updateUserResult = await unitOfWork.UserManager.UpdateAsync(usersResult);

                    await unitOfWork.SaveChangesAsync();

                    if (updateUserResult.Succeeded == true)
                    {

                        result.Succeeded = true;
                        result.Data = model.AuthModel;
                        return result;

                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to update the user's authorizations !");
                        return result;
                    }

                }
                else
                {

                    result.Succeeded = false;
                    result.Errors.Add("Failed to update the user's authorizations !");
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

        public async Task<ApiResponse<AuthorizationsModel>> GetSystemAuthModel()
        {
            ApiResponse<AuthorizationsModel> result = new ApiResponse<AuthorizationsModel>();
            try
            {

                var SystemAuthorizationsResult = await unitOfWork.AuthorizationSectionsManager.GetAsync(includeProperties: "SectionAuthorizations");

                List<Entities.Models.Modules.Auth.AuthorizationSection> AuthorizationSections = SystemAuthorizationsResult.ToList();

                AuthorizationsModel SystemAuthModel = new AuthorizationsModel();

                List<AuthorizationSectionModel> SystemSectionAuthorizations = new List<AuthorizationSectionModel>();

                for (int i = 0; i < AuthorizationSections.Count; i++)
                {

                    AuthorizationSectionModel authSectionModel = new AuthorizationSectionModel();

                    authSectionModel.ID = AuthorizationSections[i].ID;

                    authSectionModel.NameAR = AuthorizationSections[i].NameAR;

                    authSectionModel.NameEN = AuthorizationSections[i].NameEN;

                    authSectionModel.Code = AuthorizationSections[i].Code;

                    authSectionModel.IsAuthorized = false;

                    authSectionModel.SectionAuthorizations = new List<SectionAuthorizationModel>();

                    for (int j = 0; j < AuthorizationSections[i].SectionAuthorizations.Count; j++)
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

                    SystemSectionAuthorizations.Add(authSectionModel);

                }

                SystemAuthModel.RoleID = "";

                SystemAuthModel.RoleNameEN = "";

                SystemAuthModel.RoleNameAR = "";

                SystemAuthModel.AuthorizationSections = SystemSectionAuthorizations;

                result.Data = SystemAuthModel;

                result.Succeeded = true;

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


