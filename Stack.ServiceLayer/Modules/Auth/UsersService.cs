
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
                                new Claim("Email", user.Email),
                                new Claim(ClaimTypes.NameIdentifier, user.Id)
                        });

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
                    Gender = model.Gender,
                    NameAR = model.NameAR,
                    NameEN = model.NameEN,
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


    }

}


