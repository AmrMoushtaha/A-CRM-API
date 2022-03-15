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
using Stack.DTOs.Models.Modules.Organizations.PurchasingGroups;
using Stack.DTOs.Requests.Modules.Organizations.PurchasingGroups;

namespace Stack.ServiceLayer.Modules.Organizations
{
    public class PurchasingGroupsService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public PurchasingGroupsService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {

            this.unitOfWork = unitOfWork;    
            this.config = config;
            this.mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<ApiResponse<PurchasingGroupDTO>> CreatePurchasingGroup(CreatePGModel model)
        {

            ApiResponse<PurchasingGroupDTO> result = new ApiResponse<PurchasingGroupDTO>();
            try
            {

                var duplicatePurchasingGroupsResult = await unitOfWork.PurchasingGroupsManager.GetAsync(a => a.Code == model.Code);

                PurchasingGroup duplicatePG = duplicatePurchasingGroupsResult.FirstOrDefault();

                if(duplicatePG == null)
                {


                    PurchasingGroup newPurchasingGroup = new PurchasingGroup();

                    newPurchasingGroup.DescriptionAR = model.DescriptionAR;

                    newPurchasingGroup.DescriptionEN = model.DescriptionEN;


                    newPurchasingGroup.PlantID = model.PlantId;

                    newPurchasingGroup.Creation_Date = await HelperFunctions.GetEgyptsCurrentLocalTime();

                    newPurchasingGroup.Created_By = _httpContextAccessor.HttpContext.User.Identity.Name;

                    newPurchasingGroup.Updated_By = newPurchasingGroup.Created_By;

                    newPurchasingGroup.Update_Date = newPurchasingGroup.Update_Date;


                    if (String.IsNullOrWhiteSpace(model.Code))
                    {

                        string randomCode;

                        while (true)
                        {

                            randomCode = await HelperFunctions.GenerateRandomNumberAsync(6);

                            var duplicateCodeResult = await unitOfWork.PurchasingGroupsManager.GetAsync(a => a.Code == randomCode);

                            PurchasingGroup duplicatePurchasingGroupCode = duplicateCodeResult.FirstOrDefault();

                            if (duplicatePurchasingGroupCode == null)
                            {
                                break;
                            }

                        }

                        newPurchasingGroup.Code = randomCode;

                    }
                    else
                    {

                        newPurchasingGroup.Code = model.Code;

                    }
                               
                    var createPurchasingGroupResult = await unitOfWork.PurchasingGroupsManager.CreateAsync(newPurchasingGroup);
             
                    if(createPurchasingGroupResult != null)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = mapper.Map<PurchasingGroupDTO>(createPurchasingGroupResult);
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to create purchasing group, Please try again !");
                        result.Errors.Add("فشل إنشاء مجموعة الشراء ، يرجى المحاولة مرة أخرى!");
                        return result;
                    } 

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to create purchasing group , a group with a similar code already exists !");
                    result.Errors.Add("فشل إنشاء مجموعة شراء ، توجد بالفعل مجموعة تحمل رمزًا مشابهًا!");
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

        public async Task<ApiResponse<PurchasingGroupDTO>> EditPurchasingGroup(EditPGModel model)
        {
            ApiResponse<PurchasingGroupDTO> result = new ApiResponse<PurchasingGroupDTO>();
            try
            {

                var duplicatePurchasingGroupsResult = await unitOfWork.PurchasingGroupsManager.GetAsync(a =>  a.Code == model.Code && a.ID != model.ID && a.PlantID == model.PlantID);

                PurchasingGroup duplicatePG = duplicatePurchasingGroupsResult.FirstOrDefault();

                if (duplicatePG == null)
                {

                    var purchasingGroupsResult = await unitOfWork.PurchasingGroupsManager.GetAsync(a => a.ID == model.ID);

                    PurchasingGroup purchasingGroupToEdit = purchasingGroupsResult.FirstOrDefault();

                    if (purchasingGroupToEdit != null)
                    {


                        if (String.IsNullOrWhiteSpace(model.Code))
                        {

                            string randomCode;

                            while (true)
                            {

                                randomCode = await HelperFunctions.GenerateRandomNumberAsync(6);

                                var duplicateCodeResult = await unitOfWork.PurchasingGroupsManager.GetAsync(a => a.Code == randomCode);

                                PurchasingGroup duplicatePurchasingGroupCode = duplicateCodeResult.FirstOrDefault();

                                if (duplicatePurchasingGroupCode == null)
                                {
                                    break;
                                }

                            }

                            purchasingGroupToEdit.Code = randomCode;

                        }
                        else
                        {

                            purchasingGroupToEdit.Code = model.Code;

                        }

                        purchasingGroupToEdit.DescriptionAR = model.DescriptionAR;

                        purchasingGroupToEdit.DescriptionEN = model.DescriptionEN;

                        //purchasingGroupToEdit.PlantID = model.PlantID;


                        purchasingGroupToEdit.Updated_By = _httpContextAccessor.HttpContext.User.Identity.Name;

                        purchasingGroupToEdit.Update_Date = await HelperFunctions.GetEgyptsCurrentLocalTime();

                        var updatePurchasingGroupResult = await unitOfWork.PurchasingGroupsManager.UpdateAsync(purchasingGroupToEdit);

                        if(updatePurchasingGroupResult == true)
                        {
                            await unitOfWork.SaveChangesAsync();
                            result.Succeeded = true;
                            result.Data = mapper.Map<PurchasingGroupDTO>(purchasingGroupToEdit);
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Failed to update purchasing group, Please try again !");
                            result.Errors.Add("فشل تعديل مجموعة الشراء ، يرجى المحاولة مرة أخرى!");
                            return result;
                        }

                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to update purchasing group, Please try again !");
                        result.Errors.Add("فشل تعديل مجموعة الشراء ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }                 

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to update purchasing group , a group with a similar code already exists !");
                    result.Errors.Add("فشل تحرير مجموعة الشراء ، توجد بالفعل مجموعة تحمل رمزًا مشابهًا!");
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

        public async Task<ApiResponse<bool>> DeletePurchasingGroup (DeleteRecordModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var purchasingGroupsResult = await unitOfWork.PurchasingGroupsManager.GetAsync(a => a.ID == (int)model.ID);

                PurchasingGroup purchasingGroupToDelete = purchasingGroupsResult.FirstOrDefault();

                if(purchasingGroupToDelete != null)
                {

                    var deletePurchasingGroupResult = await unitOfWork.PurchasingGroupsManager.RemoveAsync(purchasingGroupToDelete);

                    if(deletePurchasingGroupResult == true)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to delete purchasing group, Please try again !");
                        result.Errors.Add("فشل حذف مجموعة الشراء ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to delete purchasing group, Please try again !");
                    result.Errors.Add("فشل حذف مجموعة الشراء ، يرجى المحاولة مرة أخرى!");
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

        public async Task<ApiResponse<List<PurchasingGroupMainViewDTO>>> GetAllPurchasingGroups()
        {
            ApiResponse<List<PurchasingGroupMainViewDTO>> result = new ApiResponse<List<PurchasingGroupMainViewDTO>>();
            try
            {

                var purchasingGroupsQuery = await unitOfWork.PurchasingGroupsManager.GetAsync(includeProperties:"Plant");

                List<PurchasingGroup> purchasingGroupsList = purchasingGroupsQuery.ToList();

                if (purchasingGroupsList != null)
                {

                    result.Succeeded = true;
                    result.Data = mapper.Map<List<PurchasingGroupMainViewDTO>>(purchasingGroupsList);
                    return result;

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No purchasing groups were found !");
                    result.Errors.Add("لم يتم العثور على مجموعات شراء!");
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
