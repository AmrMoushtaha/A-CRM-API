
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
using Stack.Entities.Models.Modules.Areas;

namespace Stack.ServiceLayer.Modules.Interest
{
    public class LocationService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private static readonly HttpClient client = new HttpClient();

        public LocationService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;

        }

        public async Task<ApiResponse<bool>> Create_Location(LocationToAdd LocationToAdd)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var LocationResult = await unitOfWork.LocationManager.GetAsync(a => a.NameAR == LocationToAdd.NameAR || a.NameEN == LocationToAdd.NameEN);
                Location DuplicateLocation = LocationResult.FirstOrDefault();
                if (DuplicateLocation == null)
                {
                    Location LocationToCreate = mapper.Map<Location>(LocationToAdd);
                    LocationToCreate.ParentLocationID = null;
                    var createLocationResult = await unitOfWork.LocationManager.CreateAsync(LocationToCreate);
                    var saveResult= await unitOfWork.SaveChangesAsync();

                    if (saveResult)
                    {
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Errors.Add("Failed to create location");
                        result.Succeeded = false;
                        return result;
                    }

                }

                result.Errors.Add("Failed to create location!, Try another name");
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

        public async Task<ApiResponse<bool>> Edit_Location(LocationToEdit LocationToEdit)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var LocationResult = await unitOfWork.LocationManager.GetAsync(a => (a.NameAR == LocationToEdit.NameAR || a.NameEN == LocationToEdit.NameEN) && a.ID != LocationToEdit.ID);
                Location DuplicateLocationResult = LocationResult.FirstOrDefault();
                var LocationR = await unitOfWork.LocationManager.GetByIdAsync(LocationToEdit.ID);

                if (DuplicateLocationResult == null && LocationR != null)
                {
                    LocationR = mapper.Map<Location>(LocationToEdit); 
                    var updateResult = await unitOfWork.LocationManager.UpdateAsync(LocationR);
                    await unitOfWork.SaveChangesAsync();

                    if (updateResult)
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


        public async Task<ApiResponse<bool>> Delete_Location(long LocationToDelete)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var LocationResult = await unitOfWork.LocationManager.GetByIdAsync(LocationToDelete);

                if (LocationResult != null)
                {
                    LocationResult.IsDeleted = true;
                    var UpdateResult = await unitOfWork.LocationManager.UpdateAsync(LocationResult);
                    if(UpdateResult)
                    {
                        result.Succeeded = true;
                        return result;
                    }
                }

                result.Errors.Add("Failed to delete Location!");
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


    }

}


