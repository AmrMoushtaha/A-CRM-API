
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Stack.DTOs.Requests.Modules.Activities;
using Stack.DTOs;
using System.Threading.Tasks;
using System;
using Stack.DTOs.Enums;
using Stack.Entities.Models.Modules.Activities;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Stack.DTOs.Requests.Modules.Pool;

namespace Stack.ServiceLayer.Modules.Zoom
{
    public class ChatService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
   
        public ChatService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;


        }


    }

}


