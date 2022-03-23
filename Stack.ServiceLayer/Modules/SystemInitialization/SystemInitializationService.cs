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
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Stack.DTOs.Models.Initialization.ActivityTypes;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Stack.ServiceLayer.Modules.SystemInitialization
{
    public class SystemInitializationService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private static readonly HttpClient client = new HttpClient();
        public static List<ActivityTypeModel> _defaultActivityTypes;

        public SystemInitializationService( IOptions<List<ActivityTypeModel>> DefaultActivityTypes, UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;
            _defaultActivityTypes = DefaultActivityTypes.Value;
        }


        public async Task<ApiResponse<bool>> InitializeDefaultSystemActivityTypes()
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                for(int i = 0; i < _defaultActivityTypes.Count; i++)
                {

                    var activityTypesResult = await unitOfWork.ActivityTypesManager.GetAsync(a => a.NameAR == _defaultActivityTypes[i].NameAR || a.NameEN == _defaultActivityTypes[i].NameEN);

                    ActivityType DuplicateType = activityTypesResult.FirstOrDefault();

                    if(DuplicateType == null)
                    {

                        ActivityType typeToCreate = new ActivityType();

                        typeToCreate.NameEN = _defaultActivityTypes[i].NameEN;

                        typeToCreate.NameAR = _defaultActivityTypes[i].NameAR;

                        typeToCreate.Status = _defaultActivityTypes[i].Status;

                        typeToCreate.CreatedBy = "System";

                        var createActivityTypeResult = await unitOfWork.ActivityTypesManager.CreateAsync(typeToCreate);

                        await unitOfWork.SaveChangesAsync();

                        if(createActivityTypeResult != null)
                        {

                            for (int j = 0; j < _defaultActivityTypes[i].Sections.Count; j++)
                            {

                                Section sectionToCreate = new Section();

                                sectionToCreate.NameEN = _defaultActivityTypes[i].Sections[j].NameEN;

                                sectionToCreate.NameAR = _defaultActivityTypes[i].Sections[j].NameAR;

                                sectionToCreate.Order = _defaultActivityTypes[i].Sections[j].Order;

                                sectionToCreate.HasDecisionalQuestions = _defaultActivityTypes[i].Sections[j].HasDecisionalQuestions;

                                sectionToCreate.HasCreateInterest = _defaultActivityTypes[i].Sections[j].HasCreateInterest;

                                sectionToCreate.HasCreateRequest = _defaultActivityTypes[i].Sections[j].HasCreateRequest;

                                sectionToCreate.HasCreateResale = _defaultActivityTypes[i].Sections[j].HasCreateResale;

                                sectionToCreate.ActivityTypeID = typeToCreate.ID;

                                var createSectionResult = await unitOfWork.SectionsManager.CreateAsync(sectionToCreate);

                                await unitOfWork.SaveChangesAsync();

                                if (_defaultActivityTypes[i].Sections[j].Questions != null )
                                {

                                    for (int k = 0; k < _defaultActivityTypes[i].Sections[j].Questions.Count; k++)
                                    {

                                        SectionQuestion questionToCreate = new SectionQuestion();

                                        questionToCreate.SectionID = sectionToCreate.ID;

                                        questionToCreate.DescriptionEN = _defaultActivityTypes[i].Sections[j].Questions[k].DescriptionEN;

                                        questionToCreate.DescriptionAR = _defaultActivityTypes[i].Sections[j].Questions[k].DescriptionAR;

                                        questionToCreate.Type = _defaultActivityTypes[i].Sections[j].Questions[k].Type;

                                        questionToCreate.Order = _defaultActivityTypes[i].Sections[j].Questions[k].Order;

                                        questionToCreate.isRequired = _defaultActivityTypes[i].Sections[j].Questions[k].IsRequired;

                                        questionToCreate.IsDecisional = _defaultActivityTypes[i].Sections[j].Questions[k].IsDecisional;

                                        var createSectionQuestionResult = await unitOfWork.SectionQuestionsManager.CreateAsync(questionToCreate);

                                        await unitOfWork.SaveChangesAsync();


                                        if (_defaultActivityTypes[i].Sections[j].Questions[k].QuestionOptions != null )
                                        {

                                            for (int l = 0; l < _defaultActivityTypes[i].Sections[j].Questions[k].QuestionOptions.Count; l++)
                                            {

                                                SectionQuestionOption optionToCreate = new SectionQuestionOption();

                                                optionToCreate.ValueEN = _defaultActivityTypes[i].Sections[j].Questions[k].QuestionOptions[l].ValueEN;

                                                optionToCreate.ValueAR = _defaultActivityTypes[i].Sections[j].Questions[k].QuestionOptions[l].ValueAR;

                                                optionToCreate.RoutesTo = _defaultActivityTypes[i].Sections[j].Questions[k].QuestionOptions[l].RoutesTo;

                                                optionToCreate.QuestionID = questionToCreate.ID;

                                                var createSectionQuestionOptionResult = await unitOfWork.SectionQuestionOptionsManager.CreateAsync(optionToCreate);

                                                await unitOfWork.SaveChangesAsync();

                                            }


                                        }

                                    }

                                }
                            }

                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Data = false;
                            return result;
                        }
                        
                    }
                  
                }

                result.Succeeded = true;
                result.Data = true;
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


