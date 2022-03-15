
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using Stack.DTOs;
using Stack.DTOs.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Stack.DTOs.Requests.Modules.Auth;
using Stack.DTOs.Models.Modules.Employees;
using Stack.Entities.Models.Modules.Employees;
using Stack.DTOs.Requests.Modules.Employees;
using Stack.Repository.Common;
using Stack.Entities.Models.Modules.Auth;
using Stack.DTOs.Models.Shared;

namespace Stack.ServiceLayer.Modules.Employees
{
    public class EmployeesService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public EmployeesService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;

        }


        public async Task<ApiResponse<List<EmployeeMasterDataTableViewModel>>> GetAllEmployees()
        {
            ApiResponse<List<EmployeeMasterDataTableViewModel>> result = new ApiResponse<List<EmployeeMasterDataTableViewModel>>();
            try
            {
                var employeesQ = await unitOfWork.EmployeesManager.GetAsync(includeProperties: "ApplicationUser,Addresses,CostCenter,CostCenter.CompanyCode");
                var employees = employeesQ.ToList();

                if (employees != null && employees.Count > 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<EmployeeMasterDataTableViewModel>>(employees);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No employees found");
                    result.Errors.Add("لم يتم العثور على موظفين");
                    result.ErrorType = ErrorType.NotFound;
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

        public async Task<ApiResponse<EmployeeMainViewModel>> GetEmployee(long ID)
        {
            ApiResponse<EmployeeMainViewModel> result = new ApiResponse<EmployeeMainViewModel>();
            try
            {
                var modelQuery = await unitOfWork.EmployeesManager.GetAsync(e => e.ID == ID, includeProperties: "ApplicationUser,Addresses,CostCenter,CostCenter.CompanyCode,PhoneNumbers,Employee_Positions,Employee_Positions.Position,Employee_Positions.Position.OrgUnit");
                var model = modelQuery.FirstOrDefault();

                if (model != null)
                {
                    result.Succeeded = true;
                    List<Employee_OrgUnits> employeeOrgUnits = mapper.Map<List<Employee_OrgUnits>>(model.Employee_Positions);

                    //foreach (var position in model.Employee_Positions)
                    //{
                    //    var Employee_Position = new Employee_OrgUnits
                    //    {
                    //        OrgUnitId = position.Position.OrgUnitID,
                    //        OrgUnitDescriptionEN = position.Position.OrgUnit.DescriptionEN,
                    //        OrgUnitDescriptionAR = position.Position.OrgUnit.DescriptionAR,
                    //        PositionId = position.PositionID,
                    //        PositionDescriptionEN = position.Position.DescriptionEN,
                    //        PositionDescriptionAR = position.Position.DescriptionAR,
                    //        Percentage = position.Percentage,
                    //        StartDate = position.StartDate,
                    //    };

                    //    employeeOrgUnits.Add(Employee_Position);
                    //}

                    List<Employee_PhoneNumberDTO> employee_PhoneNumbers = new List<Employee_PhoneNumberDTO>();
                    var employeeMainPhoneNumber = new Employee_PhoneNumberDTO
                    {
                        ID = 0,
                        Number = model.ApplicationUser.PhoneNumber.ToString()
                    };
                    employee_PhoneNumbers.Add(employeeMainPhoneNumber);
                    employee_PhoneNumbers.AddRange(mapper.Map<List<Employee_PhoneNumberDTO>>(model.PhoneNumbers));

                    List<Employee_Address> employee_Addresses = mapper.Map<List<Employee_Address>>(model.Addresses);

                    EmployeeMainViewModel employeeMainViewModel = new EmployeeMainViewModel
                    {
                        ID = model.ID,
                        MasterData = new Employee_MasterData
                        {
                            NameEN = model.ApplicationUser.NameEN,
                            NameAR = model.ApplicationUser.NameAR,
                            Email = model.ApplicationUser.Email,
                            Gender = model.ApplicationUser.Gender,
                            HiringDate = model.HiringDate,
                            StartDate = model.StartDate,
                            UserName = model.ApplicationUser.UserName
                        },
                        Communication = new Employee_Communication
                        {
                            Addresses = employee_Addresses,
                            PhoneNumbers = employee_PhoneNumbers,
                        },
                        CompanyCode = new Employee_CompanyCode
                        {
                            CodeID = model.CostCenter.CompanyCode.ID,
                            Code = model.CostCenter.CompanyCode.Code,
                            OrgUnits = employeeOrgUnits
                        }
                    };
                    result.Data = employeeMainViewModel;
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Employee not found");
                    result.Errors.Add("لم يتم العثور على الموظف");
                    result.ErrorType = ErrorType.NotFound;
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

        //MasterData
        public async Task<ApiResponse<bool>> EditEmployeeMasterData(EmployeeModificationModel_MasterData modificationModel)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var modelQuery = await unitOfWork.EmployeesManager.GetAsync(e => e.ID == modificationModel.ID, includeProperties: "ApplicationUser");
                var model = modelQuery.FirstOrDefault();

                if (model != null)
                {
                    model.ApplicationUser.NameAR = modificationModel.NameAR;
                    model.ApplicationUser.NameEN = modificationModel.NameEN;
                    model.ApplicationUser.UserName = modificationModel.UserName;
                    model.ApplicationUser.Email = modificationModel.Email;
                    model.ApplicationUser.Gender = modificationModel.Gender;
                    model.StartDate = modificationModel.StartDate;
                    model.HiringDate = modificationModel.HiringDate;

                    var updateRes = await unitOfWork.EmployeesManager.UpdateAsync(model);
                    if (updateRes)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to update employee details");
                        result.Errors.Add("فشل تحديث تفاصيل الموظف");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Employee not found");
                    result.Errors.Add("لم يتم العثور على الموظف");
                    result.ErrorType = ErrorType.NotFound;
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

        //Communication
        public async Task<ApiResponse<bool>> EditEmployeeCommunication(EmployeeModificationModel_Communication modificationModel)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var modelQuery = await unitOfWork.EmployeesManager.GetAsync(e => e.ID == modificationModel.ID, includeProperties: "ApplicationUser,Addresses");
                var model = modelQuery.FirstOrDefault();

                if (model != null)
                {
                    model.ApplicationUser.PhoneNumber = modificationModel.PhoneNumber;

                    var phoneUpdateRes = await unitOfWork.EmployeesManager.UpdateAsync(model);
                    if (phoneUpdateRes)
                    {
                        await unitOfWork.SaveChangesAsync();

                        if (model.Addresses != null && model.Addresses.Count > 0)
                        {
                            var addressToUpdate = model.Addresses[0];

                            addressToUpdate.Country = modificationModel.Country;
                            addressToUpdate.Governorate = modificationModel.Governorate;
                            addressToUpdate.District = modificationModel.District;
                            addressToUpdate.Street = modificationModel.Street;
                            addressToUpdate.BuildingNumber = modificationModel.Building;
                            addressToUpdate.Updated_By = _httpContextAccessor.HttpContext.User.Identity.Name;
                            addressToUpdate.Update_Date = await HelperFunctions.GetEgyptsCurrentLocalTime();

                            var updateRes = await unitOfWork.EmployeeAddressesManager.UpdateAsync(addressToUpdate);
                            if (updateRes)
                            {
                                await unitOfWork.SaveChangesAsync();
                                result.Succeeded = true;
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("Failed to update address details");
                                result.Errors.Add("فشل تحديث تفاصيل العنوان");
                                return result;
                            }
                        }

                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        return result;

                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to update Employee details");
                        result.Errors.Add("فشل تحديث بيانات الموظف");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Employee not found");
                    result.Errors.Add("لم يتم العثور على الموظف");
                    result.ErrorType = ErrorType.NotFound;
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

        public async Task<ApiResponse<Employee_PhoneNumberDTO>> AddEmployeePhoneNumber(EmployeePhoneNumberManagementModel creationModel)
        {
            ApiResponse<Employee_PhoneNumberDTO> result = new ApiResponse<Employee_PhoneNumberDTO>();
            try
            {
                var modelQuery = await unitOfWork.EmployeePhoneNumbersManager.GetAsync(e => e.EmployeeID == creationModel.EmployeeID && e.Number == creationModel.Number);
                var model = modelQuery.FirstOrDefault();

                if (model == null)
                {
                    Employee_PhoneNumber employee_PhoneNumber = new Employee_PhoneNumber
                    {
                        EmployeeID = creationModel.EmployeeID,
                        Number = creationModel.Number,
                        Creation_Date = await HelperFunctions.GetEgyptsCurrentLocalTime(),
                        Created_By = _httpContextAccessor.HttpContext.User.Identity.Name,
                        Updated_By = _httpContextAccessor.HttpContext.User.Identity.Name,
                        Update_Date = await HelperFunctions.GetEgyptsCurrentLocalTime()
                    };

                    var creationRes = await unitOfWork.EmployeePhoneNumbersManager.CreateAsync(employee_PhoneNumber);

                    if (creationRes != null)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = mapper.Map<Employee_PhoneNumberDTO>(creationRes);
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to add phone number");
                        result.Errors.Add("فشل تحديث تفاصيل الموظف");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Phone number already registered");
                    result.Errors.Add("رقم الهاتف مسجل بالفعل");
                    result.ErrorType = ErrorType.NotFound;
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

        public async Task<ApiResponse<bool>> RemoveEmployeePhoneNumber(EmployeePhoneNumberManagementModel removalModel)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var modelQuery = await unitOfWork.EmployeePhoneNumbersManager.GetAsync(e => e.EmployeeID == removalModel.EmployeeID && e.Number == removalModel.Number);
                var model = modelQuery.FirstOrDefault();

                if (model != null)
                {
                    var updateRes = await unitOfWork.EmployeePhoneNumbersManager.RemoveAsync(model);
                    if (updateRes)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to update employee details");
                        result.Errors.Add("فشل تحديث تفاصيل الموظف");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Employee not found");
                    result.Errors.Add("لم يتم العثور على الموظف");
                    result.ErrorType = ErrorType.NotFound;
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

        public async Task<ApiResponse<long>> AddEmployeeAddress(EmployeeAddressCreationModel creationModel)
        {
            ApiResponse<long> result = new ApiResponse<long>();
            try
            {
                var modelQuery = await unitOfWork.EmployeeAddressesManager.GetAsync(e => e.EmployeeID == creationModel.ID && e.Country == creationModel.Country && e.Governorate == creationModel.Governorate
                    && e.District == creationModel.District && e.Street == creationModel.Street && e.BuildingNumber == creationModel.Building);
                var model = modelQuery.FirstOrDefault();

                if (model == null)
                {
                    EmployeeAddress creatiionModel = new EmployeeAddress
                    {
                        EmployeeID = creationModel.ID,
                        Country = creationModel.Country,
                        Governorate = creationModel.Governorate,
                        District = creationModel.District,
                        Street = creationModel.Street,
                        BuildingNumber = creationModel.Building,
                        OtherInfo = creationModel.Other_Info,
                        Creation_Date = await HelperFunctions.GetEgyptsCurrentLocalTime(),
                        Created_By = _httpContextAccessor.HttpContext.User.Identity.Name,
                        Updated_By = _httpContextAccessor.HttpContext.User.Identity.Name,
                        Update_Date = await HelperFunctions.GetEgyptsCurrentLocalTime()
                    };

                    var creationRes = await unitOfWork.EmployeeAddressesManager.CreateAsync(creatiionModel);

                    if (creationRes != null)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Data = creationRes.ID;
                        result.Succeeded = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to add address");
                        result.Errors.Add("فشل إضافة العنوان");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Address already exists");
                    result.Errors.Add("العنوان موجود بالفعل");
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

        public async Task<ApiResponse<bool>> RemoveEmployeeAddress(EmployeeAddressRemovalModel removalModel)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var modelQuery = await unitOfWork.EmployeeAddressesManager.GetAsync(e => e.ID == removalModel.AddressID);
                var model = modelQuery.FirstOrDefault();

                if (model != null)
                {
                    var removalRes = await unitOfWork.EmployeeAddressesManager.RemoveAsync(model);

                    if (removalRes)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to remove address");
                        result.Errors.Add("فشل حذف العنوان");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Address doesnt exist");
                    result.Errors.Add("العنوان غير موجود");
                    result.ErrorType = ErrorType.NotFound;
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

        public async Task<ApiResponse<EmployeeDTO>> CreateEmployee(EmployeeCreationModel model)
        {
            ApiResponse<EmployeeDTO> result = new ApiResponse<EmployeeDTO>();
            try
            {
                ApplicationUser User = new ApplicationUser
                {
                    UserName = model.MasterData.UserName,
                    NameEN = model.MasterData.NameEN,
                    NameAR = model.MasterData.NameAR,
                    Gender = model.MasterData.Gender,
                    Email = model.MasterData.Email,
                    PhoneNumber = model.Communication.PhoneNumbers.FirstOrDefault().Number,
                };



                //Get company code's cost center
                var costCenterQuery = await unitOfWork.CostCentersManager.GetAsync(c => c.CompanyCodeID == model.CompanyCode.CodeID);
                var costCenter = costCenterQuery.FirstOrDefault();

                if (costCenter != null)
                {

                    //Create application user
                    var applicationUserCreationResult = await unitOfWork.UserManager.CreateAsync(User);

                    if (applicationUserCreationResult.Succeeded)
                    {
                        await unitOfWork.SaveChangesAsync();

                        //Create Employee
                        Employee employee = new Employee
                        {
                            StartDate = model.MasterData.StartDate,
                            CostCenterID = costCenter.ID,
                            ApplicationUserID = User.Id,
                            Created_By = _httpContextAccessor.HttpContext.User.Identity.Name,
                            Creation_Date = await HelperFunctions.GetEgyptsCurrentLocalTime()
                        };

                        var employeeCreationResult = await unitOfWork.EmployeesManager.CreateAsync(employee);

                        if (employeeCreationResult != null)
                        {
                            await unitOfWork.SaveChangesAsync();

                            var employeeAddressCreationlist = new List<EmployeeAddress>();

                            //Set Employee Communication data (phone numbers/addresses)

                            //Create employee addresses
                            for (int i = 0; i < model.Communication.Addresses.Count; i++)
                            {
                                var address = model.Communication.Addresses[i];

                                var addressToCreate = new EmployeeAddress
                                {
                                    Country = address.Country,
                                    Governorate = address.Governorate,
                                    District = address.District,
                                    BuildingNumber = address.Building_Number,
                                    Street = address.Street,
                                    OtherInfo = address.Other_Info,
                                    EmployeeID = employeeCreationResult.ID,

                                };

                                var addressCreationRes = await unitOfWork.EmployeeAddressesManager.CreateAsync(addressToCreate);

                                await unitOfWork.SaveChangesAsync();

                                if (addressCreationRes == null)
                                {
                                    result.Errors.Add("Failed");
                                }
                            }

                            if (result.Errors.Count == 0)
                            {

                                //Create employee phone numbers (if found)

                                if (model.Communication.PhoneNumbers.Count > 1)
                                {
                                    for (int i = 1; i < model.Communication.PhoneNumbers.Count; i++)
                                    {
                                        var phoneNumber = model.Communication.PhoneNumbers[i];
                                        var phoneNumberToCreate = new Employee_PhoneNumber
                                        {
                                            EmployeeID = employeeCreationResult.ID,
                                            Number = phoneNumber.Number,
                                        };
                                        var numberCreationRes = await unitOfWork.EmployeePhoneNumbersManager.CreateAsync(phoneNumberToCreate);
                                        await unitOfWork.SaveChangesAsync();
                                        if (numberCreationRes == null)
                                        {
                                            result.Errors.Add("Failed to create phone number " + phoneNumber.Number);
                                        }

                                    }
                                }


                                //Create employee org units
                                for (int i = 0; i < model.CompanyCode.OrgUnits.Count; i++)
                                {
                                    var orgUnit = model.CompanyCode.OrgUnits[i];

                                    var position = new Employee_Position
                                    {
                                        PositionID = orgUnit.PositionId,
                                        EmployeeID = employeeCreationResult.ID,
                                        Percentage = orgUnit.Percentage,
                                        StartDate = orgUnit.StartDate,
                                    };

                                    var employeePositionCreationRess = await unitOfWork.EmployeePositionsManager.CreateAsync(position);

                                    await unitOfWork.SaveChangesAsync();

                                    if (employeePositionCreationRess == null)
                                    {
                                        result.Errors.Add("Failed");
                                    }
                                }

                                if (result.Errors.Count == 0)
                                {
                                    result.Succeeded = true;
                                    result.Data = mapper.Map<EmployeeDTO>(employeeCreationResult);
                                    return result;
                                }
                                else
                                {
                                    result.Succeeded = false;
                                    result.Errors.Add("Failed to create employee data");
                                    result.Errors.Add("Failed to create employee data");
                                    return result;
                                }
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("Failed to create employee data");
                                result.Errors.Add("Failed to create employee data");
                                return result;
                            }
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Failed to create employee data");
                            result.Errors.Add("Failed to create employee data");
                            return result;
                        }
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to create employee data");
                        result.Errors.Add("Failed to create employee data");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Company code not found");
                    result.Errors.Add("Company code not found");
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }
        }

        public async Task<ApiResponse<bool>> Model_Template_Stamp(CreationTemplate template, long ID)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();

            try
            {
                IQueryable<Employee> modelQuery = await unitOfWork.EmployeesManager.GetAsync(t => t.ID == ID);
                Employee model = modelQuery.FirstOrDefault();

                if (model != null)
                {
                    model.Created_By = template.Created_By;
                    model.Creation_Date = await HelperFunctions.GetEgyptsCurrentLocalTime();
                    model.Updated_By = template.Updated_By;
                    model.Update_Date = await HelperFunctions.GetEgyptsCurrentLocalTime();

                    var updateRes = await unitOfWork.EmployeesManager.UpdateAsync(model);
                    if (updateRes)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Unable to update template");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Model not found");
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

        //Employee Org Unit creation (Via Employee Edit Section)
        public async Task<ApiResponse<Employee_OrgUnits>> AddEmployeeOrgUnit(EmployeeOrgUnitCreationModel creationModel)
        {
            ApiResponse<Employee_OrgUnits> result = new ApiResponse<Employee_OrgUnits>();
            try
            {
                var modelQuery = await unitOfWork.EmployeePositionsManager.GetAsync(e => e.PositionID == creationModel.PositionID && e.EmployeeID == creationModel.EmployeeID);
                var model = modelQuery.FirstOrDefault();

                if (model == null)
                {
                    Employee_Position employee_Position = new Employee_Position
                    {
                        EmployeeID = creationModel.EmployeeID,
                        PositionID = creationModel.PositionID,
                        StartDate = creationModel.StartDate,
                        Percentage = creationModel.Percentage,
                        Created_By = _httpContextAccessor.HttpContext.User.Identity.Name,
                        Creation_Date = await HelperFunctions.GetEgyptsCurrentLocalTime()
                        
                    };
                    var creationRes = await unitOfWork.EmployeePositionsManager.CreateAsync(employee_Position);

                    if (creationRes != null) 
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Data = mapper.Map<Employee_OrgUnits>(creationRes);
                        result.Succeeded = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to add an organziational unit");
                        result.Errors.Add("فشل إضافة وحدة تنظيمية");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("organziational unit already exists");
                    result.Errors.Add("الوحدة تنظيمية موجودة بالفعل");
                    result.ErrorType = ErrorType.NotFound;
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

    }

}


