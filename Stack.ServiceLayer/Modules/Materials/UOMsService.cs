using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using Stack.DTOs;
using Stack.DTOs.Models.Modules.Materials.MaterialUOMs;
using Stack.DTOs.Requests.Modules.Materials.MaterialUOMs;
using Stack.DTOs.Requests.Shared;
using Stack.Entities.Models.Modules.Materials;
using Stack.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stack.ServiceLayer.Modules.Materials
{
    public class UOMsService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public UOMsService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {

            this.unitOfWork = unitOfWork;
            this.config = config;
            this.mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<ApiResponse<List<UOMMainViewDTO>>> GetAllUOMs()
        {
            ApiResponse<List<UOMMainViewDTO>> result = new ApiResponse<List<UOMMainViewDTO>>();
            try
            {

                var UOMsQuery = await unitOfWork.UOMsManager.GetAsync();

                List<UOM> UOMsList = UOMsQuery.ToList();

                if (UOMsList != null)
                {

                    result.Succeeded = true;
                    result.Data = mapper.Map<List<UOMMainViewDTO>>(UOMsList);
                    return result;

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No units of measure were found !");
                    result.Errors.Add("لم يتم العثور على وحدات قياس!");
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


        public async Task<ApiResponse<UOMMainViewDTO>> CreateUOM(MaterialUOMCreationModel model)
        {
            ApiResponse<UOMMainViewDTO> result = new ApiResponse<UOMMainViewDTO>();
            try
            {

                var duplicateUOMQuery = await unitOfWork.UOMsManager.GetAsync(a => a.Abbreviation == model.Abbreviation);

                UOM duplicateUOM = duplicateUOMQuery.FirstOrDefault();

                if (duplicateUOM == null)
                {

                    UOM newUOM = new UOM();

                    newUOM.Abbreviation = model.Abbreviation;

                    newUOM.Creation_Date = await HelperFunctions.GetEgyptsCurrentLocalTime();

                    newUOM.Created_By = _httpContextAccessor.HttpContext.User.Identity.Name;

                    newUOM.Updated_By = newUOM.Created_By;

                    newUOM.Update_Date = newUOM.Update_Date;

                
                    var createUOMResult = await unitOfWork.UOMsManager.CreateAsync(newUOM);

                    if (createUOMResult != null)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = mapper.Map<UOMMainViewDTO>(newUOM);
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to create new unit of measure, Please try again !");
                        result.Errors.Add("فشل إنشاء وحدة قياس جديدة ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to create new unit of measure, a unit of measure with a duplicate abbreviation already exists !");
                    result.Errors.Add("فشل إنشاء وحدة قياس جديدة ، توجد بالفعل وحدة قياس ذات اختصار مكرر!");
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


        public async Task<ApiResponse<UOMMainViewDTO>> EditUOM(EditMaterialUOMModel model)
        {
            ApiResponse<UOMMainViewDTO> result = new ApiResponse<UOMMainViewDTO>();
            try
            {

                var duplicateUOMResult = await unitOfWork.UOMsManager.GetAsync(a => a.Abbreviation == model.Abbreviation && a.ID != model.UOMID);

                UOM duplicateUOM = duplicateUOMResult.FirstOrDefault();

                if (duplicateUOM == null)
                {

                    var UOMSResult = await unitOfWork.UOMsManager.GetAsync(a => a.ID == model.UOMID);

                    UOM UOMToEdit = UOMSResult.FirstOrDefault();


                    if (UOMToEdit != null)
                    {

                        UOMToEdit.Abbreviation = model.Abbreviation;

                        UOMToEdit.Updated_By = _httpContextAccessor.HttpContext.User.Identity.Name;

                        UOMToEdit.Update_Date = await HelperFunctions.GetEgyptsCurrentLocalTime();

                        var updateUOMResult = await unitOfWork.UOMsManager.UpdateAsync(UOMToEdit);

                        if (updateUOMResult == true)
                        {
                            await unitOfWork.SaveChangesAsync();
                            result.Succeeded = true;
                            result.Data = mapper.Map<UOMMainViewDTO>(UOMToEdit);
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Failed to update unit of measure, Please try again !");
                            result.Errors.Add("فشل تحديث وحدة القياس ، يرجى المحاولة مرة أخرى!");
                            return result;
                        }

                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to update unit of measure, Please try again !");
                        result.Errors.Add("فشل تحديث وحدة القياس ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to update unit of measure , a unit of measure with a similar abbreviation already exists !");
                    result.Errors.Add("فشل تحديث وحدة القياس ، توجد بالفعل وحدة قياس لها اختصار مشابه!");
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


        public async Task<ApiResponse<bool>> DeleteUOM(DeleteRecordModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var UOMsResult = await unitOfWork.UOMsManager.GetAsync(a => a.ID == (int)model.ID);

                UOM UOMToDelete = UOMsResult.FirstOrDefault();

                if (UOMToDelete != null)
                {

                    var deleteUOMResult = await unitOfWork.UOMsManager.RemoveAsync(UOMToDelete);

                    if (deleteUOMResult == true)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to delete unit of measure, Please try again !");
                        result.Errors.Add("فشل حذف وحدة القياس ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to delete unit of measure, Please try again !");
                    result.Errors.Add("فشل حذف وحدة القياس ، يرجى المحاولة مرة أخرى!");
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
