
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using Stack.DTOs;
using Stack.DTOs.Enums;
using Stack.DTOs.Models.Modules.CR;
using Stack.Entities.Enums.Modules.CustomerStage;
using Stack.Entities.Models.Modules.CR;
using Stack.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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
                var type = requestTypeQ.FirstOrDefault();

                if (type != null)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<CRTypeViewModel>(type);
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

        public async Task<ApiResponse<CRViewModel>> GetCustomerRequestByID(long requestID)
        {
            ApiResponse<CRViewModel> result = new ApiResponse<CRViewModel>();
            try
            {

                var requestsQ = await unitOfWork.CustomerRequestManager.GetAsync(t => t.ID == requestID,
                    includeProperties: "RequestType");

                var request = requestsQ.FirstOrDefault();

                if (request != null)
                {
                    //Get related data via type
                    //if (request.RequestType.Type == (int)CustomerRequestTypes.InterestBased)
                    //{

                    //}
                    //else if (request.RequestType.Type == (int)CustomerRequestTypes.Resale)
                    //{

                    //}

                    //Get request timeline
                    var requestTimelineQ = await unitOfWork.CR_TimelineManager.GetAsync(t => t.RequestID == request.ID && t.TimelineID == request.TimelineID,
                        includeProperties: "Timeline,Timeline.Phases,Timeline.Phases.Phase," +
                    "Timeline.Phases.Phase.Inputs,Timeline.Phases.Phase.Inputs.Options");
                    var requestTimeline = requestTimelineQ.FirstOrDefault();

                    if (requestTimeline != null)
                    {
                        request.Timeline[0] = requestTimeline;
                    }

                    //Get phase input answers
                    for (int i = 0; i < request.Timeline[0].Timeline.Phases.Count; i++)
                    {
                        var phase = request.Timeline[0].Timeline.Phases[i];

                        //Get request timeline phase
                        var cr_timeline_phaseQ = await unitOfWork.CR_Timeline_PhaseManager.GetAsync(t => t.TimelinePhaseID == phase.ID && t.RequestID == request.ID);
                        var cr_timeline_phase = cr_timeline_phaseQ.FirstOrDefault();

                        if (cr_timeline_phase != null)
                        {
                            CR_Timeline_Phase phaseDetails = new CR_Timeline_Phase
                            {
                                StartDate = cr_timeline_phase.StartDate,
                                EndDate = cr_timeline_phase.EndDate,
                            };

                            List<CR_Timeline_Phase> detailsList = new List<CR_Timeline_Phase>();

                            detailsList.Add(phaseDetails);

                            phase.RequestTimelinePhaseDetails = detailsList;
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
                    result.Data = mapper.Map<CRViewModel>(request);

                    //Get Request Interest
                    if (request.InterestID != null)
                    {
                        var interestQ = await unitOfWork.LInterestManager.GetAsync(t => t.ID == request.InterestID);
                        var interest = interestQ.FirstOrDefault();

                        if (interest != null)
                        {
                            result.Data.InterestDescriptionEN = interest.DescriptionEN;
                            result.Data.InterestDescriptionAR = interest.DescriptionAR;
                        }
                    }

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
                            UniqueNumber = Guid.NewGuid().ToString().Substring(0, 5),
                            Status = (int)RequestTypeStatuses.Active,
                            RequestTypeID = model.RequestTypeID,
                            CreatedBy = userID,
                            CreationDate = await HelperFunctions.GetEgyptsCurrentLocalTime(),
                            TimelineID = customerRequestType.TimelineID,
                            InterestID = model.InterestID,
                            TypeIndex = model.TypeIndex
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


        public async Task<ApiResponse<bool>> UpdateCustomerRequestPhase(CRUpdatePhaseModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);
                if (userID != null)
                {
                    //Get request timeline phase
                    var requestTimelinePhaseQ = await unitOfWork.CR_Timeline_PhaseManager.GetAsync(t => t.TimelinePhaseID == model.PhaseID && t.RequestID == model.RequestID);
                    var requestTimelinePhase = requestTimelinePhaseQ.FirstOrDefault();
                    if (requestTimelinePhase != null)
                    {
                        for (int i = 0; i < model.Inputs.Count; i++)
                        {

                            var input = model.Inputs[i];
                            var CRPhaseInputAnswerQ = await unitOfWork.CRPhaseInputAnswerManager.GetAsync(t => t.RequestPhase.ID == requestTimelinePhase.ID && t.InputID == input.ID);
                            var CRPhaseInputAnswer = CRPhaseInputAnswerQ.FirstOrDefault();

                            if (CRPhaseInputAnswer != null)
                            {

                                CRPhaseInputAnswer.Answer = input.Answer;

                                var updateRes = await unitOfWork.CRPhaseInputAnswerManager.UpdateAsync(CRPhaseInputAnswer);

                            }
                            //Create new answer
                            else
                            {
                                CRPhaseInputAnswer answerModel = new CRPhaseInputAnswer
                                {
                                    RequestPhaseID = requestTimelinePhase.ID,
                                    InputID = input.ID,
                                    Answer = input.Answer,
                                };

                                var answerCreationRes = await unitOfWork.CRPhaseInputAnswerManager.CreateAsync(answerModel);
                                if (answerCreationRes == null)
                                {
                                    result.Errors.Add("Failed to add answer for input: " + (model.Inputs.IndexOf(input) + 1));
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
                        result.Errors.Add("Phase not found");
                        result.Errors.Add("Phase not found");
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

        public async Task<ApiResponse<bool>> CompleteCustomerRequest(long requestID)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);
                if (userID != null)
                {
                    //Get customer request
                    var customerRequestQ = await unitOfWork.CustomerRequestManager.GetAsync(t => t.ID == requestID);
                    var customerRequest = customerRequestQ.FirstOrDefault();

                    if (customerRequest != null && customerRequest.Status == (int)CRStatus.Active)
                    {
                        //Set request complete
                        customerRequest.Status = (int)CRStatus.Completed;

                        var updateRequestRes = await unitOfWork.CustomerRequestManager.UpdateAsync(customerRequest);

                        if (updateRequestRes)
                        {
                            await unitOfWork.SaveChangesAsync();
                            result.Succeeded = true;
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Error updating request");
                            result.Errors.Add("Error updating request");
                            return result;
                        }
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Request is not in progress");
                        result.Errors.Add("Request is not in progress");
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

        public async Task<ApiResponse<bool>> CancelCustomerRequest(long requestID)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);
                if (userID != null)
                {
                    //Get customer request
                    var customerRequestQ = await unitOfWork.CustomerRequestManager.GetAsync(t => t.ID == requestID);
                    var customerRequest = customerRequestQ.FirstOrDefault();

                    if (customerRequest != null && customerRequest.Status == (int)CRStatus.Active)
                    {
                        //Set request complete
                        customerRequest.Status = (int)CRStatus.Cancelled;

                        var updateRequestRes = await unitOfWork.CustomerRequestManager.UpdateAsync(customerRequest);

                        if (updateRequestRes)
                        {
                            await unitOfWork.SaveChangesAsync();
                            result.Succeeded = true;
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Error updating request");
                            result.Errors.Add("Error updating request");
                            return result;
                        }
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Request is not in progress");
                        result.Errors.Add("Request is not in progress");
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

        #endregion

        #endregion
    }

}


