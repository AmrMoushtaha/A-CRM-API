using AutoMapper;
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

namespace Stack.ServiceLayer.Modules.Activities
{
    public class ActivitiesService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private static readonly HttpClient client = new HttpClient();

        public ActivitiesService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;

        }

        public async Task<ApiResponse<bool>> CreateActivityType(CreateActivityTypeModel model) 
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                //Check if an activity typr with a duplicate name already exists !

                var duplicateTypeResult = await unitOfWork.ActivityTypesManager.GetAsync(a => a.NameEN == model.NameEN || a.NameAR == model.NameAR);

                List<ActivityType> duplicateTypeList = duplicateTypeResult.ToList();

                if(duplicateTypeList.Count > 0)
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


        public async Task<ApiResponse<SectionToAnswer>> CreateNewActivity(CreateActivityModel model)
        {
            ApiResponse<SectionToAnswer> result = new ApiResponse<SectionToAnswer>();
            try
            {

                Activity newActivity = new Activity();

                newActivity.ProcessFlowID = model.ProcessFlowID;

                newActivity.ActivityTypeID = model.ActivityTypeID;

                newActivity.IsSubmitted = false;

                newActivity.CreationDate = await HelperFunctions.GetEgyptsCurrentLocalTime();

                var createNewActivityResult = await unitOfWork.ActivitiesManager.CreateAsync(newActivity);

                await unitOfWork.SaveChangesAsync();

                //If the activity was created successfully .
                if(createNewActivityResult != null)
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

                    sectionToReturn.Type = UpcomingSection.Type;

                    sectionToReturn.Order = 0;

                    sectionToReturn.HasDecisionalQuestions = UpcomingSection.HasDecisionalQuestions;

                    sectionToReturn.Questions = new List<QuestionToAnswer>();

                    //Append section questions . 
                    for(int i = 0; i < UpcomingSection.Questions.Count; i++)
                    {

                        QuestionToAnswer question = new QuestionToAnswer();

                        question.ID = UpcomingSection.Questions[i].ID;

                        question.DescriptionAR = UpcomingSection.Questions[i].DescriptionAR;

                        question.DescriptionEN = UpcomingSection.Questions[i].DescriptionEN;

                        question.isRequired = UpcomingSection.Questions[i].isRequired;

                        question.IsDecisional = UpcomingSection.Questions[i].IsDecisional;

                        question.Order = UpcomingSection.Questions[i].Order;

                        question.Answer = "";

                        //Append section question options . 
                        if(UpcomingSection.Questions[i].QuestionOptions.Count > 0)
                        {

                            question.Options = new List<QuestionOption>();

                            for(int k = 0; k < UpcomingSection.Questions[i].QuestionOptions.Count; k++)
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



    }
                       
}


