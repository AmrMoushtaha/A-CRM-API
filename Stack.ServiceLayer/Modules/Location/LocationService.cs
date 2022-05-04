
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using Stack.DTOs;
using Stack.DTOs.Enums;
using Stack.DTOs.Requests.Modules.AreaInterest;
using Stack.Entities.Models.Modules.Areas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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

        public async Task<ApiResponse<List<Location>>> Get_LocationByType(int Type)
        {
            ApiResponse<List<Location>> result = new ApiResponse<List<Location>>();
            try
            {
                var LocationResult = await unitOfWork.LocationManager.GetAsync(a => a.LocationType == Type && !a.IsDeleted);
                List<Location> LocationList = LocationResult.ToList();
                if (LocationList != null && LocationList.Count!=0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<Location>>(LocationList);
                    return result;
                }

                result.Errors.Add("Failed to find locations!");
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

        public async Task<ApiResponse<List<Location>>> Get_Locations()
        {
            ApiResponse<List<Location>> result = new ApiResponse<List<Location>>();
            try
            {
                var LocationResult = await unitOfWork.LocationManager.GetAsync(a =>  !a.IsDeleted);
                List<Location> LocationList = LocationResult.ToList();
                if (LocationList != null && LocationList.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<Location>>(LocationList);
                    return result;
                }

                result.Errors.Add("Failed to find locations!");
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

        public async Task<ApiResponse<List<Location>>> Get_LocationByParentID(long ParentID)
        {
            ApiResponse<List<Location>> result = new ApiResponse<List<Location>>();
            try
            {
                var LocationResult = await unitOfWork.LocationManager.GetAsync(a => a.ParentLocationID == ParentID && !a.IsDeleted);
                List<Location> LocationList = LocationResult.ToList();
                if (LocationList != null && LocationList.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<Location>>(LocationList);
                    return result;
                }

                result.Errors.Add("Failed to find locations!");
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
                    LocationToCreate.ParentLocationID = LocationToCreate.ParentLocationID == 0 ? null : LocationToCreate.ParentLocationID;
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
                    LocationR.LocationType = LocationToEdit.LocationType;
                    LocationR.Latitude = LocationToEdit.Latitude;
                    LocationR.Longitude = LocationToEdit.Longitude;
                    LocationR.NameAR = LocationToEdit.NameAR;
                    LocationR.NameEN = LocationToEdit.NameEN;
                    LocationR.DescriptionAR = LocationToEdit.DescriptionAR;
                    LocationR.DescriptionEN = LocationToEdit.DescriptionEN;
                    LocationR.ParentLocationID = LocationToEdit.ParentLocationID == 0 ? null : LocationToEdit.ParentLocationID;
                    var updateResult = await unitOfWork.LocationManager.UpdateAsync(LocationR);
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
                    var SaveResult= await unitOfWork.SaveChangesAsync();
                    if (SaveResult)
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


