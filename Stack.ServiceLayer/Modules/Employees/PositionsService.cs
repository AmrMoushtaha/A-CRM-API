
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using Stack.DTOs;
using Stack.DTOs.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Stack.Entities.Models.Modules.Employees;
using Stack.DTOs.Requests.Modules.Employees;
using Stack.Repository.Common;
using Stack.DTOs.Models.Modules.Employees.Positions;
using Stack.DTOs.Requests.Shared;
using System.Collections.Generic;

namespace Stack.ServiceLayer.Modules.Employees
{
    public class PositionsService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public PositionsService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;

        }


        public async Task<ApiResponse<PositionMainViewDTO>> CreatePosition(PositionCreationModel model)
        {
            ApiResponse<PositionMainViewDTO> result = new ApiResponse<PositionMainViewDTO>();
            try
            {

                var duplicatePositionQuery = await unitOfWork.PositionsManager.GetAsync(a => a.Code == model.Code  && a.OrgUnitID == model.OrgUnitID);

                Position duplicatePosition = duplicatePositionQuery.FirstOrDefault();

                if (duplicatePosition == null)
                {

                    Position NewPosition = new Position();

                    NewPosition.OrgUnitID = model.OrgUnitID;

                    NewPosition.DescriptionAR = model.DescriptionAR;

                    NewPosition.DescriptionEN = model.DescriptionEN;

                    NewPosition.Creation_Date = await HelperFunctions.GetEgyptsCurrentLocalTime();

                    NewPosition.Created_By = _httpContextAccessor.HttpContext.User.Identity.Name;

                    NewPosition.Updated_By = NewPosition.Created_By;

                    NewPosition.Update_Date = NewPosition.Update_Date;

                    if (String.IsNullOrWhiteSpace(model.Code))
                    {

                        string randomCode;

                        while (true)
                        {

                            randomCode = await HelperFunctions.GenerateRandomNumberAsync(6);

                            var duplicateCodeResult = await unitOfWork.PositionsManager.GetAsync(a => a.Code == randomCode);

                            Position duplicatePositionCode = duplicateCodeResult.FirstOrDefault();

                            if (duplicatePositionCode == null)
                            {
                                break;
                            }

                        }



                        NewPosition.Code = randomCode;

                    }
                    else
                    {



                        NewPosition.Code = model.Code;

                    }


                    var createPositionResult = await unitOfWork.PositionsManager.CreateAsync(NewPosition);

                    if (createPositionResult != null)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = mapper.Map<PositionMainViewDTO>(NewPosition);
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to create new position, Please try again !");
                        result.Errors.Add("فشل إنشاء منصب جديد ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to create new positon, a position with a duplicate code already exists !");
                    result.Errors.Add("فشل إنشاء منصب جديد ، يوجد بالفعل منصب برمز مكرر!");
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


        public async Task<ApiResponse<PositionMainViewDTO>> EditPosition(EditPositionModel model)
        {
            ApiResponse<PositionMainViewDTO> result = new ApiResponse<PositionMainViewDTO>();
            try
            {

                var duplicatePositionModel = await unitOfWork.PositionsManager.GetAsync(a => a.Code == model.Code  && a.ID != model.PositionID && a.OrgUnitID  == model.OrgUnitID);

                Position duplicatePosition = duplicatePositionModel.FirstOrDefault();

                if (duplicatePosition == null)
                {

                    var positionResult = await unitOfWork.PositionsManager.GetAsync(a => a.ID == model.PositionID);

                    Position positionToEdit = positionResult.FirstOrDefault();


                    if (positionToEdit != null)
                    {

                        if (String.IsNullOrWhiteSpace(model.Code))
                        {

                            string randomCode;

                            while (true)
                            {

                                randomCode = await HelperFunctions.GenerateRandomNumberAsync(6);

                                var duplicateCodeResult = await unitOfWork.PositionsManager.GetAsync(a => a.Code == randomCode);

                                Position duplicatePositionCode = duplicateCodeResult.FirstOrDefault();

                                if (duplicatePositionCode == null)
                                {
                                    break;
                                }

                            }

                            positionToEdit.Code = randomCode;

                        }
                        else
                        {
                            positionToEdit.Code = model.Code;
                        }

                        positionToEdit.DescriptionAR = model.DescriptionAR;

                        positionToEdit.DescriptionEN = model.DescriptionEN;

                        positionToEdit.Updated_By = _httpContextAccessor.HttpContext.User.Identity.Name;

                        positionToEdit.Update_Date = await HelperFunctions.GetEgyptsCurrentLocalTime();

                        var updatePositionResult = await unitOfWork.PositionsManager.UpdateAsync(positionToEdit);

                        if (updatePositionResult == true)
                        {
                            await unitOfWork.SaveChangesAsync();
                            result.Succeeded = true;
                            result.Data = mapper.Map<PositionMainViewDTO>(positionToEdit);
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Failed to update position, Please try again !");
                            result.Errors.Add("فشل تحديث الموقف ، يرجى المحاولة مرة أخرى!");
                            return result;
                        }

                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to update position, Please try again !");
                        result.Errors.Add("فشل تحديث الموقف ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to update position , a position with a similar name already exists !");
                    result.Errors.Add("فشل تحديث الوظيفة ، يوجد بالفعل منصب يحمل نفس الاسم!");
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


        public async Task<ApiResponse<bool>> DeletePosition(DeleteRecordModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var positionsResult = await unitOfWork.PositionsManager.GetAsync(a => a.ID == (int)model.ID);

                Position positionToDelete = positionsResult.FirstOrDefault();

                if (positionToDelete != null)
                {

                    var deletePositionResult = await unitOfWork.PositionsManager.RemoveAsync(positionToDelete);

                    if (deletePositionResult == true)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to delete position, Please try again !");
                        result.Errors.Add("فشل حذف المنصب ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to delete position, Please try again !");
                    result.Errors.Add("فشل حذف المنصب ، يرجى المحاولة مرة أخرى!");
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



        public async Task<ApiResponse<List<PositionMainViewDTO>>> GetPositionsByOrgUnitID(int OrgUnitID)
        {
            ApiResponse<List<PositionMainViewDTO>> result = new ApiResponse<List<PositionMainViewDTO>>();
            try
            {

                var PositionsQuery = await unitOfWork.PositionsManager.GetAsync( a => a.OrgUnitID == OrgUnitID, includeProperties: "OrgUnit");

                List<Position> PositionsList = PositionsQuery.ToList();

                if (PositionsList != null)
                {

                    result.Succeeded = true;
                    result.Data = mapper.Map<List<PositionMainViewDTO>>(PositionsList);
                    return result;

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No positions were found !");
                    result.Errors.Add("لم يتم العثور على مناصب!");
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


        public async Task<ApiResponse<List<PositionMainViewDTO>>> GetAllPositions()
        {
            ApiResponse<List<PositionMainViewDTO>> result = new ApiResponse<List<PositionMainViewDTO>>();
            try
            {

                var PositionsQuery = await unitOfWork.PositionsManager.GetAsync(includeProperties: "OrgUnit");

                List<Position> PositionsList = PositionsQuery.ToList();

                if (PositionsList != null)
                {

                    result.Succeeded = true;
                    result.Data = mapper.Map<List<PositionMainViewDTO>>(PositionsList);
                    return result;

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No positions were found !");
                    result.Errors.Add("لم يتم العثور على مناصب!");
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


