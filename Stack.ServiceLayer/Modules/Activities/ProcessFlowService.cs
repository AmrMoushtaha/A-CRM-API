
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

namespace Stack.ServiceLayer.Modules.Activities
{
    public class ProcessFlowService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private static readonly HttpClient client = new HttpClient();

        public ProcessFlowService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;

        }

        public async Task<ApiResponse<bool>> CreateProcessFlow(CreateProcessFlowModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                ProcessFlow newProcessFlow = new ProcessFlow();

                newProcessFlow.CustomerID = model.customerID;

                newProcessFlow.DealID = model.DealID;

                newProcessFlow.IsComplete = false; // Default value . 

                var createProcessFlowResult = await unitOfWork.ProcessFlowsManager.CreateAsync(newProcessFlow);

                await unitOfWork.SaveChangesAsync();


                if(createProcessFlowResult != null)
                {

                    result.Succeeded = true;

                    result.Data = true;

                    return result;

                }
                else
                {

                    result.Succeeded = false;

                    result.Data  = false;

                    result.ErrorType = ErrorType.SystemError;

                    result.Errors.Add("Failed to create a new process flow !");

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


