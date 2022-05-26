
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
using Stack.DTOs.Requests.Modules.CustomerStage;
using Stack.Entities.Enums.Modules.CustomerStage;
using Stack.Entities.Enums.Modules.Auth;
using Stack.Entities.Models.Modules.CustomerStage;
using Stack.DTOs.Models.Modules.Pool;
using Stack.Entities.Enums.Modules.Pool;
using ExcelDataReader;
using System.Data;
using System.IO;
using System.Net;
using System.Web;
using System.Net.Http.Headers;
using Stack.Entities.Models.Modules.CR;
using Stack.DTOs.Models.Modules.CR;

namespace Stack.ServiceLayer.Modules.CR
{
    public class CustomerRequestService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private static readonly HttpClient client = new HttpClient();

        public CustomerRequestService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;

        }


        #region Phase
        public async Task<ApiResponse<List<PhaseViewModel>>> GetAllPhases()
        {
            ApiResponse<List<PhaseViewModel>> result = new ApiResponse<List<PhaseViewModel>>();
            try
            {

                var phasesQ = await unitOfWork.CRPhaseManager.GetAsync(includeProperties: "Inputs,Inputs.Options");
                var phases = phasesQ.ToList();

                if (phases != null && phases.Count > 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<PhaseViewModel>>(phases);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No phases found");
                    result.Errors.Add("لم يتم العثور على ");
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

        public async Task<ApiResponse<bool>> CreatePhase(PhaseCreationModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                CRPhase creationModel = new CRPhase
                {
                    CreationDate = await HelperFunctions.GetEgyptsCurrentLocalTime(),
                    TitleAR = model.TitleAR,
                    TitleEN = model.TitleEN,
                    Status = (int)PhaseStatuses.Active,
                };

                var phaseCreationRes = await unitOfWork.CRPhaseManager.CreateAsync(creationModel);


                if (phaseCreationRes != null)
                {

                    await unitOfWork.SaveChangesAsync();

                    //Create inputs
                    for (int i = 0; i < model.PhaseInputs.Count; i++)
                    {
                        var input = model.PhaseInputs[i];

                        CRPhaseInput inputCreationModel = new CRPhaseInput
                        {
                            PhaseID = phaseCreationRes.ID,
                            TitleAR = input.TitleAR,
                            TitleEN = input.TitleEN,
                            Type = input.Type,
                        };

                        var inputCreationRes = await unitOfWork.CRPhaseInputManager.CreateAsync(inputCreationModel);

                        if (inputCreationModel != null)
                        {
                            if (input.Attributes != null && input.Attributes.Count > 0)
                            {
                                await unitOfWork.SaveChangesAsync();

                                for (int j = 0; j < input.Attributes.Count; j++)
                                {
                                    var option = input.Attributes[j];
                                    CRPhaseInputOption inputOptionCreationModel = new CRPhaseInputOption
                                    {
                                        InputID = inputCreationModel.ID,
                                        TitleAR = option.LabelAR,
                                        TitleEN = option.LabelEN,
                                    };

                                    var optionCreationRes = await unitOfWork.CRPhaseInputOptionManager.CreateAsync(inputOptionCreationModel);

                                    if (optionCreationRes == null)
                                    {
                                        result.Errors.Add("Error adding option");
                                    }
                                }

                            }
                        }
                    }

                    if (result.Errors.Count > 0)
                    {
                        result.Succeeded = false;
                        return result;
                    }
                    else
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.ErrorCode = ErrorCode.A500;
                    result.Errors.Add("Could not append tag to contact");
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

        #region Timeline
        public async Task<ApiResponse<List<CRTimelineViewModel>>> GetAllTimelines()
        {
            ApiResponse<List<CRTimelineViewModel>> result = new ApiResponse<List<CRTimelineViewModel>>();
            try
            {

                var timelinesQ = await unitOfWork.CRPhaseTimelineManager.GetAsync(); //includeProperties: "Phases,Phases.Phase,Phases.Phase.Inputs"
                var timelines = timelinesQ.ToList();

                if (timelines != null && timelines.Count > 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<CRTimelineViewModel>>(timelines);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No timelines found");
                    result.Errors.Add("لم يتم العثور على ");
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

        public async Task<ApiResponse<CRTimelineViewModel>> GetTimelineByID(long TimelineID)
        {
            ApiResponse<CRTimelineViewModel> result = new ApiResponse<CRTimelineViewModel>();
            try
            {

                var timelinesQ = await unitOfWork.CRPhaseTimelineManager.GetAsync(t => t.ID == TimelineID,
                    includeProperties: "Phases,Phases.Phase,Phases.Phase.Inputs,Phases.Phase.Inputs.Options"); //
                var timelines = timelinesQ.FirstOrDefault();

                if (timelines != null)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<CRTimelineViewModel>(timelines);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Timeline not found");
                    result.Errors.Add("لم يتم العثور على ");
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

        public async Task<ApiResponse<bool>> CreateTimeline(CRTimelineCreationModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                CRTimeline creationModel = new CRTimeline
                {
                    CreationDate = await HelperFunctions.GetEgyptsCurrentLocalTime(),
                    TitleAR = model.TitleAR,
                    TitleEN = model.TitleEN,
                };

                var timelineCreationModel = await unitOfWork.CRPhaseTimelineManager.CreateAsync(creationModel);


                if (timelineCreationModel != null)
                {

                    await unitOfWork.SaveChangesAsync();

                    //Create Timeline Phases
                    for (int i = 0; i < model.Phases.Count; i++)
                    {
                        var phase = model.Phases[i];

                        CRTimeline_Phase timelinePhaseLinkModel = new CRTimeline_Phase
                        {
                            PhaseID = phase.PhaseID,
                            TimelineID = timelineCreationModel.ID,
                            ParentPhaseID = phase.ParentPhaseID,
                        };

                        var timelinePhaseLinkRes = await unitOfWork.CRTimeline_PhaseManager.CreateAsync(timelinePhaseLinkModel);

                        if (timelinePhaseLinkRes == null)
                        {
                            result.Errors.Add("Unable to add phase # " + i + i);
                        }
                        else
                        {
                            await unitOfWork.SaveChangesAsync();
                        }
                    }

                    if (result.Errors.Count > 0)
                    {
                        result.Succeeded = false;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = true;
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.ErrorCode = ErrorCode.A500;
                    result.Errors.Add("Error creating Timeline");
                    result.Errors.Add("Error creating Timeline");
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

        public async Task<ApiResponse<bool>> LinkPhasesToTimeline(CRTimelineCreationModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var timelineQ = await unitOfWork.CRPhaseTimelineManager.GetAsync(t => t.ID == model.ID);
                var timeline = timelineQ.FirstOrDefault();


                if (timeline != null)
                {

                    //Create Timeline Phases
                    for (int i = 0; i < model.Phases.Count; i++)
                    {
                        var phase = model.Phases[i];

                        CRTimeline_Phase timelinePhaseLinkModel = new CRTimeline_Phase
                        {
                            PhaseID = phase.PhaseID,
                            TimelineID = timeline.ID,
                            ParentPhaseID = phase.ParentPhaseID,
                        };

                        var timelinePhaseLinkRes = await unitOfWork.CRTimeline_PhaseManager.CreateAsync(timelinePhaseLinkModel);

                        if (timelinePhaseLinkRes == null)
                        {
                            result.Errors.Add("Unable to add phase # " + i + i);
                        }
                    }

                    if (result.Errors.Count > 0)
                    {
                        result.Succeeded = false;
                        return result;
                    }
                    else
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.ErrorCode = ErrorCode.A500;
                    result.Errors.Add("Error: Timeline not found");
                    result.Errors.Add("Error: Timeline not found");
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

        #region Customer Request

        public async Task<ApiResponse<List<CRTypeViewModel>>> GetAllRequestTypes()
        {
            ApiResponse<List<CRTypeViewModel>> result = new ApiResponse<List<CRTypeViewModel>>();
            try
            {

                var typesQ = await unitOfWork.CustomerRequestTypeManager.GetAsync(); //includeProperties: "Phases,Phases.Phase,Phases.Phase.Inputs"
                var types = typesQ.ToList();

                if (types != null && types.Count > 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<CRTypeViewModel>>(types);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No types found");
                    result.Errors.Add("لم يتم العثور على ");
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

        public async Task<ApiResponse<CRTypeViewModel>> GetRequestTypeByID(long ID)
        {
            ApiResponse<CRTypeViewModel> result = new ApiResponse<CRTypeViewModel>();
            try
            {

                var requestTypeQ = await unitOfWork.CustomerRequestTypeManager.GetAsync(t => t.ID == ID,
                    includeProperties: "PhasesTimeline,PhasesTimeline.Phases,PhasesTimeline.Phases.Phase," +
                    "PhasesTimeline.Phases.Phase.Inputs,PhasesTimeline.Phases.Phase.Inputs.Options");
                var types = requestTypeQ.FirstOrDefault();

                if (types != null)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<CRTypeViewModel>(types);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No types found");
                    result.Errors.Add("لم يتم العثور على ");
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

        public async Task<ApiResponse<bool>> CreateCustomerRequestType(CRTypeCreationModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                CustomerRequestType creationModel = new CustomerRequestType
                {
                    NameAR = model.NameAR,
                    NameEN = model.NameEN,
                    DescriptionAR = model.DescriptionAR,
                    DescriptionEN = model.DescriptionEN,
                    CreatedBy = await HelperFunctions.GetUserID(_httpContextAccessor),
                    CreationDate = await HelperFunctions.GetEgyptsCurrentLocalTime(),
                    TimelineID = model.TimelineID,
                    Type = model.Type,
                };

                var creationRes = await unitOfWork.CustomerRequestTypeManager.CreateAsync(creationModel);

                if (creationRes != null)
                {
                    await unitOfWork.SaveChangesAsync();

                    result.Succeeded = true;
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No timelines found");
                    result.Errors.Add("لم يتم العثور على ");
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


        #region Agent Customer Request Interactions

        //Get first 3 requests
        public async Task<ApiResponse<List<CRQuickViewModel>>> GetCustomerRequestQuickViewByDealID(long dealID)
        {
            ApiResponse<List<CRQuickViewModel>> result = new ApiResponse<List<CRQuickViewModel>>();
            try
            {

                var requestsQ = await unitOfWork.CustomerRequestManager.GetAsync(t => t.DealID == dealID,
                    includeProperties: "RequestType");
                var requests = requestsQ.OrderByDescending(t => t.CreationDate).Take(3).ToList();

                if (requests != null && requests.Count > 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<CRQuickViewModel>>(requests);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No types found");
                    result.Errors.Add("لم يتم العثور على ");
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

        public async Task<ApiResponse<List<CRQuickViewModel>>> GetCustomerRequestQuickViewByContactID(long ID)
        {
            ApiResponse<List<CRQuickViewModel>> result = new ApiResponse<List<CRQuickViewModel>>();
            try
            {

                var requestsQ = await unitOfWork.CustomerRequestManager.GetAsync(t => t.ContactID == ID,
                    includeProperties: "RequestType");
                var requests = requestsQ.OrderByDescending(t => t.CreationDate).Take(3).ToList();

                if (requests != null && requests.Count > 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<CRQuickViewModel>>(requests);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No types found");
                    result.Errors.Add("لم يتم العثور على ");
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

        public async Task<ApiResponse<List<CRQuickViewModel>>> GetCustomerRequestsByDealID(long dealID)
        {
            ApiResponse<List<CRQuickViewModel>> result = new ApiResponse<List<CRQuickViewModel>>();
            try
            {

                var requestsQ = await unitOfWork.CustomerRequestManager.GetAsync(t => t.DealID == dealID,
                    includeProperties: "RequestType");
                var requests = requestsQ.OrderByDescending(t => t.CreationDate).ToList();

                if (requests != null && requests.Count > 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<CRQuickViewModel>>(requests);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No types found");
                    result.Errors.Add("لم يتم العثور على ");
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
        public async Task<ApiResponse<CRTypeViewModel>> GetCustomerRequestByID(long requestID)
        {
            ApiResponse<CRTypeViewModel> result = new ApiResponse<CRTypeViewModel>();
            try
            {

                var requestsQ = await unitOfWork.CustomerRequestManager.GetAsync(t => t.ID == requestID,
                    includeProperties: "RequestType");

                var request = requestsQ.FirstOrDefault();

                if (request != null)
                {
                    var requestTimelineQ = await unitOfWork.CR_TimelineManager.GetAsync(t => t.RequestID == request.ID && t.TimelineID == request.TimelineID,
                        includeProperties: "Timeline,Timeline.Phases,Timeline.Phases.Phase," +
                    "Timeline.Phases.Phase.Inputs,Timeline.Phases.Phase.Inputs.Options");
                    var requestTimeline = requestTimelineQ.FirstOrDefault();
                    if (requestTimeline != null)
                    {
                        request.Timeline[0] = requestTimeline;
                    }

                    for (int i = 0; i < request.Timeline[0].Timeline.Phases.Count; i++)
                    {
                        var phase = request.Timeline[0].Timeline.Phases[i];

                        //Get request timeline phase
                        var cr_timeline_phaseQ = await unitOfWork.CR_Timeline_PhaseManager.GetAsync(t => t.TimelinePhaseID == phase.ID);
                        var cr_timeline_phase = cr_timeline_phaseQ.FirstOrDefault();

                        if (cr_timeline_phase != null)
                        {
                            //Get phase input answers
                            for (int j = 0; j < phase.Phase.Inputs.Count; j++)
                            {
                                var input = phase.Phase.Inputs[j];

                                var answersQ = await unitOfWork.CRPhaseInputAnswerManager.GetAsync(t => t.InputID == input.ID && t.RequestPhaseID == cr_timeline_phase.ID);
                                var answers = answersQ.ToList();
                                if (answers != null && answers.Count > 0)
                                {
                                    input.Answers = new List<CRPhaseInputAnswer>();
                                    input.Answers.AddRange(answers);
                                }
                            }
                        }

                    }

                    result.Succeeded = true;
                    result.Data = mapper.Map<CRTypeViewModel>(request);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No requests found");
                    result.Errors.Add("لم يتم العثور على ");
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

        public async Task<ApiResponse<List<CRQuickViewModel>>> GetCustomerRequestsByContactID(long ID)
        {
            ApiResponse<List<CRQuickViewModel>> result = new ApiResponse<List<CRQuickViewModel>>();
            try
            {

                var requestsQ = await unitOfWork.CustomerRequestManager.GetAsync(t => t.ContactID == ID,
                    includeProperties: "RequestType");
                var requests = requestsQ.OrderByDescending(t => t.CreationDate).ToList();

                if (requests != null && requests.Count > 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<CRQuickViewModel>>(requests);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No types found");
                    result.Errors.Add("لم يتم العثور على ");
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

        public async Task<ApiResponse<List<CRTypeViewModel>>> GetActiveRequestTypes()
        {
            ApiResponse<List<CRTypeViewModel>> result = new ApiResponse<List<CRTypeViewModel>>();
            try
            {
                var typesQ = await unitOfWork.CustomerRequestTypeManager.GetAsync(t => t.Status == (int)RequestTypeStatuses.Active);
                var types = typesQ.ToList();

                if (types != null && types.Count > 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<CRTypeViewModel>>(types);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No types found");
                    result.Errors.Add("لم يتم العثور على ");
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

        public async Task<ApiResponse<bool>> CreateCustomerRequest(CustomerRequestCreationModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);
                if (userID != null)
                {
                    var customerRequestTypeQ = await unitOfWork.CustomerRequestTypeManager.GetAsync(t => t.ID == model.RequestTypeID);
                    var customerRequestType = customerRequestTypeQ.FirstOrDefault();
                    if (customerRequestType != null)
                    {

                        //Get customer request type details
                        CustomerRequest creationModel = new CustomerRequest
                        {
                            Status = (int)RequestTypeStatuses.Active,
                            RequestTypeID = model.RequestTypeID,
                            CreatedBy = userID,
                            CreationDate = await HelperFunctions.GetEgyptsCurrentLocalTime(),
                            TimelineID = customerRequestType.TimelineID
                        };

                        if (model.CustomerStage > 0)
                        {
                            creationModel.DealID = model.RecordID;
                        }
                        else
                        {
                            creationModel.ContactID = model.RecordID;
                        }

                        var requestCreationModel = await unitOfWork.CustomerRequestManager.CreateAsync(creationModel);

                        if (requestCreationModel != null)
                        {
                            await unitOfWork.SaveChangesAsync();

                            //Link customer request to timeline
                            CR_Timeline RequestTimeline = new CR_Timeline
                            {
                                RequestID = requestCreationModel.ID,
                                TimelineID = customerRequestType.TimelineID,
                            };

                            var RequestTimelineLinkRes = await unitOfWork.CR_TimelineManager.CreateAsync(RequestTimeline);

                            if (RequestTimelineLinkRes != null)
                            {
                                await unitOfWork.SaveChangesAsync();

                                for (int i = 0; i < model.Phases.Count; i++)
                                {
                                    var phase = model.Phases[i];

                                    //Link each timeline phase to request
                                    CR_Timeline_Phase requestTimelinePhase = new CR_Timeline_Phase
                                    {
                                        RequestID = requestCreationModel.ID,
                                        StartDate = phase.StartDate,
                                        EndDate = phase.EndDate,
                                        TimelinePhaseID = phase.ID,
                                    };

                                    var requestPhaseLinkRes = await unitOfWork.CR_Timeline_PhaseManager.CreateAsync(requestTimelinePhase);

                                    if (requestPhaseLinkRes == null)
                                    {
                                        result.Errors.Add("Error while linking phase to request");
                                    }
                                    else
                                    //Create answers for each input if found
                                    {
                                        await unitOfWork.SaveChangesAsync();
                                        for (int k = 0; k < phase.Inputs.Count; k++)
                                        {
                                            var input = phase.Inputs[k];

                                            if (input.Answer != null)
                                            {
                                                CRPhaseInputAnswer answerModel = new CRPhaseInputAnswer
                                                {
                                                    RequestPhaseID = requestPhaseLinkRes.ID,
                                                    InputID = input.ID,
                                                    Answer = input.Answer,
                                                };

                                                var answerCreationRes = await unitOfWork.CRPhaseInputAnswerManager.CreateAsync(answerModel);
                                            }

                                        }
                                    }
                                }

                                if (result.Errors.Count == 0)
                                {
                                    await unitOfWork.SaveChangesAsync();

                                    result.Succeeded = true;
                                    return result;
                                }
                                else
                                {
                                    result.Succeeded = false;
                                    return result;
                                }

                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("Error creating request");
                                result.Errors.Add("Error creating request");
                                return result;
                            }
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Error creating request");
                            result.Errors.Add("Error creating request");
                            return result;
                        }
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Request Type not found");
                        result.Errors.Add("Request Type not found");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.ErrorCode = ErrorCode.A500;
                    result.Errors.Add("Not Authorized");
                    result.Errors.Add("Not Authorized");
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

        //Agent customer request creation
        //public async Task<ApiResponse<bool>> CreateCustomerRequest( model)
        //{
        //    ApiResponse<bool> result = new ApiResponse<bool>();
        //    try
        //    {
        //        CustomerRequestType creationModel = new CustomerRequestType
        //        {
        //            NameAR = model.NameAR,
        //            NameEN = model.NameEN,
        //            DescriptionAR = model.DescriptionAR,
        //            DescriptionEN = model.DescriptionEN,
        //            CreatedBy = await HelperFunctions.GetUserID(_httpContextAccessor),
        //            CreationDate = await HelperFunctions.GetEgyptsCurrentLocalTime(),
        //            TimelineID = model.TimelineID,
        //            Type = model.Type,
        //        };

        //        var creationRes = await unitOfWork.CustomerRequestTypeManager.CreateAsync(creationModel);

        //        if (creationRes != null)
        //        {
        //            await unitOfWork.SaveChangesAsync();

        //            result.Succeeded = true;
        //            return result;
        //        }
        //        else
        //        {
        //            result.Succeeded = false;
        //            result.Errors.Add("No timelines found");
        //            result.Errors.Add("لم يتم العثور على ");
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

        #endregion

        #endregion
    }

}


