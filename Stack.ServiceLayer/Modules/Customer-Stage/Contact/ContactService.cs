
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
using Stack.DTOs.Models.Modules.CustomerStage;

namespace Stack.ServiceLayer.Modules.CustomerStage
{
    public class ContactService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private static readonly HttpClient client = new HttpClient();

        public ContactService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;

        }



        public async Task<ApiResponse<ContactViewModel>> GetContact(long id)
        {
            ApiResponse<ContactViewModel> result = new ApiResponse<ContactViewModel>();
            try
            {
                var modelQuery = await unitOfWork.ContactManager.GetContactDetails(id);
                if (modelQuery != null)
                {
                    result.Succeeded = true;
                    result.Data = modelQuery;
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.ErrorCode = ErrorCode.A500;
                    result.Errors.Add("Contact not found");
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


