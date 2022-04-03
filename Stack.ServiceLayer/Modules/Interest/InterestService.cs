
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
using Stack.DTOs.Requests.Modules.AreaInterest;
using Stack.Entities.Models.Modules.AreaInterest;
using Stack.DTOs.Models.Modules.AreaInterest;

namespace Stack.ServiceLayer.Modules.Interest
{
    public class InterestService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private static readonly HttpClient client = new HttpClient();

        public InterestService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;

        }
        
        
        #region  Interest

        public async Task<ApiResponse<List<LInterest>>> Get_InterestByLevel(int Level)
        {
            ApiResponse<List<LInterest>> result = new ApiResponse<List<LInterest>>();
            try
            {
                var LevelResult = await unitOfWork.LInterestManager.GetAsync(a => a.Level == Level);
                List<LInterest> LevelList = LevelResult.ToList();
                if (LevelList != null && LevelList.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<LInterest>>(LevelList);
                    return result;
                }

                result.Errors.Add("Failed to find Levels!");
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

        public async Task<ApiResponse<List<LInterest>>> Get_InterestByParentID(long ParentID)
        {
            ApiResponse<List<LInterest>> result = new ApiResponse<List<LInterest>>();
            try
            {
                var LevelResult = await unitOfWork.LInterestManager.GetAsync(a => a.ParentLInterestID == ParentID);
                List<LInterest> LevelList = LevelResult.ToList();
                if (LevelList != null && LevelList.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<LInterest>>(LevelList);
                    return result;
                }

                result.Errors.Add("Failed to find Levels!");
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
        public async Task<ApiResponse<bool>> Create_Interest(LInterestToAdd LInterestToAdd)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var IntersetResult = await unitOfWork.LInterestManager.GetAsync(a => a.DescriptionAR == LInterestToAdd.DescriptionAR || a.DescriptionEN == LInterestToAdd.DescriptionEN);
                LInterest DuplicateLInterest = IntersetResult.FirstOrDefault();

                if (DuplicateLInterest == null)
                {
                    if (LInterestToAdd.IsSeparate && LInterestToAdd.OwnerID != null)
                    {
                        var OwnerResult = await unitOfWork.CustomerManager.GetByIdAsync(LInterestToAdd.OwnerID);
                        if (OwnerResult != null)
                        {
                            return await Save_Interest(LInterestToAdd);
                        }
                        else
                        {
                            result.Errors.Add("Failed to create Interest with selected owner");
                            result.Succeeded = false;
                            return result;
                        }
                    }
                    else if (LInterestToAdd.IsSeparate && (LInterestToAdd.OwnerID == null || LInterestToAdd.OwnerID==0))
                    {
                        result.Errors.Add("Interest cannot be sepearated without owner");
                        result.Succeeded = false;
                        return result;
                    }

                    return await Save_Interest(LInterestToAdd);

                }

                result.Errors.Add("Failed to create Interest!, Try another description");
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

        public async Task<ApiResponse<bool>> Save_Interest(LInterestToAdd LInterestToAdd)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                LInterest LevelToCreate = mapper.Map<LInterest>(LInterestToAdd);
                LevelToCreate.LocationID = LInterestToAdd.LocationID == 0 ?null: LevelToCreate.LocationID;
                LevelToCreate.ParentLInterestID = LInterestToAdd.ParentLInterestID == 0 ?null: LevelToCreate.ParentLInterestID;
                LevelToCreate.OwnerID = LInterestToAdd.OwnerID == 0 ?null: LevelToCreate.OwnerID;

                var createLevelResult = await unitOfWork.LInterestManager.CreateAsync(LevelToCreate);
                var SaveResult = await unitOfWork.SaveChangesAsync();

                if (SaveResult)
                {
                    result.Succeeded = true;
                    result.Data = true;
                    return result;
                }
                else
                {
                    result.Errors.Add("Failed to create Interest");
                    result.Succeeded = false;
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

        public async Task<ApiResponse<bool>> Delete_Interest(long LInterestToDelete)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var IntersetResult = await unitOfWork.LInterestManager.GetByIdAsync(LInterestToDelete);

                if (IntersetResult != null)
                {
                    IntersetResult.IsDeleted = true;
                    var UpdateResult = await unitOfWork.LInterestManager.UpdateAsync(IntersetResult);
                    var SaveResult = await unitOfWork.SaveChangesAsync();

                    if (SaveResult)
                    {
                        result.Succeeded = true;
                        return result;
                    }

                }

                result.Errors.Add("Failed to delete Interest!");
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

        #region  Interest input

        public async Task<ApiResponse<List<LInterestInput>>> Get_Inputs()
        {
            ApiResponse<List<LInterestInput>> result = new ApiResponse<List<LInterestInput>>();
            try
            {
                var InputsResult = await unitOfWork.LInterestInputManager.GetAsync();
                List<LInterestInput> InputsList = InputsResult.ToList();
                if (InputsList != null && InputsList.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<LInterestInput>>(InputsList);
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
                var LInterestInputResult = await unitOfWork.LInterestInputManager.GetAsync(a => a.LabelEN == InputToAdd.LabelEN || a.LabelAR == InputToAdd.LabelAR);
                LInterestInput DuplicateLInterestResult = LInterestInputResult.FirstOrDefault();

                if (DuplicateLInterestResult == null)
                {
                    LInterestInput InputToCreate = mapper.Map<LInterestInput>(InputToAdd); ;
                    var createInputResult = await unitOfWork.LInterestInputManager.CreateAsync(InputToCreate);
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
                var LInterestInputResult = await unitOfWork.LInterestInputManager.GetAsync(a =>( a.LabelEN == InputToEdit.LabelEN || a.LabelAR == InputToEdit.LabelAR)&& a.ID!= InputToEdit.ID);
                LInterestInput DuplicateLInterestResult = LInterestInputResult.FirstOrDefault();
                var InputResult = await unitOfWork.LInterestInputManager.GetByIdAsync( InputToEdit.ID);

                if (DuplicateLInterestResult == null && InputResult !=null)
                {
                    InputResult.LabelAR = InputToEdit.LabelAR;
                    InputResult.LabelEN = InputToEdit.LabelEN;
                    InputResult.Type = InputToEdit.Type;
                    InputResult.Attachment = InputToEdit.Attachment;
                    var createInputResult = await unitOfWork.LInterestInputManager.UpdateAsync(InputResult);
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
                var InputResult = await unitOfWork.LInterestInputManager.GetByIdAsync(InputToDelete);

                if (InputResult != null)
                {
                    InputResult.IsDeleted = true;
                    var UpdateResult = await unitOfWork.LInterestInputManager.UpdateAsync(InputResult);
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

        #region Interest-Input
        public async Task<ApiResponse<bool>> Create_LInterestInput(LInterestInputToAdd LInterestInputToAdd)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var LInterestInputResult = await unitOfWork.LInterest_LInterestInputManager.GetAsync(a => a.LInterestID == LInterestInputToAdd.LInterestID && a.LInterestInputID == LInterestInputToAdd.LInterestInputID);
                LInterest_LInterestInput DuplicateLInterestResult = LInterestInputResult.FirstOrDefault();

                if (DuplicateLInterestResult == null)
                {
                    LInterest_LInterestInput InputToCreate = mapper.Map<LInterest_LInterestInput>(LInterestInputToAdd); ;
                    var createInputResult = await unitOfWork.LInterest_LInterestInputManager.CreateAsync(InputToCreate);
                    await unitOfWork.SaveChangesAsync();

                    if (createInputResult != null)
                    {
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Errors.Add("Failed to create Interest Input");
                        result.Succeeded = false;
                        return result;
                    }


                }

                result.Errors.Add("Failed to create Interest Input!");
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

        public async Task<ApiResponse<bool>> Delete_LInterestInput(long ID)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var InputResult = await unitOfWork.LInterest_LInterestInputManager.GetByIdAsync(ID);

                if (InputResult != null)
                {
                    InputResult.IsDeleted = true;
                    var UpdateResult = await unitOfWork.LInterest_LInterestInputManager.UpdateAsync(InputResult);
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

        public async Task<ApiResponse<List<LInterest>>> Get_LInterestByInputID(long InputID)
        {
            ApiResponse<List<LInterest>> result = new ApiResponse<List<LInterest>>();
            try
            {
                var LevelResult = (from interestAttribute in (await unitOfWork.LInterest_LInterestInputManager.GetAsync(a => a.LInterestInputID == InputID))
                                   join interest in (await unitOfWork.LInterestManager.GetAsync()) on interestAttribute.LInterestID equals interest.ID
                                   select new LInterest
                                   {
                                       ID = interest.ID,
                                       DescriptionAR = interest.DescriptionAR,
                                       DescriptionEN = interest.DescriptionEN,
                                       IsSeparate = interest.IsSeparate,
                                       Level = interest.Level,
                                       Type = interest.Type,
                                       LocationID = interest.LocationID,
                                       OwnerID = interest.OwnerID,
                                       ParentLInterestID = interest.ParentLInterestID,
                                   }).ToList();

                if (LevelResult != null && LevelResult.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = LevelResult;
                    return result;
                }

                result.Errors.Add("Failed to find Levels!");
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

        public async Task<ApiResponse<List<InterestAttribute>>> Get_Attributes()
        {
            ApiResponse<List<InterestAttribute>> result = new ApiResponse<List<InterestAttribute>>();
            try
            {
                var InterestAttributeResult = await unitOfWork.InterestAttributesManager.GetAsync();
                List<InterestAttribute> InterestAttributeList = InterestAttributeResult.ToList();
                if (InterestAttributeList != null && InterestAttributeList.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<InterestAttribute>>(InterestAttributeList);
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
                var InterestAttributeResult = await unitOfWork.InterestAttributesManager.GetAsync(a => a.LabelEN == AttributeToAdd.LabelEN || a.LabelAR == AttributeToAdd.LabelAR);
                InterestAttribute DuplicateInterestAttributeResult = InterestAttributeResult.FirstOrDefault();

                if (DuplicateInterestAttributeResult == null)
                {
                    InterestAttribute InputToCreate = mapper.Map<InterestAttribute>(AttributeToAdd); ;
                    var createInputResult = await unitOfWork.InterestAttributesManager.CreateAsync(InputToCreate);
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
                var AttributeResult = await unitOfWork.InterestAttributesManager.GetAsync(a => (a.LabelEN == AttributeToEdit.LabelEN || a.LabelAR == AttributeToEdit.LabelAR) && a.ID != AttributeToEdit.ID);
                InterestAttribute DuplicateAttributeResult = AttributeResult.FirstOrDefault();
                var AttributeR = await unitOfWork.InterestAttributesManager.GetByIdAsync(AttributeToEdit.ID);

                if (DuplicateAttributeResult == null && AttributeR != null)
                {
                    AttributeR.LabelAR = AttributeToEdit.LabelAR;
                    AttributeR.LabelEN = AttributeToEdit.LabelEN;

                    var createInputResult = await unitOfWork.InterestAttributesManager.UpdateAsync(AttributeR);
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
                var AttributeResult = await unitOfWork.InterestAttributesManager.GetByIdAsync(ID);

                if (AttributeResult != null)
                {
                    AttributeResult.IsDeleted = true;
                    var UpdateResult = await unitOfWork.InterestAttributesManager.UpdateAsync(AttributeResult);
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

        #region  Interest-attribute

        public async Task<ApiResponse<bool>> Create_LInterestAttribute(InterestAttributeToAdd InterestAttributeToAdd)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var AttributeResult = await unitOfWork.LInterest_InterestAttributeManager.GetAsync(a => a.LInterestID == InterestAttributeToAdd.LInterestID && a.InterestAttributeID == InterestAttributeToAdd.InterestAttributeID);
                LInterest_InterestAttribute DuplicateLInterestResult = AttributeResult.FirstOrDefault();

                if (DuplicateLInterestResult == null)
                {
                    LInterest_InterestAttribute InputToCreate = mapper.Map<LInterest_InterestAttribute>(InterestAttributeToAdd); ;
                    var createInputResult = await unitOfWork.LInterest_InterestAttributeManager.CreateAsync(InputToCreate);
                    await unitOfWork.SaveChangesAsync();

                    if (createInputResult != null)
                    {
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Errors.Add("Failed to create Attribute ");
                        result.Succeeded = false;
                        return result;
                    }

                }
                result.Errors.Add("Failed to create Attribute!");
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

        public async Task<ApiResponse<bool>> Delete_LInterestAttribute(long ID)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var AttributeResult = await unitOfWork.LInterest_InterestAttributeManager.GetByIdAsync(ID);

                if (AttributeResult != null)
                {
                    AttributeResult.IsDeleted = true;
                    var UpdateResult = await unitOfWork.LInterest_InterestAttributeManager.UpdateAsync(AttributeResult);
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

        public async Task<ApiResponse<List<LInterest>>> Get_LInterestByAttributeID(long AttributeID)
        {
            ApiResponse<List<LInterest>> result = new ApiResponse<List<LInterest>>();
            try
            {
                var LevelResult = (from interestAttribute in (await unitOfWork.LInterest_InterestAttributeManager.GetAsync(a => a.InterestAttributeID == AttributeID)) join
                                   interest in (await unitOfWork.LInterestManager.GetAsync()) on interestAttribute.LInterestID equals interest.ID
                                   select new LInterest
                                   {
                                       ID = interest.ID,
                                       DescriptionAR=interest.DescriptionAR,
                                       DescriptionEN=interest.DescriptionEN,
                                       IsSeparate=interest.IsSeparate,
                                       Level=interest.Level,
                                       Type=interest.Type,
                                       LocationID=interest.LocationID,
                                       OwnerID = interest.OwnerID,
                                       ParentLInterestID=interest.ParentLInterestID,
                                   }).ToList();

                if (LevelResult != null && LevelResult.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = LevelResult;
                    return result;
                }

                result.Errors.Add("Failed to find Levels!");
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


