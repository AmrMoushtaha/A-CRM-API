using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using Stack.DTOs;
using Stack.DTOs.Models.Modules.Materials.MaterialGroups;
using Stack.DTOs.Requests.Modules.Materials.MaterialGroups;
using Stack.DTOs.Requests.Shared;
using Stack.Entities.Models.Modules.Materials;
using Stack.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stack.ServiceLayer.Modules.Materials
{
    public class MaterialGroupsService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public MaterialGroupsService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {

            this.unitOfWork = unitOfWork;
            this.config = config;
            this.mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<ApiResponse<List<MaterialGroupMainViewDTO>>> GetAllMaterialGroups()
        {
            ApiResponse<List<MaterialGroupMainViewDTO>> result = new ApiResponse<List<MaterialGroupMainViewDTO>>();
            try
            {

                var materialGroupsQuery = await unitOfWork.MaterialGroupsManager.GetAsync();

                List<MaterialGroup> materialGroupsList = materialGroupsQuery.ToList();

                if (materialGroupsList != null)
                {

                    result.Succeeded = true;
                    result.Data = mapper.Map<List<MaterialGroupMainViewDTO>>(materialGroupsList);
                    return result;

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No material groups were found !");
                    result.Errors.Add("لم يتم العثور على مجموعات للمواد!");
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


        public async Task<ApiResponse<MaterialGroupMainViewDTO>> CreateMaterialGroup(MaterialTypesCreationModel model)
        {
            ApiResponse<MaterialGroupMainViewDTO> result = new ApiResponse<MaterialGroupMainViewDTO>();
            try
            {

                var duplicateMaterialGroupQuery = await unitOfWork.MaterialGroupsManager.GetAsync(a => a.Code == model.Code);

                MaterialGroup duplicateMaterialGroup = duplicateMaterialGroupQuery.FirstOrDefault();

                if (duplicateMaterialGroup == null)
                {

                    MaterialGroup newMaterialGroup = new MaterialGroup();

                    newMaterialGroup.DescriptionAR = model.DescriptionAR;

                    newMaterialGroup.DescriptionEN = model.DescriptionEN;

                    newMaterialGroup.Creation_Date = await HelperFunctions.GetEgyptsCurrentLocalTime();

                    newMaterialGroup.Created_By = _httpContextAccessor.HttpContext.User.Identity.Name;

                    newMaterialGroup.Updated_By = newMaterialGroup.Created_By;

                    newMaterialGroup.Update_Date = newMaterialGroup.Update_Date;

                    if (String.IsNullOrWhiteSpace(model.Code))
                    {

                        string randomCode;

                        while (true)
                        {

                            randomCode = await HelperFunctions.GenerateRandomNumberAsync(6);

                            var duplicateCodeResult = await unitOfWork.MaterialGroupsManager.GetAsync(a => a.Code == randomCode);

                            MaterialGroup duplicateMaterialGroupCode = duplicateCodeResult.FirstOrDefault();

                            if (duplicateMaterialGroupCode == null)
                            {
                                break;
                            }

                        }

                        newMaterialGroup.Code = randomCode;

                    }
                    else
                    {
                        newMaterialGroup.Code = model.Code;
                    }


                    var createMaterialGroupResult = await unitOfWork.MaterialGroupsManager.CreateAsync(newMaterialGroup);

                    if (createMaterialGroupResult != null)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = mapper.Map<MaterialGroupMainViewDTO>(newMaterialGroup);
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to create new material group, Please try again !");
                        result.Errors.Add("فشل إنشاء مجموعة مواد جديدة ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to create new material group, a material group with a duplicate code already exists !");
                    result.Errors.Add("فشل إنشاء مجموعة مواد جديدة ، توجد بالفعل مجموعة مواد برمز مكرر!");
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


        public async Task<ApiResponse<MaterialGroupMainViewDTO>> EditMaterialGroup(EditMaterialGroupModel model)
        {
            ApiResponse<MaterialGroupMainViewDTO> result = new ApiResponse<MaterialGroupMainViewDTO>();
            try
            {

                var duplicateMaterialGroupResult = await unitOfWork.MaterialGroupsManager.GetAsync(a => a.Code == model.Code && a.ID != model.MaterialGroupID);

                MaterialGroup duplicateMaterialGroup = duplicateMaterialGroupResult.FirstOrDefault();

                if (duplicateMaterialGroup == null)
                {

                    var materialGroupsResult = await unitOfWork.MaterialGroupsManager.GetAsync(a => a.ID == model.MaterialGroupID);

                    MaterialGroup materialGroupToEdit = materialGroupsResult.FirstOrDefault();


                    if (materialGroupToEdit != null)
                    {

                        if (String.IsNullOrWhiteSpace(model.Code))
                        {

                            string randomCode;

                            while (true)
                            {

                                randomCode = await HelperFunctions.GenerateRandomNumberAsync(6);

                                var duplicateCodeResult = await unitOfWork.MaterialGroupsManager.GetAsync(a => a.Code == randomCode);

                                MaterialGroup duplicateMatGroupCode = duplicateCodeResult.FirstOrDefault();

                                if (duplicateMatGroupCode == null)
                                {
                                    break;
                                }

                            }

                            materialGroupToEdit.Code = randomCode;

                        }
                        else
                        {
                            materialGroupToEdit.Code = model.Code;
                        }

                        materialGroupToEdit.DescriptionAR = model.DescriptionAR;

                        materialGroupToEdit.DescriptionEN = model.DescriptionEN;

                        materialGroupToEdit.Updated_By = _httpContextAccessor.HttpContext.User.Identity.Name;

                        materialGroupToEdit.Update_Date = await HelperFunctions.GetEgyptsCurrentLocalTime();

                        var updateMatGroupResult = await unitOfWork.MaterialGroupsManager.UpdateAsync(materialGroupToEdit);

                        if (updateMatGroupResult == true)
                        {
                            await unitOfWork.SaveChangesAsync();
                            result.Succeeded = true;
                            result.Data = mapper.Map<MaterialGroupMainViewDTO>(materialGroupToEdit);
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Failed to update material group, Please try again !");
                            result.Errors.Add("فشل تحديث مجموعة المواد ، يرجى المحاولة مرة أخرى!");
                            return result;
                        }

                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to update material group, Please try again !");
                        result.Errors.Add("فشل تحديث مجموعة المواد ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to update material group , a material group with a similar name already exists !");
                    result.Errors.Add("فشل تحديث مجموعة المواد ، توجد بالفعل مجموعة مواد تحمل اسمًا مشابهًا!");
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


        public async Task<ApiResponse<bool>> DeleteMaterialGroup(DeleteRecordModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var materialGroupsResult = await unitOfWork.MaterialGroupsManager.GetAsync(a => a.ID == (int)model.ID);

                MaterialGroup groupToDelete = materialGroupsResult.FirstOrDefault();

                if (groupToDelete != null)
                {

                    var deleteMaterialGroupResult = await unitOfWork.MaterialGroupsManager.RemoveAsync(groupToDelete);

                    if (deleteMaterialGroupResult == true)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to delete material group, Please try again !");
                        result.Errors.Add("فشل حذف مجموعة المواد ، يرجى المحاولة مرة أخرى!");
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
