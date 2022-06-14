
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
using Stack.DTOs.Requests.Modules.CustomerStage;
using Stack.Entities.Enums.Modules.Pool;
using Stack.Entities.Models.Modules.CustomerStage;
using Stack.Entities.Enums.Modules.CustomerStage;
using Stack.DTOs.Models.Modules.General;

namespace Stack.ServiceLayer.Modules.CustomerStage
{
    public class CustomerService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private static readonly HttpClient client = new HttpClient();

        public CustomerService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;

        }
        public async Task<ApiResponse<List<Customer>>> Get_Customers( )
        {
            ApiResponse<List<Customer>> result = new ApiResponse<List<Customer>>();
            try
            {
                var CustomerResult =( await unitOfWork.CustomerManager.GetAsync()).ToList();
                if ( CustomerResult.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<Customer>>(CustomerResult);
                    return result;
                }

                result.Errors.Add("Failed to find Customers!");
                result.Succeeded = false;
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


