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
using Stack.Entities.Enums.Modules.CustomerStage;
using Stack.DTOs.Requests.Modules.CustomerStage;
using Stack.Entities.Enums.Modules.Pool;

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

                //StageModel doneDealStage = new StageModel();


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

                //doneDealStage.StageNameEN = "Done-Deal";
                //opportunityStage.StageNameAR = "صفقة منتهية";
                //opportunityStage.Statuses = new List<StatusModel>();




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

                //result.Data.Stages.Add(doneDealStage);


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


        #region Create Single Record

        //Assign to other users
        public async Task<ApiResponse<RecordCreationResponse>> CreateSingleStageRecord_AssignToUser(RecordCreationModel creationModel)
        {
            ApiResponse<RecordCreationResponse> result = new ApiResponse<RecordCreationResponse>();
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (userID != null)
                {

                    //Verify record creator's ID
                    var user = await unitOfWork.UserManager.GetUserById(userID);
                    if (user != null)
                    {
                        //Get Designated Pool for assigned user
                        var poolQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == creationModel.PoolID && t.UserID == creationModel.AssigneeID);
                        var poolUser = poolQuery.FirstOrDefault();

                        if (poolUser != null)
                        {
                            Pool pool = poolUser.Pool;

                            //Verify record duplication
                            var recordDuplicationCheckQ = await unitOfWork.ContactManager.GetAsync(t => t.PrimaryPhoneNumber == creationModel.PrimaryPhoneNumber);
                            var recordDuplicationCheck = recordDuplicationCheckQ.FirstOrDefault();

                            //Phone number not duplicated
                            if (recordDuplicationCheck == null)
                            {
                                //Verify pool configuration for capacity checks

                                //Capacity configuration found for designated pool and selected stage is a lead/opportunity
                                if (pool.ConfigurationType == (int)PoolConfigurationTypes.Capacity ||
                                    pool.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignmentCapacity
                                    && (creationModel.RecordType == (int)CustomerStageIndicator.Lead || creationModel.RecordType == (int)CustomerStageIndicator.Opportunity))
                                {
                                    //Get assignee's pool records and verify with current user's capacity
                                    var assignedContactsQuery = await unitOfWork.ContactManager.GetAsync(t => t.AssignedUserID == creationModel.AssigneeID && t.PoolID == pool.ID && t.IsFinalized == false
                                     && t.CapacityCalculated == true);
                                    var assignedContacts = assignedContactsQuery.Count();

                                    //Full capacity
                                    if (assignedContacts == poolUser.Capacity)
                                    {
                                        //Verify whether creator is a pool admin
                                        var poolAdminQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == creationModel.PoolID && t.UserID == userID && t.IsAdmin == true);
                                        var poolAdmin = poolAdminQuery.FirstOrDefault();
                                        if (poolAdmin != null || poolAdmin == null && poolUser.IsAdmin == false) //Creator is pool admin
                                        {
                                            //Increase user capacity and assign record to user
                                            poolUser.Capacity += 1;

                                            var userCapacityUpdate = await unitOfWork.PoolUserManager.UpdateAsync(poolUser);

                                            //Create record
                                            var creationResult = await CreateSingleRecord(creationModel, poolUser);

                                            return creationResult;

                                        }
                                        else //Creator has no pool priviliges, return with capacity increase request pop up
                                        {
                                            result.Succeeded = false;
                                            result.ErrorType = ErrorType.IncreaseCapacity;
                                            return result;
                                        }
                                    }
                                    //Available slots found, create and assign record immediatly
                                    else
                                    {
                                        var creationResult = await CreateSingleRecord(creationModel, poolUser);

                                        return creationResult;

                                    }
                                }
                                //Normal pool configuration (No capacity config. found) / Selected record does not opt for capacity
                                else
                                {
                                    //Create and assign record immediatly
                                    var creationResult = await CreateSingleRecord(creationModel, poolUser);
                                    return creationResult;
                                }
                            }
                            //Duplicate Contact found
                            else
                            {
                                //Verify record relation with it's corresponding pool (user and admin)
                                var record_PoolUserQ = await unitOfWork.PoolUserManager.GetAsync(t => t.UserID == creationModel.AssigneeID
                                && t.PoolID == recordDuplicationCheck.PoolID, includeProperties: "Pool");
                                var record_PoolUser = record_PoolUserQ.FirstOrDefault();
                                //User is in the same pool as the record
                                if (record_PoolUser != null)
                                {
                                    //Verify that the record is not currently assigned under this pool's admin

                                    //Contact is assigned
                                    if (recordDuplicationCheck.AssignedUserID != null)
                                    {
                                        var assignee_AdminCheckQ = await unitOfWork.PoolUserManager.GetAsync(t => t.UserID == recordDuplicationCheck.AssignedUserID
                                        && t.PoolID == record_PoolUser.PoolID
                                        && t.IsAdmin == true);
                                        var assignee_Admin = assignee_AdminCheckQ.FirstOrDefault();

                                        //Record is assigned to a pool admin, return with request transfer popup
                                        if (assignee_Admin != null)
                                        {
                                            result.Succeeded = false;
                                            result.ErrorType = ErrorType.Record_DifferentPool;
                                            return result;
                                        }
                                        //Record is not assigned to an admin
                                        else
                                        {
                                            //Verify whether record is only a contact or has on-going deals

                                            //Contact has deals
                                            if (recordDuplicationCheck.CustomerID != null)
                                            {
                                                //Return with request transfer popup
                                                result.Succeeded = false;
                                                result.ErrorType = ErrorType.Record_DifferentPool;
                                                return result;
                                            }
                                            //Contact only
                                            else
                                            {
                                                //Verify creator's relation with such pool
                                                var poolAdminQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == recordDuplicationCheck.PoolID
                                                && t.UserID == userID && t.IsAdmin == true);
                                                var poolAdmin = poolAdminQuery.FirstOrDefault();

                                                //One of the users (creator / agent) is a pool admin
                                                if (poolAdmin != null || record_PoolUser.IsAdmin == true)
                                                {
                                                    //Assign record to user
                                                    recordDuplicationCheck.AssignedUserID = creationModel.AssigneeID;

                                                    if (creationModel.RecordType == (int)CustomerStageIndicator.Lead || creationModel.RecordType == (int)CustomerStageIndicator.Opportunity
                                                       && (record_PoolUser.Pool.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignmentCapacity
                                                       || record_PoolUser.Pool.ConfigurationType == (int)PoolConfigurationTypes.Capacity))
                                                    {
                                                        recordDuplicationCheck.CapacityCalculated = true;

                                                        //Calculate user's capacity
                                                        var assignedPoolRecordsQuery = await unitOfWork.ContactManager.GetAsync(t => t.PoolID == pool.ID && t.AssignedUserID == creationModel.AssigneeID
                                                         && t.IsFinalized == false && t.CapacityCalculated == true);
                                                        var assignedPoolRecords = assignedPoolRecordsQuery.Count();

                                                        if (assignedPoolRecords == record_PoolUser.Capacity)
                                                        {
                                                            record_PoolUser.Capacity += 1;

                                                            //Update user's capacity for such pool

                                                            var updatePoolCapacityRes = await unitOfWork.PoolUserManager.UpdateAsync(record_PoolUser);
                                                        }

                                                    }
                                                    //Update current contact

                                                    var updateRes = await unitOfWork.ContactManager.UpdateAsync(recordDuplicationCheck);

                                                    if (updateRes)
                                                    {
                                                        //Create customer
                                                        Customer customer = new Customer
                                                        {
                                                            FullNameEN = creationModel.FullNameEN,
                                                            FullNameAR = creationModel.FullNameAR,
                                                            Address = creationModel.Address,
                                                            AssignedUserID = creationModel.AssigneeID,
                                                            Email = creationModel.Email,
                                                            PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
                                                            Occupation = creationModel.Occupation,
                                                            ContactID = recordDuplicationCheck.ID,
                                                            LeadSourceName = recordDuplicationCheck.LeadSourceName,
                                                            LeadSourceType = recordDuplicationCheck.LeadSourceType,
                                                        };

                                                        var customerCreationRes = await unitOfWork.CustomerManager.CreateAsync(customer);

                                                        if (customerCreationRes != null)
                                                        {
                                                            await unitOfWork.SaveChangesAsync();

                                                            //Create deal

                                                            Deal deal = new Deal
                                                            {
                                                                CustomerID = customerCreationRes.ID
                                                            };

                                                            var dealCreationRes = await unitOfWork.DealManager.CreateAsync(deal);

                                                            if (dealCreationRes != null)
                                                            {
                                                                await unitOfWork.SaveChangesAsync();

                                                                //Create related record
                                                                if (creationModel.RecordType == (int)CustomerStageIndicator.Lead)
                                                                {
                                                                    Lead record = new Lead
                                                                    {
                                                                        AssignedUserID = creationModel.AssigneeID,
                                                                        DealID = dealCreationRes.ID,
                                                                        IsFresh = true,
                                                                        State = (int)CustomerStageState.Initial,
                                                                        StatusID = creationModel.StatusID,
                                                                    };

                                                                    var recordCreationRes = await unitOfWork.LeadManager.CreateAsync(record);
                                                                    if (recordCreationRes != null)
                                                                    {
                                                                        await unitOfWork.SaveChangesAsync();
                                                                        RecordCreationResponse responseModel = new RecordCreationResponse
                                                                        {
                                                                            CapacityIncreaseCount = poolUser.Capacity
                                                                        };
                                                                        result.Succeeded = true;
                                                                        result.Data = responseModel;
                                                                        return result;
                                                                    }
                                                                    else
                                                                    {
                                                                        result.Succeeded = false;
                                                                        result.Errors.Add("Error creating record, please try again");
                                                                        result.Errors.Add("Error creating record, please try again");
                                                                        return result;
                                                                    }
                                                                }
                                                                else if (creationModel.RecordType == (int)CustomerStageIndicator.Prospect)
                                                                {
                                                                    Prospect record = new Prospect
                                                                    {
                                                                        AssignedUserID = creationModel.AssigneeID,
                                                                        DealID = dealCreationRes.ID,
                                                                        IsFresh = true,
                                                                        State = (int)CustomerStageState.Initial,
                                                                        StatusID = creationModel.StatusID,
                                                                    };
                                                                    var recordCreationRes = await unitOfWork.ProspectManager.CreateAsync(record);
                                                                    if (recordCreationRes != null)
                                                                    {
                                                                        await unitOfWork.SaveChangesAsync();
                                                                        RecordCreationResponse responseModel = new RecordCreationResponse
                                                                        {
                                                                            CapacityIncreaseCount = record_PoolUser.Capacity
                                                                        };
                                                                        result.Succeeded = true;
                                                                        result.Data = responseModel;
                                                                        return result;
                                                                    }
                                                                    else
                                                                    {
                                                                        result.Succeeded = false;
                                                                        result.Errors.Add("Error creating record, please try again");
                                                                        result.Errors.Add("Error creating record, please try again");
                                                                        return result;
                                                                    }
                                                                }
                                                                else if (creationModel.RecordType == (int)CustomerStageIndicator.Opportunity)
                                                                {
                                                                    Opportunity record = new Opportunity
                                                                    {
                                                                        AssignedUserID = creationModel.AssigneeID,
                                                                        DealID = dealCreationRes.ID,
                                                                        IsFresh = true,
                                                                        State = (int)CustomerStageState.Initial,
                                                                        StatusID = creationModel.StatusID,
                                                                    };
                                                                    var recordCreationRes = await unitOfWork.OpportunityManager.CreateAsync(record);
                                                                    if (recordCreationRes != null)
                                                                    {
                                                                        await unitOfWork.SaveChangesAsync();
                                                                        RecordCreationResponse responseModel = new RecordCreationResponse
                                                                        {
                                                                            CapacityIncreaseCount = record_PoolUser.Capacity
                                                                        };
                                                                        result.Succeeded = true;
                                                                        result.Data = responseModel;
                                                                        return result;
                                                                    }
                                                                    else
                                                                    {
                                                                        result.Succeeded = false;
                                                                        result.Errors.Add("Error creating record, please try again");
                                                                        result.Errors.Add("Error creating record, please try again");
                                                                        return result;
                                                                    }
                                                                }
                                                                else if (creationModel.RecordType == (int)CustomerStageIndicator.DoneDeal)
                                                                {
                                                                    DoneDeal record = new DoneDeal
                                                                    {
                                                                        AssignedUserID = creationModel.AssigneeID,
                                                                        DealID = dealCreationRes.ID,
                                                                        State = (int)CustomerStageState.Initial,

                                                                    };
                                                                    var recordCreationRes = await unitOfWork.DoneDealManager.CreateAsync(record);
                                                                    if (recordCreationRes != null)
                                                                    {
                                                                        recordDuplicationCheck.IsFinalized = true;
                                                                        var finalizeContactRes = await unitOfWork.ContactManager.UpdateAsync(recordDuplicationCheck);

                                                                        await unitOfWork.SaveChangesAsync();
                                                                        RecordCreationResponse responseModel = new RecordCreationResponse
                                                                        {
                                                                            CapacityIncreaseCount = record_PoolUser.Capacity
                                                                        };

                                                                        result.Succeeded = true;
                                                                        result.Data = responseModel;
                                                                        return result;
                                                                    }
                                                                    else
                                                                    {
                                                                        result.Succeeded = false;
                                                                        result.Errors.Add("Error creating record, please try again");
                                                                        result.Errors.Add("Error creating record, please try again");
                                                                        return result;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    result.Succeeded = false;
                                                                    result.Errors.Add("Invalid Customer Stage");
                                                                    result.Errors.Add("Invalid Customer Stage");
                                                                    return result;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                result.Succeeded = false;
                                                                result.Errors.Add("Error creating record, please try again");
                                                                result.Errors.Add("Error creating record, please try again");
                                                                return result;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            result.Succeeded = false;
                                                            result.Errors.Add("Error creating record, please try again");
                                                            result.Errors.Add("Error creating record, please try again");
                                                            return result;
                                                        }

                                                    }
                                                    else
                                                    {
                                                        result.Succeeded = false;
                                                        result.Errors.Add("Error assigning record to user");
                                                        result.Errors.Add("Error assigning record to user");
                                                        return result;
                                                    }
                                                }
                                                else
                                                //Request transfer popup
                                                {
                                                    result.Succeeded = false;
                                                    result.ErrorType = ErrorType.Record_DifferentPool;
                                                    return result;
                                                }
                                            }
                                        }
                                    }
                                    //Contact is not assigned
                                    else
                                    {
                                        //Update contact assignment and add new deal 
                                        if (recordDuplicationCheck.CustomerID != null)
                                        {
                                            recordDuplicationCheck.AssignedUserID = creationModel.AssigneeID;
                                            recordDuplicationCheck.Customer.AssignedUserID = creationModel.AssigneeID;

                                            //Iterate all customer deals and overtake their assignee ID
                                            for (int i = 0; i < recordDuplicationCheck.Customer.Deals.Count; i++)
                                            {
                                                var currentDeal = recordDuplicationCheck.Customer.Deals[i];

                                                //get active stage for deal
                                                if (currentDeal.ActiveStageType == (int)CustomerStageIndicator.Prospect)
                                                {
                                                    var activeStageQ = await unitOfWork.ProspectManager.GetAsync(t => t.ID == currentDeal.ActiveStageID);
                                                    var activeStage = activeStageQ.FirstOrDefault();

                                                    if (activeStage != null)
                                                    {
                                                        activeStage.AssignedUserID = creationModel.AssigneeID;

                                                        await unitOfWork.ProspectManager.UpdateAsync(activeStage);
                                                    }
                                                }
                                                else if (currentDeal.ActiveStageType == (int)CustomerStageIndicator.Lead)
                                                {
                                                    var activeStageQ = await unitOfWork.LeadManager.GetAsync(t => t.ID == currentDeal.ActiveStageID);
                                                    var activeStage = activeStageQ.FirstOrDefault();

                                                    if (activeStage != null)
                                                    {
                                                        activeStage.AssignedUserID = creationModel.AssigneeID;

                                                        await unitOfWork.LeadManager.UpdateAsync(activeStage);
                                                    }
                                                }
                                                else if (currentDeal.ActiveStageType == (int)CustomerStageIndicator.Opportunity)
                                                {
                                                    var activeStageQ = await unitOfWork.OpportunityManager.GetAsync(t => t.ID == currentDeal.ActiveStageID);
                                                    var activeStage = activeStageQ.FirstOrDefault();

                                                    if (activeStage != null)
                                                    {
                                                        activeStage.AssignedUserID = creationModel.AssigneeID;

                                                        await unitOfWork.OpportunityManager.UpdateAsync(activeStage);
                                                    }
                                                }
                                                //else if (deal.activeStageType == (int)CustomerStageIndicator.DoneDeal)
                                                //{
                                                //    var activeStageQ = await unitOfWork.DoneDealManager.GetAsync(t => t.ID == deal.activeStageID);
                                                //    var activeStage = activeStageQ.FirstOrDefault();

                                                //    if (activeStage != null)
                                                //    {
                                                //        activeStage.AssignedUserID = creationModel.AssigneeID;

                                                //        await unitOfWork.DoneDealManager.UpdateAsync(activeStage);
                                                //    }
                                                //}
                                            }

                                            //Create new deal flow for such user
                                            //Create deal

                                            Deal deal = new Deal
                                            {
                                                CustomerID = recordDuplicationCheck.CustomerID.Value
                                            };

                                            var dealCreationRes = await unitOfWork.DealManager.CreateAsync(deal);

                                            if (dealCreationRes != null)
                                            {
                                                await unitOfWork.SaveChangesAsync();

                                                //Create related record
                                                if (creationModel.RecordType == (int)CustomerStageIndicator.Lead)
                                                {
                                                    Lead record = new Lead
                                                    {
                                                        AssignedUserID = creationModel.AssigneeID,
                                                        DealID = dealCreationRes.ID,
                                                        IsFresh = true,
                                                        State = (int)CustomerStageState.Initial,
                                                        StatusID = creationModel.StatusID,
                                                    };


                                                    var recordCreationRes = await unitOfWork.LeadManager.CreateAsync(record);
                                                    if (recordCreationRes != null)
                                                    {
                                                        await unitOfWork.SaveChangesAsync();
                                                        RecordCreationResponse responseModel = new RecordCreationResponse
                                                        {
                                                            CapacityIncreaseCount = poolUser.Capacity
                                                        };
                                                        result.Succeeded = true;
                                                        result.Data = responseModel;
                                                        return result;
                                                    }
                                                    else
                                                    {
                                                        result.Succeeded = false;
                                                        result.Errors.Add("Error creating record, please try again");
                                                        result.Errors.Add("Error creating record, please try again");
                                                        return result;
                                                    }
                                                }
                                                else if (creationModel.RecordType == (int)CustomerStageIndicator.Prospect)
                                                {
                                                    Prospect record = new Prospect
                                                    {
                                                        AssignedUserID = creationModel.AssigneeID,
                                                        DealID = dealCreationRes.ID,
                                                        IsFresh = true,
                                                        State = (int)CustomerStageState.Initial,
                                                        StatusID = creationModel.StatusID,
                                                    };

                                                    var recordCreationRes = await unitOfWork.ProspectManager.CreateAsync(record);
                                                    if (recordCreationRes != null)
                                                    {
                                                        await unitOfWork.SaveChangesAsync();
                                                        RecordCreationResponse responseModel = new RecordCreationResponse
                                                        {
                                                            CapacityIncreaseCount = record_PoolUser.Capacity
                                                        };
                                                        result.Succeeded = true;
                                                        result.Data = responseModel;
                                                        return result;
                                                    }
                                                    else
                                                    {
                                                        result.Succeeded = false;
                                                        result.Errors.Add("Error creating record, please try again");
                                                        result.Errors.Add("Error creating record, please try again");
                                                        return result;
                                                    }
                                                }
                                                else if (creationModel.RecordType == (int)CustomerStageIndicator.Opportunity)
                                                {
                                                    Opportunity record = new Opportunity
                                                    {
                                                        AssignedUserID = creationModel.AssigneeID,
                                                        DealID = dealCreationRes.ID,
                                                        IsFresh = true,
                                                        State = (int)CustomerStageState.Initial,
                                                        StatusID = creationModel.StatusID,
                                                    };
                                                    var recordCreationRes = await unitOfWork.OpportunityManager.CreateAsync(record);
                                                    if (recordCreationRes != null)
                                                    {
                                                        await unitOfWork.SaveChangesAsync();
                                                        RecordCreationResponse responseModel = new RecordCreationResponse
                                                        {
                                                            CapacityIncreaseCount = record_PoolUser.Capacity
                                                        };
                                                        result.Succeeded = true;
                                                        result.Data = responseModel;
                                                        return result;
                                                    }
                                                    else
                                                    {
                                                        result.Succeeded = false;
                                                        result.Errors.Add("Error creating record, please try again");
                                                        result.Errors.Add("Error creating record, please try again");
                                                        return result;
                                                    }
                                                }
                                                else if (creationModel.RecordType == (int)CustomerStageIndicator.DoneDeal)
                                                {
                                                    DoneDeal record = new DoneDeal
                                                    {
                                                        AssignedUserID = creationModel.AssigneeID,
                                                        DealID = dealCreationRes.ID,
                                                        State = (int)CustomerStageState.Initial,

                                                    };
                                                    var recordCreationRes = await unitOfWork.DoneDealManager.CreateAsync(record);
                                                    if (recordCreationRes != null)
                                                    {
                                                        recordDuplicationCheck.IsFinalized = true;
                                                        var finalizeContactRes = await unitOfWork.ContactManager.UpdateAsync(recordDuplicationCheck);

                                                        await unitOfWork.SaveChangesAsync();
                                                        RecordCreationResponse responseModel = new RecordCreationResponse
                                                        {
                                                            CapacityIncreaseCount = record_PoolUser.Capacity
                                                        };

                                                        result.Succeeded = true;
                                                        result.Data = responseModel;
                                                        return result;
                                                    }
                                                    else
                                                    {
                                                        result.Succeeded = false;
                                                        result.Errors.Add("Error creating record, please try again");
                                                        result.Errors.Add("Error creating record, please try again");
                                                        return result;
                                                    }
                                                }
                                                else
                                                {
                                                    result.Succeeded = false;
                                                    result.Errors.Add("Invalid Customer Stage");
                                                    result.Errors.Add("Invalid Customer Stage");
                                                    return result;
                                                }
                                            }
                                            else
                                            {
                                                result.Succeeded = false;
                                                result.Errors.Add("Error creating record, please try again");
                                                result.Errors.Add("Error creating record, please try again");
                                                return result;
                                            }
                                        }
                                        //Contact only
                                        else
                                        {
                                            //create deal flow for such contact
                                            //Create customer
                                            Customer customer = new Customer
                                            {
                                                FullNameEN = creationModel.FullNameEN,
                                                FullNameAR = creationModel.FullNameAR,
                                                Address = creationModel.Address,
                                                AssignedUserID = creationModel.AssigneeID,
                                                Email = creationModel.Email,
                                                PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
                                                Occupation = creationModel.Occupation,
                                                ContactID = recordDuplicationCheck.ID,
                                                LeadSourceName = recordDuplicationCheck.LeadSourceName,
                                                LeadSourceType = recordDuplicationCheck.LeadSourceType,
                                            };

                                            var customerCreationRes = await unitOfWork.CustomerManager.CreateAsync(customer);

                                            if (customerCreationRes != null)
                                            {
                                                await unitOfWork.SaveChangesAsync();

                                                //Create deal

                                                Deal deal = new Deal
                                                {
                                                    CustomerID = customerCreationRes.ID
                                                };

                                                var dealCreationRes = await unitOfWork.DealManager.CreateAsync(deal);

                                                if (dealCreationRes != null)
                                                {
                                                    await unitOfWork.SaveChangesAsync();

                                                    //Create related record
                                                    if (creationModel.RecordType == (int)CustomerStageIndicator.Lead)
                                                    {
                                                        Lead record = new Lead
                                                        {
                                                            AssignedUserID = creationModel.AssigneeID,
                                                            DealID = dealCreationRes.ID,
                                                            IsFresh = true,
                                                            State = (int)CustomerStageState.Initial,
                                                            StatusID = creationModel.StatusID,
                                                        };

                                                        var recordCreationRes = await unitOfWork.LeadManager.CreateAsync(record);
                                                        if (recordCreationRes != null)
                                                        {
                                                            await unitOfWork.SaveChangesAsync();
                                                            RecordCreationResponse responseModel = new RecordCreationResponse
                                                            {
                                                                CapacityIncreaseCount = poolUser.Capacity
                                                            };
                                                            result.Succeeded = true;
                                                            result.Data = responseModel;
                                                            return result;
                                                        }
                                                        else
                                                        {
                                                            result.Succeeded = false;
                                                            result.Errors.Add("Error creating record, please try again");
                                                            result.Errors.Add("Error creating record, please try again");
                                                            return result;
                                                        }
                                                    }
                                                    else if (creationModel.RecordType == (int)CustomerStageIndicator.Prospect)
                                                    {
                                                        Prospect record = new Prospect
                                                        {
                                                            AssignedUserID = creationModel.AssigneeID,
                                                            DealID = dealCreationRes.ID,
                                                            IsFresh = true,
                                                            State = (int)CustomerStageState.Initial,
                                                            StatusID = creationModel.StatusID,
                                                        };
                                                        var recordCreationRes = await unitOfWork.ProspectManager.CreateAsync(record);
                                                        if (recordCreationRes != null)
                                                        {
                                                            await unitOfWork.SaveChangesAsync();
                                                            RecordCreationResponse responseModel = new RecordCreationResponse
                                                            {
                                                                CapacityIncreaseCount = record_PoolUser.Capacity
                                                            };
                                                            result.Succeeded = true;
                                                            result.Data = responseModel;
                                                            return result;
                                                        }
                                                        else
                                                        {
                                                            result.Succeeded = false;
                                                            result.Errors.Add("Error creating record, please try again");
                                                            result.Errors.Add("Error creating record, please try again");
                                                            return result;
                                                        }
                                                    }
                                                    else if (creationModel.RecordType == (int)CustomerStageIndicator.Opportunity)
                                                    {
                                                        Opportunity record = new Opportunity
                                                        {
                                                            AssignedUserID = creationModel.AssigneeID,
                                                            DealID = dealCreationRes.ID,
                                                            IsFresh = true,
                                                            State = (int)CustomerStageState.Initial,
                                                            StatusID = creationModel.StatusID,
                                                        };
                                                        var recordCreationRes = await unitOfWork.OpportunityManager.CreateAsync(record);
                                                        if (recordCreationRes != null)
                                                        {
                                                            await unitOfWork.SaveChangesAsync();
                                                            RecordCreationResponse responseModel = new RecordCreationResponse
                                                            {
                                                                CapacityIncreaseCount = record_PoolUser.Capacity
                                                            };
                                                            result.Succeeded = true;
                                                            result.Data = responseModel;
                                                            return result;
                                                        }
                                                        else
                                                        {
                                                            result.Succeeded = false;
                                                            result.Errors.Add("Error creating record, please try again");
                                                            result.Errors.Add("Error creating record, please try again");
                                                            return result;
                                                        }
                                                    }
                                                    else if (creationModel.RecordType == (int)CustomerStageIndicator.DoneDeal)
                                                    {
                                                        DoneDeal record = new DoneDeal
                                                        {
                                                            AssignedUserID = creationModel.AssigneeID,
                                                            DealID = dealCreationRes.ID,
                                                            State = (int)CustomerStageState.Initial,

                                                        };
                                                        var recordCreationRes = await unitOfWork.DoneDealManager.CreateAsync(record);
                                                        if (recordCreationRes != null)
                                                        {
                                                            recordDuplicationCheck.IsFinalized = true;
                                                            var finalizeContactRes = await unitOfWork.ContactManager.UpdateAsync(recordDuplicationCheck);

                                                            await unitOfWork.SaveChangesAsync();
                                                            RecordCreationResponse responseModel = new RecordCreationResponse
                                                            {
                                                                CapacityIncreaseCount = record_PoolUser.Capacity
                                                            };

                                                            result.Succeeded = true;
                                                            result.Data = responseModel;
                                                            return result;
                                                        }
                                                        else
                                                        {
                                                            result.Succeeded = false;
                                                            result.Errors.Add("Error creating record, please try again");
                                                            result.Errors.Add("Error creating record, please try again");
                                                            return result;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        result.Succeeded = false;
                                                        result.Errors.Add("Invalid Customer Stage");
                                                        result.Errors.Add("Invalid Customer Stage");
                                                        return result;
                                                    }
                                                }
                                                else
                                                {
                                                    result.Succeeded = false;
                                                    result.Errors.Add("Error creating record, please try again");
                                                    result.Errors.Add("Error creating record, please try again");
                                                    return result;
                                                }
                                            }
                                            else
                                            {
                                                result.Succeeded = false;
                                                result.Errors.Add("Error creating record, please try again");
                                                result.Errors.Add("Error creating record, please try again");
                                                return result;
                                            }
                                        }
                                    }


                                }
                                //User is not in the same pool as the record, verify creator's relation with such pool
                                else
                                {
                                    var record_PoolAdminQ = await unitOfWork.PoolUserManager.GetAsync(t => t.UserID == userID && t.PoolID == recordDuplicationCheck.PoolID && t.IsAdmin == true);
                                    var record_PoolAdmin = record_PoolAdminQ.FirstOrDefault();

                                    //Creator is pool admin for record's pool
                                    if (record_PoolAdmin != null)
                                    {
                                        //Verify that the record is not currently assigned under a different pool admin
                                        var assignee_AdminCheckQ = await unitOfWork.PoolUserManager.GetAsync(t => t.UserID == recordDuplicationCheck.AssignedUserID && t.PoolID == recordDuplicationCheck.PoolID && t.IsAdmin == true);
                                        var assignee_Admin = assignee_AdminCheckQ.FirstOrDefault();
                                        //Record is assigned to a pool admin, return with request transfer popup
                                        if (assignee_Admin != null)
                                        {
                                            result.Succeeded = false;
                                            result.ErrorType = ErrorType.Record_DifferentPool;
                                            return result;
                                        }
                                        //Record is not assigned to a pool admin, proceed
                                        else
                                        {
                                            //Verify whether record is only a contact or has on-going deals

                                            //Contact has deals
                                            if (recordDuplicationCheck.CustomerID != null)
                                            {
                                                //Return with request transfer popup
                                                result.Succeeded = false;
                                                result.ErrorType = ErrorType.Record_DifferentPool;
                                                return result;
                                            }
                                            //Contact only
                                            else
                                            {
                                                //Return with transfer popup (transfer record to selected pool / add user to current pool)
                                                result.Succeeded = false;
                                                result.ErrorType = ErrorType.Record_DifferentPool_Admin;
                                                return result;
                                            }
                                        }
                                    }
                                    //Request transfer / dismiss popup
                                    else
                                    {
                                        result.Succeeded = false;
                                        result.ErrorType = ErrorType.Record_DifferentPool;
                                        return result;
                                    }
                                }

                            }
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Pool does not exist");
                            result.Errors.Add("Pool does not exist");
                            return result;
                        }
                    }
                    else // user not found
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Unauthorized");
                        return result;
                    }
                }
                else //Invalid user token
                {
                    result.Succeeded = false;
                    result.ErrorCode = ErrorCode.A500;
                    result.Errors.Add("Unauthorized");
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



        //Create record 
        public async Task<ApiResponse<RecordCreationResponse>> CreateSingleRecord(RecordCreationModel creationModel, Pool_User PoolUser)
        {
            ApiResponse<RecordCreationResponse> result = new ApiResponse<RecordCreationResponse>();
            try
            {
                //Create new contact
                Contact contact = new Contact
                {
                    FullNameEN = creationModel.FullNameEN,
                    FullNameAR = creationModel.FullNameAR,
                    Address = creationModel.Address,
                    AssignedUserID = PoolUser.UserID,
                    Email = creationModel.Email,
                    PoolID = creationModel.PoolID,
                    IsFinalized = false,
                    IsFresh = false,
                    PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
                    Occupation = creationModel.Occupation,
                    State = (int)CustomerStageState.Initial,
                };

                if (creationModel.RecordType == (int)CustomerStageIndicator.Lead || creationModel.RecordType == (int)CustomerStageIndicator.Opportunity)
                {
                    contact.CapacityCalculated = true;
                }

                var contactCreationRes = await unitOfWork.ContactManager.CreateAsync(contact);
                if (contactCreationRes != null)
                {
                    await unitOfWork.SaveChangesAsync();

                    //Create customer
                    Customer customer = new Customer
                    {
                        FullNameEN = creationModel.FullNameEN,
                        FullNameAR = creationModel.FullNameAR,
                        Address = creationModel.Address,
                        AssignedUserID = PoolUser.UserID,
                        Email = creationModel.Email,
                        PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
                        Occupation = creationModel.Occupation,
                        ContactID = contactCreationRes.ID,
                        LeadSourceName = contactCreationRes.LeadSourceName,
                        LeadSourceType = contactCreationRes.LeadSourceType,
                    };

                    var customerCreationRes = await unitOfWork.CustomerManager.CreateAsync(customer);
                    if (customerCreationRes != null)
                    {
                        await unitOfWork.SaveChangesAsync();

                        //Create deal

                        Deal deal = new Deal
                        {
                            CustomerID = customerCreationRes.ID
                        };

                        var dealCreationRes = await unitOfWork.DealManager.CreateAsync(deal);

                        if (dealCreationRes != null)
                        {
                            await unitOfWork.SaveChangesAsync();

                            //Create related record
                            if (creationModel.RecordType == (int)CustomerStageIndicator.Lead)
                            {
                                Lead record = new Lead
                                {
                                    AssignedUserID = creationModel.AssigneeID,
                                    DealID = dealCreationRes.ID,
                                    IsFresh = true,
                                    State = (int)CustomerStageState.Initial,
                                    StatusID = creationModel.StatusID,
                                };

                                var recordCreationRes = await unitOfWork.LeadManager.CreateAsync(record);
                                if (recordCreationRes != null)
                                {
                                    await unitOfWork.SaveChangesAsync();
                                    RecordCreationResponse responseModel = new RecordCreationResponse
                                    {
                                        CapacityIncreaseCount = PoolUser.Capacity
                                    };
                                    result.Succeeded = true;
                                    result.Data = responseModel;
                                    return result;
                                }
                                else
                                {
                                    result.Succeeded = false;
                                    result.Errors.Add("Error creating record, please try again");
                                    result.Errors.Add("Error creating record, please try again");
                                    return result;
                                }
                            }
                            else if (creationModel.RecordType == (int)CustomerStageIndicator.Prospect)
                            {
                                Prospect record = new Prospect
                                {
                                    AssignedUserID = creationModel.AssigneeID,
                                    DealID = dealCreationRes.ID,
                                    IsFresh = true,
                                    State = (int)CustomerStageState.Initial,
                                    StatusID = creationModel.StatusID,
                                };
                                var recordCreationRes = await unitOfWork.ProspectManager.CreateAsync(record);
                                if (recordCreationRes != null)
                                {
                                    await unitOfWork.SaveChangesAsync();
                                    RecordCreationResponse responseModel = new RecordCreationResponse
                                    {
                                        CapacityIncreaseCount = PoolUser.Capacity
                                    };
                                    result.Succeeded = true;
                                    result.Data = responseModel;
                                    return result;
                                }
                                else
                                {
                                    result.Succeeded = false;
                                    result.Errors.Add("Error creating record, please try again");
                                    result.Errors.Add("Error creating record, please try again");
                                    return result;
                                }
                            }
                            else if (creationModel.RecordType == (int)CustomerStageIndicator.Opportunity)
                            {
                                Opportunity record = new Opportunity
                                {
                                    AssignedUserID = creationModel.AssigneeID,
                                    DealID = dealCreationRes.ID,
                                    IsFresh = true,
                                    State = (int)CustomerStageState.Initial,
                                    StatusID = creationModel.StatusID,
                                };
                                var recordCreationRes = await unitOfWork.OpportunityManager.CreateAsync(record);
                                if (recordCreationRes != null)
                                {
                                    await unitOfWork.SaveChangesAsync();
                                    RecordCreationResponse responseModel = new RecordCreationResponse
                                    {
                                        CapacityIncreaseCount = PoolUser.Capacity
                                    };
                                    result.Succeeded = true;
                                    result.Data = responseModel;
                                    return result;
                                }
                                else
                                {
                                    result.Succeeded = false;
                                    result.Errors.Add("Error creating record, please try again");
                                    result.Errors.Add("Error creating record, please try again");
                                    return result;
                                }
                            }
                            else if (creationModel.RecordType == (int)CustomerStageIndicator.DoneDeal)
                            {
                                DoneDeal record = new DoneDeal
                                {
                                    AssignedUserID = creationModel.AssigneeID,
                                    DealID = dealCreationRes.ID,
                                    State = (int)CustomerStageState.Initial,

                                };
                                var recordCreationRes = await unitOfWork.DoneDealManager.CreateAsync(record);
                                if (recordCreationRes != null)
                                {
                                    contact.IsFinalized = true;
                                    var finalizeContactRes = await unitOfWork.ContactManager.UpdateAsync(contactCreationRes);

                                    await unitOfWork.SaveChangesAsync();
                                    RecordCreationResponse responseModel = new RecordCreationResponse
                                    {
                                        CapacityIncreaseCount = PoolUser.Capacity
                                    };

                                    result.Succeeded = true;
                                    result.Data = responseModel;
                                    return result;
                                }
                                else
                                {
                                    result.Succeeded = false;
                                    result.Errors.Add("Error creating record, please try again");
                                    result.Errors.Add("Error creating record, please try again");
                                    return result;
                                }
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("Invalid Customer Stage");
                                result.Errors.Add("Invalid Customer Stage");
                                return result;
                            }
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Error creating record, please try again");
                            result.Errors.Add("Error creating record, please try again");
                            return result;
                        }
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Error creating record, please try again");
                        result.Errors.Add("Error creating record, please try again");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Error creating record, please try again");
                    result.Errors.Add("Error creating record, please try again");
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

        #endregion

    }

}
