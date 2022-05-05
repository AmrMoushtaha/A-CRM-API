
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
using Newtonsoft.Json;

namespace Stack.ServiceLayer.Modules.Auth
{
    public class UsersService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private static readonly HttpClient client = new HttpClient();

        public UsersService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;

        }

        //Login function that returns a JWT Bearer Token . 
        public async Task<ApiResponse<JwtAccessToken>> LoginAsync(LoginModel model)
        {
            ApiResponse<JwtAccessToken> result = new ApiResponse<JwtAccessToken>();
            try
            {

                //Find user by email . 
                var user = await unitOfWork.UserManager.FindByNameAsync(model.UserName);

                if (user != null)
                {

                    //Check user password . 
                    bool res = await unitOfWork.UserManager.CheckPasswordAsync(user, model.Password);

                    if (res)
                    {

                        // Creating JWT Bearer Token . 
                        ClaimsIdentity claims = new ClaimsIdentity(new[]
                        {
                                new Claim(ClaimTypes.Name, user.UserName),
                                new Claim(ClaimTypes.NameIdentifier, user.Id)


                        });


                        //claims.AddClaim(new Claim("AuthModel", user.SystemAuthorizations));

                        IList<string> userRoles = await unitOfWork.UserManager.GetRolesAsync(user);

                        if (userRoles != null && userRoles.Count() > 0)
                        {
                            foreach (string role in userRoles)
                            {
                                claims.AddClaim(new Claim(ClaimTypes.Role, role));
                            }
                        }

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Token:Key").Value));

                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                        var egyptsDateResult = await HelperFunctions.GetEgyptsCurrentLocalTime();


                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(claims),
                            NotBefore = egyptsDateResult,
                            Expires = egyptsDateResult.AddHours(8), // Set Token Validity Period. 
                            SigningCredentials = creds,
                            IssuedAt = egyptsDateResult
                        };

                        var tokenHandler = new JwtSecurityTokenHandler();
                        var token = tokenHandler.CreateToken(tokenDescriptor);

                        result.Data = new JwtAccessToken();
                        result.Data.Token = tokenHandler.WriteToken(token);
                        result.Data.Expiration = token.ValidTo;

                        result.Succeeded = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Invalid login attempt.");
                        result.ErrorType = ErrorType.LogicalError;
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Invalid login attempt.");
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

        public async Task<ApiResponse<bool>> CreateDummyUser(CreateDummyUserModel model)
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

                    result.Data = true;
                    result.Succeeded = true;
                    return result;

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

        public async Task<ApiResponse<bool>> CreateNewUser(UserCreationModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                ApplicationUser user = new ApplicationUser
                {

                    Email = model.Email,
                    UserName = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Status = (int)UserStatus.Activated

                };


                var userExists = await unitOfWork.UserManager.FindByNameAsync(model.Username);

                if(userExists == null)
                {

                    user.SystemAuthorizations = JsonConvert.SerializeObject(model.AuthModel);

                    var createUserResult = await unitOfWork.UserManager.CreateAsync(user, model.Password);

                    await unitOfWork.SaveChangesAsync();



                    if (createUserResult.Succeeded)
                    {

                        //Add the user to the selected role .

                        var roleResult = await unitOfWork.RoleManager.FindByIdAsync(model.AuthModel.RoleID);

                        var roleAssignmentRes = await unitOfWork.UserManager.AddToRoleAsync(user, roleResult.Name);

                        await unitOfWork.SaveChangesAsync();

                        result.Data = true;
                        result.Succeeded = true;
                        return result;

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
                else
                {

                    result.Succeeded = false;
                    result.Data = false;
                    result.Errors.Add("Failed to create the new user, A user with similar username already exists !");
                    result.Errors.Add("فشل إنشاء المستخدم الجديد ، يوجد بالفعل مستخدم باسم مستخدم مشابه!");
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


        public async Task<ApiResponse<bool>> UpdateUserDetails(UpdateUserModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

               var userResult = await unitOfWork.UserManager.FindByIdAsync(model.UserID);

               if(userResult != null)
               {

                    if(model.Username != userResult.UserName)
                    {

                        var duplicateUsernameResult = await unitOfWork.UserManager.FindDuplicateUsername(model.UserID, model.Username);

                        if(duplicateUsernameResult == null)
                        {

                            userResult.UserName = model.Username;


                        }
                        else
                        {

                            result.Succeeded = false;
                            result.Data = false;
                            result.Errors.Add("Failed to update user details, a user with a similar username arleady exists !");
                            result.Errors.Add("فشل تحديث تفاصيل المستخدم ، يوجد مستخدم باسم مستخدم مشابه جاهز!");
                            return result;

                        }

                    }

                    var oldAuthModel = JsonConvert.DeserializeObject<AuthorizationsModel>(userResult.SystemAuthorizations);    
                    userResult.FirstName = model.FirstName;
                    userResult.LastName = model.LastName;
                    userResult.Email = model.Email;
                    userResult.PhoneNumber = model.PhoneNumber;
                    userResult.SystemAuthorizations = JsonConvert.SerializeObject(model.AuthModel);


                    //if the user role has been updated . 
                    if (oldAuthModel.RoleID != model.AuthModel.RoleID)
                    {

                        //Unassign the user from his old role . 

                        var removeFromRoleResult = await unitOfWork.UserManager.RemoveFromRoleAsync(userResult, model.AuthModel.RoleNameEN);

                        //Add the user to the selected role .

                        var roleResult = await unitOfWork.RoleManager.FindByIdAsync(model.AuthModel.RoleID);

                        var roleAssignmentRes = await unitOfWork.UserManager.AddToRoleAsync(userResult, roleResult.Name);

                    }

                    var updateUserResult = await unitOfWork.UserManager.UpdateAsync(userResult);

                    await unitOfWork.SaveChangesAsync();

                    if(updateUserResult.Succeeded == true)
                    {

                        result.Succeeded = true;
                        result.Data = true;
                        return result;

                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Data = false;
                        result.Errors.Add("Failed to update user details, Please try again !");
                        result.Errors.Add("فشل تحديث تفاصيل المستخدم ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }


               }
               else
               {
                    result.Succeeded = false;
                    result.Data = false;
                    result.Errors.Add("Failed to update user details, Please try again !");
                    result.Errors.Add("فشل تحديث تفاصيل المستخدم ، يرجى المحاولة مرة أخرى!");
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


        public async Task<ApiResponse<bool>> UpdateUserPassword(UpdatePasswordModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var userResult = await unitOfWork.UserManager.FindByIdAsync(model.UserID);

                if (userResult != null)
                {

                    userResult.PasswordHash = unitOfWork.UserManager.PasswordHasher.HashPassword(userResult, model.Password);

                    var updateResult = await unitOfWork.UserManager.UpdateAsync(userResult);

                    if(updateResult.Succeeded == true)
                    {

                        result.Succeeded = true;
                        result.Data = true;
                        return result;

                    }
                    else
                    {

                        result.Succeeded = false;
                        result.Data = false;
                        result.Errors.Add("Failed to update user password, Please try again !");
                        result.Errors.Add("فشل تحديث كلمة مرور المستخدم ، يرجى المحاولة مرة أخرى!");
                        return result;

                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Data = false;
                    result.Errors.Add("Failed to update user password, Please try again !");
                    result.Errors.Add("فشل تحديث كلمة مرور المستخدم ، يرجى المحاولة مرة أخرى!");
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

        /// <summary>
        /// Fetch the list of system users . 
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse<List<ApplicationUserDTO>>> GetAllSystemUsers()
        {
            ApiResponse<List<ApplicationUserDTO>> result = new ApiResponse<List<ApplicationUserDTO>>();
            try
            {

                
                
                List<ApplicationUser> usersList = await unitOfWork.UserManager.GetAllSystemUsers();


                 var  usersToReturn = mapper.Map<List<ApplicationUserDTO>>(usersList);

                //usersToReturn = usersToReturn.FindAll(a => a.AuthModel.RoleNameEN != "Administrator");


                result.Data = usersToReturn;

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


        //Get user details via http context accessor
        public async Task<ApiResponse<ApplicationUserDTO>> GetUserDetails()
        {
            ApiResponse<ApplicationUserDTO> result = new ApiResponse<ApplicationUserDTO>();
            try
            {

                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    var userAccount = await unitOfWork.UserManager.GetUserById(userID);
                    if (userAccount != null)
                    {
                        result.Data = mapper.Map<ApplicationUserDTO>(userAccount);
                        result.Succeeded = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to get user account details, Please try again !");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to get user account details, Please try again !");
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

        //public async Task<ApiResponse<List<ApplicationUserDTO>>> GetAllSystemEmployees()
        //{
        //    ApiResponse<List<ApplicationUserDTO>> result = new ApiResponse<List<ApplicationUserDTO>>();
        //    try
        //    {
        //        var usersQ = await unitOfWork.UserManager.GetUsersInRoleAsync("Employee");
        //        var users = usersQ.ToList();

        //        if (users != null && users.Count > 0)
        //        {

        //            result.Data = mapper.Map<List<ApplicationUserDTO>>(users);
        //            result.Succeeded = true;
        //            return result;

        //        }
        //        else
        //        {
        //            result.Succeeded = false;
        //            result.Errors.Add("No users found");
        //            result.ErrorType = ErrorType.NotFound;
        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Succeeded = false;
        //        result.Errors.Add(ex.Message);
        //        result.ErrorType = ErrorType.SystemError;
        //        return result;
        //    }

        //}




        //public async Task<ApiResponse<bool>> SeedDB()
        //{
        //    ApiResponse<bool> result = new ApiResponse<bool>();
        //    try
        //    {

        //        ApplicationUser userCreationModel = new ApplicationUser
        //        {
        //            UserName = "User",
        //        };

        //        for (int i = 1; i < 1001; i++)
        //        {
        //            var model = new ApplicationUser
        //            {
        //                UserName = userCreationModel.UserName + i.ToString(),
        //            };

        //            var createUserResult = await unitOfWork.UserManager.CreateAsync(model, "P@ssw0rd");

        //            await unitOfWork.SaveChangesAsync();

        //            if (!createUserResult.Succeeded)
        //            {

        //                result.Errors.Add("Fail");

        //            }
        //        }

        //        result.Succeeded = true;
        //        result.Data = true;
        //        return result;

        //    }
        //    catch (Exception ex)
        //    {
        //        result.Succeeded = false;
        //        result.Errors.Add(ex.Message);
        //        result.ErrorType = ErrorType.SystemError;
        //        return result;
        //    }

        //}

    }

}


