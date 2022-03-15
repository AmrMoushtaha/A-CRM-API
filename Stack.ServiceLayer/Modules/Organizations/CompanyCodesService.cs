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
using Stack.DTOs.Models.Modules.Organizations.CompanyCodes;
using Stack.DTOs.Requests.Modules.Organizations.CompanyCodes;


namespace Stack.ServiceLayer.Modules.Organizations
{
    public class CompanyCodesService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;


        public CompanyCodesService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;

        }

        public async Task<ApiResponse<CompanyCodeDTO>> CreateCompanyCode(CreateCompanyCodeModel model)
        {
            ApiResponse<CompanyCodeDTO> result = new ApiResponse<CompanyCodeDTO>();
            try
            {

                var duplicateCompanyCodeQuery = await unitOfWork.CompanyCodesManager.GetAsync(a => a.Code == model.Code);

                CompanyCode duplicateCompanyCode = duplicateCompanyCodeQuery.FirstOrDefault();

                if (duplicateCompanyCode == null)
                {

                    CompanyCode newCompanyCode = new CompanyCode();

                    newCompanyCode.Currency = model.Currency;

                    newCompanyCode.DescriptionAR = model.DescriptionAR;

                    newCompanyCode.DescriptionEN = model.DescriptionEN;

                    newCompanyCode.Creation_Date = await HelperFunctions.GetEgyptsCurrentLocalTime();

                    newCompanyCode.Created_By = _httpContextAccessor.HttpContext.User.Identity.Name;

                    newCompanyCode.Updated_By = newCompanyCode.Created_By;

                    newCompanyCode.Update_Date = newCompanyCode.Update_Date;


                    if (String.IsNullOrWhiteSpace(model.Code))
                    {

                        string randomCode;

                        while (true)
                        {

                            randomCode = await HelperFunctions.GenerateRandomNumberAsync(6);

                            var duplicateCodeResult = await unitOfWork.CompanyCodesManager.GetAsync(a => a.Code == randomCode);

                            CompanyCode duplicateCode = duplicateCodeResult.FirstOrDefault();

                            if (duplicateCode == null)
                            {
                                break;
                            }

                        }

                        newCompanyCode.Code = randomCode;

                    }
                    else
                    {

                        newCompanyCode.Code = model.Code;

                    }


                    var createCompanyCodeResult = await unitOfWork.CompanyCodesManager.CreateAsync(newCompanyCode);

                    if (createCompanyCodeResult != null)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = mapper.Map<CompanyCodeDTO>(newCompanyCode);
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to create company code, Please try again !");
                        result.Errors.Add("فشل إنشاء رمز الشركة ، يرجى المحاولة مرة أخرى");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to create company code, a duplicate code already exists!");
                    result.Errors.Add("فشل إنشاء رمز الشركة ، يوجد بالفعل رمز مكرر!");
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

        public async Task<ApiResponse<CompanyCodeDTO>> EditCompanyCode(EditCompanyCodeModel model)
        {
            ApiResponse<CompanyCodeDTO> result = new ApiResponse<CompanyCodeDTO>();
            try
            {

                var duplicateCompanyCodeQuery = await unitOfWork.CompanyCodesManager.GetAsync(a => a.Code == model.Code && a.ID != model.ID);

                CompanyCode duplicateCompanyCode = duplicateCompanyCodeQuery.FirstOrDefault();

                if (duplicateCompanyCode == null)
                {

                    var companyCodeResult = await unitOfWork.CompanyCodesManager.GetAsync(a => a.ID == model.ID);

                    CompanyCode CompanyCodeToEdit = companyCodeResult.FirstOrDefault();


                    if (CompanyCodeToEdit != null)
                    {

                        if (String.IsNullOrWhiteSpace(model.Code))
                        {

                            string randomCode;

                            while (true)
                            {

                                randomCode = await HelperFunctions.GenerateRandomNumberAsync(6);

                                var duplicateCodeResult = await unitOfWork.CompanyCodesManager.GetAsync(a => a.Code == randomCode);

                                CompanyCode duplicateCode = duplicateCodeResult.FirstOrDefault();

                                if (duplicateCode == null)
                                {
                                    break;
                                }

                            }

                            CompanyCodeToEdit.Code = randomCode;

                        }
                        else
                        {

                            CompanyCodeToEdit.Code = model.Code;

                        }

                        CompanyCodeToEdit.Currency = model.Currency;

                        CompanyCodeToEdit.DescriptionAR = model.DescriptionAR;

                        CompanyCodeToEdit.DescriptionEN = model.DescriptionEN;

                        CompanyCodeToEdit.Updated_By = _httpContextAccessor.HttpContext.User.Identity.Name;

                        CompanyCodeToEdit.Update_Date = await HelperFunctions.GetEgyptsCurrentLocalTime();

                        var updateCompanyCodeResult = await unitOfWork.CompanyCodesManager.UpdateAsync(CompanyCodeToEdit);

                        if (updateCompanyCodeResult == true)
                        {
                            await unitOfWork.SaveChangesAsync();
                            result.Succeeded = true;
                            result.Data = mapper.Map<CompanyCodeDTO>(CompanyCodeToEdit);
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Failed to update company code, Please try again !");
                            result.Errors.Add("فشل تحديث رمز الشركة ، يرجى المحاولة مرة أخرى!");
                            return result;
                        }

                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to update company code, Please try again !");
                        result.Errors.Add("فشل تحديث رمز الشركة ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to update company code , a company with a similar code already exists !");
                    result.Errors.Add("فشل تحديث رمز الشركة ، توجد بالفعل شركة برمز مشابه!");
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

        public async Task<ApiResponse<bool>> DeleteCompanyCode(DeleteRecordModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var companyCodeResult = await unitOfWork.CompanyCodesManager.GetAsync(a => a.ID == (int)model.ID);

                CompanyCode companyCodeToDelete = companyCodeResult.FirstOrDefault();

                if (companyCodeToDelete != null)
                {

                    var deleteCompanyCodeResult = await unitOfWork.CompanyCodesManager.RemoveAsync(companyCodeToDelete);

                    if (deleteCompanyCodeResult == true)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to delete company code, Please try again !");
                        result.Errors.Add("فشل حذف رمز الشركة ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to delete company code, Please try again !");
                    result.Errors.Add("فشل حذف رمز الشركة ، يرجى المحاولة مرة أخرى!");
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

        public async Task<ApiResponse<List<CompanyCodeDTO>>> GetAllCompanyCodes()
        {
            ApiResponse<List<CompanyCodeDTO>> result = new ApiResponse<List<CompanyCodeDTO>>();
            try
            {

                var companyCodesQuery = await unitOfWork.CompanyCodesManager.GetAsync();

                List<CompanyCode> CompanyCodesList = companyCodesQuery.ToList();

                if (CompanyCodesList != null)
                {

                    result.Succeeded = true;
                    result.Data = mapper.Map<List<CompanyCodeDTO>>(CompanyCodesList);
                    return result;
                    
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No company codes were found !");
                    result.Errors.Add("لم يتم العثور على رموز الشركة!");
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
