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
using Stack.Entities.Models.Modules.Auth;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Stack.DTOs.Models.Initialization.ActivityTypes;
using Microsoft.Extensions.Logging;
using System.Linq;
using Stack.DTOs.Requests.Modules.System;
using Stack.Entities.Enums.Modules.Auth;

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
        public static List<DTOs.Requests.Modules.System.AuthorizationSection> _systemAuthorizationSections;

        public SystemInitializationService( IOptions<List<ActivityTypeModel>> DefaultActivityTypes, UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor, IOptions<List<DTOs.Requests.Modules.System.AuthorizationSection>> authorizationSections)
        {
            this.config = config;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            _defaultActivityTypes = DefaultActivityTypes.Value;
            _systemAuthorizationSections = authorizationSections.Value;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<ApiResponse<bool>> InitializeSystem(string Password)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                //Initialize system roles
                if (!await unitOfWork.RoleManager.RoleExistsAsync(UserRoles.Administrator.ToString()))
                {
                    var role = new ApplicationRole();
                    role.Name = UserRoles.Administrator.ToString();
                    var res = await unitOfWork.RoleManager.CreateAsync(role);
                    await unitOfWork.SaveChangesAsync();
                }

                ApplicationUser user = new ApplicationUser
                {

                    UserName = "SysAdmin",
                    FirstName = "Administrator",
                    LastName = "Administrator",
                    PhoneNumber = "01000000000",
                    LockoutEnabled = true
                };

                var createUserResult = await unitOfWork.UserManager.CreateAsync(user, Password);

                if (createUserResult.Succeeded)
                {
                    var roleAssignmentRes = await unitOfWork.UserManager.AddToRoleAsync(user, UserRoles.Administrator.ToString());
                    if (roleAssignmentRes.Succeeded)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Data = false;
                        result.Errors.Add("Administrator creation failed!");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Data = false;
                    result.Errors.Add("Administrator creation failed!");
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

                        typeToCreate.ColorCode = _defaultActivityTypes[i].ColorCode;

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

        public async Task<ApiResponse<bool>> InitializeSystemAuthorizationsScheme()
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                if(_systemAuthorizationSections != null)
                {

                    for(int i = 0; i < _systemAuthorizationSections.Count; i++)
                    {

                        var sectionExists = await unitOfWork.AuthorizationSectionsManager.GetAsync(a => a.Code == _systemAuthorizationSections[i].Code);

                        Entities.Models.Modules.Auth.AuthorizationSection existingSection = sectionExists.FirstOrDefault();

                        if(existingSection != null)
                        {
                            for (int j = 0; j < _systemAuthorizationSections[i].SectionAuthorizations.Count; j++)
                            {

                                var sectionAuthorizationExists = await unitOfWork.SectionAuthorizationsManager.GetAsync(a => a.Code == _systemAuthorizationSections[i].Code && a.AuthorizationSectionID == existingSection.ID);

                                Entities.Models.Modules.Auth.SectionAuthorization existingSectionAuthorization = sectionAuthorizationExists.FirstOrDefault();

                                if(existingSectionAuthorization == null)
                                {
                                    Entities.Models.Modules.Auth.SectionAuthorization newSectionAuthorization = new Entities.Models.Modules.Auth.SectionAuthorization();

                                    newSectionAuthorization.NameEN = _systemAuthorizationSections[i].SectionAuthorizations[j].NameEN;

                                    newSectionAuthorization.NameAR = _systemAuthorizationSections[i].SectionAuthorizations[j].NameAR;

                                    newSectionAuthorization.Code = _systemAuthorizationSections[i].SectionAuthorizations[j].Code;

                                    newSectionAuthorization.AuthorizationSectionID = existingSection.ID;

                                    var createSectionAuthorizationResult = await unitOfWork.SectionAuthorizationsManager.CreateAsync(newSectionAuthorization);
                                }
                         
                            }

                            await unitOfWork.SaveChangesAsync();
                        }
                        else
                        {
                            Entities.Models.Modules.Auth.AuthorizationSection newSection = new Entities.Models.Modules.Auth.AuthorizationSection();

                            newSection.NameAR = _systemAuthorizationSections[i].NameAR;

                            newSection.NameEN = _systemAuthorizationSections[i].NameEN;

                            newSection.Code = _systemAuthorizationSections[i].Code;

                            var createAuthSectionResult = await unitOfWork.AuthorizationSectionsManager.CreateAsync(newSection);

                            await unitOfWork.SaveChangesAsync();

                            if (createAuthSectionResult != null)
                            {

                                for (int j = 0; j < _systemAuthorizationSections[i].SectionAuthorizations.Count; j++)
                                {

                                    var sectionAuthorizationExists = await unitOfWork.SectionAuthorizationsManager.GetAsync(a => a.Code == _systemAuthorizationSections[i].Code && a.AuthorizationSectionID == createAuthSectionResult.ID);

                                    Entities.Models.Modules.Auth.SectionAuthorization existingSectionAuthorization = sectionAuthorizationExists.FirstOrDefault();

                                    if (existingSectionAuthorization == null)
                                    {
                                        Entities.Models.Modules.Auth.SectionAuthorization newSectionAuthorization = new Entities.Models.Modules.Auth.SectionAuthorization();

                                        newSectionAuthorization.NameEN = _systemAuthorizationSections[i].SectionAuthorizations[j].NameEN;

                                        newSectionAuthorization.NameAR = _systemAuthorizationSections[i].SectionAuthorizations[j].NameAR;

                                        newSectionAuthorization.Code = _systemAuthorizationSections[i].SectionAuthorizations[j].Code;

                                        newSectionAuthorization.AuthorizationSectionID = createAuthSectionResult.ID;

                                        var createSectionAuthorizationResult = await unitOfWork.SectionAuthorizationsManager.CreateAsync(newSectionAuthorization);
                                    }

                                }


                                await unitOfWork.SaveChangesAsync();

                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("Error : Failed to initialize system authorizations scheme !");
                                return result;
                            }
                        }
                        
                    }

                    result.Succeeded = true;
                    result.Data = true;
                    return result;

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Error : Failed to initialize system authorizations scheme !");
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


