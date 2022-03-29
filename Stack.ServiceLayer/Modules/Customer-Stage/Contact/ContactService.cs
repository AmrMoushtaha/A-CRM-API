
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
                                //Agent role - assign contact to self
                                if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.Agent.ToString()))
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
                                        StatusID = creationModel.StatusID
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
                                else if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.TeamLeader.ToString()))
                                {
                                    if (creationModel.AssigneeID == null) //Assign to self
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
                                            StatusID = creationModel.StatusID
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
                                    else //assign to employee
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
                                            StatusID = creationModel.StatusID
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
                                else // Create contact as unassigned
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


