using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using Stack.DTOs;
using Stack.DTOs.Models.Modules.Materials.MaterialGroups;
using Stack.DTOs.Models.Modules.Materials.MaterialTypes;
using Stack.DTOs.Requests.Modules.Materials.Material;
using Stack.DTOs.Requests.Modules.Materials.MaterialGroups;
using Stack.DTOs.Requests.Modules.Materials.MaterialTypes;
using Stack.DTOs.Requests.Shared;
using Stack.Entities.Models.Modules.Materials;
using Stack.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stack.ServiceLayer.Modules.Materials
{
    public class MaterialService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public MaterialService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {

            this.unitOfWork = unitOfWork;
            this.config = config;
            this.mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<ApiResponse<List<MaterialMainViewDTO>>> GetAllMaterials()
        {
            ApiResponse<List<MaterialMainViewDTO>> result = new ApiResponse<List<MaterialMainViewDTO>>();
            try
            {

                var modelQuery = await unitOfWork.MaterialsManager.GetAsync(includeProperties: "MaterialGroup,MaterialType");

                List<Material> materialsList = modelQuery.ToList();

                if (materialsList != null)
                {

                    result.Succeeded = true;
                    result.Data = mapper.Map<List<MaterialMainViewDTO>>(materialsList);
                    return result;

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No materials were found !");
                    result.Errors.Add("لم يتم العثور على مواد!");
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

        public async Task<ApiResponse<MaterialMainViewDTO>> CreateMaterial(MaterialCreationModel model)
        {
            ApiResponse<MaterialMainViewDTO> result = new ApiResponse<MaterialMainViewDTO>();
            try
            {

                var duplicateModelQuery = await unitOfWork.MaterialsManager.GetAsync(a => a.Code == model.Code);

                Material duplicateModel = duplicateModelQuery.FirstOrDefault();

                if (duplicateModel == null)
                {

                    Material creationModel = new Material();

                    creationModel.DescriptionAR = model.DescriptionAR;

                    creationModel.DescriptionEN = model.DescriptionEN;

                    creationModel.PriceIndicator = "E";
                    creationModel.EAN = model.EAN;
                    creationModel.Stock = model.Stock;
                    creationModel.MovingAverage = model.MovingAverage;
                    creationModel.SafetyStock = model.SafetyStock;
                    creationModel.StandardPrice = model.StandardPrice;
                    creationModel.MinimumOrderAmount = model.MinimumOrderAmount;
                    creationModel.ReOrderPoint = model.ReOrderPoint;
                    creationModel.MaxStock = model.MaxStock;
                    creationModel.LeadTime = model.LeadTime;
                    creationModel.MaterialTypeID = model.MaterialTypeID;
                    creationModel.MaterialGroupID = model.MaterialGroupID;

                    creationModel.Creation_Date = await HelperFunctions.GetEgyptsCurrentLocalTime();

                    creationModel.Created_By = _httpContextAccessor.HttpContext.User.Identity.Name;

                    creationModel.Updated_By = creationModel.Created_By;

                    creationModel.Update_Date = creationModel.Update_Date;


                    if (String.IsNullOrWhiteSpace(model.Code))
                    {

                        string randomCode;

                        while (true)
                        {

                            randomCode = await HelperFunctions.GenerateRandomNumberAsync(6);

                            var duplicateCodeResult = await unitOfWork.MaterialsManager.GetAsync(a => a.Code == randomCode);

                            Material duplicateMaterialCode = duplicateCodeResult.FirstOrDefault();

                            if (duplicateMaterialCode == null)
                            {
                                break;
                            }

                        }

                        creationModel.Code = randomCode;

                    }
                    else
                    {
                        creationModel.Code = model.Code;
                    }


                    var createMaterialResult = await unitOfWork.MaterialsManager.CreateAsync(creationModel);

                    if (createMaterialResult != null)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = mapper.Map<MaterialMainViewDTO>(creationModel);
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to create new material, Please try again !");
                        result.Errors.Add("فشل إنشاء ماده جديدة ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to create new material, a material with a duplicate code already exists !");
                    result.Errors.Add("فشل إنشاء ماده جديدة ، يوجد بالفعل ماده برمز مكرر!");
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


        public async Task<ApiResponse<MaterialMainViewDTO>> EditMaterial(EditMaterialModel model)
        {
            ApiResponse<MaterialMainViewDTO> result = new ApiResponse<MaterialMainViewDTO>();
            try
            {

                var duplicateModelQuery = await unitOfWork.MaterialsManager.GetAsync(a => a.Code == model.Code && a.ID != model.ID);

                Material duplicateModel = duplicateModelQuery.FirstOrDefault();

                if (duplicateModel == null)
                {

                    var materialResult = await unitOfWork.MaterialsManager.GetAsync(a => a.ID == model.ID);

                    Material materialToEdit = materialResult.FirstOrDefault();


                    if (materialToEdit != null)
                    {

                        if (String.IsNullOrWhiteSpace(model.Code))
                        {

                            string randomCode;

                            while (true)
                            {

                                randomCode = await HelperFunctions.GenerateRandomNumberAsync(6);

                                var duplicateCodeResult = await unitOfWork.MaterialsManager.GetAsync(a => a.Code == randomCode);

                                Material duplicateMatCode = duplicateCodeResult.FirstOrDefault();

                                if (duplicateMatCode == null)
                                {
                                    break;
                                }

                            }

                            materialToEdit.Code = randomCode;

                        }
                        else
                        {
                            materialToEdit.Code = model.Code;
                        }

                        materialToEdit.DescriptionAR = model.DescriptionAR;
                        materialToEdit.DescriptionEN = model.DescriptionEN;
                        materialToEdit.PriceIndicator = model.PriceIndicator;
                        materialToEdit.EAN = model.EAN;
                        materialToEdit.Stock = model.Stock;
                        materialToEdit.MovingAverage = model.MovingAverage;
                        materialToEdit.MinimumOrderAmount = model.MinimumOrderAmount;
                        materialToEdit.SafetyStock = model.SafetyStock;
                        materialToEdit.StandardPrice = model.StandardPrice;
                        materialToEdit.ReOrderPoint = model.ReOrderPoint;
                        materialToEdit.MaxStock = model.MaxStock;
                        materialToEdit.LeadTime = model.LeadTime;
                        materialToEdit.MaterialTypeID = model.MaterialTypeID;
                        materialToEdit.MaterialGroupID = model.MaterialGroupID;

                        materialToEdit.Updated_By = _httpContextAccessor.HttpContext.User.Identity.Name;
                        materialToEdit.Update_Date = await HelperFunctions.GetEgyptsCurrentLocalTime();

                        var updateMatResult = await unitOfWork.MaterialsManager.UpdateAsync(materialToEdit);
                        if (updateMatResult == true)
                        {
                            await unitOfWork.SaveChangesAsync();
                            result.Succeeded = true;
                            result.Data = mapper.Map<MaterialMainViewDTO>(materialToEdit);
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Failed to update material, Please try again !");
                            result.Errors.Add("فشل تحديث المادة ، يرجى المحاولة مرة أخرى!");
                            return result;
                        }

                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to update material, Please try again !");
                        result.Errors.Add("فشل تحديث المادة ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to update material, a material with a similar name already exists !");
                    result.Errors.Add("فشل تحديث المادة ، يوجد بالفعل المادة تحمل اسمًا مشابهًا!");
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


        public async Task<ApiResponse<bool>> DeleteMaterial(DeleteRecordModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var MaterialsResult = await unitOfWork.MaterialsManager.GetAsync(a => a.ID == (int)model.ID);

                Material typeToDelete = MaterialsResult.FirstOrDefault();

                if (typeToDelete != null)
                {

                    var deleteMaterialResult = await unitOfWork.MaterialsManager.RemoveAsync(typeToDelete);

                    if (deleteMaterialResult == true)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to delete material, Please try again !");
                        result.Errors.Add("فشل حذف المادة ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to delete material, Please try again !");
                    result.Errors.Add("فشل حذف المادة ، يرجى المحاولة مرة أخرى!");
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
