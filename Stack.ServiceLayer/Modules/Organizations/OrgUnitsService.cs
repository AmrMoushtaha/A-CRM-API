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
using Stack.DTOs.Requests.Modules.Organizations.OrgUnits;
using Stack.DTOs.Models.Modules.Organizations.OrgUnits;
using Stack.Entities.Models.Modules.Employees;
using Stack.DTOs.Models.Modules.Employees.Positions;

namespace Stack.ServiceLayer.Modules.Organizations
{
    public class OrgUnitsService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public OrgUnitsService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
            this.mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<OrgUnitDTO>> CreateOrgUnit(CreateOrgUnitModel model)
        {
            ApiResponse<OrgUnitDTO> result = new ApiResponse<OrgUnitDTO>();
            try
            {

                var duplicateOrgUnitsResult = await unitOfWork.OrgUnitsManager.GetAsync(a => a.Code == model.Code);

                OrgUnit duplicateOrgUnit = duplicateOrgUnitsResult.FirstOrDefault();

                if (duplicateOrgUnit == null)
                {

                    OrgUnit newOrgUnit = new OrgUnit();

                    newOrgUnit.CompanyCodeID = model.CompanyCodeID;

                    newOrgUnit.IsPurchasing = model.IsPurchasing;

                    newOrgUnit.ManagerId = model.ManagerID;

                    newOrgUnit.DescriptionAR = model.DescriptionAR;

                    newOrgUnit.DescriptionEN = model.DescriptionEN;

                    newOrgUnit.Creation_Date = await HelperFunctions.GetEgyptsCurrentLocalTime();

                    newOrgUnit.Created_By = _httpContextAccessor.HttpContext.User.Identity.Name;

                    newOrgUnit.Updated_By = newOrgUnit.Created_By;

                    newOrgUnit.Update_Date = newOrgUnit.Update_Date;     

                    if (String.IsNullOrWhiteSpace(model.Code))
                    {

                        string randomCode;

                        while (true)
                        {

                            randomCode = await HelperFunctions.GenerateRandomNumberAsync(6);

                            var duplicatedCodeResult = await unitOfWork.OrgUnitsManager.GetAsync(a => a.Code == randomCode);

                            OrgUnit duplicateCodeOrgUnit = duplicateOrgUnitsResult.FirstOrDefault();

                            if (duplicateOrgUnit == null)
                            {
                                break;
                            }

                        }

                        newOrgUnit.Code = randomCode;

                    }
                    else
                    {

                        newOrgUnit.Code = model.Code;

                    }

        
                    var createOrgUnitResult = await unitOfWork.OrgUnitsManager.CreateAsync(newOrgUnit);

                    if (createOrgUnitResult != null)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = mapper.Map<OrgUnitDTO>(createOrgUnitResult);
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to create organizational unit, Please try again !");
                        result.Errors.Add("فشل إنشاء وحدة تنظيمية ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to create organizational unit, a unit with a similar code already exists, Please try again !");
                    result.Errors.Add("فشل إنشاء وحدة تنظيمية ، توجد بالفعل وحدة برمز مشابه ، يرجى المحاولة مرة أخرى!");
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

        public async Task<ApiResponse<OrgUnitDTO>> EditOrgUnit(EditOrgUnitModel model)
        {
            ApiResponse<OrgUnitDTO> result = new ApiResponse<OrgUnitDTO>();
            try
            {

                var duplicateOrgUnitsResult = await unitOfWork.OrgUnitsManager.GetAsync(a => a.Code == model.Code && a.ID != model.ID);

                OrgUnit duplicateOrgUnit = duplicateOrgUnitsResult.FirstOrDefault();

                if (duplicateOrgUnit == null)
                {

                    var orgUnitsResult = await unitOfWork.OrgUnitsManager.GetAsync(a => a.ID == model.ID);

                    OrgUnit orgUnitToEdit = orgUnitsResult.FirstOrDefault();

                    if (orgUnitToEdit != null)
                    {

                        if (String.IsNullOrWhiteSpace(model.Code))
                        {

                            string randomCode;

                            while (true)
                            {

                                randomCode = await HelperFunctions.GenerateRandomNumberAsync(6);

                                var duplicatedCodeResult = await unitOfWork.OrgUnitsManager.GetAsync(a => a.Code == randomCode);

                                OrgUnit duplicateCodeOrgUnit = duplicateOrgUnitsResult.FirstOrDefault();

                                if (duplicateOrgUnit == null)
                                {
                                    break;
                                }

                            }

                            orgUnitToEdit.Code = randomCode;

                        }
                        else
                        {

                            orgUnitToEdit.Code = model.Code;

                        }

                        orgUnitToEdit.DescriptionAR = model.DescriptionAR;

                        orgUnitToEdit.DescriptionEN = model.DescriptionEN;

                        orgUnitToEdit.Updated_By = _httpContextAccessor.HttpContext.User.Identity.Name;

                        orgUnitToEdit.Update_Date = await HelperFunctions.GetEgyptsCurrentLocalTime();

                        orgUnitToEdit.ManagerId = model.ManagerID;

                        //orgUnitToEdit.CompanyCodeID = model.CompanyCodeID;

                        orgUnitToEdit.IsPurchasing = model.IsPurchasing;

                        var updateOrgUnitResult = await unitOfWork.OrgUnitsManager.UpdateAsync(orgUnitToEdit);

                        if (updateOrgUnitResult == true)
                        {
                            await unitOfWork.SaveChangesAsync();
                            result.Succeeded = true;
                            result.Data = mapper.Map<OrgUnitDTO>(orgUnitToEdit);
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Failed to update organizational unit, Please try again !");
                            result.Errors.Add("فشل تعديل وحدة تنظيمية ، يرجى المحاولة مرة أخرى!");
                            return result;
                        }

                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to update organizational unit, Please try again !");
                        result.Errors.Add("فشل تعديل وحدة تنظيمية ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to create organizational unit , a unit with a similar code already exists !");
                    result.Errors.Add("فشل إنشاء وحدة تنظيمية ، توجد بالفعل مجموعة بنفس الرمز!");
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

        public async Task<ApiResponse<bool>> DeleteOrgUnit(DeleteRecordModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var orgUnitsResult = await unitOfWork.OrgUnitsManager.GetAsync(a => a.ID == (int)model.ID);

                OrgUnit orgUnitToDelete = orgUnitsResult.FirstOrDefault();

                if (orgUnitToDelete != null)
                {

                    var deleteOrgUnitResult = await unitOfWork.OrgUnitsManager.RemoveAsync(orgUnitToDelete);

                    if (deleteOrgUnitResult == true)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to delete organizational unit, Please try again !");
                        result.Errors.Add("فشل حذف وحدة تنظيمية ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to delete organizational unit, Please try again !");
                    result.Errors.Add("فشل حذف وحدة تنظيمية ، يرجى المحاولة مرة أخرى!");
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

        public async Task<ApiResponse<List<OrgUnitsMainViewDTO>>> GetAllOrgUnits()
        {
            ApiResponse<List<OrgUnitsMainViewDTO>> result = new ApiResponse<List<OrgUnitsMainViewDTO>>();
            try
            {

                var OrgUnitsQuery = await unitOfWork.OrgUnitsManager.GetAsync(includeProperties: "CompanyCode");

                List<OrgUnit> OrgUnits = OrgUnitsQuery.ToList();

                List<OrgUnitsMainViewDTO> OrgUnitsList = mapper.Map<List<OrgUnitsMainViewDTO>>(OrgUnits);

                if (OrgUnitsList != null)
                {

                    for (int i = 0; i < OrgUnitsList.Count; i++)
                    {

                        var employeeResult = await unitOfWork.EmployeesManager.GetAsync(a => a.ID == OrgUnitsList[i].ManagerId, includeProperties: "ApplicationUser");

                        Employee OrgUnitManager = employeeResult.FirstOrDefault();

                        if (OrgUnitManager != null)
                        {

                            OrgUnitsList[i].ManagerNameEN = OrgUnitManager.ApplicationUser.NameEN;

                            OrgUnitsList[i].ManagerNameAR = OrgUnitManager.ApplicationUser.NameAR;

                        }
                        else
                        {

                            OrgUnitsList[i].ManagerNameEN = "-";

                            OrgUnitsList[i].ManagerNameAR = "-";

                        }

                    }

                    result.Succeeded = true;
                    result.Data = OrgUnitsList;
                    return result;

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No organizational units were found !");
                    result.Errors.Add("لم يتم العثور على وحدات تنظيمية!");
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

        public async Task<ApiResponse<List<OrgUnitsMainViewDTO>>> GetAllCompanyCodeOrgUnits(long companyCodeID)
        {
            ApiResponse<List<OrgUnitsMainViewDTO>> result = new ApiResponse<List<OrgUnitsMainViewDTO>>();
            try
            {
                var OrgUnitsQuery = await unitOfWork.OrgUnitsManager.GetAsync(o => o.CompanyCodeID == companyCodeID , includeProperties: "CompanyCode");
                var OrgUnitsList = OrgUnitsQuery.ToList();

                if (OrgUnitsList != null)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<OrgUnitsMainViewDTO>>(OrgUnitsList);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No organizational units were found !");
                    result.Errors.Add("لم يتم العثور على وحدات تنظيمية!");
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

        public async Task<ApiResponse<List<PositionDTO>>> GetAllAvailableOrgUnitPositions(long orgUnitID)
        {
            ApiResponse<List<PositionDTO>> result = new ApiResponse<List<PositionDTO>>();
            try
            {
                var positionQuery = await unitOfWork.PositionsManager.GetAsync(o => o.OrgUnitID == orgUnitID);
                var positions = positionQuery.ToList();

                if (positions != null)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<PositionDTO>>(positions);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No organizational unit positions were found !");
                    result.Errors.Add("لم يتم العثور على مناصب وحدات تنظيمية!");
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
