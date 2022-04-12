
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using Stack.DTOs;
using Stack.DTOs.Enums;
using Stack.DTOs.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Stack.Repository.Common;
using System.IdentityModel.Tokens.Jwt;
using Stack.DTOs.Requests.Modules.Auth;
using Stack.Entities.Models.Modules.Auth;
using Stack.DTOs.Models.Modules.Auth;
using Stack.DTOs.Models.Modules.CustomerStage;
using Stack.DTOs.Requests.Modules.CustomerStage;
using Stack.Entities.Enums.Modules.CustomerStage;
using Stack.Entities.Enums.Modules.Auth;
using Stack.Entities.Models.Modules.CustomerStage;
using Stack.DTOs.Models.Modules.Pool;
using Stack.Entities.Enums.Modules.Pool;

namespace Stack.ServiceLayer.Modules.CustomerStage
{
    public class ContactService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private static readonly HttpClient client = new HttpClient();

        public ContactService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;

        }


        //Assigned Contacts
        public async Task<ApiResponse<List<ContactListViewModel>>> GetAssignedContacts()
        {
            ApiResponse<List<ContactListViewModel>> result = new ApiResponse<List<ContactListViewModel>>();
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (userID != null)
                {
                    var assignedRecords = await unitOfWork.ContactManager.GetAssignedContacts(userID);

                    if (assignedRecords != null && assignedRecords.Count > 0)
                    {
                        result.Succeeded = true;
                        result.Data = assignedRecords;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("No contacts found");
                        result.Errors.Add("لم يتم العثور على جهات اتصال");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Unauthorized");
                    result.Errors.Add("غير مصرح");
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

        public async Task<ApiResponse<ContactViewModel>> GetContact(long id)
        {
            ApiResponse<ContactViewModel> result = new ApiResponse<ContactViewModel>();
            try
            {
                var modelQuery = await unitOfWork.ContactManager.GetContactDetails(id);
                if (modelQuery != null)
                {
                    result.Succeeded = true;
                    result.Data = modelQuery;
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.ErrorCode = ErrorCode.A500;
                    result.Errors.Add("Contact not found");
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

        public async Task<ApiResponse<bool>> CreateContact(ContactCreationModel creationModel)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (userID != null)
                {
                    var modelQuery = await unitOfWork.ContactManager.GetAsync(t => t.PrimaryPhoneNumber == creationModel.PrimaryPhoneNumber, includeProperties: "PhoneNumbers,Status,Pool");
                    var model = modelQuery.FirstOrDefault();

                    //Phone number not duplicated
                    if (model == null)
                    {
                        //Get user role
                        var user = await unitOfWork.UserManager.GetUserById(userID);
                        if (user != null)
                        {
                            //Get Designated Pool
                            var poolQuery = await unitOfWork.PoolManager.GetAsync(t => t.ID == creationModel.PoolID);
                            var pool = poolQuery.FirstOrDefault();

                            if (pool != null)
                            {
                                //Identify pool configuration type
                                if (pool.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignment) //Auto assignemnt config.
                                {
                                    //Can assign to others
                                    if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.TeamLeader.ToString()))
                                    {
                                        var modelToCreate = new Contact
                                        {
                                            PoolID = creationModel.PoolID,
                                            FullNameEN = creationModel.FullNameEN,
                                            FullNameAR = creationModel.FullNameAR,
                                            Address = creationModel.Address,
                                            AssignedUserID = creationModel.AssigneeID,
                                            Email = creationModel.Email,
                                            LeadSourceName = creationModel.LeadSourceName,
                                            LeadSourceType = creationModel.LeadSourceType,
                                            Occupation = creationModel.Occupation,
                                            PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
                                            StatusID = creationModel.StatusID,
                                            IsFinalized = false
                                        };

                                        modelToCreate.State = (int)CustomerStageState.Initial;
                                        var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
                                        if (creationModelResult != null)
                                        {
                                            await unitOfWork.SaveChangesAsync();
                                            result.Succeeded = true;
                                            result.Data = true;
                                            return result;
                                        }
                                        else
                                        {
                                            result.Succeeded = false;
                                            result.Errors.Add("Unable to create contact, please try again later ");
                                            return result;
                                        }
                                    }
                                    //Can assign to self
                                    else if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.Agent.ToString()))
                                    {
                                        var modelToCreate = new Contact
                                        {
                                            PoolID = creationModel.PoolID,
                                            FullNameEN = creationModel.FullNameEN,
                                            FullNameAR = creationModel.FullNameAR,
                                            Address = creationModel.Address,
                                            AssignedUserID = userID,
                                            Email = creationModel.Email,
                                            LeadSourceName = creationModel.LeadSourceName,
                                            LeadSourceType = creationModel.LeadSourceType,
                                            Occupation = creationModel.Occupation,
                                            PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
                                            StatusID = creationModel.StatusID,
                                            IsFinalized = false
                                        };

                                        modelToCreate.State = (int)CustomerStageState.Initial;

                                        var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
                                        if (creationModelResult != null)
                                        {
                                            await unitOfWork.SaveChangesAsync();
                                            result.Succeeded = true;
                                            result.Data = true;
                                            return result;
                                        }
                                        else
                                        {
                                            result.Succeeded = false;
                                            result.Errors.Add("Unable to create contact, please try again later ");
                                            return result;
                                        }
                                    }
                                    //Error
                                    else
                                    {
                                        result.Succeeded = false;
                                        result.Errors.Add("Not authorized");
                                        return result;
                                    }
                                }
                                else if (pool.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignmentCapacity) //Auto assignment w/ capacity config.
                                {
                                    //Can assign to others
                                    if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.TeamLeader.ToString()))
                                    {
                                        //Verify selected user's pool capacity
                                        var selectedAssigneeQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == pool.ID && t.UserID == creationModel.AssigneeID);
                                        var selectedAssignee = selectedAssigneeQuery.FirstOrDefault();

                                        if (selectedAssignee != null)
                                        {
                                            //Get assignee's pool contacts and verify with current user's capacity
                                            var assignedContactsQuery = await unitOfWork.ContactManager.GetAsync(t => t.AssignedUserID == selectedAssignee.UserID && t.PoolID == pool.ID && t.IsFinalized == false);
                                            var assignedContacts = assignedContactsQuery.ToList();

                                            if (assignedContacts != null)
                                            {
                                                if (assignedContacts.Count < selectedAssignee.Capacity.Value) //assign contact to user
                                                {
                                                    var modelToCreate = new Contact
                                                    {
                                                        PoolID = creationModel.PoolID,
                                                        FullNameEN = creationModel.FullNameEN,
                                                        FullNameAR = creationModel.FullNameAR,
                                                        Address = creationModel.Address,
                                                        AssignedUserID = creationModel.AssigneeID,
                                                        Email = creationModel.Email,
                                                        LeadSourceName = creationModel.LeadSourceName,
                                                        LeadSourceType = creationModel.LeadSourceType,
                                                        Occupation = creationModel.Occupation,
                                                        PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
                                                        StatusID = creationModel.StatusID,
                                                        IsFinalized = false
                                                    };

                                                    modelToCreate.State = (int)CustomerStageState.Initial;
                                                    var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
                                                    if (creationModelResult != null)
                                                    {
                                                        await unitOfWork.SaveChangesAsync();
                                                        result.Succeeded = true;
                                                        result.Data = true;
                                                        return result;
                                                    }
                                                    else
                                                    {
                                                        result.Succeeded = false;
                                                        result.Errors.Add("Unable to create contact, please try again later ");
                                                        return result;
                                                    }
                                                }
                                                else //Capacity exceeded, Verify creator pool priviliges
                                                {
                                                    var poolAdminQuery = await unitOfWork.PoolAdminManager.GetAsync(t => t.PoolID == pool.ID && t.UserID == userID);
                                                    var poolAdmin = poolAdminQuery.FirstOrDefault();

                                                    if (poolAdmin != null) //Selected agent capacity increase response
                                                    {
                                                        result.Succeeded = false;
                                                        result.Errors.Add("Selected user's capacity is full, would you like to increase this user's capacity?");
                                                        result.Errors.Add("Selected user's capacity is full, would you like to increase this user's capacity?");
                                                        result.ErrorType = ErrorType.IncreaseCapacity;
                                                        return result;
                                                    }
                                                    else //Re-selection error
                                                    {
                                                        result.Succeeded = false;
                                                        result.Errors.Add("Selected user's capacity is full");
                                                        result.Errors.Add("Selected user's capacity is full");
                                                        result.ErrorType = ErrorType.ReselectUser;
                                                        return result;
                                                    }
                                                }
                                            }
                                            else //assign contact to user
                                            {
                                                var modelToCreate = new Contact
                                                {
                                                    PoolID = creationModel.PoolID,
                                                    FullNameEN = creationModel.FullNameEN,
                                                    FullNameAR = creationModel.FullNameAR,
                                                    Address = creationModel.Address,
                                                    AssignedUserID = creationModel.AssigneeID,
                                                    Email = creationModel.Email,
                                                    LeadSourceName = creationModel.LeadSourceName,
                                                    LeadSourceType = creationModel.LeadSourceType,
                                                    Occupation = creationModel.Occupation,
                                                    PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
                                                    StatusID = creationModel.StatusID,
                                                    IsFinalized = false
                                                };

                                                modelToCreate.State = (int)CustomerStageState.Initial;
                                                var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
                                                if (creationModelResult != null)
                                                {
                                                    await unitOfWork.SaveChangesAsync();
                                                    result.Succeeded = true;
                                                    result.Data = true;
                                                    return result;
                                                }
                                                else
                                                {
                                                    result.Succeeded = false;
                                                    result.Errors.Add("Unable to create contact, please try again later ");
                                                    return result;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            result.Succeeded = false;
                                            result.Errors.Add("Selected assignee does not exist");
                                            result.Errors.Add("Selected assignee does not exist");
                                            return result;
                                        }
                                    }
                                    //Can assign to self
                                    else if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.Agent.ToString()))
                                    {
                                        var modelToCreate = new Contact
                                        {
                                            PoolID = creationModel.PoolID,
                                            FullNameEN = creationModel.FullNameEN,
                                            FullNameAR = creationModel.FullNameAR,
                                            Address = creationModel.Address,
                                            AssignedUserID = userID,
                                            Email = creationModel.Email,
                                            LeadSourceName = creationModel.LeadSourceName,
                                            LeadSourceType = creationModel.LeadSourceType,
                                            Occupation = creationModel.Occupation,
                                            PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
                                            StatusID = creationModel.StatusID,
                                            IsFinalized = false
                                        };

                                        modelToCreate.State = (int)CustomerStageState.Initial;

                                        var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
                                        if (creationModelResult != null)
                                        {
                                            await unitOfWork.SaveChangesAsync();
                                            result.Succeeded = true;
                                            result.Data = true;
                                            return result;
                                        }
                                        else
                                        {
                                            result.Succeeded = false;
                                            result.Errors.Add("Unable to create contact, please try again later ");
                                            result.Errors.Add("Unable to create contact, please try again later ");
                                            return result;
                                        }
                                    }
                                    else
                                    {
                                        result.Succeeded = false;
                                        result.Errors.Add("Not authorized");
                                        return result;
                                    }
                                }
                                else if (pool.ConfigurationType == (int)PoolConfigurationTypes.Capacity)
                                {
                                    //Can assign to others
                                    if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.TeamLeader.ToString()))
                                    {
                                        //Verify selected user's pool capacity
                                        var selectedAssigneeQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == pool.ID && t.UserID == creationModel.AssigneeID);
                                        var selectedAssignee = selectedAssigneeQuery.FirstOrDefault();

                                        if (selectedAssignee != null)
                                        {
                                            //Get assignee's pool contacts and verify with current user's capacity
                                            var assignedContactsQuery = await unitOfWork.ContactManager.GetAsync(t => t.AssignedUserID == selectedAssignee.UserID && t.PoolID == pool.ID && t.IsFinalized == false);
                                            var assignedContacts = assignedContactsQuery.ToList();


                                            if (assignedContacts != null)
                                            {
                                                if (assignedContacts.Count < selectedAssignee.Capacity.Value) //assign contact to user
                                                {
                                                    var modelToCreate = new Contact
                                                    {
                                                        PoolID = creationModel.PoolID,
                                                        FullNameEN = creationModel.FullNameEN,
                                                        FullNameAR = creationModel.FullNameAR,
                                                        Address = creationModel.Address,
                                                        AssignedUserID = creationModel.AssigneeID,
                                                        Email = creationModel.Email,
                                                        LeadSourceName = creationModel.LeadSourceName,
                                                        LeadSourceType = creationModel.LeadSourceType,
                                                        Occupation = creationModel.Occupation,
                                                        PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
                                                        StatusID = creationModel.StatusID,
                                                        IsFinalized = false
                                                    };

                                                    modelToCreate.State = (int)CustomerStageState.Initial;
                                                    var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
                                                    if (creationModelResult != null)
                                                    {
                                                        await unitOfWork.SaveChangesAsync();
                                                        result.Succeeded = true;
                                                        result.Data = true;
                                                        return result;
                                                    }
                                                    else
                                                    {
                                                        result.Succeeded = false;
                                                        result.Errors.Add("Unable to create contact, please try again later ");
                                                        return result;
                                                    }
                                                }
                                                else //Capacity exceeded, Verify creator pool priviliges
                                                {
                                                    var poolAdminQuery = await unitOfWork.PoolAdminManager.GetAsync(t => t.PoolID == pool.ID && t.UserID == userID);
                                                    var poolAdmin = poolAdminQuery.FirstOrDefault();

                                                    if (poolAdmin != null) //Selected agent capacity increase response
                                                    {
                                                        result.Succeeded = false;
                                                        result.Errors.Add("Selected user's capacity is full, would you like to increase this user's capacity?");
                                                        result.Errors.Add("Selected user's capacity is full, would you like to increase this user's capacity?");
                                                        result.ErrorType = ErrorType.IncreaseCapacity;
                                                        return result;
                                                    }
                                                    else //Re-selection error
                                                    {
                                                        result.Succeeded = false;
                                                        result.Errors.Add("Selected user's capacity is full");
                                                        result.Errors.Add("Selected user's capacity is full");
                                                        result.ErrorType = ErrorType.ReselectUser;
                                                        return result;
                                                    }
                                                }
                                            }
                                            else //assign contact to user
                                            {
                                                var modelToCreate = new Contact
                                                {
                                                    PoolID = creationModel.PoolID,
                                                    FullNameEN = creationModel.FullNameEN,
                                                    FullNameAR = creationModel.FullNameAR,
                                                    Address = creationModel.Address,
                                                    AssignedUserID = creationModel.AssigneeID,
                                                    Email = creationModel.Email,
                                                    LeadSourceName = creationModel.LeadSourceName,
                                                    LeadSourceType = creationModel.LeadSourceType,
                                                    Occupation = creationModel.Occupation,
                                                    PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
                                                    StatusID = creationModel.StatusID,
                                                    IsFinalized = false
                                                };

                                                modelToCreate.State = (int)CustomerStageState.Initial;
                                                var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
                                                if (creationModelResult != null)
                                                {
                                                    await unitOfWork.SaveChangesAsync();
                                                    result.Succeeded = true;
                                                    result.Data = true;
                                                    return result;
                                                }
                                                else
                                                {
                                                    result.Succeeded = false;
                                                    result.Errors.Add("Unable to create contact, please try again later ");
                                                    return result;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            result.Succeeded = false;
                                            result.Errors.Add("Selected assignee does not exist");
                                            result.Errors.Add("Selected assignee does not exist");
                                            return result;
                                        }
                                    }
                                    //Can assign to self
                                    else if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.Agent.ToString()))
                                    {
                                        var modelToCreate = new Contact
                                        {
                                            PoolID = creationModel.PoolID,
                                            FullNameEN = creationModel.FullNameEN,
                                            FullNameAR = creationModel.FullNameAR,
                                            Address = creationModel.Address,
                                            AssignedUserID = userID,
                                            Email = creationModel.Email,
                                            LeadSourceName = creationModel.LeadSourceName,
                                            LeadSourceType = creationModel.LeadSourceType,
                                            Occupation = creationModel.Occupation,
                                            PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
                                            StatusID = creationModel.StatusID,
                                            IsFinalized = false
                                        };

                                        modelToCreate.State = (int)CustomerStageState.Initial;

                                        var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
                                        if (creationModelResult != null)
                                        {
                                            await unitOfWork.SaveChangesAsync();
                                            result.Succeeded = true;
                                            result.Data = true;
                                            return result;
                                        }
                                        else
                                        {
                                            result.Succeeded = false;
                                            result.Errors.Add("Unable to create contact, please try again later ");
                                            result.Errors.Add("Unable to create contact, please try again later ");
                                            return result;
                                        }
                                    }
                                    //Create contact as unassigned
                                    else
                                    {
                                        var modelToCreate = new Contact
                                        {
                                            PoolID = creationModel.PoolID,
                                            FullNameEN = creationModel.FullNameEN,
                                            FullNameAR = creationModel.FullNameAR,
                                            Address = creationModel.Address,
                                            Email = creationModel.Email,
                                            LeadSourceName = creationModel.LeadSourceName,
                                            LeadSourceType = creationModel.LeadSourceType,
                                            Occupation = creationModel.Occupation,
                                            PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
                                            StatusID = creationModel.StatusID
                                        };

                                        modelToCreate.State = (int)CustomerStageState.Unassigned;

                                        var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
                                        if (creationModelResult != null)
                                        {
                                            await unitOfWork.SaveChangesAsync();
                                            result.Succeeded = true;
                                            result.Data = true;
                                            return result;
                                        }
                                        else
                                        {
                                            result.Succeeded = false;
                                            result.Errors.Add("Unable to create contact, please try again later ");
                                            return result;
                                        }
                                    }
                                }
                                else //Default config type
                                {
                                    //Can assign to others
                                    if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.TeamLeader.ToString()))
                                    {
                                        var modelToCreate = new Contact
                                        {
                                            PoolID = creationModel.PoolID,
                                            FullNameEN = creationModel.FullNameEN,
                                            FullNameAR = creationModel.FullNameAR,
                                            Address = creationModel.Address,
                                            AssignedUserID = creationModel.AssigneeID,
                                            Email = creationModel.Email,
                                            LeadSourceName = creationModel.LeadSourceName,
                                            LeadSourceType = creationModel.LeadSourceType,
                                            Occupation = creationModel.Occupation,
                                            PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
                                            StatusID = creationModel.StatusID,
                                            IsFinalized = false
                                        };

                                        modelToCreate.State = (int)CustomerStageState.Initial;
                                        var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
                                        if (creationModelResult != null)
                                        {
                                            await unitOfWork.SaveChangesAsync();
                                            result.Succeeded = true;
                                            result.Data = true;
                                            return result;
                                        }
                                        else
                                        {
                                            result.Succeeded = false;
                                            result.Errors.Add("Unable to create contact, please try again later ");
                                            return result;
                                        }
                                    }
                                    //Can assign to self
                                    else if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.Agent.ToString()))
                                    {
                                        var modelToCreate = new Contact
                                        {
                                            PoolID = creationModel.PoolID,
                                            FullNameEN = creationModel.FullNameEN,
                                            FullNameAR = creationModel.FullNameAR,
                                            Address = creationModel.Address,
                                            AssignedUserID = userID,
                                            Email = creationModel.Email,
                                            LeadSourceName = creationModel.LeadSourceName,
                                            LeadSourceType = creationModel.LeadSourceType,
                                            Occupation = creationModel.Occupation,
                                            PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
                                            StatusID = creationModel.StatusID,
                                            IsFinalized = false
                                        };

                                        modelToCreate.State = (int)CustomerStageState.Initial;

                                        var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
                                        if (creationModelResult != null)
                                        {
                                            await unitOfWork.SaveChangesAsync();
                                            result.Succeeded = true;
                                            result.Data = true;
                                            return result;
                                        }
                                        else
                                        {
                                            result.Succeeded = false;
                                            result.Errors.Add("Unable to create contact, please try again later ");
                                            result.Errors.Add("Unable to create contact, please try again later ");
                                            return result;
                                        }
                                    }
                                    else //Create contact as unassigned
                                    {
                                        var modelToCreate = new Contact
                                        {
                                            PoolID = creationModel.PoolID,
                                            FullNameEN = creationModel.FullNameEN,
                                            FullNameAR = creationModel.FullNameAR,
                                            Address = creationModel.Address,
                                            Email = creationModel.Email,
                                            LeadSourceName = creationModel.LeadSourceName,
                                            LeadSourceType = creationModel.LeadSourceType,
                                            Occupation = creationModel.Occupation,
                                            PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
                                            StatusID = creationModel.StatusID
                                        };

                                        modelToCreate.State = (int)CustomerStageState.Unassigned;

                                        var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
                                        if (creationModelResult != null)
                                        {
                                            await unitOfWork.SaveChangesAsync();
                                            result.Succeeded = true;
                                            result.Data = true;
                                            return result;
                                        }
                                        else
                                        {
                                            result.Succeeded = false;
                                            result.Errors.Add("Unable to create contact, please try again later ");
                                            return result;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("Pool does not exist");
                                result.Errors.Add("Pool does not exist");
                                return result;
                            }
                        }
                        else // user not found
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Unauthorized");
                            return result;
                        }
                    }
                    else //Duplicate Contact
                    {
                        //duplicated contact is unassigned
                        if (model.AssignedUserID == null && model.Status.Status == CustomerStageState.Unassigned.ToString())
                        {
                            //assign contact to user
                            model.AssignedUserID = userID;

                            model.State = (int)CustomerStageState.Initial;

                            var assignModelResult = await unitOfWork.ContactManager.UpdateAsync(model);
                            if (assignModelResult)
                            {
                                await unitOfWork.SaveChangesAsync();
                                result.Succeeded = true;
                                result.Data = true;
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("Unable to assign existing contact, please try again later ");
                                return result;
                            }

                        }
                        else //duplicate contact is assigned
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Phone number cannot be duplicated");
                            return result;
                        }
                    }
                }
                else //Invalid user token
                {
                    result.Succeeded = false;
                    result.ErrorCode = ErrorCode.A500;
                    result.Errors.Add("Unauthorized");
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

        public async Task<ApiResponse<BulkAssignmentResponse>> BulkContactCreation(BulkContactCreationModel creationModel)
        {
            ApiResponse<BulkAssignmentResponse> result = new ApiResponse<BulkAssignmentResponse>();
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (userID != null)
                {
                    //Get user role
                    var user = await unitOfWork.UserManager.GetUserById(userID);
                    if (user != null)
                    {
                        //Get Designated Pool
                        var poolQuery = await unitOfWork.PoolManager.GetAsync(t => t.ID == creationModel.PoolID);
                        var pool = poolQuery.FirstOrDefault();

                        if (pool != null)
                        {
                            //Identify pool configuration type

                            if (pool.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignment) //Auto assignemnt config.
                            {
                                //Can assign to others
                                if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.Administrator.ToString()))
                                {
                                    //Get all pool users
                                    var poolUsersQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == pool.ID);
                                    var poolUsers = poolUsersQuery.ToList();

                                    //Assign to specified users
                                    if (creationModel.AssignedUsers != null && creationModel.AssignedUsers.Count > 0)
                                    {
                                        var assignedUsersCount = creationModel.AssignedUsers.Count;
                                        var contactsCount = creationModel.Contacts.Count;

                                        int rationedRecordsCount = contactsCount / assignedUsersCount;  //calculated rationed records to be assigned to each user
                                        //Calculate remaining records if any
                                        int multipliedRecords = rationedRecordsCount * assignedUsersCount;
                                        int remainingRecordsCount = contactsCount - multipliedRecords; //Remaining rationed records

                                        //Order specified users by priority

                                        List<SpecifiedPoolUser> specifiedPoolUsers = new List<SpecifiedPoolUser>();

                                        for (int i = 0; i < creationModel.AssignedUsers.Count; i++)
                                        {
                                            var specifiedUserID = creationModel.AssignedUsers[i];

                                            var matchedUser = poolUsers.Where(t => t.UserID == specifiedUserID).FirstOrDefault();

                                            if (matchedUser != null)
                                            {

                                                specifiedPoolUsers.Add(new SpecifiedPoolUser
                                                {
                                                    UserID = specifiedUserID,
                                                    Index = poolUsers.IndexOf(matchedUser),
                                                });

                                            }
                                        }

                                        specifiedPoolUsers = specifiedPoolUsers.OrderBy(t => t.Index).ToList();

                                        //If number of records are divisable by pool users count
                                        if (rationedRecordsCount > 0)
                                        {
                                            //rationalize records for each user
                                            for (int i = 0; i < specifiedPoolUsers.Count; i++)
                                            {
                                                var rations = creationModel.Contacts.Take(rationedRecordsCount).ToList();

                                                //Create record
                                                for (int j = 0; j < rations.Count; j++)
                                                {
                                                    var currentRation = rations[j];
                                                    Contact recordCreationModel = new Contact
                                                    {
                                                        PoolID = pool.ID,
                                                        FullNameEN = currentRation.FullNameEN,
                                                        FullNameAR = currentRation.FullNameAR,
                                                        Address = currentRation.Address,
                                                        AssignedUserID = specifiedPoolUsers[i].UserID,
                                                        Email = currentRation.Email,
                                                        LeadSourceName = currentRation.LeadSourceName,
                                                        LeadSourceType = currentRation.LeadSourceType,
                                                        Occupation = currentRation.Occupation,
                                                        PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
                                                        StatusID = currentRation.StatusID,
                                                        State = (int)CustomerStageState.Initial,
                                                        IsFinalized = false,
                                                    };

                                                    var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);
                                                }

                                                //Remove assigned rations from contact creation list
                                                creationModel.Contacts = creationModel.Contacts.Skip(rationedRecordsCount).ToList();
                                            }

                                            //Re-iterate selected users and assign remaining records if they exist (round robin)

                                            if (remainingRecordsCount > 0 && creationModel.Contacts.Count > 0)
                                            {
                                                while (creationModel.Contacts.Count > 0)
                                                {
                                                    for (int i = 0; i < specifiedPoolUsers.Count; i++)
                                                    {
                                                        var currentRation = creationModel.Contacts.Take(1).FirstOrDefault();
                                                        if (currentRation != null)
                                                        {
                                                            Contact recordCreationModel = new Contact
                                                            {
                                                                PoolID = pool.ID,
                                                                FullNameEN = currentRation.FullNameEN,
                                                                FullNameAR = currentRation.FullNameAR,
                                                                Address = currentRation.Address,
                                                                AssignedUserID = specifiedPoolUsers[i].UserID,
                                                                Email = currentRation.Email,
                                                                LeadSourceName = currentRation.LeadSourceName,
                                                                LeadSourceType = currentRation.LeadSourceType,
                                                                Occupation = currentRation.Occupation,
                                                                PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
                                                                StatusID = currentRation.StatusID,
                                                                State = (int)CustomerStageState.Initial,
                                                                IsFinalized = false,
                                                            };

                                                            var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);

                                                            //Remove assigned rations from contact creation list
                                                            creationModel.Contacts = creationModel.Contacts.Skip(1).ToList();
                                                        }
                                                        else //No more records to assign
                                                        {
                                                            break;
                                                        }
                                                    }
                                                }


                                                //Commit record creation
                                                await unitOfWork.SaveChangesAsync();
                                                result.Succeeded = true;
                                                return result;
                                            }
                                            else //Commit record creation
                                            {
                                                await unitOfWork.SaveChangesAsync();
                                                result.Succeeded = true;
                                                return result;
                                            }

                                        }
                                        else //Round robin process (1 record per user till all records are assigned)
                                        {
                                            while (creationModel.Contacts.Count > 0)
                                            {
                                                for (int i = 0; i < specifiedPoolUsers.Count; i++)
                                                {
                                                    var currentRation = creationModel.Contacts.Take(1).FirstOrDefault();
                                                    if (currentRation != null)
                                                    {
                                                        Contact recordCreationModel = new Contact
                                                        {
                                                            PoolID = pool.ID,
                                                            FullNameEN = currentRation.FullNameEN,
                                                            FullNameAR = currentRation.FullNameAR,
                                                            Address = currentRation.Address,
                                                            AssignedUserID = specifiedPoolUsers[i].UserID,
                                                            Email = currentRation.Email,
                                                            LeadSourceName = currentRation.LeadSourceName,
                                                            LeadSourceType = currentRation.LeadSourceType,
                                                            Occupation = currentRation.Occupation,
                                                            PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
                                                            StatusID = currentRation.StatusID,
                                                            State = (int)CustomerStageState.Initial,
                                                            IsFinalized = false,
                                                        };

                                                        var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);

                                                        //Remove assigned rations from contact creation list
                                                        creationModel.Contacts = creationModel.Contacts.Skip(1).ToList();
                                                    }
                                                    else //No more records to assign
                                                    {
                                                        break;
                                                    }
                                                }
                                            }

                                            //Commit record creation
                                            await unitOfWork.SaveChangesAsync();
                                            result.Succeeded = true;
                                            return result;
                                        }

                                    }
                                    else //Assign to existing pool prioritized users
                                    {
                                        var assignedUsersCount = poolUsers.Count;
                                        var contactsCount = creationModel.Contacts.Count;

                                        int rationedRecordsCount = contactsCount / assignedUsersCount;  //calculated rationed records to be assigned to each user
                                        //Calculate remaining records if any
                                        int multipliedRecords = rationedRecordsCount * assignedUsersCount;
                                        int remainingRecordsCount = contactsCount - multipliedRecords; //Remaining rationed records

                                        //If number of records are divisable by pool users count
                                        if (rationedRecordsCount > 0)
                                        {
                                            //rationalize records for each user
                                            for (int i = 0; i < poolUsers.Count; i++)
                                            {
                                                var rations = creationModel.Contacts.Take(rationedRecordsCount).ToList();

                                                //Create record
                                                for (int j = 0; j < rations.Count; j++)
                                                {
                                                    var currentRation = rations[j];
                                                    Contact recordCreationModel = new Contact
                                                    {
                                                        PoolID = pool.ID,
                                                        FullNameEN = currentRation.FullNameEN,
                                                        FullNameAR = currentRation.FullNameAR,
                                                        Address = currentRation.Address,
                                                        AssignedUserID = poolUsers[i].UserID,
                                                        Email = currentRation.Email,
                                                        LeadSourceName = currentRation.LeadSourceName,
                                                        LeadSourceType = currentRation.LeadSourceType,
                                                        Occupation = currentRation.Occupation,
                                                        PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
                                                        StatusID = currentRation.StatusID,
                                                        State = (int)CustomerStageState.Initial,
                                                        IsFinalized = false,
                                                    };

                                                    var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);
                                                }

                                                //Remove assigned rations from contact creation list
                                                creationModel.Contacts = creationModel.Contacts.Skip(rationedRecordsCount).ToList();
                                            }

                                            //Re-iterate selected users and assign remaining records if they exist (round robin)

                                            if (remainingRecordsCount > 0 && creationModel.Contacts.Count > 0)
                                            {
                                                while (creationModel.Contacts.Count > 0)
                                                {
                                                    for (int i = 0; i < poolUsers.Count; i++)
                                                    {
                                                        var currentRation = creationModel.Contacts.Take(1).FirstOrDefault();
                                                        if (currentRation != null)
                                                        {
                                                            Contact recordCreationModel = new Contact
                                                            {
                                                                PoolID = pool.ID,
                                                                FullNameEN = currentRation.FullNameEN,
                                                                FullNameAR = currentRation.FullNameAR,
                                                                Address = currentRation.Address,
                                                                AssignedUserID = poolUsers[i].UserID,
                                                                Email = currentRation.Email,
                                                                LeadSourceName = currentRation.LeadSourceName,
                                                                LeadSourceType = currentRation.LeadSourceType,
                                                                Occupation = currentRation.Occupation,
                                                                PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
                                                                StatusID = currentRation.StatusID,
                                                                State = (int)CustomerStageState.Initial,
                                                                IsFinalized = false,
                                                            };

                                                            var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);

                                                            //Remove assigned rations from contact creation list
                                                            creationModel.Contacts = creationModel.Contacts.Skip(1).ToList();
                                                        }
                                                        else //No more records to assign
                                                        {
                                                            break;
                                                        }
                                                    }
                                                }


                                                //Commit record creation
                                                await unitOfWork.SaveChangesAsync();
                                                result.Succeeded = true;
                                                return result;
                                            }
                                            else //Commit record creation
                                            {
                                                await unitOfWork.SaveChangesAsync();
                                                result.Succeeded = true;
                                                return result;
                                            }

                                        }
                                        else //Round robin process (1 record per user till all records are assigned)
                                        {
                                            while (creationModel.Contacts.Count > 0)
                                            {
                                                for (int i = 0; i < poolUsers.Count; i++)
                                                {
                                                    var currentRation = creationModel.Contacts.Take(1).FirstOrDefault();
                                                    if (currentRation != null)
                                                    {
                                                        Contact recordCreationModel = new Contact
                                                        {
                                                            PoolID = pool.ID,
                                                            FullNameEN = currentRation.FullNameEN,
                                                            FullNameAR = currentRation.FullNameAR,
                                                            Address = currentRation.Address,
                                                            AssignedUserID = poolUsers[i].UserID,
                                                            Email = currentRation.Email,
                                                            LeadSourceName = currentRation.LeadSourceName,
                                                            LeadSourceType = currentRation.LeadSourceType,
                                                            Occupation = currentRation.Occupation,
                                                            PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
                                                            StatusID = currentRation.StatusID,
                                                            State = (int)CustomerStageState.Initial,
                                                            IsFinalized = false,
                                                        };

                                                        var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);

                                                        //Remove assigned rations from contact creation list
                                                        creationModel.Contacts = creationModel.Contacts.Skip(1).ToList();
                                                    }
                                                    else //No more records to assign
                                                    {
                                                        break;
                                                    }
                                                }
                                            }

                                            //Commit record creation
                                            await unitOfWork.SaveChangesAsync();
                                            result.Succeeded = true;
                                            return result;
                                        }
                                    }
                                }
                                else //Can assign to self
                                {
                                    for (int i = 0; i < creationModel.Contacts.Count; i++)
                                    {
                                        var contact = creationModel.Contacts[i];

                                        var modelToCreate = new Contact
                                        {
                                            PoolID = creationModel.PoolID,
                                            FullNameEN = contact.FullNameEN,
                                            FullNameAR = contact.FullNameAR,
                                            Address = contact.Address,
                                            AssignedUserID = userID,
                                            Email = contact.Email,
                                            LeadSourceName = contact.LeadSourceName,
                                            LeadSourceType = contact.LeadSourceType,
                                            Occupation = contact.Occupation,
                                            PrimaryPhoneNumber = contact.PrimaryPhoneNumber,
                                            StatusID = contact.StatusID,
                                            State = (int)CustomerStageState.Initial,
                                            IsFinalized = false
                                        };

                                        modelToCreate.State = (int)CustomerStageState.Initial;

                                        var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
                                        if (creationModelResult == null)
                                        {
                                            result.Errors.Add("" + (i + 1));
                                        }
                                    }

                                    //Commit record creation
                                    await unitOfWork.SaveChangesAsync();
                                    result.Succeeded = true;
                                    return result;
                                }
                            }//END OF Auto Assignment flow
                            else if (pool.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignmentCapacity)
                            {

                                if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.Administrator.ToString()))//Can assign to others
                                {
                                    //Get all pool users
                                    var poolUsersQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == pool.ID);
                                    var poolUsers = poolUsersQuery.ToList();


                                    //Assign to specified users
                                    if (creationModel.AssignedUsers != null && creationModel.AssignedUsers.Count > 0)
                                    {
                                        int totalCapacity = 0;

                                        //Order specified users by priority
                                        List<SpecifiedPoolUser> specifiedPoolUsers = new List<SpecifiedPoolUser>();

                                        for (int i = 0; i < creationModel.AssignedUsers.Count; i++)
                                        {
                                            var specifiedUserID = creationModel.AssignedUsers[i];

                                            var matchedUser = poolUsers.Where(t => t.UserID == specifiedUserID).FirstOrDefault();

                                            if (matchedUser != null)
                                            {
                                                var assignedPoolContactsQuery = await unitOfWork.ContactManager.GetAsync(t => t.PoolID == pool.ID && t.AssignedUserID == specifiedUserID
                                                && t.IsFinalized == false);
                                                var assignedPoolContactsCount = assignedPoolContactsQuery.Count();
                                                specifiedPoolUsers.Add(new SpecifiedPoolUser
                                                {
                                                    UserID = specifiedUserID,
                                                    Index = poolUsers.IndexOf(matchedUser),
                                                    UserPoolCapacity = matchedUser.Capacity,
                                                    AssignedRecordsCount = assignedPoolContactsCount
                                                });

                                                int userAvailableCapacity = matchedUser.Capacity.Value - assignedPoolContactsCount;
                                                totalCapacity += userAvailableCapacity;

                                            }
                                        }

                                        specifiedPoolUsers = specifiedPoolUsers.OrderBy(t => t.Index).ToList();

                                        //Iterate users and assign contacts (round robin process)
                                        while (creationModel.Contacts.Count > 0 && totalCapacity > 0)
                                        {
                                            for (int i = 0; i < specifiedPoolUsers.Count; i++)
                                            {
                                                var specifiedUser = specifiedPoolUsers[i];

                                                var currentRation = creationModel.Contacts.Take(1).FirstOrDefault();
                                                if (currentRation != null)
                                                {
                                                    //Verify current capacity
                                                    int userAvailableCapacity = specifiedUser.UserPoolCapacity.Value - specifiedUser.AssignedRecordsCount.Value;
                                                    if (userAvailableCapacity > 0) //Available slots found, assign contact
                                                    {
                                                        Contact recordCreationModel = new Contact
                                                        {
                                                            PoolID = pool.ID,
                                                            FullNameEN = currentRation.FullNameEN,
                                                            FullNameAR = currentRation.FullNameAR,
                                                            Address = currentRation.Address,
                                                            AssignedUserID = specifiedUser.UserID,
                                                            Email = currentRation.Email,
                                                            LeadSourceName = currentRation.LeadSourceName,
                                                            LeadSourceType = currentRation.LeadSourceType,
                                                            Occupation = currentRation.Occupation,
                                                            PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
                                                            StatusID = currentRation.StatusID,
                                                            State = (int)CustomerStageState.Initial,
                                                            IsFinalized = false,
                                                        };

                                                        var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);

                                                        //Remove assigned rations from contact creation list
                                                        creationModel.Contacts = creationModel.Contacts.Skip(1).ToList();

                                                        //Adjust current user capacity record & total capacity 
                                                        if (creationRes != null)
                                                        {
                                                            specifiedUser.AssignedRecordsCount += 1;
                                                            totalCapacity -= 1;
                                                        }
                                                    }
                                                }
                                                else //No more records to assign
                                                {
                                                    break;
                                                }
                                            }
                                        }

                                        //Verify whether any records are still remaining

                                        if (creationModel.Contacts.Count > 0)
                                        {
                                            //Return with capacity re-evaluation response if user is a pool administrator
                                            var poolAdminQuery = await unitOfWork.PoolAdminManager.GetAsync(t => t.PoolID == pool.ID && t.UserID == userID);
                                            var isPoolAdmin = poolAdminQuery.FirstOrDefault();
                                            if (isPoolAdmin == null)
                                            {
                                                result.Succeeded = false;
                                                result.Data = new BulkAssignmentResponse
                                                {
                                                    RemainingSlots = creationModel.Contacts.Count,
                                                    Success = false
                                                };
                                                result.ErrorType = ErrorType.IncreaseCapacity;
                                                return result;
                                            }
                                            else //Create remaining records as unassigned if creator is not a pool administrator
                                            {
                                                for (int i = 0; i < creationModel.Contacts.Count; i++)
                                                {
                                                    var contact = creationModel.Contacts[i];
                                                    Contact recordCreationModel = new Contact
                                                    {
                                                        PoolID = pool.ID,
                                                        FullNameEN = contact.FullNameEN,
                                                        FullNameAR = contact.FullNameAR,
                                                        Address = contact.Address,
                                                        Email = contact.Email,
                                                        LeadSourceName = contact.LeadSourceName,
                                                        LeadSourceType = contact.LeadSourceType,
                                                        Occupation = contact.Occupation,
                                                        PrimaryPhoneNumber = contact.PrimaryPhoneNumber,
                                                        StatusID = contact.StatusID,
                                                        State = (int)CustomerStageState.Unassigned,
                                                        IsFinalized = false,
                                                    };

                                                    var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);
                                                }

                                                //Commit record creation
                                                await unitOfWork.SaveChangesAsync();
                                                result.Succeeded = true;
                                                return result;
                                            }
                                        }
                                        else //No more records remaining, commit record creation
                                        {
                                            await unitOfWork.SaveChangesAsync();
                                            result.Succeeded = true;
                                            return result;
                                        }
                                    }
                                    else //Assign to existing pool prioritized users
                                    {
                                        int totalCapacity = 0;

                                        List<SpecifiedPoolUser> prioritizedPoolUsers = new List<SpecifiedPoolUser>();

                                        //calculate each user's capacity and assigned records
                                        for (int i = 0; i < poolUsers.Count; i++)
                                        {
                                            var prioritizedUserID = creationModel.AssignedUsers[i];

                                            var matchedUser = poolUsers.Where(t => t.UserID == prioritizedUserID).FirstOrDefault();

                                            if (matchedUser != null)
                                            {
                                                var assignedPoolContactsQuery = await unitOfWork.ContactManager.GetAsync(t => t.PoolID == pool.ID && t.AssignedUserID == prioritizedUserID
                                                && t.IsFinalized == false);
                                                var assignedPoolContactsCount = assignedPoolContactsQuery.Count();
                                                prioritizedPoolUsers.Add(new SpecifiedPoolUser
                                                {
                                                    UserID = prioritizedUserID,
                                                    Index = poolUsers.IndexOf(matchedUser),
                                                    UserPoolCapacity = matchedUser.Capacity,
                                                    AssignedRecordsCount = assignedPoolContactsCount
                                                });

                                                int userAvailableCapacity = matchedUser.Capacity.Value - assignedPoolContactsCount;
                                                totalCapacity += userAvailableCapacity;

                                            }
                                        }

                                        //Iterate users and assign contacts (round robin process)
                                        while (creationModel.Contacts.Count > 0 && totalCapacity > 0)
                                        {
                                            for (int i = 0; i < prioritizedPoolUsers.Count; i++)
                                            {
                                                var prioritizedUser = prioritizedPoolUsers[i];

                                                var currentRation = creationModel.Contacts.Take(1).FirstOrDefault();
                                                if (currentRation != null)
                                                {
                                                    //Verify current capacity
                                                    int userAvailableCapacity = prioritizedUser.UserPoolCapacity.Value - prioritizedUser.AssignedRecordsCount.Value;
                                                    if (userAvailableCapacity > 0) //Available slots found, assign contact
                                                    {
                                                        Contact recordCreationModel = new Contact
                                                        {
                                                            PoolID = pool.ID,
                                                            FullNameEN = currentRation.FullNameEN,
                                                            FullNameAR = currentRation.FullNameAR,
                                                            Address = currentRation.Address,
                                                            AssignedUserID = prioritizedUser.UserID,
                                                            Email = currentRation.Email,
                                                            LeadSourceName = currentRation.LeadSourceName,
                                                            LeadSourceType = currentRation.LeadSourceType,
                                                            Occupation = currentRation.Occupation,
                                                            PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
                                                            StatusID = currentRation.StatusID,
                                                            State = (int)CustomerStageState.Initial,
                                                            IsFinalized = false,
                                                        };

                                                        var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);

                                                        //Remove assigned rations from contact creation list
                                                        creationModel.Contacts = creationModel.Contacts.Skip(1).ToList();

                                                        //Adjust current user capacity record & total capacity 
                                                        if (creationRes != null)
                                                        {
                                                            prioritizedUser.AssignedRecordsCount += 1;
                                                            totalCapacity -= 1;
                                                        }
                                                    }
                                                }
                                                else //No more records to assign
                                                {
                                                    break;
                                                }
                                            }
                                        }

                                        //Verify whether any records are still remaining

                                        if (creationModel.Contacts.Count > 0)
                                        {
                                            //Return with capacity re-evaluation response if user is a pool administrator
                                            var poolAdminQuery = await unitOfWork.PoolAdminManager.GetAsync(t => t.PoolID == pool.ID && t.UserID == userID);
                                            var isPoolAdmin = poolAdminQuery.FirstOrDefault();
                                            if (isPoolAdmin == null)
                                            {
                                                result.Succeeded = false;
                                                result.Data = new BulkAssignmentResponse
                                                {
                                                    RemainingSlots = creationModel.Contacts.Count,
                                                    Success = false
                                                };
                                                result.ErrorType = ErrorType.IncreaseCapacity;
                                                return result;
                                            }
                                            else //Create remaining records as unassigned if creator is not a pool administrator
                                            {
                                                for (int i = 0; i < creationModel.Contacts.Count; i++)
                                                {
                                                    var contact = creationModel.Contacts[i];
                                                    Contact recordCreationModel = new Contact
                                                    {
                                                        PoolID = pool.ID,
                                                        FullNameEN = contact.FullNameEN,
                                                        FullNameAR = contact.FullNameAR,
                                                        Address = contact.Address,
                                                        Email = contact.Email,
                                                        LeadSourceName = contact.LeadSourceName,
                                                        LeadSourceType = contact.LeadSourceType,
                                                        Occupation = contact.Occupation,
                                                        PrimaryPhoneNumber = contact.PrimaryPhoneNumber,
                                                        StatusID = contact.StatusID,
                                                        State = (int)CustomerStageState.Unassigned,
                                                        IsFinalized = false,
                                                    };

                                                    var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);
                                                }

                                                //Commit record creation
                                                await unitOfWork.SaveChangesAsync();
                                                result.Succeeded = true;
                                                return result;
                                            }
                                        }
                                        else //No more records remaining, commit record creation
                                        {
                                            await unitOfWork.SaveChangesAsync();
                                            result.Succeeded = true;
                                            return result;
                                        }
                                    }
                                }
                                else //Can assign to self
                                {
                                    for (int i = 0; i < creationModel.Contacts.Count; i++)
                                    {
                                        var contact = creationModel.Contacts[i];

                                        var modelToCreate = new Contact
                                        {
                                            PoolID = creationModel.PoolID,
                                            FullNameEN = contact.FullNameEN,
                                            FullNameAR = contact.FullNameAR,
                                            Address = contact.Address,
                                            AssignedUserID = userID,
                                            Email = contact.Email,
                                            LeadSourceName = contact.LeadSourceName,
                                            LeadSourceType = contact.LeadSourceType,
                                            Occupation = contact.Occupation,
                                            PrimaryPhoneNumber = contact.PrimaryPhoneNumber,
                                            StatusID = contact.StatusID,
                                            State = (int)CustomerStageState.Initial,
                                            IsFinalized = false
                                        };

                                        modelToCreate.State = (int)CustomerStageState.Initial;

                                        var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
                                        if (creationModelResult == null)
                                        {
                                            result.Errors.Add("" + (i + 1));
                                        }
                                    }

                                    //Commit record creation
                                    await unitOfWork.SaveChangesAsync();
                                    result.Succeeded = true;
                                    return result;
                                }
                            } //END OF Auto Assignment W/Capacity flow
                            else if (pool.ConfigurationType == (int)PoolConfigurationTypes.Capacity)
                            {
                                //Can assign to others
                                if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.Administrator.ToString()) && (creationModel.AssignedUsers != null && creationModel.AssignedUsers.Count > 0))
                                {
                                    //Get all pool users
                                    var poolUsersQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == pool.ID);
                                    var poolUsers = poolUsersQuery.ToList();

                                    int totalCapacity = 0;

                                    //Calculate current user's capacity
                                    List<SpecifiedPoolUser> specifiedPoolUsers = new List<SpecifiedPoolUser>();

                                    for (int i = 0; i < creationModel.AssignedUsers.Count; i++)
                                    {
                                        var specifiedUserID = creationModel.AssignedUsers[i];

                                        var matchedUser = poolUsers.Where(t => t.UserID == specifiedUserID).FirstOrDefault();

                                        if (matchedUser != null)
                                        {
                                            var assignedPoolContactsQuery = await unitOfWork.ContactManager.GetAsync(t => t.PoolID == pool.ID && t.AssignedUserID == specifiedUserID
                                            && t.IsFinalized == false);
                                            var assignedPoolContactsCount = assignedPoolContactsQuery.Count();
                                            specifiedPoolUsers.Add(new SpecifiedPoolUser
                                            {
                                                UserID = specifiedUserID,
                                                UserPoolCapacity = matchedUser.Capacity,
                                                AssignedRecordsCount = assignedPoolContactsCount
                                            });

                                            int userAvailableCapacity = matchedUser.Capacity.Value - assignedPoolContactsCount;
                                            totalCapacity += userAvailableCapacity;

                                        }
                                    }

                                    //Iterate users and assign contacts (round robin process)
                                    while (creationModel.Contacts.Count > 0 && totalCapacity > 0)
                                    {
                                        for (int i = 0; i < specifiedPoolUsers.Count; i++)
                                        {
                                            var specifiedUser = specifiedPoolUsers[i];

                                            var currentRation = creationModel.Contacts.Take(1).FirstOrDefault();
                                            if (currentRation != null)
                                            {
                                                //Verify current capacity
                                                int userAvailableCapacity = specifiedUser.UserPoolCapacity.Value - specifiedUser.AssignedRecordsCount.Value;
                                                if (userAvailableCapacity > 0) //Available slots found, assign contact
                                                {
                                                    Contact recordCreationModel = new Contact
                                                    {
                                                        PoolID = pool.ID,
                                                        FullNameEN = currentRation.FullNameEN,
                                                        FullNameAR = currentRation.FullNameAR,
                                                        Address = currentRation.Address,
                                                        AssignedUserID = specifiedUser.UserID,
                                                        Email = currentRation.Email,
                                                        LeadSourceName = currentRation.LeadSourceName,
                                                        LeadSourceType = currentRation.LeadSourceType,
                                                        Occupation = currentRation.Occupation,
                                                        PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
                                                        StatusID = currentRation.StatusID,
                                                        State = (int)CustomerStageState.Initial,
                                                        IsFinalized = false,
                                                    };

                                                    var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);

                                                    //Remove assigned rations from contact creation list
                                                    creationModel.Contacts = creationModel.Contacts.Skip(1).ToList();

                                                    //Adjust current user capacity record & total capacity 
                                                    if (creationRes != null)
                                                    {
                                                        specifiedUser.AssignedRecordsCount += 1;
                                                        totalCapacity -= 1;
                                                    }
                                                }
                                            }
                                            else //No more records to assign
                                            {
                                                break;
                                            }
                                        }
                                    }

                                    //Create remaining records as unassigned if found

                                    if (creationModel.Contacts.Count > 0)
                                    {
                                        for (int i = 0; i < creationModel.Contacts.Count; i++)
                                        {
                                            var contact = creationModel.Contacts[i];
                                            Contact recordCreationModel = new Contact
                                            {
                                                PoolID = pool.ID,
                                                FullNameEN = contact.FullNameEN,
                                                FullNameAR = contact.FullNameAR,
                                                Address = contact.Address,
                                                Email = contact.Email,
                                                LeadSourceName = contact.LeadSourceName,
                                                LeadSourceType = contact.LeadSourceType,
                                                Occupation = contact.Occupation,
                                                PrimaryPhoneNumber = contact.PrimaryPhoneNumber,
                                                StatusID = contact.StatusID,
                                                State = (int)CustomerStageState.Unassigned,
                                                IsFinalized = false,
                                            };

                                            var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);
                                        }

                                        //Commit record creation
                                        await unitOfWork.SaveChangesAsync();
                                        result.Succeeded = true;
                                        return result;
                                    }
                                    else //No more records remaining, commit record creation
                                    {
                                        await unitOfWork.SaveChangesAsync();
                                        result.Succeeded = true;
                                        return result;
                                    }
                                }
                                else if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.Agent.ToString()))//Can assign to self
                                {
                                    for (int i = 0; i < creationModel.Contacts.Count; i++)
                                    {
                                        var contact = creationModel.Contacts[i];

                                        var modelToCreate = new Contact
                                        {
                                            PoolID = creationModel.PoolID,
                                            FullNameEN = contact.FullNameEN,
                                            FullNameAR = contact.FullNameAR,
                                            Address = contact.Address,
                                            AssignedUserID = userID,
                                            Email = contact.Email,
                                            LeadSourceName = contact.LeadSourceName,
                                            LeadSourceType = contact.LeadSourceType,
                                            Occupation = contact.Occupation,
                                            PrimaryPhoneNumber = contact.PrimaryPhoneNumber,
                                            StatusID = contact.StatusID,
                                            State = (int)CustomerStageState.Initial,
                                            IsFinalized = false
                                        };

                                        var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
                                        if (creationModelResult == null)
                                        {
                                            result.Errors.Add("" + (i + 1));
                                        }
                                    }

                                    //Commit record creation
                                    await unitOfWork.SaveChangesAsync();
                                    result.Succeeded = true;
                                    return result;
                                }
                                else //Create all records as unassigned
                                {
                                    for (int i = 0; i < creationModel.Contacts.Count; i++)
                                    {
                                        var contact = creationModel.Contacts[i];

                                        var modelToCreate = new Contact
                                        {
                                            PoolID = creationModel.PoolID,
                                            FullNameEN = contact.FullNameEN,
                                            FullNameAR = contact.FullNameAR,
                                            Address = contact.Address,
                                            Email = contact.Email,
                                            LeadSourceName = contact.LeadSourceName,
                                            LeadSourceType = contact.LeadSourceType,
                                            Occupation = contact.Occupation,
                                            PrimaryPhoneNumber = contact.PrimaryPhoneNumber,
                                            StatusID = contact.StatusID,
                                            State = (int)CustomerStageState.Unassigned,
                                            IsFinalized = false
                                        };


                                        var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
                                        if (creationModelResult == null)
                                        {
                                            result.Errors.Add("" + (i + 1));
                                        }
                                    }

                                    //Commit record creation
                                    await unitOfWork.SaveChangesAsync();
                                    result.Succeeded = true;
                                    return result;
                                }
                            } //END OF Capacity flow
                            else //Default pool configuration flow
                            {
                                //Can assign to others
                                if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.Administrator.ToString()) && (creationModel.AssignedUsers != null && creationModel.AssignedUsers.Count > 0))
                                {
                                    while (creationModel.Contacts.Count > 0)
                                    {
                                        for (int i = 0; i < creationModel.AssignedUsers.Count; i++)
                                        {
                                            var assignedUserID = creationModel.AssignedUsers[i];

                                            var currentRation = creationModel.Contacts.Take(1).FirstOrDefault();
                                            if (currentRation != null)
                                            {
                                                Contact recordCreationModel = new Contact
                                                {
                                                    PoolID = pool.ID,
                                                    FullNameEN = currentRation.FullNameEN,
                                                    FullNameAR = currentRation.FullNameAR,
                                                    Address = currentRation.Address,
                                                    AssignedUserID = assignedUserID,
                                                    Email = currentRation.Email,
                                                    LeadSourceName = currentRation.LeadSourceName,
                                                    LeadSourceType = currentRation.LeadSourceType,
                                                    Occupation = currentRation.Occupation,
                                                    PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
                                                    StatusID = currentRation.StatusID,
                                                    State = (int)CustomerStageState.Initial,
                                                    IsFinalized = false,
                                                };

                                                var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);

                                                //Remove assigned rations from contact creation list
                                                creationModel.Contacts = creationModel.Contacts.Skip(1).ToList();
                                            }
                                            else //No more records to assign
                                            {
                                                break;
                                            }
                                        }
                                    }

                                    //Commit record creation
                                    await unitOfWork.SaveChangesAsync();
                                    result.Succeeded = true;
                                    return result;
                                }
                                //Can assign to self
                                else if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.Administrator.ToString()) && creationModel.AssignedUsers == null)
                                {
                                    for (int i = 0; i < creationModel.Contacts.Count; i++)
                                    {
                                        var contact = creationModel.Contacts[i];

                                        var modelToCreate = new Contact
                                        {
                                            PoolID = creationModel.PoolID,
                                            FullNameEN = contact.FullNameEN,
                                            FullNameAR = contact.FullNameAR,
                                            Address = contact.Address,
                                            AssignedUserID = userID,
                                            Email = contact.Email,
                                            LeadSourceName = contact.LeadSourceName,
                                            LeadSourceType = contact.LeadSourceType,
                                            Occupation = contact.Occupation,
                                            PrimaryPhoneNumber = contact.PrimaryPhoneNumber,
                                            StatusID = contact.StatusID,
                                            State = (int)CustomerStageState.Initial,
                                            IsFinalized = false
                                        };

                                        modelToCreate.State = (int)CustomerStageState.Initial;

                                        var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
                                        if (creationModelResult == null)
                                        {
                                            result.Errors.Add("" + (i + 1));
                                        }
                                    }

                                    //Commit record creation
                                    await unitOfWork.SaveChangesAsync();
                                    result.Succeeded = true;
                                    return result;
                                }
                                //Create all records as unassigned
                                else
                                {
                                    for (int i = 0; i < creationModel.Contacts.Count; i++)
                                    {
                                        var contact = creationModel.Contacts[i];

                                        var modelToCreate = new Contact
                                        {
                                            PoolID = creationModel.PoolID,
                                            FullNameEN = contact.FullNameEN,
                                            FullNameAR = contact.FullNameAR,
                                            Address = contact.Address,
                                            Email = contact.Email,
                                            LeadSourceName = contact.LeadSourceName,
                                            LeadSourceType = contact.LeadSourceType,
                                            Occupation = contact.Occupation,
                                            PrimaryPhoneNumber = contact.PrimaryPhoneNumber,
                                            StatusID = contact.StatusID,
                                            State = (int)CustomerStageState.Unassigned,
                                            IsFinalized = false
                                        };

                                        var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
                                        if (creationModelResult == null)
                                        {
                                            result.Errors.Add("" + (i + 1));
                                        }
                                    }

                                    //Commit record creation
                                    await unitOfWork.SaveChangesAsync();
                                    result.Succeeded = true;
                                    return result;
                                }
                            } //END OF Default pool configuration flow
                        }
                        else //Pool not found
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Pool not found or does not exist");
                            return result;
                        }
                    }
                    else //Invalid user token
                    {
                        result.Succeeded = false;
                        result.ErrorCode = ErrorCode.A500;
                        result.Errors.Add("Unauthorized");
                        return result;
                    }
                }
                else //Invalid user token
                {
                    result.Succeeded = false;
                    result.ErrorCode = ErrorCode.A500;
                    result.Errors.Add("Unauthorized");
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

        public async Task<ApiResponse<bool>> AddComment(AddCommentModel commentModel)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (userID != null)
                {
                    var user = await unitOfWork.UserManager.GetUserById(userID);

                    var modelQuery = await unitOfWork.ContactManager.GetAsync(t => t.ID == commentModel.ReferenceID);
                    var model = modelQuery.FirstOrDefault();

                    if (model != null)
                    {
                        if (model.AssignedUserID == userID)
                        {
                            ContactComment newComment = new ContactComment
                            {
                                ContactID = commentModel.ReferenceID,
                                CreatedBy = user.FirstName + " " + user.LastName,
                                Comment = commentModel.Comment,
                            };

                            var creationRes = await unitOfWork.ContactCommentManager.CreateAsync(newComment);
                            if (creationRes != null)
                            {
                                await unitOfWork.SaveChangesAsync();
                                result.Succeeded = true;
                                result.Data = true;
                                return result;
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("Unable to add comment, please try again later");
                                return result;
                            }
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Contact is not assigned to you");
                            return result;
                        }
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.ErrorCode = ErrorCode.A500;
                        result.Errors.Add("Contact not found");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.ErrorCode = ErrorCode.A500;
                    result.Errors.Add("Unauthorized");
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

        public async Task<ApiResponse<List<ContactStatusViewModel>>> GetAvailableContactStatuses()
        {
            ApiResponse<List<ContactStatusViewModel>> result = new ApiResponse<List<ContactStatusViewModel>>();
            try
            {
                var statusQuery = await unitOfWork.ContactStatusManager.GetAsync();
                var statuses = statusQuery.ToList();

                if (statuses != null && statuses.Count > 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<ContactStatusViewModel>>(statuses);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No statuses found");
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

        //get available tags for contact (filters out current contact tags)
        public async Task<ApiResponse<List<TagsDTO>>> GetAvailableTags(long id)
        {
            ApiResponse<List<TagsDTO>> result = new ApiResponse<List<TagsDTO>>();
            try
            {
                //Filter contact tags - Todo
                var modelQuery = await unitOfWork.TagManager.GetAsync();
                var list = modelQuery.ToList();

                if (list != null && list.Count > 0)
                {
                    //Get existing contact tags
                    var existingTagsQuery = await unitOfWork.ContactTagManager.GetAsync(t => t.ContactID == id, includeProperties: "Tag");
                    var existingTags = existingTagsQuery.ToList();
                    if (existingTags != null && existingTags.Count > 0)
                    {
                        //Filter existing tags
                        foreach (var tag in existingTags)
                        {
                            list.Remove(tag.Tag);
                        }
                    }
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<TagsDTO>>(list);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.ErrorCode = ErrorCode.A500;
                    result.Errors.Add("No tags found");
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

        public async Task<ApiResponse<bool>> AppendTagToContact(TagAppendanceModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                Contact_Tag appendanceModel = new Contact_Tag
                {
                    TagID = model.TagID,
                    ContactID = model.ReferenceID,
                };

                var appendanceResult = await unitOfWork.ContactTagManager.CreateAsync(appendanceModel);


                if (appendanceResult != null)
                {
                    await unitOfWork.SaveChangesAsync();
                    result.Succeeded = true;
                    result.Data = true;
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.ErrorCode = ErrorCode.A500;
                    result.Errors.Add("Could not append tag to contact");
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


