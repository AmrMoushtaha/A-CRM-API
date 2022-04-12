﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Stack.DTOs.Requests.Modules.Activities;
using Stack.DTOs;
using System.Threading.Tasks;
using Stack.Entities.Models.Modules.Activities;
using System.Collections.Generic;
using System.Linq;
using Stack.DTOs.Enums;
using System;
using Stack.Repository.Common;
using Stack.DTOs.Models.Modules.Activities;
using Stack.Entities.Enums.Modules.Activities;
using Stack.DTOs.Models.Shared;
using Stack.Entities.Models.Modules.CustomerStage;
using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Enums.Modules.CustomerStage;

namespace Stack.ServiceLayer.Modules.Activities
{
    public class ActivitiesService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private static readonly HttpClient client = new HttpClient();

        public ActivitiesService( UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
            this.mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<List<ActivityTypeMainViewDTO>>> GetAllActiveActivityTypes()
        {
            ApiResponse<List<ActivityTypeMainViewDTO>> result = new ApiResponse<List<ActivityTypeMainViewDTO>>();
            try
            {

                var activityTypesResult = await unitOfWork.ActivityTypesManager.GetAsync(a => a.Status == ActivityTypeStatus.Activated.ToString());

                List<ActivityType> activityTypesList = activityTypesResult.ToList();

                if(activityTypesList != null && activityTypesList.Count > 0)
                {

                    result.Data = mapper.Map<List<ActivityTypeMainViewDTO>>(activityTypesList);

                    result.Succeeded = true;

                    return result;

                }
                else
                {

                    result.ErrorType = ErrorType.SystemError;

                    result.Succeeded = false;

                    result.Errors.Add("No activity types were found, Please contact your system administrator !");

                    result.Errors.Add("لم يتم العثور على أنواع الأنشطة ، يرجى الاتصال بمسؤول النظام!");

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

        public async Task<ApiResponse<List<ActivityHistoryViewDTO>>> GetActivityHistoryByDealID(long DealID)
        {
            ApiResponse<List<ActivityHistoryViewDTO>> result = new ApiResponse<List<ActivityHistoryViewDTO>>();
            try
            {

                var processFlowResult = await unitOfWork.ProcessFlowsManager.GetAsync(a => a.DealID == DealID);

                ProcessFlow referenceProcessFlow = processFlowResult.FirstOrDefault();

                if (referenceProcessFlow != null)
                {

                    var activitiesResult = await unitOfWork.ActivitiesManager.GetAsync(a => a.ProcessFlow.DealID == DealID && a.IsSubmitted == true, includeProperties:"ActivityType");

                    List<Activity> activitiesList = activitiesResult.ToList();

                    if (activitiesList.Count == 0 || activitiesList == null)
                    {
                        activitiesList = activitiesList.OrderBy(a => a.CreationDate).ToList();
                        result.Data = mapper.Map<List<ActivityHistoryViewDTO>>(activitiesList);
                        result.Succeeded = true;
                        return result;

                    }
                    else
                    {

                        result.Succeeded = false;
                        result.Errors.Add("No previous activities were found for this contact ! ");
                        result.Errors.Add("لم يتم العثور على أنشطة سابقة لجهة الاتصال هذه!");
                        return result;

                    }

                }
                else
                {

                    result.Succeeded = false;
                    result.Errors.Add("No previous activities were found for this contact ! ");
                    result.Errors.Add("لم يتم العثور على أنشطة سابقة لجهة الاتصال هذه!");
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

        public async Task<ApiResponse<List<ActivityHistoryViewDTO>>> GetActivityHistoryByContactID(long ContactID)
        {
            ApiResponse<List<ActivityHistoryViewDTO>> result = new ApiResponse<List<ActivityHistoryViewDTO>>();
            try
            {

                var processFlowResult = await unitOfWork.ProcessFlowsManager.GetAsync(a => a.DealID == ContactID);

                ProcessFlow referenceProcessFlow = processFlowResult.FirstOrDefault();

                if (referenceProcessFlow != null)
                {

                    var activitiesResult = await unitOfWork.ActivitiesManager.GetAsync(a => a.ProcessFlow.ContactID == ContactID && a.IsSubmitted == true, includeProperties: "ActivityType");

                    List<Activity> activitiesList = activitiesResult.ToList();

                    if (activitiesList.Count == 0 || activitiesList == null)
                    {

                        activitiesList = activitiesList.OrderBy(a => a.CreationDate).ToList();
                        result.Data = mapper.Map<List<ActivityHistoryViewDTO>>(activitiesList);
                        result.Succeeded = true;
                        return result;

                    }
                    else
                    {

                        result.Succeeded = false;
                        result.Errors.Add("No previous activities were found for this contact ! ");
                        result.Errors.Add("لم يتم العثور على أنشطة سابقة لجهة الاتصال هذه!");
                        return result;

                    }

                }
                else
                {

                    result.Succeeded = false;
                    result.Errors.Add("No previous activities were found for this contact ! ");
                    result.Errors.Add("لم يتم العثور على أنشطة سابقة لجهة الاتصال هذه!");
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

        public async Task<ApiResponse<bool>> CreateActivityType(CreateActivityTypeModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                //Check if an activity typr with a duplicate name already exists !

                var duplicateTypeResult = await unitOfWork.ActivityTypesManager.GetAsync(a => a.NameEN == model.NameEN || a.NameAR == model.NameAR );

                List<ActivityType> duplicateTypeList = duplicateTypeResult.ToList();

                if (duplicateTypeList.Count > 0)
                {

                    result.Succeeded = false;
                    result.Data = false;
                    result.Errors.Add("Failed to create the new activity type, an activity type with a duplicate name arlready exists !");
                    return result;

                }

                //If not create the new activity type . 

                ActivityType newActivityType = new ActivityType();

                newActivityType.NameAR = model.NameAR;

                newActivityType.NameEN = model.NameEN;

                newActivityType.Status = ActivityTypeStatus.Activated.ToString();


                var createActivityTypeResult = await unitOfWork.ActivityTypesManager.CreateAsync(newActivityType);

                await unitOfWork.SaveChangesAsync();


                if (createActivityTypeResult != null)
                {

                    result.Succeeded = true;

                    result.Data = true;

                    return result;

                }
                else
                {

                    result.Succeeded = false;

                    result.Data = false;

                    result.ErrorType = ErrorType.SystemError;

                    result.Errors.Add("Failed to create a new section !");

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


        public async Task<ApiResponse<bool>> DeleteActivity(DeletionModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var activitiesResult = await unitOfWork.ActivitiesManager.GetAsync(a => a.ID == model.ID);

                Activity activityToDelete = activitiesResult.ToList().FirstOrDefault();

                if (activityToDelete != null)
                {

                    var deleteActivityResult = await unitOfWork.ActivitiesManager.RemoveAsync(activityToDelete);

                    if (deleteActivityResult == true)
                    {

                       var saveResult =  await unitOfWork.SaveChangesAsync();

                        result.Succeeded = true;
                        result.Data = true;
                        return result;

                    }
                    else

                    {

                        result.Succeeded = false;
                        result.Data = false;
                        result.Errors.Add("Failed to delete activity, Please try again !");
                        result.ErrorCode = ErrorCode.A500;
                        return result;

                    }

                }
                else
                {

                    result.Succeeded = false;
                    result.Data = false;
                    result.Errors.Add("Failed to delete activity, Please try again !");
                    result.ErrorCode = ErrorCode.A500;
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

        public async Task<ApiResponse<bool>> SubmitActivity(ActivitySubmissionModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var ActivitiesResult = await unitOfWork.ActivitiesManager.GetAsync(a => a.ID == model.ActivityID);

                Activity referenceActivity = ActivitiesResult.FirstOrDefault();

                var processFlowsResult = await unitOfWork.ProcessFlowsManager.GetAsync(a => a.ID == referenceActivity.ProcessFlowID);

                ProcessFlow referenceProcessFlow = processFlowsResult.FirstOrDefault();

                ApplicationUser referenceUser = await unitOfWork.UserManager.FindByNameAsync(this._httpContextAccessor.HttpContext.User.Identity.Name);


                if (referenceActivity != null && referenceProcessFlow != null && referenceUser != null)
                {

                    if (model.CurrentStage != model.NewStage)
                    {
                        //if the current stage is Contact, create a new customer &  deal and assign it to the existing process flow .
                        if (model.CurrentStage == "Contact")
                        {

                            var contactsResult = await unitOfWork.ContactManager.GetAsync(a => a.ID == model.RecordID);

                            Contact referenceContact = contactsResult.FirstOrDefault();

                            referenceContact.AssignedUserID = referenceUser.Id;

                            referenceContact.State = (int)CustomerStageState.Converted;

                            var updateContactResult = await unitOfWork.ContactManager.UpdateAsync(referenceContact);

                            await unitOfWork.SaveChangesAsync();

                            //Create a new deal record . 
                            Deal newDeal = new Deal();

                            //if the contact has no customer record assigned to it and this is the first activity for this contact .
                            if (referenceContact.CustomerID == null)
                            {

                                //Create a new customer record . 
                                Customer newCustomer = new Customer();

                                newCustomer.FullNameEN = referenceContact.FullNameEN;

                                newCustomer.FullNameAR = referenceContact.FullNameAR;

                                // Assign the user to this customer .
                                newCustomer.AssignedUserID = referenceUser.Id;

                                var createCustomerResult = await unitOfWork.CustomerManager.CreateAsync(newCustomer);

                                await unitOfWork.SaveChangesAsync();

                                newDeal.CustomerID = createCustomerResult.ID;

                                var createDealResult = await unitOfWork.DealManager.CreateAsync(newDeal);

                                await unitOfWork.SaveChangesAsync();

                                // link the newly created deal to the existing process flow .
                                referenceProcessFlow.DealID = createDealResult.ID;

                                var updateProcessFlowResult = await unitOfWork.ProcessFlowsManager.UpdateAsync(referenceProcessFlow);

                            }
                            else
                            {
                                // link the existing customer record to this deal . 
                                newDeal.CustomerID = (long)referenceContact.CustomerID;

                                var createDealResult = await unitOfWork.DealManager.CreateAsync(newDeal);

                                await unitOfWork.SaveChangesAsync();

                                // link the newly created deal to the existing process flow .
                                referenceProcessFlow.DealID = createDealResult.ID;

                                var updateProcessFlowResult = await unitOfWork.ProcessFlowsManager.UpdateAsync(referenceProcessFlow);

                            }

                            //Create the new stage record and link it to the newly created deal record. 
                            if (model.NewStage == "Prospect")
                            {

                                Prospect newStageRecord = new Prospect();

                                newStageRecord.State = (int)CustomerStageState.Initial;

                                newStageRecord.IsFresh = true;

                                newStageRecord.AssignedUserID = referenceUser.Id;

                                newStageRecord.DealID = newDeal.ID;

                                var createNewStageRecordResult = await unitOfWork.ProspectManager.CreateAsync(newStageRecord);

                            }

                            if (model.NewStage == "Lead")
                            {

                                Lead newStageRecord = new Lead();

                                newStageRecord.State = (int)CustomerStageState.Initial;

                                newStageRecord.IsFresh = true;

                                newStageRecord.AssignedUserID = referenceUser.Id;

                                newStageRecord.DealID = newDeal.ID;

                                var createNewStageRecordResult = await unitOfWork.LeadManager.CreateAsync(newStageRecord);

                            }

                            if (model.NewStage == "Opportunity")
                            {

                                Opportunity newStageRecord = new Opportunity();

                                newStageRecord.State = (int)CustomerStageState.Initial;

                                newStageRecord.IsFresh = true;

                                newStageRecord.AssignedUserID = referenceUser.Id;

                                newStageRecord.DealID = newDeal.ID;

                                var createNewStageRecordResult = await unitOfWork.OpportunityManager.CreateAsync(newStageRecord);

                            }

                            await unitOfWork.SaveChangesAsync();

                        }
                        else
                        {

                            long referenceDealID = 0;

                            if (model.CurrentStage == "Prospect")
                            {
                                //Update the current prospect record to converted .
                                var prospectsResult = await unitOfWork.ProspectManager.GetAsync(a => a.ID == model.RecordID);

                                Prospect referenceProspect = prospectsResult.FirstOrDefault();

                                referenceProspect.State = (int)CustomerStageState.Converted;

                                referenceDealID = referenceProspect.DealID;

                                var updateProspectResutlt = await unitOfWork.ProspectManager.UpdateAsync(referenceProspect);

                            }

                            if (model.CurrentStage == "Lead")
                            {
                                //Update the current lead record to converted .
                                var leadsResult = await unitOfWork.LeadManager.GetAsync(a => a.ID == model.RecordID);

                                Lead referenceLead = leadsResult.FirstOrDefault();

                                referenceLead.State = (int)CustomerStageState.Converted;

                                referenceDealID = referenceLead.DealID;

                                var updateLeadResult = await unitOfWork.LeadManager.UpdateAsync(referenceLead);

                            }

                            if (model.CurrentStage == "Opportunity")
                            {
                                //Update the current opportunity record to converted .
                                var opportunitiesResult = await unitOfWork.OpportunityManager.GetAsync(a => a.ID == model.RecordID);

                                Opportunity referenceOpportunity = opportunitiesResult.FirstOrDefault();

                                referenceOpportunity.State = (int)CustomerStageState.Converted;

                                referenceDealID = referenceOpportunity.DealID;

                                var updateProspectResutlt = await unitOfWork.OpportunityManager.UpdateAsync(referenceOpportunity);

                            }


                            var dealsResult = await unitOfWork.DealManager.GetAsync(a => a.ID == referenceDealID);

                            Deal referenceDeal = dealsResult.FirstOrDefault();


                            //Create the new stage record and link it to the current deal record .
                            if (model.NewStage == "Prospect")
                            {

                                Prospect newStageRecord = new Prospect();

                                newStageRecord.State = (int)CustomerStageState.Initial;

                                newStageRecord.IsFresh = true;

                                newStageRecord.AssignedUserID = referenceUser.Id;

                                newStageRecord.DealID = referenceDealID;

                                var createNewStageRecordResult = await unitOfWork.ProspectManager.CreateAsync(newStageRecord);

                            }

                            if (model.NewStage == "Lead")
                            {

                                Lead newStageRecord = new Lead();

                                newStageRecord.State = (int)CustomerStageState.Initial;

                                newStageRecord.IsFresh = true;

                                newStageRecord.AssignedUserID = referenceUser.Id;

                                newStageRecord.DealID = referenceDealID;

                                var createNewStageRecordResult = await unitOfWork.LeadManager.CreateAsync(newStageRecord);

                            }

                            if (model.NewStage == "Opportunity")

                            {

                                Opportunity newStageRecord = new Opportunity();

                                newStageRecord.State = (int)CustomerStageState.Initial;

                                newStageRecord.IsFresh = true;

                                newStageRecord.AssignedUserID = referenceUser.Id;

                                newStageRecord.DealID = referenceDealID;

                                var createNewStageRecordResult = await unitOfWork.OpportunityManager.CreateAsync(newStageRecord);

                            }


                            //if the new stage is Done-deal set the related process flow completed flag to true and update the related contact record is finalized flag to true . 
                            if (model.NewStage == "Done-Deal")
                            {

                                referenceProcessFlow.IsComplete = true;

                                var contactsResult = await unitOfWork.ContactManager.GetAsync(a => a.ID == referenceProcessFlow.ContactID);

                                Contact referenceContact = contactsResult.FirstOrDefault();

                                referenceContact.IsFinalized = true;

                                referenceContact.IsFresh = false;

                                var updateProcessFlowResult = await unitOfWork.ProcessFlowsManager.UpdateAsync(referenceProcessFlow);

                                var updateContactResult = await unitOfWork.ContactManager.UpdateAsync(referenceContact);

                            }

                            await unitOfWork.SaveChangesAsync();

                        }

                    }






                   




                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to submit activity, Please try again !");
                    result.Errors.Add("Failed to submit activity, Please try again !");
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


        //Section navigation.
        public async Task<ApiResponse<SectionToAnswer>> GetNextActivitySection(SectionToAnswer model)
    {
        ApiResponse<SectionToAnswer> result = new ApiResponse<SectionToAnswer>();
        try
        {

            //Get an update the end date of the current activity section . 
            var activitySectionsResult = await unitOfWork.ActivitySectionsManager.GetAsync(a => a.ID == model.ID);

            ActivitySection currentSection = activitySectionsResult.FirstOrDefault();

            currentSection.EndDate = await HelperFunctions.GetEgyptsCurrentLocalTime();

            currentSection.IsSubmitted = true;

            var updateCurrentSectionResult = await unitOfWork.ActivitySectionsManager.UpdateAsync(currentSection);

            await unitOfWork.SaveChangesAsync();

            string sectionToRouteTo = "";

            SectionToAnswer sectionToReturn = new SectionToAnswer();

            Section nextSectionReference = new Section();

            //Save question answers for the current section  .
            for (int i = 0; i < model.Questions.Count; i++)
            {

                SectionQuestionAnswer newSectionQuestionAnswer = new SectionQuestionAnswer();

                newSectionQuestionAnswer.QuestionID = model.Questions[i].ID;

                newSectionQuestionAnswer.Value = model.Questions[i].Answer;

                newSectionQuestionAnswer.DateValue = model.Questions[i].DateAnswer;

                newSectionQuestionAnswer.ActivitySectionID = model.ID;

                var createSectionQuestionAnswer = await unitOfWork.SectionQuestionAnswersManager.CreateAsync(newSectionQuestionAnswer);

                await unitOfWork.SaveChangesAsync();

                //If the question had options to choose from . save the selected answer . 
                if (model.Questions[i].Options.Count > 0 && model.Questions[i].Options != null)
                {

                    QuestionOption SelectedOption = model.Questions[i].Options.Find(a => a.IsSelected == true);

                    //Get the id of the section to route to in case the question is a decisional question . 
                    if (model.Questions[i].IsDecisional == true)
                    {
                        if(SelectedOption != null)
                        {
                            sectionToRouteTo = SelectedOption.RoutesTo;
                        }        
                    }

                    SelectedOption newSelectedOption = new SelectedOption();

                    newSelectedOption.SectionQuestionOptionID = SelectedOption.ID;

                    newSelectedOption.SectionQuestionAnswerID = newSectionQuestionAnswer.ID;


                    var createSelectedOptionResult = await unitOfWork.SelectedOptionsManager.CreateAsync(newSelectedOption);

                    await unitOfWork.SaveChangesAsync();


                }

            }

            //Get the order of the next section .
            var activityTypeSectionsResult = await unitOfWork.SectionsManager.GetAsync(a => a.ActivityTypeID == model.ActivityTypeID);

            List<Section> activityTypeSections = activityTypeSectionsResult.ToList();

            activityTypeSections = activityTypeSections.OrderBy(a => a.Order).ToList();

            int currentSectionIndex = activityTypeSections.FindIndex(a => a.Order == model.ActivityTypeSectionOrder+1);


            // Return SectionToAnswer with IsFinal section = true .
            if ((currentSectionIndex + 1) == activityTypeSections.Count)
            {

                sectionToReturn.IsFinalSection = true;

                sectionToReturn.ActivityID = model.ActivityID;

                sectionToReturn.ActivityTypeID = model.ActivityTypeID;

                result.Succeeded = true;

                result.Data = sectionToReturn;

                return result;

            }
            else
            {

                int nextSectionOrder = activityTypeSections[currentSectionIndex + 1].Order;

                // If the activity has no decisional questions get the next activity section by order . 
                if (model.HasDecisionalQuestions == false)
                {

                    activityTypeSectionsResult = await unitOfWork.SectionsManager.GetAsync(a => a.ActivityTypeID == model.ActivityTypeID && a.Order == nextSectionOrder, includeProperties: "Questions,Questions.QuestionOptions");

                    nextSectionReference = activityTypeSectionsResult.FirstOrDefault();

                }
                else // if the section has decisional questions find the section that the activity routes to . 
                {

                    if (sectionToRouteTo == "Submit")
                    {
                        sectionToReturn.IsFinalSection = true;

                        sectionToReturn.ActivityID = model.ActivityID;

                        sectionToReturn.ActivityTypeID = model.ActivityTypeID;

                        result.Succeeded = true;

                        result.Data = sectionToReturn;

                        return result;
                    }
                    else
                    {

                        activityTypeSectionsResult = await unitOfWork.SectionsManager.GetAsync(a => a.Order == long.Parse(sectionToRouteTo) && a.ActivityTypeID == model.ActivityTypeID, includeProperties: "Questions,Questions.QuestionOptions");

                        nextSectionReference = activityTypeSectionsResult.FirstOrDefault();

                    }

                }

                //Create the new activity section .

                ActivitySection nextSection = new ActivitySection();

                nextSection.ActivityID = model.ActivityID;

                nextSection.Order = model.Order + 1;

                nextSection.SectionID = nextSectionReference.ID;

                nextSection.StartDate = await HelperFunctions.GetEgyptsCurrentLocalTime();

                var createNextSectionResult = await unitOfWork.ActivitySectionsManager.CreateAsync(nextSection);

                await unitOfWork.SaveChangesAsync();

                //Create the upcoming section model and return it . 

                sectionToReturn.ID = nextSection.ID;

                sectionToReturn.NameAR = nextSectionReference.NameAR;

                sectionToReturn.NameAR = nextSectionReference.NameAR;

                sectionToReturn.IsFinalSection = false;

                sectionToReturn.ActivityID = model.ActivityID;

                sectionToReturn.ActivityTypeID = model.ActivityTypeID;

                sectionToReturn.Order = nextSection.Order;

                sectionToReturn.ActivityTypeSectionOrder = nextSectionReference.Order;

                sectionToReturn.HasDecisionalQuestions = nextSectionReference.HasDecisionalQuestions;

                sectionToReturn.HasCreateIntrest = nextSectionReference.HasCreateInterest;

                sectionToReturn.HasCreateRequest = nextSectionReference.HasCreateRequest;

                sectionToReturn.HasCreateResale = nextSectionReference.HasCreateResale;

                sectionToReturn.Questions = new List<QuestionToAnswer>();

                //Append section questions . 
                for (int i = 0; i < nextSectionReference.Questions.Count; i++)
                {

                    QuestionToAnswer question = new QuestionToAnswer();

                    question.ID = nextSectionReference.Questions[i].ID;

                    question.DescriptionAR = nextSectionReference.Questions[i].DescriptionAR;

                    question.DescriptionEN = nextSectionReference.Questions[i].DescriptionEN;

                    question.isRequired = nextSectionReference.Questions[i].isRequired;

                    question.IsDecisional = nextSectionReference.Questions[i].IsDecisional;

                    question.Type = nextSectionReference.Questions[i].Type;

                    question.Order = nextSectionReference.Questions[i].Order;

                    question.Answer = "";

                    if(question.Type == "Date")
                    {

                        question.DateAnswer = await HelperFunctions.GetEgyptsCurrentLocalTime();

                    }

                    //Append section question options . 
                    if (nextSectionReference.Questions[i].QuestionOptions.Count > 0)
                    {

                        question.Options = new List<QuestionOption>();

                        for (int k = 0; k < nextSectionReference.Questions[i].QuestionOptions.Count; k++)
                        {

                            QuestionOption questionOption = new QuestionOption();

                            questionOption.ValueEN = nextSectionReference.Questions[i].QuestionOptions[k].ValueEN;

                            questionOption.ValueAR = nextSectionReference.Questions[i].QuestionOptions[k].ValueAR;

                            questionOption.RoutesTo = nextSectionReference.Questions[i].QuestionOptions[k].RoutesTo;

                            questionOption.ID = nextSectionReference.Questions[i].QuestionOptions[k].ID;

                            questionOption.IsSelected = false;

                            question.Options.Add(questionOption);

                        }

                    }

                    sectionToReturn.Questions.Add(question);

                }

                //Re-arrange section questions . 
                sectionToReturn.Questions = sectionToReturn.Questions.OrderBy(a => a.Order).ToList();

                result.Data = sectionToReturn;

                result.Succeeded = true;

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

        public async Task<ApiResponse<SectionToAnswer>> CreateNewContactActivity(CreateContactActivityModel model)
        {
            ApiResponse<SectionToAnswer> result = new ApiResponse<SectionToAnswer>();
            try
            {


                Activity newActivity = new Activity();

                var processFlowsResult = await unitOfWork.ProcessFlowsManager.GetAsync(a => a.ContactID == model.ContactID);


                ProcessFlow referenceProcessFlow = processFlowsResult.FirstOrDefault();

                //Check if a process flow already exists for this contact . if not create one .
                if (referenceProcessFlow != null)
                {

                    newActivity.ProcessFlowID = referenceProcessFlow.ID;
                }
                else
                {

                    ProcessFlow newProcessFlow = new ProcessFlow();

                    newProcessFlow.ContactID = model.ContactID;

                    newProcessFlow.CreationDate = await HelperFunctions.GetEgyptsCurrentLocalTime();

                    newProcessFlow.IsComplete = false;

                    var createProcessFlowResult = await unitOfWork.ProcessFlowsManager.CreateAsync(newProcessFlow);

                    await unitOfWork.SaveChangesAsync();

                    if (createProcessFlowResult != null)
                    {

                        newActivity.ProcessFlowID = referenceProcessFlow.ID;

                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to create a new activity, Please try again !");
                        result.Errors.Add("فشل إنشاء نشاط جديد ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }


                }

                newActivity.ActivityTypeID = model.ActivityTypeID;

                newActivity.IsSubmitted = false;

                newActivity.CreationDate = await HelperFunctions.GetEgyptsCurrentLocalTime();

                var createNewActivityResult = await unitOfWork.ActivitiesManager.CreateAsync(newActivity);

                await unitOfWork.SaveChangesAsync();

                //If the activity was created successfully .
                if (createNewActivityResult != null)
                {

                    //Get the initial section reference . 

                    var activityTypeSectionsResults = await unitOfWork.SectionsManager.GetAsync(a => a.ActivityTypeID == model.ActivityTypeID, includeProperties: "Questions,Questions.QuestionOptions");

                    List<Section> ActivityTypeSections = activityTypeSectionsResults.OrderBy(x => x.Order).ToList();

                    Section UpcomingSection = ActivityTypeSections[0];

                    //Create the initial activity section .

                    ActivitySection InitialSection = new ActivitySection();

                    InitialSection.ActivityID = newActivity.ID;

                    InitialSection.Order = 0; // default value for the initial section .

                    InitialSection.SectionID = UpcomingSection.ID;

                    InitialSection.StartDate = await HelperFunctions.GetEgyptsCurrentLocalTime();

                    var createInitialSectionResult = await unitOfWork.ActivitySectionsManager.CreateAsync(InitialSection);

                    await unitOfWork.SaveChangesAsync();

                    //Create the upcoming section model and return it . 

                    SectionToAnswer sectionToReturn = new SectionToAnswer();

                    sectionToReturn.ID = InitialSection.ID;

                    sectionToReturn.NameAR = UpcomingSection.NameAR;

                    sectionToReturn.NameAR = UpcomingSection.NameAR;

                    sectionToReturn.IsFinalSection = false;

                    sectionToReturn.ActivityID = newActivity.ID;

                    sectionToReturn.ActivityTypeID = newActivity.ActivityTypeID;

                    sectionToReturn.Order = 0; // Initial activity section order . 

                    sectionToReturn.HasDecisionalQuestions = UpcomingSection.HasDecisionalQuestions;

                    sectionToReturn.Questions = new List<QuestionToAnswer>();

                    //Append section questions . 
                    for (int i = 0; i < UpcomingSection.Questions.Count; i++)
                    {

                        QuestionToAnswer question = new QuestionToAnswer();

                        question.ID = UpcomingSection.Questions[i].ID;

                        question.DescriptionAR = UpcomingSection.Questions[i].DescriptionAR;

                        question.DescriptionEN = UpcomingSection.Questions[i].DescriptionEN;

                        question.isRequired = UpcomingSection.Questions[i].isRequired;

                        question.IsDecisional = UpcomingSection.Questions[i].IsDecisional;

                        question.Order = UpcomingSection.Questions[i].Order;

                        question.Type = UpcomingSection.Questions[i].Type;

                        question.Answer = "";


                        if (question.Type == "Date")
                        {

                            question.DateAnswer = await HelperFunctions.GetEgyptsCurrentLocalTime();

                        }

                        //Append section question options . 
                        if (UpcomingSection.Questions[i].QuestionOptions.Count > 0)
                        {

                            question.Options = new List<QuestionOption>();

                            for (int k = 0; k < UpcomingSection.Questions[i].QuestionOptions.Count; k++)
                            {

                                QuestionOption questionOption = new QuestionOption();

                                questionOption.ValueEN = UpcomingSection.Questions[i].QuestionOptions[k].ValueEN;

                                questionOption.ValueAR = UpcomingSection.Questions[i].QuestionOptions[k].ValueAR;

                                questionOption.RoutesTo = UpcomingSection.Questions[i].QuestionOptions[k].RoutesTo;

                                questionOption.ID = UpcomingSection.Questions[i].QuestionOptions[k].ID;

                                questionOption.IsSelected = false;

                                question.Options.Add(questionOption);

                            }

                        }

                        sectionToReturn.Questions.Add(question);

                    }

                    //Re-arrange section questions . 
                    sectionToReturn.Questions = sectionToReturn.Questions.OrderBy(a => a.Order).ToList();

                    result.Data = sectionToReturn;

                    result.Succeeded = true;

                    return result;

                }
                else
                {

                    result.Succeeded = false;
                    result.Errors.Add("Failed to create a new activity, Please try again !");
                    result.Errors.Add("فشل إنشاء نشاط جديد ، يرجى المحاولة مرة أخرى!");
                    result.ErrorType = ErrorType.SystemError;
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

        public async Task<ApiResponse<SectionToAnswer>> CreateNewDealActivity(CreateDealActivityModel model)
        {
            ApiResponse<SectionToAnswer> result = new ApiResponse<SectionToAnswer>();
            try
            {


                Activity newActivity = new Activity();

                var processFlowsResult = await unitOfWork.ProcessFlowsManager.GetAsync(a => a.DealID == model.DealID);


                ProcessFlow referenceProcessFlow = processFlowsResult.FirstOrDefault();

                //Check if a process flow already exists for this contact . if not create one .
                if (referenceProcessFlow != null)
                {

                    newActivity.ProcessFlowID = referenceProcessFlow.ID;
                }
                else
                {

                    ProcessFlow newProcessFlow = new ProcessFlow();

                    newProcessFlow.DealID = model.DealID;

                    newProcessFlow.CreationDate = await HelperFunctions.GetEgyptsCurrentLocalTime();

                    newProcessFlow.IsComplete = false;

                    var createProcessFlowResult = await unitOfWork.ProcessFlowsManager.CreateAsync(newProcessFlow);

                    await unitOfWork.SaveChangesAsync();

                    if (createProcessFlowResult != null)
                    {

                        newActivity.ProcessFlowID = referenceProcessFlow.ID;

                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to create a new activity, Please try again !");
                        result.Errors.Add("فشل إنشاء نشاط جديد ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }


                }

                newActivity.ActivityTypeID = model.ActivityTypeID;

                newActivity.IsSubmitted = false;

                newActivity.CreationDate = await HelperFunctions.GetEgyptsCurrentLocalTime();

                var createNewActivityResult = await unitOfWork.ActivitiesManager.CreateAsync(newActivity);

                await unitOfWork.SaveChangesAsync();

                //If the activity was created successfully .
                if (createNewActivityResult != null)
                {

                    //Get the initial section reference . 

                    var activityTypeSectionsResults = await unitOfWork.SectionsManager.GetAsync(a => a.ActivityTypeID == model.ActivityTypeID, includeProperties: "Questions,Questions.QuestionOptions");

                    List<Section> ActivityTypeSections = activityTypeSectionsResults.OrderBy(x => x.Order).ToList();

                    Section UpcomingSection = ActivityTypeSections[0];

                    //Create the initial activity section .

                    ActivitySection InitialSection = new ActivitySection();

                    InitialSection.ActivityID = newActivity.ID;

                    InitialSection.Order = 0; // default value for the initial section .

                    InitialSection.SectionID = UpcomingSection.ID;

                    InitialSection.StartDate = await HelperFunctions.GetEgyptsCurrentLocalTime();

                    var createInitialSectionResult = await unitOfWork.ActivitySectionsManager.CreateAsync(InitialSection);

                    await unitOfWork.SaveChangesAsync();

                    //Create the upcoming section model and return it . 

                    SectionToAnswer sectionToReturn = new SectionToAnswer();

                    sectionToReturn.ID = InitialSection.ID;

                    sectionToReturn.NameAR = UpcomingSection.NameAR;

                    sectionToReturn.NameAR = UpcomingSection.NameAR;

                    sectionToReturn.IsFinalSection = false;

                    sectionToReturn.ActivityID = newActivity.ID;

                    sectionToReturn.ActivityTypeID = newActivity.ActivityTypeID;

                    sectionToReturn.Order = 0; // Initial activity section order . 

                    sectionToReturn.HasDecisionalQuestions = UpcomingSection.HasDecisionalQuestions;

                    sectionToReturn.Questions = new List<QuestionToAnswer>();

                    //Append section questions . 
                    for (int i = 0; i < UpcomingSection.Questions.Count; i++)
                    {

                        QuestionToAnswer question = new QuestionToAnswer();

                        question.ID = UpcomingSection.Questions[i].ID;

                        question.DescriptionAR = UpcomingSection.Questions[i].DescriptionAR;

                        question.DescriptionEN = UpcomingSection.Questions[i].DescriptionEN;

                        question.isRequired = UpcomingSection.Questions[i].isRequired;

                        question.IsDecisional = UpcomingSection.Questions[i].IsDecisional;

                        question.Order = UpcomingSection.Questions[i].Order;

                        question.Answer = "";

                        if (question.Type == "Date")
                        {

                            question.DateAnswer = await HelperFunctions.GetEgyptsCurrentLocalTime();

                        }


                        //Append section question options . 
                        if (UpcomingSection.Questions[i].QuestionOptions.Count > 0)
                        {

                            question.Options = new List<QuestionOption>();

                            for (int k = 0; k < UpcomingSection.Questions[i].QuestionOptions.Count; k++)
                            {

                                QuestionOption questionOption = new QuestionOption();

                                questionOption.ValueEN = UpcomingSection.Questions[i].QuestionOptions[k].ValueEN;

                                questionOption.ValueAR = UpcomingSection.Questions[i].QuestionOptions[k].ValueAR;

                                questionOption.RoutesTo = UpcomingSection.Questions[i].QuestionOptions[k].RoutesTo;

                                questionOption.ID = UpcomingSection.Questions[i].QuestionOptions[k].ID;

                                questionOption.IsSelected = false;

                                question.Options.Add(questionOption);

                            }

                        }

                        sectionToReturn.Questions.Add(question);

                    }

                    //Re-arrange section questions . 
                    sectionToReturn.Questions = sectionToReturn.Questions.OrderBy(a => a.Order).ToList();

                    result.Data = sectionToReturn;

                    result.Succeeded = true;

                    return result;

                }
                else
                {

                    result.Succeeded = false;
                    result.Errors.Add("Failed to create a new activity, Please try again !");
                    result.Errors.Add("فشل إنشاء نشاط جديد ، يرجى المحاولة مرة أخرى!");
                    result.ErrorType = ErrorType.SystemError;
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

        public async Task<ApiResponse<SectionToAnswer>> GetCurrentActivitySectionByDealID(long ID)
        {
            ApiResponse<SectionToAnswer> result = new ApiResponse<SectionToAnswer>();
            try
            {

                var activitiesResult = await unitOfWork.ActivitiesManager.GetAsync(a => a.ProcessFlow.DealID == ID && a.IsSubmitted == false);

                Activity unsubmittedActivity = activitiesResult.FirstOrDefault();

                if (unsubmittedActivity != null)
                {


                    //Get an update the end date of the current activity section .  
                    var activitySectionsResult = await unitOfWork.ActivitySectionsManager.GetAsync(a => a.ActivityID == unsubmittedActivity.ID && a.IsSubmitted == false, includeProperties: "Section,Activity,Section.Questions,Section.Questions.QuestionOptions");

                    ActivitySection currentSection = activitySectionsResult.FirstOrDefault();

                    currentSection.EndDate = await HelperFunctions.GetEgyptsCurrentLocalTime();

                    var updateCurrentSectionResult = await unitOfWork.ActivitySectionsManager.UpdateAsync(currentSection);

                    await unitOfWork.SaveChangesAsync();

                    if (currentSection != null)
                    {

                        //Create the upcoming section model and return it . 

                        SectionToAnswer sectionToReturn = new SectionToAnswer();

                        sectionToReturn.ID = currentSection.ID;

                        sectionToReturn.NameAR = currentSection.Section.NameAR;

                        sectionToReturn.NameAR = currentSection.Section.NameAR;

                        sectionToReturn.IsFinalSection = false;

                        sectionToReturn.ActivityID = (long)currentSection.ActivityID;

                        sectionToReturn.ActivityTypeID = currentSection.Activity.ActivityTypeID;

                        sectionToReturn.Order = currentSection.Order;

                        sectionToReturn.ActivityTypeSectionOrder = currentSection.Section.Order;

                        sectionToReturn.HasDecisionalQuestions = sectionToReturn.HasDecisionalQuestions;

                        sectionToReturn.Questions = new List<QuestionToAnswer>();

                        if (sectionToReturn.IsFinalSection == false)
                        {

                            //Append section questions . 
                            for (int i = 0; i < currentSection.Section.Questions.Count; i++)
                            {

                                QuestionToAnswer question = new QuestionToAnswer();

                                question.ID = currentSection.Section.Questions[i].ID;

                                question.DescriptionAR = currentSection.Section.Questions[i].DescriptionAR;

                                question.DescriptionEN = currentSection.Section.Questions[i].DescriptionEN;

                                question.isRequired = currentSection.Section.Questions[i].isRequired;

                                question.IsDecisional = currentSection.Section.Questions[i].IsDecisional;

                                question.Type = currentSection.Section.Questions[i].Type;

                                question.Order = currentSection.Section.Questions[i].Order;

                                question.Answer = "";


                                if (question.Type == "Date")
                                {

                                    question.DateAnswer = await HelperFunctions.GetEgyptsCurrentLocalTime();

                                }

                                //Append section question options . 
                                if (currentSection.Section.Questions[i].QuestionOptions.Count > 0)
                                {

                                    question.Options = new List<QuestionOption>();

                                    for (int k = 0; k < currentSection.Section.Questions[i].QuestionOptions.Count; k++)
                                    {

                                        QuestionOption questionOption = new QuestionOption();

                                        questionOption.ValueEN = currentSection.Section.Questions[i].QuestionOptions[k].ValueEN;

                                        questionOption.ValueAR = currentSection.Section.Questions[i].QuestionOptions[k].ValueAR;

                                        questionOption.RoutesTo = currentSection.Section.Questions[i].QuestionOptions[k].RoutesTo;

                                        questionOption.ID = currentSection.Section.Questions[i].QuestionOptions[k].ID;

                                        questionOption.IsSelected = false;

                                        question.Options.Add(questionOption);

                                    }

                                }

                                sectionToReturn.Questions.Add(question);

                            }

                            //Re-arrange section questions . 
                            sectionToReturn.Questions = sectionToReturn.Questions.OrderBy(a => a.Order).ToList();


                        }


                        result.Data = sectionToReturn;

                        result.Succeeded = true;

                        return result;


                    }
                    else
                    {

                        SectionToAnswer sectionToReturn = new SectionToAnswer();

                        sectionToReturn.IsFinalSection = true;

                        result.Data = sectionToReturn;

                        result.Succeeded = true;

                        return result;

                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No unsubmitted activities were found !");
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

        public async Task<ApiResponse<SectionToAnswer>> GetCurrentActivitySectionByContactID(long ID)
        {
            ApiResponse<SectionToAnswer> result = new ApiResponse<SectionToAnswer>();
            try
            {

                var activitiesResult = await unitOfWork.ActivitiesManager.GetAsync(a => a.ProcessFlow.ContactID == ID && a.IsSubmitted == false);

                Activity unsubmittedActivity = activitiesResult.FirstOrDefault();

                if (unsubmittedActivity != null)
                {

                    //Get an update the end date of the current activity section .  
                    var activitySectionsResult = await unitOfWork.ActivitySectionsManager.GetAsync(a => a.ActivityID == unsubmittedActivity.ID && a.IsSubmitted == false, includeProperties: "Section,Activity,Section.Questions,Section.Questions.QuestionOptions");

                    ActivitySection currentSection = activitySectionsResult.ToList().FirstOrDefault();

                    if (currentSection != null)
                    {

                        currentSection.EndDate = await HelperFunctions.GetEgyptsCurrentLocalTime();

                        var updateCurrentSectionResult = await unitOfWork.ActivitySectionsManager.UpdateAsync(currentSection);

                        await unitOfWork.SaveChangesAsync();

                        //Create the upcoming section model and return it . 

                        SectionToAnswer sectionToReturn = new SectionToAnswer();

                        sectionToReturn.ID = currentSection.ID;

                        sectionToReturn.NameAR = currentSection.Section.NameAR;

                        sectionToReturn.NameEN = currentSection.Section.NameEN;

                        sectionToReturn.IsFinalSection = false;

                        sectionToReturn.ActivityID = (long)currentSection.ActivityID;

                        sectionToReturn.ActivityTypeID = currentSection.Activity.ActivityTypeID;

                        sectionToReturn.Order = currentSection.Order;

                        sectionToReturn.ActivityTypeSectionOrder = currentSection.Section.Order;

                        sectionToReturn.HasDecisionalQuestions = sectionToReturn.HasDecisionalQuestions;

                        sectionToReturn.Questions = new List<QuestionToAnswer>();

                        if (sectionToReturn.IsFinalSection == false)
                        {

                            //Append section questions . 
                            for (int i = 0; i < currentSection.Section.Questions.Count; i++)
                            {

                                QuestionToAnswer question = new QuestionToAnswer();

                                question.ID = currentSection.Section.Questions[i].ID;

                                question.DescriptionAR = currentSection.Section.Questions[i].DescriptionAR;

                                question.DescriptionEN = currentSection.Section.Questions[i].DescriptionEN;

                                question.isRequired = currentSection.Section.Questions[i].isRequired;

                                question.IsDecisional = currentSection.Section.Questions[i].IsDecisional;

                                question.Type = currentSection.Section.Questions[i].Type;

                                question.Order = currentSection.Section.Questions[i].Order;

                                question.Answer = "";


                                if (question.Type == "Date")
                                {

                                    question.DateAnswer = await HelperFunctions.GetEgyptsCurrentLocalTime();

                                }

                                //Append section question options . 
                                if (currentSection.Section.Questions[i].QuestionOptions.Count > 0)
                                {

                                    question.Options = new List<QuestionOption>();

                                    for (int k = 0; k < currentSection.Section.Questions[i].QuestionOptions.Count; k++)
                                    {

                                        QuestionOption questionOption = new QuestionOption();

                                        questionOption.ValueEN = currentSection.Section.Questions[i].QuestionOptions[k].ValueEN;

                                        questionOption.ValueAR = currentSection.Section.Questions[i].QuestionOptions[k].ValueAR;

                                        questionOption.RoutesTo = currentSection.Section.Questions[i].QuestionOptions[k].RoutesTo;

                                        questionOption.ID = currentSection.Section.Questions[i].QuestionOptions[k].ID;

                                        questionOption.IsSelected = false;

                                        question.Options.Add(questionOption);

                                    }

                                }

                                sectionToReturn.Questions.Add(question);

                            }

                            //Re-arrange section questions . 
                            sectionToReturn.Questions = sectionToReturn.Questions.OrderBy(a => a.Order).ToList();


                        }


                        result.Data = sectionToReturn;

                        result.Succeeded = true;

                        return result;


                    }
                    else
                    {

                        SectionToAnswer sectionToReturn = new SectionToAnswer();

                        sectionToReturn.IsFinalSection = true;

                        sectionToReturn.ActivityID = unsubmittedActivity.ID;

                        result.Data = sectionToReturn;

                        result.Succeeded = true;

                        return result;

                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No unsubmitted activities were found !");
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

        public async Task<ApiResponse<SectionToAnswer>> GetPreviousActivitySection(SectionToAnswer model)
        {
            ApiResponse<SectionToAnswer> result = new ApiResponse<SectionToAnswer>();
            try
            {

                if(model.IsFinalSection == false)
                {
                    var activitySectionsResult = await unitOfWork.ActivitySectionsManager.GetAsync(a => a.ID == model.ID, includeProperties: "Section,Section.Questions,Section.Questions.QuestionOptions");

                    ActivitySection currentSection = activitySectionsResult.ToList().FirstOrDefault();

                    var previousActivitySectionsResult = await unitOfWork.ActivitySectionsManager.GetAsync(a => a.Order == model.Order - 1 && a.ActivityID == model.ActivityID, includeProperties: "Section,Section.Questions,Section.Questions.QuestionOptions");

                    ActivitySection previousSection = previousActivitySectionsResult.ToList().FirstOrDefault();


                    if (previousSection != null && currentSection != null)
                    {

                        long sectionToReturnID = (long)previousSection.SectionID;

                        var removeCurrentSectionResult = await unitOfWork.ActivitySectionsManager.RemoveAsync(currentSection);

                        var removePreviousSectionResult = await unitOfWork.ActivitySectionsManager.RemoveAsync(previousSection);


                        if (removeCurrentSectionResult == true && removePreviousSectionResult == true)
                        {

                            await unitOfWork.SaveChangesAsync();

                            var activityTypeSectionsResult = await unitOfWork.SectionsManager.GetAsync(a => a.ID == sectionToReturnID, includeProperties: "Questions,Questions.QuestionOptions");

                            Section nextSectionReference = activityTypeSectionsResult.ToList().FirstOrDefault();

                            //Create the new activity section .

                            ActivitySection nextSection = new ActivitySection();

                            nextSection.ActivityID = model.ActivityID;

                            nextSection.Order = model.Order - 1;

                            nextSection.SectionID = nextSectionReference.ID;

                            nextSection.StartDate = await HelperFunctions.GetEgyptsCurrentLocalTime();

                            var createNextSectionResult = await unitOfWork.ActivitySectionsManager.CreateAsync(nextSection);

                            await unitOfWork.SaveChangesAsync();

                            if (createNextSectionResult != null)
                            {

                                SectionToAnswer sectionToReturn = new SectionToAnswer();

                                //Create the upcoming section model and return it . 

                                sectionToReturn.ID = nextSection.ID;

                                sectionToReturn.NameAR = nextSectionReference.NameAR;

                                sectionToReturn.NameAR = nextSectionReference.NameAR;

                                sectionToReturn.IsFinalSection = false;

                                sectionToReturn.ActivityID = model.ActivityID;

                                sectionToReturn.ActivityTypeID = model.ActivityTypeID;

                                sectionToReturn.Order = nextSection.Order;

                                sectionToReturn.ActivityTypeSectionOrder = nextSectionReference.Order;

                                sectionToReturn.HasDecisionalQuestions = nextSectionReference.HasDecisionalQuestions;

                                sectionToReturn.HasCreateIntrest = nextSectionReference.HasCreateInterest;

                                sectionToReturn.HasCreateRequest = nextSectionReference.HasCreateRequest;

                                sectionToReturn.HasCreateResale = nextSectionReference.HasCreateResale;

                                sectionToReturn.Questions = new List<QuestionToAnswer>();

                                //Append section questions . 
                                for (int i = 0; i < nextSectionReference.Questions.Count; i++)
                                {

                                    int l = i;

                                    QuestionToAnswer question = new QuestionToAnswer();

                                    question.ID = nextSectionReference.Questions[i].ID;

                                    question.DescriptionAR = nextSectionReference.Questions[i].DescriptionAR;

                                    question.DescriptionEN = nextSectionReference.Questions[i].DescriptionEN;

                                    question.Type = nextSectionReference.Questions[i].Type;

                                    question.isRequired = nextSectionReference.Questions[i].isRequired;

                                    question.IsDecisional = nextSectionReference.Questions[i].IsDecisional;

                                    question.Order = nextSectionReference.Questions[i].Order;


                                    if (question.Type == "Date")
                                    {

                                        question.DateAnswer = await HelperFunctions.GetEgyptsCurrentLocalTime();

                                    }

                                    question.Answer = "";

                                    //Append section question options . 
                                    if (nextSectionReference.Questions[i].QuestionOptions.Count > 0)
                                    {

                                        question.Options = new List<QuestionOption>();

                                        for (int k = 0; k < nextSectionReference.Questions[i].QuestionOptions.Count; k++)
                                        {

                                            int z = k;

                                            QuestionOption questionOption = new QuestionOption();

                                            questionOption.ValueEN = nextSectionReference.Questions[i].QuestionOptions[k].ValueEN;

                                            questionOption.ValueAR = nextSectionReference.Questions[i].QuestionOptions[k].ValueAR;

                                            questionOption.RoutesTo = nextSectionReference.Questions[i].QuestionOptions[k].RoutesTo;

                                            questionOption.ID = nextSectionReference.Questions[i].QuestionOptions[k].ID;

                                            questionOption.IsSelected = false;

                                            question.Options.Add(questionOption);

                                        }

                                    }

                                    sectionToReturn.Questions.Add(question);

                                }

                                //Re-arrange section questions . 
                                sectionToReturn.Questions = sectionToReturn.Questions.OrderBy(a => a.Order).ToList();

                                result.Data = sectionToReturn;

                                result.Succeeded = true;

                                return result;

                            }
                            else
                            {

                                result.Succeeded = false;
                                result.Errors.Add("Failed to go back, Please try again !");
                                result.ErrorType = ErrorType.SystemError;
                                return result;

                            }

                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Failed to go back, Please try again !");
                            result.ErrorType = ErrorType.SystemError;
                            return result;
                        }
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to go back, Please try again !");
                        result.ErrorType = ErrorType.SystemError;
                        return result;
                    }

                }
                else
                {

                    var activitySectionsResult = await unitOfWork.ActivitySectionsManager.GetAsync(a => a.ActivityID == model.ActivityID, includeProperties: "Section,Section.Questions,Section.Questions.QuestionOptions");

                    List<ActivitySection> ActivitySectionsList = activitySectionsResult.OrderByDescending(a => a.Order).ToList();

                    ActivitySection lastActivitySection = ActivitySectionsList.FirstOrDefault();

                    if(lastActivitySection != null)
                    {

                        long sectionToReturnID = (long)lastActivitySection.SectionID;

                        long ActivitySectionOrder = lastActivitySection.Order;

                        var removeLastSectionResult = await unitOfWork.ActivitySectionsManager.RemoveAsync(lastActivitySection);


                        if (removeLastSectionResult == true)
                        {

                            await unitOfWork.SaveChangesAsync();

                            var activityTypeSectionsResult = await unitOfWork.SectionsManager.GetAsync(a => a.ID == sectionToReturnID, includeProperties: "Questions,Questions.QuestionOptions");

                            Section nextSectionReference = activityTypeSectionsResult.ToList().FirstOrDefault();

                            //Create the new activity section .

                            ActivitySection nextSection = new ActivitySection();

                            nextSection.ActivityID = model.ActivityID;

                            nextSection.Order = (int)ActivitySectionOrder;

                            nextSection.SectionID = nextSectionReference.ID;

                            nextSection.StartDate = await HelperFunctions.GetEgyptsCurrentLocalTime();

                            var createNextSectionResult = await unitOfWork.ActivitySectionsManager.CreateAsync(nextSection);

                            await unitOfWork.SaveChangesAsync();

                            if (createNextSectionResult != null)
                            {

                                SectionToAnswer sectionToReturn = new SectionToAnswer();

                                //Create the upcoming section model and return it . 

                                sectionToReturn.ID = nextSection.ID;

                                sectionToReturn.NameAR = nextSectionReference.NameAR;

                                sectionToReturn.NameAR = nextSectionReference.NameAR;

                                sectionToReturn.IsFinalSection = false;

                                sectionToReturn.ActivityID = model.ActivityID;

                                sectionToReturn.ActivityTypeID = model.ActivityTypeID;

                                sectionToReturn.Order = nextSection.Order;

                                sectionToReturn.ActivityTypeSectionOrder = nextSectionReference.Order;

                                sectionToReturn.HasDecisionalQuestions = nextSectionReference.HasDecisionalQuestions;

                                sectionToReturn.HasCreateIntrest = nextSectionReference.HasCreateInterest;

                                sectionToReturn.HasCreateRequest = nextSectionReference.HasCreateRequest;

                                sectionToReturn.HasCreateResale = nextSectionReference.HasCreateResale;

                                sectionToReturn.Questions = new List<QuestionToAnswer>();

                                //Append section questions . 
                                for (int i = 0; i < nextSectionReference.Questions.Count; i++)
                                {

                                    int l = i;

                                    QuestionToAnswer question = new QuestionToAnswer();

                                    question.ID = nextSectionReference.Questions[i].ID;

                                    question.DescriptionAR = nextSectionReference.Questions[i].DescriptionAR;

                                    question.DescriptionEN = nextSectionReference.Questions[i].DescriptionEN;

                                    question.Type = nextSectionReference.Questions[i].Type;

                                    question.isRequired = nextSectionReference.Questions[i].isRequired;

                                    question.IsDecisional = nextSectionReference.Questions[i].IsDecisional;

                                    question.Order = nextSectionReference.Questions[i].Order;


                                    if (question.Type == "Date")
                                    {

                                        question.DateAnswer = await HelperFunctions.GetEgyptsCurrentLocalTime();

                                    }

                                    question.Answer = "";

                                    //Append section question options . 
                                    if (nextSectionReference.Questions[i].QuestionOptions.Count > 0)
                                    {

                                        question.Options = new List<QuestionOption>();

                                        for (int k = 0; k < nextSectionReference.Questions[i].QuestionOptions.Count; k++)
                                        {

                                            int z = k;

                                            QuestionOption questionOption = new QuestionOption();

                                            questionOption.ValueEN = nextSectionReference.Questions[i].QuestionOptions[k].ValueEN;

                                            questionOption.ValueAR = nextSectionReference.Questions[i].QuestionOptions[k].ValueAR;

                                            questionOption.RoutesTo = nextSectionReference.Questions[i].QuestionOptions[k].RoutesTo;

                                            questionOption.ID = nextSectionReference.Questions[i].QuestionOptions[k].ID;

                                            questionOption.IsSelected = false;

                                            question.Options.Add(questionOption);

                                        }

                                    }

                                    sectionToReturn.Questions.Add(question);

                                }

                                //Re-arrange section questions . 
                                sectionToReturn.Questions = sectionToReturn.Questions.OrderBy(a => a.Order).ToList();

                                result.Data = sectionToReturn;

                                result.Succeeded = true;

                                return result;

                            }
                            else
                            {

                                result.Succeeded = false;
                                result.Errors.Add("Failed to go back, Please try again !");
                                result.ErrorType = ErrorType.SystemError;
                                return result;

                            }

                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Failed to go back, Please try again !");
                            result.ErrorType = ErrorType.SystemError;
                            return result;
                        }


                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to go back, Please try again !");
                        result.ErrorType = ErrorType.SystemError;
                        return result;
                    }


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


