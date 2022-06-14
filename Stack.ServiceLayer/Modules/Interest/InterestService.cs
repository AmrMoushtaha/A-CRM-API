
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using Stack.DTOs;
using Stack.DTOs.Enums;
using Stack.DTOs.Requests.Modules.Interest;
using Stack.Entities.Models.Modules.Interest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;

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
        

        public async Task<ApiResponse<List<LInterest>>> Get_InterestByLevel(long Level)
        {
            ApiResponse<List<LInterest>> result = new ApiResponse<List<LInterest>>();
            try
            {
                var LevelResult = await unitOfWork.LInterestManager.GetAsync(a => a.LevelID == Level,includeProperties: "LInterestInput");
                List<LInterest> LevelList = LevelResult.ToList();
                if (LevelList != null && LevelList.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<LInterest>>(LevelList);
                    return result;
                }

                result.Errors.Add("Failed to find interests!");
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

        public async Task<ApiResponse<List<LInterest>>> Get_FilteredInterests(FilterInterests FilterInterests)
        {
            ApiResponse<List<LInterest>> result = new ApiResponse<List<LInterest>>();
            try
            {
                Expression<Func<LInterest, bool>> filter = x => !x.IsDeleted
                 && (string.IsNullOrEmpty(FilterInterests.DescriptionAR) || x.DescriptionAR.Contains(FilterInterests.DescriptionAR.Trim()))
                 && (string.IsNullOrEmpty(FilterInterests.DescriptionEN) || x.DescriptionEN.Contains(FilterInterests.DescriptionEN.Trim()))
                 && (FilterInterests.LevelID == 0 || x.LevelID.Equals(FilterInterests.LevelID))
                 && (FilterInterests.OwnerID.Count == 0 || FilterInterests.OwnerID.Contains((long)x.OwnerID))
                 && (FilterInterests.ParentLInterestID.Count == 0 || FilterInterests.ParentLInterestID.Contains((long)x.ParentLInterestID))
                 && (FilterInterests.AttributeID.Count == 0|| x.LInterestInput.Any(a => FilterInterests.AttributeID.Any(x => x == a.SelectedAttributeID)))
                 && x.IsSeparate.Equals(FilterInterests.IsSeparate);

                var LevelResult = await unitOfWork.LInterestManager.GetAsync(filter, includeProperties: "LInterestInput");

                List<LInterest> LevelList = LevelResult.ToList();
                //LevelList = LevelList.Where(L => L.LInterestInput.Any(a =>  FilterInterests.AttributeID.Any(x=>x== a.SelectedAttributeID))).ToList();

                var sortOrder = FilterInterests.SortingDirection == "Ascending" ?
                      FilterInterests.SortingAttribute : FilterInterests.SortingAttribute + "_" + FilterInterests.SortingDirection;
           
                switch (sortOrder)
                {
                    case "Date":
                        LevelList = LevelList.OrderBy(s => s.CreatedAt).ToList();
                        break;
                    case "Date_Descending":
                        LevelList = LevelList.OrderByDescending(s => s.CreatedAt).ToList();
                        break;
                    case "alphabeticalAR":
                        LevelList = LevelList.OrderBy(s => s.DescriptionAR).ToList();
                        break;
                    case "alphabeticalAR_Descending":
                        LevelList = LevelList.OrderByDescending(s => s.DescriptionAR).ToList();
                        break;
                    case "alphabeticalEN":
                        LevelList = LevelList.OrderBy(s => s.DescriptionEN).ToList();
                        break;
                    case "alphabeticalEN_Descending":
                        LevelList = LevelList.OrderByDescending(s => s.DescriptionEN).ToList();
                        break;
                    default:
                        LevelList = LevelList.OrderBy(s => s.ID).ToList();
                        break;
                }

                if (LevelList != null && LevelList.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<LInterest>>(LevelList);
                    return result;
                }

                result.Errors.Add("Failed to find interests!");
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
        public async Task<ApiResponse<LInterest>> Get_InterestByID(long ID)
        {
            ApiResponse<LInterest> result = new ApiResponse<LInterest>();
            try
            {
                var LevelResult = await unitOfWork.LInterestManager.GetAsync(a => a.ID == ID, includeProperties: "LInterestInput");
                LInterest LevelList = LevelResult.FirstOrDefault();
                if (LevelList != null )
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<LInterest>(LevelList);
                    return result;
                }

                result.Errors.Add("Failed to find interests!");
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
                //var IntersetResult = await unitOfWork.LInterestManager.GetAsync(a => a.DescriptionAR == LInterestToAdd.DescriptionAR 
                //|| a.DescriptionEN == LInterestToAdd.DescriptionEN);
                //LInterest DuplicateLInterest = IntersetResult.FirstOrDefault();
                //if (DuplicateLInterest == null)
                //{
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
                LevelToCreate.OwnerType = LInterestToAdd.OwnerID == 0 ?null: LevelToCreate.OwnerType;
                LevelToCreate.CreatedAt =DateTime.Now;
                var createLevelResult = await unitOfWork.LInterestManager.CreateAsync(LevelToCreate);
                var SaveResult = await unitOfWork.SaveChangesAsync();

                if (SaveResult)
                {
                    return await Create_LInterestInput(LInterestToAdd.LInterestInputs, createLevelResult.ID);

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


        public async Task<ApiResponse<bool>> Edit_Interest(LInterestToEdit LInterestToAdd)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                if (LInterestToAdd.IsSeparate && LInterestToAdd.OwnerID != null)
                {
                    var OwnerResult = await unitOfWork.CustomerManager.GetByIdAsync(LInterestToAdd.OwnerID);
                    if (OwnerResult != null)
                    {
                        return await Save_update_Interest(LInterestToAdd);
                    }
                    else
                    {
                        result.Errors.Add("Failed to update Interest with selected owner");
                        result.Succeeded = false;
                        return result;
                    }
                }
                else if (LInterestToAdd.IsSeparate && (LInterestToAdd.OwnerID == null || LInterestToAdd.OwnerID == 0))
                {
                    result.Errors.Add("Interest cannot be sepearated without owner");
                    result.Succeeded = false;
                    return result;
                }

                return await Save_update_Interest(LInterestToAdd);



            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

        public async Task<ApiResponse<bool>> Save_update_Interest(LInterestToEdit LInterestToAdd)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                LInterest LInterestToupdate = mapper.Map<LInterest>(LInterestToAdd);

                LInterestToupdate.LocationID = LInterestToAdd.LocationID == 0 ? null : LInterestToupdate.LocationID;
                LInterestToupdate.ParentLInterestID = LInterestToAdd.ParentLInterestID == 0 ? null : LInterestToupdate.ParentLInterestID;
                LInterestToupdate.OwnerID = LInterestToAdd.OwnerID == 0 ? null : LInterestToupdate.OwnerID;
                LInterestToupdate.OwnerType = LInterestToAdd.OwnerID == 0 ? null : LInterestToupdate.OwnerType;

                var createLevelResult = await unitOfWork.LInterestManager.UpdateAsync(LInterestToupdate);
                var SaveResult = await unitOfWork.SaveChangesAsync();
                var LinteretInputs = (await unitOfWork.LInterestInputManager.GetAsync(a => a.LInterestID == LInterestToupdate.ID)).ToList();


                var LinterestInputsToEdit = LInterestToAdd.LInterestInputs.FindAll(a => a.InputType != 8 && a.InputType != 9 && a.InputType != 1
                && a.PredefinedInputType != 1 && a.PredefinedInputType != 2);

                var Create_LInterestInputResult = await Create_LInterestInput(mapper.Map<List<LInterestInputToEdit>>(LinterestInputsToEdit.FindAll(a => a.ID == 0)), LInterestToupdate.ID);


                var Edit_LInterestInputResult = await Edit_LInterestInput(LinterestInputsToEdit.FindAll(a => a.ID != 0), LInterestToupdate.ID);


                var MultiLinteretInputs = (await unitOfWork.LInterestInputManager.GetAsync(a => a.LInterestID == LInterestToupdate.ID && (a.InputType == 8 || a.InputType == 9 || a.InputType == 1
                    || a.PredefinedInputType == 1 || a.PredefinedInputType == 2))).ToList();


                //filter  (gallery 8 / multiselect 1 / checkbox 9 / pre. multiselect /pre. checkbox)
                var multiLinterestInputsToEdit = LInterestToAdd.LInterestInputs.FindAll(a => a.InputType == 8 || a.InputType == 9 || a.InputType == 1
                || a.PredefinedInputType == 1 || a.PredefinedInputType == 2);

                if (multiLinterestInputsToEdit.Count != 0)
                {
                    var multiLinterestInputsToCreate = multiLinterestInputsToEdit.Where(m => !LinteretInputs.Exists(l => (l.Attachment == m.Attachment && !string.IsNullOrEmpty(m.Attachment))
                    || (l.SelectedAttributeID == m.SelectedAttributeID && m.SelectedAttributeID != 0 && m.SelectedAttributeID != null))).ToList();

                    var Create_multiLinterestInputsToCreate = await Create_LInterestInput(mapper.Map<List<LInterestInputToEdit>>(multiLinterestInputsToCreate), LInterestToupdate.ID);


                    var multiLinterestInputsToRemove = MultiLinteretInputs.Where(m => !multiLinterestInputsToEdit.Exists(l => (l.Attachment == m.Attachment && !string.IsNullOrEmpty(m.Attachment))
                    || (l.SelectedAttributeID == m.SelectedAttributeID && m.SelectedAttributeID != 0 && m.SelectedAttributeID != null))).ToList();

                    var Remove_multiLinterestInputs = await Remove_LInterestInput(multiLinterestInputsToRemove, LInterestToupdate.ID);

                }


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

        public async Task<ApiResponse<bool>> Create_LInterestInput(List<LInterestInputToEdit> LInterestInputs, long interestID)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var errors = 0;
                for (var i = 0; i < LInterestInputs.Count; i++)
                {
                    var LInterestInputToAdd = LInterestInputs[i];
                    var inputResult = await unitOfWork.LInputManager.GetByIdAsync(LInterestInputToAdd.InputID);
                    if (inputResult != null)
                    {
                        LInterestInput InputToCreate = mapper.Map<LInterestInput>(LInterestInputToAdd);
                        InputToCreate.LInterestID = interestID;
                        InputToCreate.InputType = inputResult.Type;
                        InputToCreate.PredefinedInputType = inputResult.PredefinedInputType;
                        InputToCreate.AttributeID = inputResult.AttributeID;
                        InputToCreate.ID = 0;
                        if ((InputToCreate.Attachment != "" && InputToCreate.Attachment != null) ||
                            (InputToCreate.SelectedAttributeID != 0 && InputToCreate.SelectedAttributeID != null))
                        {
                            var createInputResult = await unitOfWork.LInterestInputManager.CreateAsync(InputToCreate);
                            var saveResult = await unitOfWork.SaveChangesAsync();
                            if (!saveResult)
                            {
                                result.Errors.Add("Failed to create Interest Input for input " + LInterestInputToAdd.InputID);
                                errors++;
                            }
                        }
                    }

                }
                if (errors == 0)
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
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }
        public async Task<ApiResponse<bool>> Create_LInterestInput(List<LInterestInputToAdd> LInterestInputs,long interestID)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var errors = 0;
                for (var i = 0; i < LInterestInputs.Count; i++)
                {
                    var LInterestInputToAdd = LInterestInputs[i];
                    var inputResult = await unitOfWork.LInputManager.GetByIdAsync(LInterestInputToAdd.InputID);
                    if (inputResult != null)
                    {
                        LInterestInput InputToCreate = mapper.Map<LInterestInput>(LInterestInputToAdd);
                        InputToCreate.LInterestID = interestID;
                        InputToCreate.InputType = inputResult.Type;
                        InputToCreate.PredefinedInputType = inputResult.PredefinedInputType;
                        InputToCreate.AttributeID = inputResult.AttributeID;

                        if ((InputToCreate.Attachment != "" && InputToCreate.Attachment != null) ||
                            (InputToCreate.SelectedAttributeID != 0 && InputToCreate.SelectedAttributeID != null))
                        {
                            var createInputResult = await unitOfWork.LInterestInputManager.CreateAsync(InputToCreate);
                            var saveResult = await unitOfWork.SaveChangesAsync();
                            if (!saveResult)
                            {
                                result.Errors.Add("Failed to create Interest Input for input " + LInterestInputToAdd.InputID);
                                errors++;
                            }
                        }
                    }

                }
                if (errors == 0)
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
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

        public async Task<ApiResponse<bool>> Remove_LInterestInput(List<LInterestInput> LInterestInputs, long interestID)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var errors = 0;
                for (var i = 0; i < LInterestInputs.Count; i++)
                {

                    var createInputResult = await unitOfWork.LInterestInputManager.RemoveAsync(LInterestInputs[i]);
                    var saveResult = await unitOfWork.SaveChangesAsync();
                    if (!saveResult)
                    {
                        result.Errors.Add("Failed to delete Interest Input for input " + LInterestInputs[i].ID);
                        errors++;
                    }

                }
                if (errors == 0)
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
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }
        public async Task<ApiResponse<bool>> Edit_LInterestInput(List<LInterestInputToEdit> LInterestInputs, long interestID)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var errors = 0;
                for (var i = 0; i < LInterestInputs.Count; i++)
                {
                    var LInterestInputToAdd = LInterestInputs[i];
                    var LinteretInput = await unitOfWork.LInterestInputManager.GetByIdAsync( LInterestInputToAdd.ID);

                   // LinteretInput = mapper.Map<LInterestInput>(LInterestInputToAdd);
                    LinteretInput.Attachment = LInterestInputToAdd.Attachment;
                    LinteretInput.SelectedAttributeID = LInterestInputToAdd.SelectedAttributeID;

                    if ((LinteretInput.Attachment != "" && LinteretInput.Attachment != null) ||
                        (LinteretInput.SelectedAttributeID != 0 && LinteretInput.SelectedAttributeID != null))
                    {
                        var createInputResult = await unitOfWork.LInterestInputManager.UpdateAsync(LinteretInput);
                        if (!createInputResult)
                        {
                            result.Errors.Add("Failed to update Interest Input for input " + LInterestInputToAdd.ID);
                            errors++;
                        }
                    }

                }
                 await unitOfWork.SaveChangesAsync();
                if (errors == 0)
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
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

        //public async Task<ApiResponse<List<LInterest>>> Get_LInterestByInputID(long InputID)
        //{
        //    ApiResponse<List<LInterest>> result = new ApiResponse<List<LInterest>>();
        //    try
        //    {
        //        var LevelResult = (from interestAttribute in (await unitOfWork.LInterest_LInterestInputManager.GetAsync(a => a.LInterestInputID == InputID))
        //                           join interest in (await unitOfWork.LInterestManager.GetAsync()) on interestAttribute.LInterestID equals interest.ID
        //                           select new LInterest
        //                           {
        //                               ID = interest.ID,
        //                               DescriptionAR = interest.DescriptionAR,
        //                               DescriptionEN = interest.DescriptionEN,
        //                               IsSeparate = interest.IsSeparate,
        //                               Level = interest.Level,
        //                               LocationID = interest.LocationID,
        //                               OwnerID = interest.OwnerID,
        //                               ParentLInterestID = interest.ParentLInterestID,
        //                           }).ToList();

        //        if (LevelResult != null && LevelResult.Count != 0)
        //        {
        //            result.Succeeded = true;
        //            result.Data = LevelResult;
        //            return result;
        //        }

        //        result.Errors.Add("Failed to find Levels!");
        //        result.Succeeded = false;
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Succeeded = false;
        //        result.Errors.Add(ex.Message);
        //        result.ErrorType = ErrorType.SystemError;
        //        return result;
        //    }

        //}





    }

}


