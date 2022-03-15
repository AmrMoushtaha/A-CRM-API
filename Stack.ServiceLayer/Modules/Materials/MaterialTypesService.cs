using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using Stack.DTOs;
using Stack.DTOs.Models.Modules.Materials.MaterialGroups;
using Stack.DTOs.Models.Modules.Materials.MaterialTypes;
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
    public class MaterialTypesService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public MaterialTypesService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {

            this.unitOfWork = unitOfWork;
            this.config = config;
            this.mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<ApiResponse<List<MaterialTypesMainViewDTO>>> GetAllMaterialTypes()
        {
            ApiResponse<List<MaterialTypesMainViewDTO>> result = new ApiResponse<List<MaterialTypesMainViewDTO>>();
            try
            {

                var materialTypesQuery = await unitOfWork.MaterialTypesManager.GetAsync();

                List<MaterialType> materialTypesList = materialTypesQuery.ToList();

                if (materialTypesList != null)
                {

                    result.Succeeded = true;
                    result.Data = mapper.Map<List<MaterialTypesMainViewDTO>>(materialTypesList);
                    return result;

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No material types were found !");
                    result.Errors.Add("لم يتم العثور على انواع للمواد!");
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


        public async Task<ApiResponse<MaterialTypesMainViewDTO>> CreateMaterialType(MaterialTypesCreationModel model)
        {
            ApiResponse<MaterialTypesMainViewDTO> result = new ApiResponse<MaterialTypesMainViewDTO>();
            try
            {

                var duplicateModelQuery = await unitOfWork.MaterialTypesManager.GetAsync(a => a.Code == model.Code);

                MaterialType duplicateModel = duplicateModelQuery.FirstOrDefault();

                if (duplicateModel == null)
                {

                    MaterialType creationModel = new MaterialType();

                    creationModel.DescriptionAR = model.DescriptionAR;

                    creationModel.DescriptionEN = model.DescriptionEN;

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

                            var duplicateCodeResult = await unitOfWork.MaterialTypesManager.GetAsync(a => a.Code == randomCode);

                            MaterialType duplicateMaterialGroupCode = duplicateCodeResult.FirstOrDefault();

                            if (duplicateMaterialGroupCode == null)
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


                    var createMaterialTypeResult = await unitOfWork.MaterialTypesManager.CreateAsync(creationModel);

                    if (createMaterialTypeResult != null)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = mapper.Map<MaterialTypesMainViewDTO>(creationModel);
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to create new material type, Please try again !");
                        result.Errors.Add("فشل إنشاء نوع مواد جديدة ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to create new material type, a material type with a duplicate code already exists !");
                    result.Errors.Add("فشل إنشاء نوع مواد جديدة ، يوجد بالفعل نوع مواد برمز مكرر!");
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


        public async Task<ApiResponse<MaterialTypesMainViewDTO>> EditMaterialType(EditMaterialTypeModel model)
        {
            ApiResponse<MaterialTypesMainViewDTO> result = new ApiResponse<MaterialTypesMainViewDTO>();
            try
            {

                var duplicateModelQuery = await unitOfWork.MaterialTypesManager.GetAsync(a => a.Code == model.Code && a.ID != model.ID);

                MaterialType duplicateModel = duplicateModelQuery.FirstOrDefault();

                if (duplicateModel == null)
                {

                    var materialTypeResult = await unitOfWork.MaterialTypesManager.GetAsync(a => a.ID == model.ID);

                    MaterialType materialTypeToEdit = materialTypeResult.FirstOrDefault();


                    if (materialTypeToEdit != null)
                    {

                        if (String.IsNullOrWhiteSpace(model.Code))
                        {

                            string randomCode;

                            while (true)
                            {

                                randomCode = await HelperFunctions.GenerateRandomNumberAsync(6);

                                var duplicateCodeResult = await unitOfWork.MaterialTypesManager.GetAsync(a => a.Code == randomCode);

                                MaterialType duplicateMatTypeCode = duplicateCodeResult.FirstOrDefault();

                                if (duplicateMatTypeCode == null)
                                {
                                    break;
                                }

                            }

                            materialTypeToEdit.Code = randomCode;

                        }
                        else
                        {
                            materialTypeToEdit.Code = model.Code;
                        }

                        materialTypeToEdit.DescriptionAR = model.DescriptionAR;

                        materialTypeToEdit.DescriptionEN = model.DescriptionEN;

                        materialTypeToEdit.Updated_By = _httpContextAccessor.HttpContext.User.Identity.Name;

                        materialTypeToEdit.Update_Date = await HelperFunctions.GetEgyptsCurrentLocalTime();

                        var updateMatTypeResult = await unitOfWork.MaterialTypesManager.UpdateAsync(materialTypeToEdit);

                        if (updateMatTypeResult == true)
                        {
                            await unitOfWork.SaveChangesAsync();
                            result.Succeeded = true;
                            result.Data = mapper.Map<MaterialTypesMainViewDTO>(materialTypeToEdit);
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Failed to update material type, Please try again !");
                            result.Errors.Add("فشل تحديث نوع المواد ، يرجى المحاولة مرة أخرى!");
                            return result;
                        }

                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to update material type, Please try again !");
                        result.Errors.Add("فشل تحديث نوع المواد ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to update material type , a material type with a similar name already exists !");
                    result.Errors.Add("فشل تحديث نوع المواد ، يوجد بالفعل نوع مواد تحمل اسمًا مشابهًا!");
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


        public async Task<ApiResponse<bool>> DeleteMaterialType(DeleteRecordModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var materialTypesResult = await unitOfWork.MaterialTypesManager.GetAsync(a => a.ID == (int)model.ID);

                MaterialType typeToDelete = materialTypesResult.FirstOrDefault();

                if (typeToDelete != null)
                {

                    var deleteMaterialTypeResult = await unitOfWork.MaterialTypesManager.RemoveAsync(typeToDelete);

                    if (deleteMaterialTypeResult == true)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to delete material type, Please try again !");
                        result.Errors.Add("فشل حذف نوع المواد ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to delete material group, Please try again !");
                    result.Errors.Add("فشل حذف مجموعة المواد ، يرجى المحاولة مرة أخرى!");
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
