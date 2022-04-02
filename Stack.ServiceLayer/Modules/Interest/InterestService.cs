
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
                    else if (LInterestToAdd.IsSeparate && LInterestToAdd.OwnerID == null)
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

                var createLevelResult = await unitOfWork.LInterestManager.CreateAsync(LevelToCreate);
               var test= await unitOfWork.SaveChangesAsync();

                if (createLevelResult != null)
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
                    if(UpdateResult)
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
                    InputResult = mapper.Map<LInterestInput>(InputToEdit); ;
                    var createInputResult = await unitOfWork.LInterestInputManager.UpdateAsync(InputResult);
                    await unitOfWork.SaveChangesAsync();

                    if (createInputResult)
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
                    if (UpdateResult)
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


