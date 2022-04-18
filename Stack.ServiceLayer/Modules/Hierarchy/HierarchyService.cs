
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using Stack.DTOs;
using Stack.DTOs.Enums;
using Stack.DTOs.Requests.Modules.Hierarchy;
using Stack.DTOs.Requests.Modules.Interest;
using Stack.Entities.Models.Modules.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Stack.ServiceLayer.Modules.Hierarchy
{
    public class HierarchyService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private static readonly HttpClient client = new HttpClient();

        public HierarchyService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;

        }
        
        
        #region  Level

        public async Task<ApiResponse<List<Level>>> Get_Levels()
        {
            ApiResponse<List<Level>> result = new ApiResponse<List<Level>>();
            try
            {
                var LevelResult = await unitOfWork.LevelManager.GetAsync();
                List<Level> LevelsList = LevelResult.ToList();
                if (LevelsList != null && LevelsList.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<Level>>(LevelsList);
                    return result;
                }

                result.Errors.Add("Failed to find levels !");
                result.Succeeded = false;
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

        public async Task<ApiResponse<bool>> Create_Level(LevelToAdd LevelToAdd)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var InterestLevelResult = await unitOfWork.LevelManager.GetAsync(a => a.LabelAR == LevelToAdd.LabelAR
                || a.LabelEN == LevelToAdd.LabelEN);
                Level DuplicateLevelResult = InterestLevelResult.FirstOrDefault();

                if (DuplicateLevelResult == null)
                {
                    Level levelToCreate = mapper.Map<Level>(LevelToAdd); ;
                    var createLevelResult = await unitOfWork.LevelManager.CreateAsync(levelToCreate);
                    await unitOfWork.SaveChangesAsync();

                    if (createLevelResult != null)
                    {
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Errors.Add("Failed to create level");
                        result.Succeeded = false;
                        return result;
                    }


                }

                result.Errors.Add("Failed to create level!, Try another label");
                result.Succeeded = false;
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

        public async Task<ApiResponse<bool>> Edit_Level(LevelToEdit LevelToEdit)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var LevelResult = await unitOfWork.LevelManager.GetAsync(a => (a.LabelEN == LevelToEdit.LabelEN 
                || a.LabelAR == LevelToEdit.LabelAR) && a.ID != LevelToEdit.ID);
                Level DuplicateLevelResult = LevelResult.FirstOrDefault();
                var LevelR = await unitOfWork.LevelManager.GetByIdAsync(LevelToEdit.ID);

                if (DuplicateLevelResult == null && LevelR != null)
                {
                    LevelR.LabelAR = LevelToEdit.LabelAR;
                    LevelR.LabelEN = LevelToEdit.LabelEN;
                    LevelR.Type = LevelToEdit.Type;
                    
                    var createInputResult = await unitOfWork.LevelManager.UpdateAsync(LevelR);
                    var SaveResult = await unitOfWork.SaveChangesAsync();

                    if (SaveResult)
                    {
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Errors.Add("Failed to update Level");
                        result.Succeeded = false;
                        return result;
                    }


                }

                result.Errors.Add("Failed to update Level!, Try another Label");
                result.Succeeded = false;
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

        #endregion


        #region  Section
        public async Task<ApiResponse<List<LSection>>> Get_SectionsByLevelID(long levelID)
        {
            ApiResponse<List<LSection>> result = new ApiResponse<List<LSection>>();
            try
            {
                var SectionResult = await unitOfWork.SectionManager.GetAsync(a=>a.LevelID== levelID);
                List<LSection> SectionsList = SectionResult.ToList();
                if (SectionsList != null && SectionsList.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<LSection>>(SectionsList);
                    return result;
                }

                result.Errors.Add("Failed to find Sections !");
                result.Succeeded = false;
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

        public async Task<ApiResponse<List<LSection>>> Get_Sections()
        {
            ApiResponse<List<LSection>> result = new ApiResponse<List<LSection>>();
            try
            {
                var SectionResult = await unitOfWork.SectionManager.GetAsync();
                List<LSection> SectionsList = SectionResult.ToList();
                if (SectionsList != null && SectionsList.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<LSection>>(SectionsList);
                    return result;
                }

                result.Errors.Add("Failed to find Sections !");
                result.Succeeded = false;
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

        public async Task<ApiResponse<bool>> Create_Section(SectionToAdd SectionToAdd)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var SectionResult = await unitOfWork.SectionManager.GetAsync(a => (a.LabelAR == SectionToAdd.LabelAR
                || a.LabelEN == SectionToAdd.LabelEN)&& a.LevelID==SectionToAdd.LevelID );
                LSection DuplicateSectionResult = SectionResult.FirstOrDefault();

                if (DuplicateSectionResult == null)
                {
                    LSection SectionToCreate = mapper.Map<LSection>(SectionToAdd); ;
                    var createSectionResult = await unitOfWork.SectionManager.CreateAsync(SectionToCreate);
                    await unitOfWork.SaveChangesAsync();

                    if (createSectionResult != null)
                    {
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Errors.Add("Failed to create section");
                        result.Succeeded = false;
                        return result;
                    }


                }

                result.Errors.Add("Failed to create section!, Try another label");
                result.Succeeded = false;
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
    
        public async Task<ApiResponse<bool>> Edit_Section(SectionToEdit SectionToEdit)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var SectionResult = await unitOfWork.SectionManager.GetAsync(a => (a.LabelEN == SectionToEdit.LabelEN
                || a.LabelAR == SectionToEdit.LabelAR) && a.ID != SectionToEdit.ID && a.LevelID==SectionToEdit.LevelID);
                LSection DuplicateSectionResult = SectionResult.FirstOrDefault();
                var SectionR = await unitOfWork.SectionManager.GetByIdAsync(SectionToEdit.ID);

                if (DuplicateSectionResult == null && SectionR != null)
                {
                    SectionR.LabelAR = SectionToEdit.LabelAR;
                    SectionR.LabelEN = SectionToEdit.LabelEN;
                    SectionR.LevelID = SectionToEdit.LevelID;

                    var createInputResult = await unitOfWork.SectionManager.UpdateAsync(SectionR);
                    var SaveResult = await unitOfWork.SaveChangesAsync();

                    if (SaveResult)
                    {
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Errors.Add("Failed to update section");
                        result.Succeeded = false;
                        return result;
                    }


                }

                result.Errors.Add("Failed to update section!, Try another Label");
                result.Succeeded = false;
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

        #endregion


        #region  attribute

        public async Task<ApiResponse<List<LAttribute>>> Get_Attributes()
        {
            ApiResponse<List<LAttribute>> result = new ApiResponse<List<LAttribute>>();
            try
            {
                var InterestAttributeResult = await unitOfWork.AttributesManager.GetAsync();
                List<LAttribute> InterestAttributeList = InterestAttributeResult.ToList();
                if (InterestAttributeList != null && InterestAttributeList.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<LAttribute>>(InterestAttributeList);
                    return result;
                }

                result.Errors.Add("Failed to find Attributes !");
                result.Succeeded = false;
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

        public async Task<ApiResponse<bool>> Create_Attribute(AttributeToAdd AttributeToAdd)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var InterestAttributeResult = await unitOfWork.AttributesManager.GetAsync(a => a.LabelEN == AttributeToAdd.LabelEN || a.LabelAR == AttributeToAdd.LabelAR);
                LAttribute DuplicateInterestAttributeResult = InterestAttributeResult.FirstOrDefault();

                if (DuplicateInterestAttributeResult == null)
                {
                    LAttribute InputToCreate = mapper.Map<LAttribute>(AttributeToAdd); ;
                    var createInputResult = await unitOfWork.AttributesManager.CreateAsync(InputToCreate);
                    await unitOfWork.SaveChangesAsync();

                    if (createInputResult != null)
                    {
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Errors.Add("Failed to create Attribute");
                        result.Succeeded = false;
                        return result;
                    }


                }

                result.Errors.Add("Failed to create Attribute!, Try another label");
                result.Succeeded = false;
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

        public async Task<ApiResponse<bool>> Edit_Attribute(AttributeToEdit AttributeToEdit)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var AttributeResult = await unitOfWork.AttributesManager.GetAsync(a => 
                (a.LabelEN == AttributeToEdit.LabelEN || a.LabelAR == AttributeToEdit.LabelAR) && a.ID != AttributeToEdit.ID && a.ParentAttributeID==AttributeToEdit.ParentAttributeID);
                LAttribute DuplicateAttributeResult = AttributeResult.FirstOrDefault();
                var AttributeR = await unitOfWork.AttributesManager.GetByIdAsync(AttributeToEdit.ID);

                if (DuplicateAttributeResult == null && AttributeR != null)
                {
                    AttributeR.LabelAR = AttributeToEdit.LabelAR;
                    AttributeR.LabelEN = AttributeToEdit.LabelEN;
                    AttributeR.ParentAttributeID = AttributeToEdit.ParentAttributeID;

                    var createInputResult = await unitOfWork.AttributesManager.UpdateAsync(AttributeR);
                    var SaveResult = await unitOfWork.SaveChangesAsync();

                    if (SaveResult)
                    {
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Errors.Add("Failed to update Attribute");
                        result.Succeeded = false;
                        return result;
                    }


                }

                result.Errors.Add("Failed to update Attribute!, Try another label");
                result.Succeeded = false;
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

        public async Task<ApiResponse<bool>> Delete_Attribute(long ID)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var AttributeResult = await unitOfWork.AttributesManager.GetByIdAsync(ID);

                if (AttributeResult != null)
                {
                    AttributeResult.IsDeleted = true;
                    var UpdateResult = await unitOfWork.AttributesManager.UpdateAsync(AttributeResult);
                    var SaveResult = await unitOfWork.SaveChangesAsync();
                    if (SaveResult)
                    {
                        result.Succeeded = true;
                        return result;
                    }

                }

                result.Errors.Add("Failed to delete Attribute!");
                result.Succeeded = false;
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
        #endregion


        #region   input

        public async Task<ApiResponse<List<Input>>> Get_Inputs()
        {
            ApiResponse<List<Input>> result = new ApiResponse<List<Input>>();
            try
            {
                var InputsResult = await unitOfWork.LInputManager.GetAsync();
                List<Input> InputsList = InputsResult.ToList();
                if (InputsList != null && InputsList.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<Input>>(InputsList);
                    return result;
                }

                result.Errors.Add("Failed to find Inputs!");
                result.Succeeded = false;
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

        public async Task<ApiResponse<List<Input>>> Get_InputsByLevelID(long LevelID)
        {
            ApiResponse<List<Input>> result = new ApiResponse<List<Input>>();
            try
            {
                var InputsResult = await unitOfWork.LInputManager.GetAsync(a => a.Section.LevelID == LevelID);
                List<Input> InputsList = InputsResult.ToList();
                if (InputsList != null && InputsList.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<Input>>(InputsList);
                    return result;
                }

                result.Errors.Add("Failed to find Inputs!");
                result.Succeeded = false;
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

        public async Task<ApiResponse<List<Input>>> Get_InputsBySectionID(long sectionID)
        {
            ApiResponse<List<Input>> result = new ApiResponse<List<Input>>();
            try
            {
                var InputsResult = await unitOfWork.LInputManager.GetAsync(a=>a.SectionID==sectionID);
                List<Input> InputsList = InputsResult.ToList();
                if (InputsList != null && InputsList.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<Input>>(InputsList);
                    return result;
                }

                result.Errors.Add("Failed to find Inputs!");
                result.Succeeded = false;
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
      
        public async Task<ApiResponse<bool>> Create_Input(InputToAdd InputToAdd)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var LInterestInputResult = await unitOfWork.LInputManager.GetAsync(a => a.LabelEN == InputToAdd.LabelEN || a.LabelAR == InputToAdd.LabelAR);
                Input DuplicateLInterestResult = LInterestInputResult.FirstOrDefault();

                if (DuplicateLInterestResult == null)
                {
                    Input InputToCreate = mapper.Map<Input>(InputToAdd); ;
                    var createInputResult = await unitOfWork.LInputManager.CreateAsync(InputToCreate);
                    await unitOfWork.SaveChangesAsync();

                    if (createInputResult != null)
                    {
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Errors.Add("Failed to create Input");
                        result.Succeeded = false;
                        return result;
                    }


                }

                result.Errors.Add("Failed to create Input!, Try another label");
                result.Succeeded = false;
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

        public async Task<ApiResponse<bool>> Edit_Input(InputToEdit InputToEdit)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var LInterestInputResult = await unitOfWork.LInputManager.GetAsync(a => (a.LabelEN == InputToEdit.LabelEN || a.LabelAR == InputToEdit.LabelAR) && a.ID != InputToEdit.ID);
                Input DuplicateLInterestResult = LInterestInputResult.FirstOrDefault();
                var InputResult = await unitOfWork.LInputManager.GetByIdAsync(InputToEdit.ID);

                if (DuplicateLInterestResult == null && InputResult != null)
                {
                    InputResult.LabelAR = InputToEdit.LabelAR;
                    InputResult.LabelEN = InputToEdit.LabelEN;
                    InputResult.Type = InputToEdit.Type;
                    var createInputResult = await unitOfWork.LInputManager.UpdateAsync(InputResult);
                    var SaveResult = await unitOfWork.SaveChangesAsync();

                    if (SaveResult)
                    {
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Errors.Add("Failed to update Input");
                        result.Succeeded = false;
                        return result;
                    }


                }

                result.Errors.Add("Failed to update Input!, Try another label");
                result.Succeeded = false;
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

        public async Task<ApiResponse<bool>> Delete_Input(long InputToDelete)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var InputResult = await unitOfWork.LInputManager.GetByIdAsync(InputToDelete);

                if (InputResult != null)
                {
                    InputResult.IsDeleted = true;
                    var UpdateResult = await unitOfWork.LInputManager.UpdateAsync(InputResult);
                    var SaveResult = await unitOfWork.SaveChangesAsync();
                    if (SaveResult)
                    {
                        result.Succeeded = true;
                        return result;
                    }

                }

                result.Errors.Add("Failed to delete Input!");
                result.Succeeded = false;
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
        #endregion


    }

}


