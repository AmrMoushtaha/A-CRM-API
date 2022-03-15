using AutoMapper;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Stack.Entities.Models.Modules.Organizations;
using Stack.Repository.Common;
using Stack.DTOs.Requests.Shared;
using Stack.DTOs;
using Stack.DTOs.Models.Modules.Organizations.CostCenters;
using Stack.DTOs.Requests.Modules.Organizations.CostCenters;

namespace Stack.ServiceLayer.Modules.Organizations
{
    public class CostCentersService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public CostCentersService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;    
            this.config = config;
            this.mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<CostCenterDTO>> CreateCostCenter(CreateCostCenterModel model)
        {
            ApiResponse<CostCenterDTO> result = new ApiResponse<CostCenterDTO>();
            try
            {

                var duplicateCostCenterQuery = await unitOfWork.CostCentersManager.GetAsync(a => a.Code == model.Code);

                CostCenter duplicateCostCenter = duplicateCostCenterQuery.FirstOrDefault();

                if (duplicateCostCenter == null)
                {

                    CostCenter newCostCenter = new CostCenter();

                    newCostCenter.CompanyCodeID = model.CompanyCodeID;

                    newCostCenter.DescriptionAR = model.DescriptionAR;

                    newCostCenter.DescriptionEN = model.DescriptionEN;

                    newCostCenter.Creation_Date = await HelperFunctions.GetEgyptsCurrentLocalTime();

                    newCostCenter.Created_By = _httpContextAccessor.HttpContext.User.Identity.Name;

                    newCostCenter.Updated_By = newCostCenter.Created_By;

                    newCostCenter.Update_Date = newCostCenter.Update_Date;

                    if (String.IsNullOrWhiteSpace(model.Code))
                    {

                        string randomCode;

                        while (true)
                        {

                            randomCode = await HelperFunctions.GenerateRandomNumberAsync(6);

                            var duplicateCodeResult = await unitOfWork.CostCentersManager.GetAsync(a => a.Code == randomCode);

                            CostCenter duplicateCodeCenter = duplicateCodeResult.FirstOrDefault();

                            if (duplicateCodeCenter == null)
                            {
                                break;
                            }

                        }

                        newCostCenter.Code = randomCode;

                    }
                    else
                    {

                        newCostCenter.Code = model.Code;

                    }
                    

                    var createCostCenterResult = await unitOfWork.CostCentersManager.CreateAsync(newCostCenter);

                    if (createCostCenterResult != null)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = mapper.Map<CostCenterDTO>(newCostCenter);
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to create cost center, Please try again !");
                        result.Errors.Add("فشل إنشاء مركز تكلفة ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                     result.Succeeded = false;
                        result.Errors.Add("Failed to create cost center, a cost center with a duplicate code already exists !");
                        result.Errors.Add("فشل إنشاء مركز تكلفة ، يوجد بالفعل مركز تكلفة برمز مكرر!");
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

        public async Task<ApiResponse<CostCenterDTO>> EditCostCenter(EditCostCenterModel model)
        {
            ApiResponse<CostCenterDTO> result = new ApiResponse<CostCenterDTO>();
            try
            {

                var duplicateCostCenterResult = await unitOfWork.CostCentersManager.GetAsync(a => a.Code == model.Code && a.ID != model.ID);

                CostCenter duplicateCostCenter = duplicateCostCenterResult.FirstOrDefault();

                if (duplicateCostCenter == null)
                {

                    var costCenterResult = await unitOfWork.CostCentersManager.GetAsync(a => a.ID == model.ID);

                    CostCenter costCenterToEdit = costCenterResult.FirstOrDefault();


                    if (costCenterToEdit != null)
                    {


                        if (String.IsNullOrWhiteSpace(model.Code))
                        {

                            string randomCode;

                            while (true)
                            {

                                randomCode = await HelperFunctions.GenerateRandomNumberAsync(6);

                                var duplicateCodeResult = await unitOfWork.CostCentersManager.GetAsync(a => a.Code == randomCode);

                                CostCenter duplicateCodeCenter = duplicateCodeResult.FirstOrDefault();

                                if (duplicateCodeCenter == null)
                                {
                                    break;
                                }

                            }

                            costCenterToEdit.Code = randomCode;

                        }
                        else
                        {

                            costCenterToEdit.Code = model.Code;

                        }

                        costCenterToEdit.DescriptionAR = model.DescriptionAR;

                        costCenterToEdit.DescriptionEN = model.DescriptionEN;

                        costCenterToEdit.Updated_By = _httpContextAccessor.HttpContext.User.Identity.Name;

                        costCenterToEdit.Update_Date = await HelperFunctions.GetEgyptsCurrentLocalTime();

                        var updateCostCenterResult = await unitOfWork.CostCentersManager.UpdateAsync(costCenterToEdit);

                        if (updateCostCenterResult == true)
                        {
                            await unitOfWork.SaveChangesAsync();
                            result.Succeeded = true;
                            result.Data = mapper.Map<CostCenterDTO>(costCenterToEdit);
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Failed to update cost center, Please try again !");
                            result.Errors.Add("فشل تحديث مركز التكلفة ، يرجى المحاولة مرة أخرى!");
                            return result;
                        }

                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to update cost center, Please try again !");
                        result.Errors.Add("فشل تحديث مركز التكلفة ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to update cost center , a center with a similar code already exists !");
                    result.Errors.Add("فشل تحديث مركز التكلفة ، يوجد بالفعل مركز يحمل نفس الرمز!");
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

        public async Task<ApiResponse<bool>> DeleteCostCenter(DeleteRecordModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var costCentersResult = await unitOfWork.CostCentersManager.GetAsync(a => a.ID == (int)model.ID);

                CostCenter costCenterToDelete = costCentersResult.FirstOrDefault();

                if (costCenterToDelete != null)
                {

                    var deleteCostCenterResult = await unitOfWork.CostCentersManager.RemoveAsync(costCenterToDelete);

                    if (deleteCostCenterResult == true)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to delete cost center, Please try again !");
                        result.Errors.Add("فشل حذف مركز التكلفة ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to delete cost center, Please try again !");
                    result.Errors.Add("فشل حذف مركز التكلفة ، يرجى المحاولة مرة أخرى!");
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

        public async Task<ApiResponse<List<CostCenterMainViewDTO>>> GetAllCostCenters()
        {
            ApiResponse<List<CostCenterMainViewDTO>> result = new ApiResponse<List<CostCenterMainViewDTO>>();
            try
            {

                var CostCentersQuery = await unitOfWork.CostCentersManager.GetAsync(includeProperties:"CompanyCode");

                List<CostCenter> costCentersList = CostCentersQuery.ToList();

                if (costCentersList != null)
                {

                    result.Succeeded = true;
                    result.Data = mapper.Map<List<CostCenterMainViewDTO>>(costCentersList);
                    return result;

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No cost centers were found !");
                    result.Errors.Add("لم يتم العثور على مراكز تكلفة!");
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

        public async Task<ApiResponse<List<CostCenterMainViewDTO>>> GetCostCentersByCompanyCodeID(int ID)
        {
            ApiResponse<List<CostCenterMainViewDTO>> result = new ApiResponse<List<CostCenterMainViewDTO>>();
            try
            {

                var CostCentersQuery = await unitOfWork.CostCentersManager.GetAsync( a=> a.CompanyCodeID == ID ,includeProperties: "CompanyCode");

                List<CostCenter> costCentersList = CostCentersQuery.ToList();

                if (costCentersList != null)
                {

                    result.Succeeded = true;
                    result.Data = mapper.Map<List<CostCenterMainViewDTO>>(costCentersList);
                    return result;

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No cost centers were found !");
                    result.Errors.Add("لم يتم العثور على مراكز تكلفة!");
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
