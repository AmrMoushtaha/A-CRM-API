using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using Stack.DTOs;
using Stack.DTOs.Models.Modules.Materials.Plants;
using Stack.DTOs.Requests.Modules.Materials.Plants;
using Stack.DTOs.Requests.Shared;
using Stack.Entities.Models.Modules.Materials;
using Stack.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stack.ServiceLayer.Modules.Materials
{
    public class PlantsService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public PlantsService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {

            this.unitOfWork = unitOfWork;
            this.config = config;
            this.mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<ApiResponse<List<PlantMainViewDTO>>> GetAllPlants()
        {
            ApiResponse<List<PlantMainViewDTO>> result = new ApiResponse<List<PlantMainViewDTO>>();
            try
            {

                var plantsQuery = await unitOfWork.PlantsManager.GetAsync(includeProperties: "CompanyCode");

                List<Plant> plantsList = plantsQuery.ToList();

                if (plantsList != null)
                {

                    result.Succeeded = true;
                    result.Data = mapper.Map<List<PlantMainViewDTO>>(plantsList);
                    return result;

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No plants groups were found !");
                    result.Errors.Add("لم يتم العثور على مصانع !");
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }

        }


        public async Task<ApiResponse<PlantMainViewDTO>> CreatePlant(PlantCreationModel model)
        {
            ApiResponse<PlantMainViewDTO> result = new ApiResponse<PlantMainViewDTO>();
            try
            {

                var duplicatePlantQuery = await unitOfWork.PlantsManager.GetAsync(a => a.Code == model.Code);

                Plant duplicatePlant = duplicatePlantQuery.FirstOrDefault();

                if (duplicatePlant == null)
                {

                    Plant newPlant = new Plant();

                    newPlant.DescriptionAR = model.DescriptionAR;

                    newPlant.DescriptionEN = model.DescriptionEN;

                    newPlant.CompanyCodeID = model.CompanyCodeID;

                    newPlant.Creation_Date = await HelperFunctions.GetEgyptsCurrentLocalTime();

                    newPlant.Created_By = _httpContextAccessor.HttpContext.User.Identity.Name;

                    newPlant.Updated_By = newPlant.Created_By;

                    newPlant.Update_Date = newPlant.Update_Date;

                    if (String.IsNullOrWhiteSpace(model.Code))
                    {

                        string randomCode;

                        while (true)
                        {

                            randomCode = await HelperFunctions.GenerateRandomNumberAsync(6);

                            var duplicateCodeResult = await unitOfWork.PlantsManager.GetAsync(a => a.Code == randomCode);

                            Plant duplicatePlantCode = duplicateCodeResult.FirstOrDefault();

                            if (duplicatePlantCode == null)
                            {
                                break;
                            }

                        }

                        newPlant.Code = randomCode;

                    }
                    else
                    {
                        newPlant.Code = model.Code;
                    }


                    var createPlantResult = await unitOfWork.PlantsManager.CreateAsync(newPlant);

                    if (createPlantResult != null)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = mapper.Map<PlantMainViewDTO>(newPlant);
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to create new plant, Please try again !");
                        result.Errors.Add("فشل إنشاء مصنع جديد ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to create new plant, a plant with a duplicate code already exists !");
                    result.Errors.Add("فشل إنشاء مصنع جديد ، يوجد بالفعل مصنع برمز مكرر!");
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }

        }


        public async Task<ApiResponse<PlantMainViewDTO>> EditPlant(EditPlantModel model)
        {
            ApiResponse<PlantMainViewDTO> result = new ApiResponse<PlantMainViewDTO>();
            try
            {

                var duplicatePlantModel = await unitOfWork.PlantsManager.GetAsync(a => a.Code == model.Code && a.ID != model.PlantID && a.CompanyCodeID == model.CompanyCodeID);

                Plant duplicatePlant = duplicatePlantModel.FirstOrDefault();

                if (duplicatePlant == null)
                {

                    var plantsResult = await unitOfWork.PlantsManager.GetAsync(a => a.ID == model.PlantID);

                    Plant plantToEdit = plantsResult.FirstOrDefault();


                    if (plantToEdit != null)
                    {

                        if (String.IsNullOrWhiteSpace(model.Code))
                        {

                            string randomCode;

                            while (true)
                            {

                                randomCode = await HelperFunctions.GenerateRandomNumberAsync(6);

                                var duplicateCodeResult = await unitOfWork.PlantsManager.GetAsync(a => a.Code == randomCode);

                                Plant duplicatePlantCode = duplicateCodeResult.FirstOrDefault();

                                if (duplicatePlantCode == null)
                                {
                                    break;
                                }

                            }

                            plantToEdit.Code = randomCode;

                        }
                        else
                        {
                            plantToEdit.Code = model.Code;
                        }

                        plantToEdit.DescriptionAR = model.DescriptionAR;

                        plantToEdit.DescriptionEN = model.DescriptionEN;

                        plantToEdit.Updated_By = _httpContextAccessor.HttpContext.User.Identity.Name;

                        plantToEdit.Update_Date = await HelperFunctions.GetEgyptsCurrentLocalTime();

                        var updatePlantResult = await unitOfWork.PlantsManager.UpdateAsync(plantToEdit);

                        if (updatePlantResult == true)
                        {
                            await unitOfWork.SaveChangesAsync();
                            result.Succeeded = true;
                            result.Data = mapper.Map<PlantMainViewDTO>(plantToEdit);
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Failed to update plant, Please try again !");
                            result.Errors.Add("فشل تحديث المصنع ، يرجى المحاولة مرة أخرى!");
                            return result;
                        }

                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to update plant, Please try again !");
                        result.Errors.Add("فشل تحديث المصنع ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to update plant , a plant with a similar name already exists !");
                    result.Errors.Add("فشل تحديث المصنع ، يوجد بالفعل مصنع يحمل نفس الاسم!");
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }

        }


        public async Task<ApiResponse<bool>> DeletePlant(DeleteRecordModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var plantsResult = await unitOfWork.PlantsManager.GetAsync(a => a.ID == (int)model.ID);

                Plant plantToDelete = plantsResult.FirstOrDefault();

                if (plantToDelete != null)
                {

                    var deletePlantResult = await unitOfWork.PlantsManager.RemoveAsync(plantToDelete);

                    if (deletePlantResult == true)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to delete plant, Please try again !");
                        result.Errors.Add("فشل حذف المصنع ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to delete position, Please try again !");
                    result.Errors.Add("فشل حذف المصنع ، يرجى المحاولة مرة أخرى!");
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }

        }


    }

}
