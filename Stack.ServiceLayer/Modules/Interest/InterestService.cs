﻿
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using Stack.DTOs;
using Stack.DTOs.Enums;
using Stack.DTOs.Requests.Modules.Interest;
using Stack.Entities.Models.Modules.Hierarchy;
using Stack.Entities.Models.Modules.Interest;
using System;
using System.Collections.Generic;
using System.Linq;
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
        
        
        #region  Interest

        public async Task<ApiResponse<List<LInterest>>> Get_InterestByLevel(long Level)
        {
            ApiResponse<List<LInterest>> result = new ApiResponse<List<LInterest>>();
            try
            {
                var LevelResult = await unitOfWork.LInterestManager.GetAsync(a => a.LevelID == Level);
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
                var IntersetResult = await unitOfWork.LInterestManager.GetAsync(a => a.DescriptionAR == LInterestToAdd.DescriptionAR 
                || a.DescriptionEN == LInterestToAdd.DescriptionEN);
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


        #region Interest-Input
        //public async Task<ApiResponse<bool>> Create_LInterestInput(LInterestInputToAdd LInterestInputToAdd)
        //{
        //    ApiResponse<bool> result = new ApiResponse<bool>();
        //    try
        //    {
        //        var LInterestInputResult = await unitOfWork.LInterestInputManager.GetAsync(a => a.LInterestID == LInterestInputToAdd.LInterestID && a.LInterestInputID == LInterestInputToAdd.LInterestInputID);
        //        LInterest_LInterestInput DuplicateLInterestResult = LInterestInputResult.FirstOrDefault();

        //        if (DuplicateLInterestResult == null)
        //        {
        //            LInterest_LInterestInput InputToCreate = mapper.Map<LInterest_LInterestInput>(LInterestInputToAdd); ;
        //            var createInputResult = await unitOfWork.LInterest_LInterestInputManager.CreateAsync(InputToCreate);
        //            await unitOfWork.SaveChangesAsync();

        //            if (createInputResult != null)
        //            {
        //                result.Succeeded = true;
        //                result.Data = true;
        //                return result;
        //            }
        //            else
        //            {
        //                result.Errors.Add("Failed to create Interest Input");
        //                result.Succeeded = false;
        //                return result;
        //            }


        //        }

        //        result.Errors.Add("Failed to create Interest Input!");
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

        //public async Task<ApiResponse<bool>> Delete_LInterestInput(long ID)
        //{
        //    ApiResponse<bool> result = new ApiResponse<bool>();
        //    try
        //    {
        //        var InputResult = await unitOfWork.LInterest_LInterestInputManager.GetByIdAsync(ID);

        //        if (InputResult != null)
        //        {
        //            InputResult.IsDeleted = true;
        //            var UpdateResult = await unitOfWork.LInterest_LInterestInputManager.UpdateAsync(InputResult);
        //            var SaveResult = await unitOfWork.SaveChangesAsync();
        //            if (SaveResult)
        //            {
        //                result.Succeeded = true;
        //                return result;
        //            }

        //        }

        //        result.Errors.Add("Failed to delete Input!");
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

        #endregion


    }

}


