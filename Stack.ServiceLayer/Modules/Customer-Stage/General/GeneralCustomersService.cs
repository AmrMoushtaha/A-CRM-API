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
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using Stack.DTOs.Requests.Modules.Activities;
using Stack.DTOs;
using Stack.Entities.Models.Modules.Activities;
using Stack.DTOs.Enums;
using Stack.DTOs.Models.Modules.Activities;
using Stack.Entities.Enums.Modules.Activities;
using Stack.DTOs.Models.Shared;
using Stack.DTOs.Models.Modules.General;
using Stack.Entities.Models.Modules.CustomerStage;

namespace Stack.ServiceLayer.Modules.CustomerStage
{
    public class GeneralCustomersService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private static readonly HttpClient client = new HttpClient();

        public GeneralCustomersService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;

        }

        public async Task<ApiResponse<StageChangeModel>> GetContactPossibleStages()
        {
            ApiResponse<StageChangeModel> result = new ApiResponse<StageChangeModel>();
            try
            {

                result.Data = new StageChangeModel();

                result.Data.Stages = new List<StageModel>();

                var leadStatusesResult = await unitOfWork.LeadStatusManager.GetAsync(a => a.Status == "Activated");

                var prospectStatusesResult = await unitOfWork.ProspectStatusManager.GetAsync(a => a.Status == "Activated");

                var opportunityStatusesResult = await unitOfWork.OpportunityStatusManager.GetAsync(a => a.Status == "Activated");

                var contactStatusesResult = await unitOfWork.ContactStatusManager.GetAsync(a => a.Status == "Activated");

                List<LeadStatus> leadStatuses = leadStatusesResult.ToList();

                List<ProspectStatus> ProspectStatuses = prospectStatusesResult.ToList();

                List<OpportunityStatus> OpportunityStatuses = opportunityStatusesResult.ToList();

                List<ContactStatus> ContactStatuses = contactStatusesResult.ToList();

                StageModel leadStage = new StageModel();

                StageModel prospectStage = new StageModel();

                StageModel opportunityStage = new StageModel();

                StageModel contactStage = new StageModel();

                StageModel doneDealStage = new StageModel();


                leadStage.StageNameEN = "Lead";
                leadStage.StageNameAR = "عميل محتمل";
                leadStage.Statuses = new List<StatusModel>();

                prospectStage.StageNameEN = "Prospect";
                prospectStage.StageNameAR = "عميل متردد";
                prospectStage.Statuses = new List<StatusModel>();

                opportunityStage.StageNameEN = "Opportunity";
                opportunityStage.StageNameAR = "فرصة مبيعات";
                opportunityStage.Statuses = new List<StatusModel>();


                contactStage.StageNameEN = "Contact";
                contactStage.StageNameAR = "رقم هاتف";
                contactStage.Statuses = new List<StatusModel>();

                doneDealStage.StageNameEN = "Done-Deal";
                opportunityStage.StageNameAR = "صفقة منتهية";
                opportunityStage.Statuses = new List<StatusModel>();




                if (leadStatuses != null && leadStatuses.Count > 0 )
                {

                    for(int i = 0; i < leadStatuses.Count; i++)
                    {

                        StatusModel statusToAdd = new StatusModel();

                        statusToAdd.ID = leadStatuses[i].ID;

                        statusToAdd.EN = leadStatuses[i].EN;

                        statusToAdd.AR = leadStatuses[i].AR;

                        leadStage.Statuses.Add(statusToAdd);

                    }

                }

                if (ProspectStatuses != null && ProspectStatuses.Count > 0)
                {

                    for (int i = 0; i < ProspectStatuses.Count; i++)
                    {

                        StatusModel statusToAdd = new StatusModel();

                        statusToAdd.ID = ProspectStatuses[i].ID;

                        statusToAdd.EN = ProspectStatuses[i].EN;

                        statusToAdd.AR = ProspectStatuses[i].AR;

                        prospectStage.Statuses.Add(statusToAdd);


                    }

                }

                if (OpportunityStatuses != null && OpportunityStatuses.Count > 0)
                {

                    for (int i = 0; i < OpportunityStatuses.Count; i++)
                    {

                        StatusModel statusToAdd = new StatusModel();

                        statusToAdd.ID = OpportunityStatuses[i].ID;

                        statusToAdd.EN = OpportunityStatuses[i].EN;

                        statusToAdd.AR = OpportunityStatuses[i].AR;

                        opportunityStage.Statuses.Add(statusToAdd);


                    }

                }

                if (ContactStatuses != null && ContactStatuses.Count > 0)
                {

                    for (int i = 0; i < ContactStatuses.Count; i++)
                    {

                        StatusModel statusToAdd = new StatusModel();

                        statusToAdd.ID = ContactStatuses[i].ID;

                        statusToAdd.EN = ContactStatuses[i].EN;

                        statusToAdd.AR = ContactStatuses[i].AR;

                        contactStage.Statuses.Add(statusToAdd);


                    }

                }


                result.Data.Stages.Add(leadStage);

                result.Data.Stages.Add(prospectStage);
         
                result.Data.Stages.Add(opportunityStage);

                result.Data.Stages.Add(contactStage);

                result.Data.Stages.Add(doneDealStage);


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

        public async Task<ApiResponse<StageChangeModel>> GetDealPossibleStages()
        {
            ApiResponse<StageChangeModel> result = new ApiResponse<StageChangeModel>();
            try
            {

                result.Data = new StageChangeModel();

                result.Data.Stages = new List<StageModel>();

                var leadStatusesResult = await unitOfWork.LeadStatusManager.GetAsync(a => a.Status == "Activated");

                var prospectStatusesResult = await unitOfWork.ProspectStatusManager.GetAsync(a => a.Status == "Activated");

                var opportunityStatusesResult = await unitOfWork.OpportunityStatusManager.GetAsync(a => a.Status == "Activated");

                List<LeadStatus> leadStatuses = leadStatusesResult.ToList();

                List<ProspectStatus> ProspectStatuses = prospectStatusesResult.ToList();

                List<OpportunityStatus> OpportunityStatuses = opportunityStatusesResult.ToList();

                StageModel leadStage = new StageModel();

                StageModel prospectStage = new StageModel();

                StageModel opportunityStage = new StageModel();

                StageModel doneDealStage = new StageModel();


                leadStage.StageNameEN = "Lead";
                leadStage.StageNameAR = "عميل محتمل";
                leadStage.Statuses = new List<StatusModel>();

                prospectStage.StageNameEN = "Prospect";
                prospectStage.StageNameAR = "عميل متردد";
                prospectStage.Statuses = new List<StatusModel>();

                opportunityStage.StageNameEN = "Opportunity";
                opportunityStage.StageNameAR = "فرصة مبيعات";
                opportunityStage.Statuses = new List<StatusModel>();


                doneDealStage.StageNameEN = "Done-Deal";
                opportunityStage.StageNameAR = "صفقة منتهية";
                opportunityStage.Statuses = new List<StatusModel>();


                if (leadStatuses != null && leadStatuses.Count > 0)
                {

                    for (int i = 0; i < leadStatuses.Count; i++)
                    {

                        StatusModel statusToAdd = new StatusModel();

                        statusToAdd.ID = leadStatuses[i].ID;

                        statusToAdd.EN = leadStatuses[i].EN;

                        statusToAdd.AR = leadStatuses[i].AR;

                        leadStage.Statuses.Add(statusToAdd);

                    }

                }

                if (ProspectStatuses != null && ProspectStatuses.Count > 0)
                {

                    for (int i = 0; i < ProspectStatuses.Count; i++)
                    {

                        StatusModel statusToAdd = new StatusModel();

                        statusToAdd.ID = ProspectStatuses[i].ID;

                        statusToAdd.EN = ProspectStatuses[i].EN;

                        statusToAdd.AR = ProspectStatuses[i].AR;

                        prospectStage.Statuses.Add(statusToAdd);


                    }

                }

                if (OpportunityStatuses != null && OpportunityStatuses.Count > 0)
                {

                    for (int i = 0; i < OpportunityStatuses.Count; i++)
                    {

                        StatusModel statusToAdd = new StatusModel();

                        statusToAdd.ID = OpportunityStatuses[i].ID;

                        statusToAdd.EN = OpportunityStatuses[i].EN;

                        statusToAdd.AR = OpportunityStatuses[i].AR;

                        opportunityStage.Statuses.Add(statusToAdd);


                    }

                }


                result.Data.Stages.Add(leadStage);

                result.Data.Stages.Add(prospectStage);

                result.Data.Stages.Add(opportunityStage);

                result.Data.Stages.Add(doneDealStage);


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
