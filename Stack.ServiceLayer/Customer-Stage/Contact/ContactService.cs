
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
using ExcelDataReader;
using System.Data;
using System.IO;
using System.Net;
using System.Web;
using System.Net.Http.Headers;

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

        public async Task<ApiResponse<List<Customer>>> Get_Contacts()
        {
            ApiResponse<List<Customer>> result = new ApiResponse<List<Customer>>();
            try
            {
                var CustomerResult = (await unitOfWork.ContactManager.GetAsync()).ToList();
                if (CustomerResult.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<Customer>>(CustomerResult);
                    return result;
                }

                result.Errors.Add("Failed to find Contacts!");
                result.Succeeded = false;
                return result;
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

        #region Assigned
        public async Task<ApiResponse<List<ContactListViewModel>>> GetAssignedContacts()
        {
            ApiResponse<List<ContactListViewModel>> result = new ApiResponse<List<ContactListViewModel>>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

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

        public async Task<ApiResponse<List<ContactListViewModel>>> GetAssignedFreshContacts()
        {
            ApiResponse<List<ContactListViewModel>> result = new ApiResponse<List<ContactListViewModel>>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    var assignedRecords = await unitOfWork.ContactManager.GetAssignedFreshContacts(userID);

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
        #endregion

        #region Read
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
        #endregion

        #region Bulk Import
        public async Task<ApiResponse<List<ExcelTranscribedRecord>>> UploadContactExcelFile(UploadFileModel encodedFile)
        {
            ApiResponse<List<ExcelTranscribedRecord>> result = new ApiResponse<List<ExcelTranscribedRecord>>();
            try
            {
                return await Task.Run(async () =>
                {

                    //Convert uploaded base 64 string to file
                    byte[] bytes = Convert.FromBase64String(encodedFile.EncodedFile);
                    //MemoryStream conversionStream = new MemoryStream(bytes);


                    List<ExcelTranscribedRecord> contacts = new List<ExcelTranscribedRecord>();

                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                    //Access resources folder and add current excel file
                    var folderName = Path.Combine("Resources");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                    var fullPath = Path.Combine(pathToSave, "excel-template.xlsx");

                    File.WriteAllBytes(fullPath, bytes);

                    //using (var stream = new FileStream(fullPath, FileMode.Create))
                    //{
                    //    form.CopyTo(stream);
                    //}
                    //End of file storage method

                    //Read file and transcribe into record model
                    using (var stream = File.Open(fullPath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            do
                            {
                                while (reader.Read())
                                {

                                    contacts.Add(new ExcelTranscribedRecord
                                    {
                                        FullNameAR = (reader.IsDBNull(0)) ? null : reader.GetValue(0).ToString(),
                                        FullNameEN = (reader.IsDBNull(1)) ? null : reader.GetValue(1).ToString(),
                                        PrimaryPhoneNumber = (reader.IsDBNull(2)) ? null : reader.GetValue(2).ToString(),
                                        Email = (reader.IsDBNull(3)) ? null : reader.GetValue(3).ToString(),
                                        Address = (reader.IsDBNull(4)) ? null : reader.GetValue(4).ToString(),
                                        Occupation = (reader.IsDBNull(5)) ? null : reader.GetValue(5).ToString()
                                    });
                                }
                            } while (reader.NextResult());
                            // The result of each spreadsheet is in result.Tables
                        }
                    }

                    //Remove excel processing file from resources folder
                    File.Delete(fullPath);
                    if (contacts.Count > 1)
                    {
                        contacts = contacts.Skip(1).ToList(); //Remove header row

                        //Validate records
                        var validatedRecordsResult = await ValidateBulkContactsList(contacts);

                        return validatedRecordsResult;

                    }
                    else //Empty
                    {
                        result.Succeeded = false;
                        result.Errors.Add("File invalid");
                        return result;
                    }
                });
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }
        }

        //Validates uploaded excel file for contact duplication
        public async Task<ApiResponse<List<ExcelTranscribedRecord>>> ValidateBulkContactsList(List<ExcelTranscribedRecord> contactsList)
        {
            ApiResponse<List<ExcelTranscribedRecord>> result = new ApiResponse<List<ExcelTranscribedRecord>>();
            try
            {
                //Iterate each record and validate duplication
                for (int i = 0; i < contactsList.Count; i++)
                {
                    var record = contactsList[i];
                    if (record.PrimaryPhoneNumber == null)
                    {
                        record.IsValid = false;
                        continue;
                    }
                    //Validate record via phoneNumber
                    var recordExistsQuery = await unitOfWork.ContactManager.GetAsync(t => t.PrimaryPhoneNumber == record.PrimaryPhoneNumber);
                    var recordExists = recordExistsQuery.FirstOrDefault();

                    if (recordExists != null) //record exists, set record as invalid
                    {
                        record.IsValid = false;
                    }
                    else //record does not exist, set record as valid
                    {
                        record.IsValid = true;
                    }
                }

                contactsList = contactsList.OrderBy(t => t.IsValid).ToList();

                result.Succeeded = true;
                result.Data = contactsList;
                return result;
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }
        }

        //Bulk contact creation (post-verification)
        public async Task<ApiResponse<BulkAssignmentResponse>> BulkContactCreation(BulkContactCreationModel creationModel)
        {
            ApiResponse<BulkAssignmentResponse> result = new ApiResponse<BulkAssignmentResponse>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    //Get user role
                    var user = await unitOfWork.UserManager.GetUserById(userID);
                    if (user != null)
                    {
                        var userAuthModel = mapper.Map<ApplicationUserDTO>(user).AuthModel;

                        //Get Designated Pool
                        var poolQuery = await unitOfWork.PoolManager.GetAsync(t => t.ID == creationModel.PoolID);
                        var pool = poolQuery.FirstOrDefault();

                        if (pool != null)
                        {
                            //verify user authorizations
                            var poolAuthSectionQ = await unitOfWork.AuthorizationSectionsManager.GetAsync(t => t.Code == AuthorizationSectionCodes.Pool.ToString());
                            var poolAuthSection = poolAuthSectionQ.FirstOrDefault();

                            var sectionAuthorized = userAuthModel.AuthorizationSections.Where(t => t.Code == "1" && t.IsAuthorized == true).FirstOrDefault();
                            //User authorized
                            if (sectionAuthorized != null)
                            {
                                bool canAssignToSelf = false;
                                bool canAssignToOther = false;
                                canAssignToSelf = sectionAuthorized.SectionAuthorizations.Where(t => t.Code == "5").FirstOrDefault().IsAuthorized;
                                canAssignToOther = sectionAuthorized.SectionAuthorizations.Where(t => t.Code == "7").FirstOrDefault().IsAuthorized;
                                //Identify pool configuration type

                                if (pool.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignment ||
                                pool.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignmentCapacity) //Auto assignemnt config.
                                {
                                    //Can assign to others
                                    if (canAssignToOther && creationModel.AssignedUsers != null && creationModel.AssignedUsers.Count > 0)
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
                                                            ChannelID = currentRation.ChannelID,
                                                            LSTID = currentRation.LSTID,
                                                            LSNID = currentRation.LSNID,
                                                            Occupation = currentRation.Occupation,
                                                            PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
                                                            StatusID = currentRation.StatusID,
                                                            State = (int)CustomerStageState.Initial,
                                                            IsFinalized = false,
                                                            CapacityCalculated = false,
                                                            IsFresh = true
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
                                                                    ChannelID = currentRation.ChannelID,
                                                                    LSTID = currentRation.LSTID,
                                                                    LSNID = currentRation.LSNID,
                                                                    Occupation = currentRation.Occupation,
                                                                    PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
                                                                    StatusID = currentRation.StatusID,
                                                                    State = (int)CustomerStageState.Initial,
                                                                    IsFinalized = false,
                                                                    IsFresh = true,
                                                                    CapacityCalculated = false,
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
                                                                ChannelID = currentRation.ChannelID,
                                                                LSTID = currentRation.LSTID,
                                                                LSNID = currentRation.LSNID,
                                                                Occupation = currentRation.Occupation,
                                                                PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
                                                                StatusID = currentRation.StatusID,
                                                                State = (int)CustomerStageState.Initial,
                                                                IsFinalized = false,
                                                                CapacityCalculated = false,
                                                                IsFresh = true
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
                                        //Assign to existing pool prioritized users
                                        else
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
                                                            ChannelID = currentRation.ChannelID,
                                                            LSTID = currentRation.LSTID,
                                                            LSNID = currentRation.LSNID,
                                                            Occupation = currentRation.Occupation,
                                                            PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
                                                            StatusID = currentRation.StatusID,
                                                            State = (int)CustomerStageState.Initial,
                                                            IsFinalized = false,
                                                            CapacityCalculated = false,
                                                            IsFresh = true
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
                                                                    ChannelID = currentRation.ChannelID,
                                                                    LSTID = currentRation.LSTID,
                                                                    LSNID = currentRation.LSNID,
                                                                    Occupation = currentRation.Occupation,
                                                                    PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
                                                                    StatusID = currentRation.StatusID,
                                                                    State = (int)CustomerStageState.Initial,
                                                                    IsFinalized = false,
                                                                    CapacityCalculated = false,
                                                                    IsFresh = true
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
                                                                ChannelID = currentRation.ChannelID,
                                                                LSTID = currentRation.LSTID,
                                                                LSNID = currentRation.LSNID,
                                                                Occupation = currentRation.Occupation,
                                                                PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
                                                                StatusID = currentRation.StatusID,
                                                                State = (int)CustomerStageState.Initial,
                                                                IsFinalized = false,
                                                                CapacityCalculated = false,
                                                                IsFresh = true
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
                                    //Can assign to self
                                    else if (canAssignToSelf
                                        && (creationModel.AssignedUsers == null || creationModel.AssignedUsers.Count == 0))
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
                                                ChannelID = contact.ChannelID,
                                                LSTID = contact.LSTID,
                                                LSNID = contact.LSNID,
                                                Occupation = contact.Occupation,
                                                PrimaryPhoneNumber = contact.PrimaryPhoneNumber,
                                                StatusID = contact.StatusID,
                                                State = (int)CustomerStageState.Initial,
                                                IsFinalized = false,
                                                CapacityCalculated = false,
                                                IsFresh = true
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
                                    //Assign to existing pool prioritized users
                                    else
                                    {
                                        //Get all pool users
                                        var poolUsersQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == pool.ID);
                                        var poolUsers = poolUsersQuery.ToList();

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
                                                        ChannelID = currentRation.ChannelID,
                                                        LSTID = currentRation.LSTID,
                                                        LSNID = currentRation.LSNID,
                                                        Occupation = currentRation.Occupation,
                                                        PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
                                                        StatusID = currentRation.StatusID,
                                                        State = (int)CustomerStageState.Initial,
                                                        IsFinalized = false,
                                                        CapacityCalculated = false,
                                                        IsFresh = true
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
                                                                ChannelID = currentRation.ChannelID,
                                                                LSTID = currentRation.LSTID,
                                                                LSNID = currentRation.LSNID,
                                                                Occupation = currentRation.Occupation,
                                                                PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
                                                                StatusID = currentRation.StatusID,
                                                                State = (int)CustomerStageState.Initial,
                                                                IsFinalized = false,
                                                                CapacityCalculated = false,
                                                                IsFresh = true
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
                                                            ChannelID = currentRation.ChannelID,
                                                            LSTID = currentRation.LSTID,
                                                            LSNID = currentRation.LSNID,
                                                            Occupation = currentRation.Occupation,
                                                            PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
                                                            StatusID = currentRation.StatusID,
                                                            State = (int)CustomerStageState.Initial,
                                                            IsFinalized = false,
                                                            CapacityCalculated = false,
                                                            IsFresh = true
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
                                }//END OF Auto Assignment flow
                                 //Default pool configuration flow
                                else
                                {
                                    //Can assign to others
                                    if (canAssignToOther && (creationModel.AssignedUsers != null && creationModel.AssignedUsers.Count > 0))
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
                                                        ChannelID = currentRation.ChannelID,
                                                        LSTID = currentRation.LSTID,
                                                        LSNID = currentRation.LSNID,
                                                        Occupation = currentRation.Occupation,
                                                        PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
                                                        StatusID = currentRation.StatusID,
                                                        State = (int)CustomerStageState.Initial,
                                                        IsFinalized = false,
                                                        CapacityCalculated = false,
                                                        IsFresh = true
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
                                    else if (canAssignToSelf && creationModel.AssignedUsers == null)
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
                                                ChannelID = contact.ChannelID,
                                                LSTID = contact.LSTID,
                                                LSNID = contact.LSNID,
                                                Occupation = contact.Occupation,
                                                PrimaryPhoneNumber = contact.PrimaryPhoneNumber,
                                                StatusID = contact.StatusID,
                                                State = (int)CustomerStageState.Initial,
                                                IsFinalized = false,
                                                CapacityCalculated = false,
                                                IsFresh = true
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
                                                ChannelID = contact.ChannelID,
                                                LSTID = contact.LSTID,
                                                LSNID = contact.LSNID,
                                                Occupation = contact.Occupation,
                                                PrimaryPhoneNumber = contact.PrimaryPhoneNumber,
                                                StatusID = contact.StatusID,
                                                State = (int)CustomerStageState.Unassigned,
                                                IsFinalized = false,
                                                CapacityCalculated = false,
                                                IsFresh = true
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
                                }
                                //END OF Default pool configuration flow
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.ErrorCode = ErrorCode.A500;
                                result.Errors.Add("Unauthorized");
                                return result;
                            }

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
        #endregion

        #region Creation
        //Single contact creation
        public async Task<ApiResponse<bool>> CreateContact(ContactCreationModel creationModel)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

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
                            var userAuthModel = mapper.Map<ApplicationUserDTO>(user).AuthModel;

                            //Get Designated Pool
                            var poolQuery = await unitOfWork.PoolManager.GetAsync(t => t.ID == creationModel.PoolID);
                            var pool = poolQuery.FirstOrDefault();

                            if (pool != null)
                            {
                                //Verify user authorization
                                var poolAuthSectionQ = await unitOfWork.AuthorizationSectionsManager.GetAsync(t => t.Code == AuthorizationSectionCodes.Pool.ToString());
                                var poolAuthSection = poolAuthSectionQ.FirstOrDefault();

                                var sectionAuthorized = userAuthModel.AuthorizationSections.Where(t => t.Code == "1" && t.IsAuthorized == true).FirstOrDefault();
                                //User authorized
                                if (sectionAuthorized != null)
                                {
                                    bool canAssignToSelf = false;
                                    bool canAssignToOther = false;
                                    canAssignToSelf = sectionAuthorized.SectionAuthorizations.Where(t => t.Code == "5").FirstOrDefault().IsAuthorized;
                                    canAssignToOther = sectionAuthorized.SectionAuthorizations.Where(t => t.Code == "7").FirstOrDefault().IsAuthorized;

                                    //Pool Configuration 
                                    if (pool.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignment
                                        || pool.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignmentCapacity) //Auto assignemnt config.
                                    {
                                        //Can assign to others
                                        if (canAssignToOther && creationModel.AssigneeID != null && creationModel.AssigneeID != userID)
                                        {
                                            var modelToCreate = new Contact
                                            {
                                                PoolID = creationModel.PoolID,
                                                FullNameEN = creationModel.FullNameEN,
                                                FullNameAR = creationModel.FullNameAR,
                                                Address = creationModel.Address,
                                                AssignedUserID = creationModel.AssigneeID,
                                                Email = creationModel.Email,
                                                ChannelID = creationModel.ChannelID,
                                                LSTID = creationModel.LSTID,
                                                LSNID = creationModel.LSNID,
                                                Occupation = creationModel.Occupation,
                                                PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
                                                StatusID = creationModel.StatusID,
                                                State = (int)CustomerStageState.Initial,
                                                IsFinalized = false,
                                                IsFresh = true
                                            };

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
                                        else if (canAssignToSelf && creationModel.AssigneeID != null && creationModel.AssigneeID == userID)
                                        {
                                            var modelToCreate = new Contact
                                            {
                                                PoolID = creationModel.PoolID,
                                                FullNameEN = creationModel.FullNameEN,
                                                FullNameAR = creationModel.FullNameAR,
                                                Address = creationModel.Address,
                                                AssignedUserID = userID,
                                                Email = creationModel.Email,
                                                ChannelID = creationModel.ChannelID,
                                                LSTID = creationModel.LSTID,
                                                LSNID = creationModel.LSNID,
                                                Occupation = creationModel.Occupation,
                                                PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
                                                StatusID = creationModel.StatusID,
                                                State = (int)CustomerStageState.Initial,
                                                IsFinalized = false,
                                                IsFresh = true
                                            };


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
                                        //Assign to highest priority
                                        else
                                        {
                                            var poolUsersQ = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == pool.ID);
                                            var poolUser = poolUsersQ.FirstOrDefault();

                                            if (poolUser != null)
                                            {
                                                var modelToCreate = new Contact
                                                {
                                                    PoolID = creationModel.PoolID,
                                                    FullNameEN = creationModel.FullNameEN,
                                                    FullNameAR = creationModel.FullNameAR,
                                                    Address = creationModel.Address,
                                                    Email = creationModel.Email,
                                                    ChannelID = creationModel.ChannelID,
                                                    LSTID = creationModel.LSTID,
                                                    LSNID = creationModel.LSNID,
                                                    Occupation = creationModel.Occupation,
                                                    PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
                                                    State = (int)CustomerStageState.Initial,
                                                    StatusID = creationModel.StatusID,
                                                    AssignedUserID = poolUser.UserID,
                                                    IsFresh = true
                                                };

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
                                            else
                                            {
                                                result.Succeeded = false;
                                                result.Errors.Add("No users in this pool, please make sure there are members");
                                                result.Errors.Add("No users in this pool, please make sure there are members");
                                                return result;
                                            }
                                        }
                                    }
                                    //Default config type
                                    else
                                    {

                                        //Can assign to others
                                        if (canAssignToOther && creationModel.AssigneeID != null && creationModel.AssigneeID != userID)
                                        {
                                            var modelToCreate = new Contact
                                            {
                                                PoolID = creationModel.PoolID,
                                                FullNameEN = creationModel.FullNameEN,
                                                FullNameAR = creationModel.FullNameAR,
                                                Address = creationModel.Address,
                                                AssignedUserID = creationModel.AssigneeID,
                                                Email = creationModel.Email,
                                                ChannelID = creationModel.ChannelID,
                                                LSNID = creationModel.LSNID,
                                                LSTID = creationModel.LSTID,
                                                Occupation = creationModel.Occupation,
                                                PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
                                                StatusID = creationModel.StatusID,
                                                IsFinalized = false,
                                                IsFresh = true
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
                                        else if (canAssignToSelf && creationModel.AssigneeID != null && creationModel.AssigneeID == userID)
                                        {
                                            var modelToCreate = new Contact
                                            {
                                                PoolID = creationModel.PoolID,
                                                FullNameEN = creationModel.FullNameEN,
                                                FullNameAR = creationModel.FullNameAR,
                                                Address = creationModel.Address,
                                                AssignedUserID = userID,
                                                Email = creationModel.Email,
                                                ChannelID = creationModel.ChannelID,
                                                LSTID = creationModel.LSTID,
                                                LSNID = creationModel.LSNID,
                                                Occupation = creationModel.Occupation,
                                                PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
                                                StatusID = creationModel.StatusID,
                                                State = (int)CustomerStageState.Initial,
                                                IsFinalized = false,
                                                IsFresh = true
                                            };

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
                                                ChannelID = creationModel.ChannelID,
                                                LSTID = creationModel.LSTID,
                                                LSNID = creationModel.LSNID,
                                                Occupation = creationModel.Occupation,
                                                PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
                                                State = (int)CustomerStageState.Unassigned,
                                                StatusID = creationModel.StatusID,
                                                IsFresh = true
                                            };

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
                                    result.Errors.Add("Unauthorized");
                                    result.Errors.Add("Unauthorized");
                                    return result;
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
                            result.Errors.Add("Unauthorized");
                            return result;
                        }
                    }
                    else //Duplicate Contact
                    {
                        //duplicated contact is unassigned
                        if (model.AssignedUserID == null && model.State == (int)CustomerStageState.Unassigned)
                        {
                            //Transfer contact to current user's pool
                            if (model.PoolID != creationModel.PoolID)
                            {
                                model.PoolID = creationModel.PoolID;
                            }
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
                                result.Errors.Add("Unable to assign existing contact, please try again later ");
                                return result;
                            }

                        }
                        else //duplicate contact is assigned
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Phone number cannot be duplicated");
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
        #endregion

        #region Comments
        public async Task<ApiResponse<CommentReponseModel>> AddComment(AddCommentModel commentModel)
        {
            ApiResponse<CommentReponseModel> result = new ApiResponse<CommentReponseModel>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {
                    var user = await unitOfWork.UserManager.GetUserById(userID);

                    if (commentModel.RecordType > (int)CustomerStageIndicator.Contact)
                    {
                        var modelQuery = await unitOfWork.CustomerManager.GetAsync(t => t.ID == commentModel.ReferenceID);
                        var model = modelQuery.FirstOrDefault();

                        if (model != null)
                        {
                            if (model.AssignedUserID == userID)
                            {
                                CustomerComment newComment = new CustomerComment
                                {
                                    CustomerID = commentModel.ReferenceID,
                                    CreatedBy = user.FirstName + " " + user.LastName,
                                    Comment = commentModel.Comment,
                                    CreationDate = await HelperFunctions.GetEgyptsCurrentLocalTime()
                                };

                                var creationRes = await unitOfWork.CustomerCommentManager.CreateAsync(newComment);
                                if (creationRes != null)
                                {
                                    await unitOfWork.SaveChangesAsync();
                                    result.Succeeded = true;
                                    result.Data = mapper.Map<CommentReponseModel>(creationRes);
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
                                    CreationDate = await HelperFunctions.GetEgyptsCurrentLocalTime()
                                };

                                var creationRes = await unitOfWork.ContactCommentManager.CreateAsync(newComment);
                                if (creationRes != null)
                                {
                                    await unitOfWork.SaveChangesAsync();
                                    result.Succeeded = true;
                                    result.Data = mapper.Map<CommentReponseModel>(creationRes);
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
        #endregion

        #region Tags
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

        public async Task<ApiResponse<List<TagsDTO>>> GetAvailableCustomerTags(long id)
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
                    var existingTagsQuery = await unitOfWork.CustomerTagManager.GetAsync(t => t.CustomerID == id, includeProperties: "Tag");
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
                bool succeeded = false;

                if (model.RecordType > (int)CustomerStageIndicator.Contact)
                {
                    Contact_Tag appendanceModel = new Contact_Tag
                    {
                        TagID = model.TagID,
                        ContactID = model.ReferenceID,
                    };
                    var appendanceResult = await unitOfWork.ContactTagManager.CreateAsync(appendanceModel);

                    if (appendanceResult != null)
                    {
                        succeeded = true;
                    }
                }
                else
                {
                    Customer_Tag appendanceModel = new Customer_Tag
                    {
                        TagID = model.TagID,
                        CustomerID = model.ReferenceID,
                    };
                    var appendanceResult = await unitOfWork.CustomerTagManager.CreateAsync(appendanceModel);
                    if (appendanceResult != null)
                    {
                        succeeded = true;
                    }
                }


                if (succeeded)
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
        #endregion

        //Archive

        //public async Task<ApiResponse<BulkAssignmentResponse>> BulkContactCreation(BulkContactCreationModel creationModel)
        //{
        //    ApiResponse<BulkAssignmentResponse> result = new ApiResponse<BulkAssignmentResponse>();
        //    try
        //    {
        //        var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

        //        if (userID != null)
        //        {
        //            //Get user role
        //            var user = await unitOfWork.UserManager.GetUserById(userID);
        //            if (user != null)
        //            {
        //                //Get Designated Pool
        //                var poolQuery = await unitOfWork.PoolManager.GetAsync(t => t.ID == creationModel.PoolID);
        //                var pool = poolQuery.FirstOrDefault();

        //                if (pool != null)
        //                {
        //                    //Identify pool configuration type

        //                    if (pool.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignment ||
        //                        pool.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignmentCapacity) //Auto assignemnt config.
        //                    {
        //                        //Can assign to others
        //                        if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.Administrator.ToString()))
        //                        {
        //                            //Get all pool users
        //                            var poolUsersQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == pool.ID);
        //                            var poolUsers = poolUsersQuery.ToList();

        //                            //Assign to specified users
        //                            if (creationModel.AssignedUsers != null && creationModel.AssignedUsers.Count > 0)
        //                            {
        //                                var assignedUsersCount = creationModel.AssignedUsers.Count;
        //                                var contactsCount = creationModel.Contacts.Count;

        //                                int rationedRecordsCount = contactsCount / assignedUsersCount;  //calculated rationed records to be assigned to each user
        //                                                                                                //Calculate remaining records if any
        //                                int multipliedRecords = rationedRecordsCount * assignedUsersCount;
        //                                int remainingRecordsCount = contactsCount - multipliedRecords; //Remaining rationed records

        //                                //Order specified users by priority

        //                                List<SpecifiedPoolUser> specifiedPoolUsers = new List<SpecifiedPoolUser>();

        //                                for (int i = 0; i < creationModel.AssignedUsers.Count; i++)
        //                                {
        //                                    var specifiedUserID = creationModel.AssignedUsers[i];

        //                                    var matchedUser = poolUsers.Where(t => t.UserID == specifiedUserID).FirstOrDefault();

        //                                    if (matchedUser != null)
        //                                    {

        //                                        specifiedPoolUsers.Add(new SpecifiedPoolUser
        //                                        {
        //                                            UserID = specifiedUserID,
        //                                            Index = poolUsers.IndexOf(matchedUser),
        //                                        });

        //                                    }
        //                                }

        //                                specifiedPoolUsers = specifiedPoolUsers.OrderBy(t => t.Index).ToList();

        //                                //If number of records are divisable by pool users count
        //                                if (rationedRecordsCount > 0)
        //                                {
        //                                    //rationalize records for each user
        //                                    for (int i = 0; i < specifiedPoolUsers.Count; i++)
        //                                    {
        //                                        var rations = creationModel.Contacts.Take(rationedRecordsCount).ToList();

        //                                        //Create record
        //                                        for (int j = 0; j < rations.Count; j++)
        //                                        {
        //                                            var currentRation = rations[j];
        //                                            Contact recordCreationModel = new Contact
        //                                            {
        //                                                PoolID = pool.ID,
        //                                                FullNameEN = currentRation.FullNameEN,
        //                                                FullNameAR = currentRation.FullNameAR,
        //                                                Address = currentRation.Address,
        //                                                AssignedUserID = specifiedPoolUsers[i].UserID,
        //                                                Email = currentRation.Email,
        //                                                LeadSourceName = currentRation.LeadSourceName,
        //                                                LeadSourceType = currentRation.LeadSourceType,
        //                                                Occupation = currentRation.Occupation,
        //                                                PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
        //                                                StatusID = currentRation.StatusID,
        //                                                State = (int)CustomerStageState.Initial,
        //                                                IsFinalized = false,
        //                                            };

        //                                            var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);
        //                                        }

        //                                        //Remove assigned rations from contact creation list
        //                                        creationModel.Contacts = creationModel.Contacts.Skip(rationedRecordsCount).ToList();
        //                                    }

        //                                    //Re-iterate selected users and assign remaining records if they exist (round robin)

        //                                    if (remainingRecordsCount > 0 && creationModel.Contacts.Count > 0)
        //                                    {
        //                                        while (creationModel.Contacts.Count > 0)
        //                                        {
        //                                            for (int i = 0; i < specifiedPoolUsers.Count; i++)
        //                                            {
        //                                                var currentRation = creationModel.Contacts.Take(1).FirstOrDefault();
        //                                                if (currentRation != null)
        //                                                {
        //                                                    Contact recordCreationModel = new Contact
        //                                                    {
        //                                                        PoolID = pool.ID,
        //                                                        FullNameEN = currentRation.FullNameEN,
        //                                                        FullNameAR = currentRation.FullNameAR,
        //                                                        Address = currentRation.Address,
        //                                                        AssignedUserID = specifiedPoolUsers[i].UserID,
        //                                                        Email = currentRation.Email,
        //                                                        LeadSourceName = currentRation.LeadSourceName,
        //                                                        LeadSourceType = currentRation.LeadSourceType,
        //                                                        Occupation = currentRation.Occupation,
        //                                                        PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
        //                                                        StatusID = currentRation.StatusID,
        //                                                        State = (int)CustomerStageState.Initial,
        //                                                        IsFinalized = false,
        //                                                    };

        //                                                    var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);

        //                                                    //Remove assigned rations from contact creation list
        //                                                    creationModel.Contacts = creationModel.Contacts.Skip(1).ToList();
        //                                                }
        //                                                else //No more records to assign
        //                                                {
        //                                                    break;
        //                                                }
        //                                            }
        //                                        }


        //                                        //Commit record creation
        //                                        await unitOfWork.SaveChangesAsync();
        //                                        result.Succeeded = true;
        //                                        return result;
        //                                    }
        //                                    else //Commit record creation
        //                                    {
        //                                        await unitOfWork.SaveChangesAsync();
        //                                        result.Succeeded = true;
        //                                        return result;
        //                                    }

        //                                }
        //                                else //Round robin process (1 record per user till all records are assigned)
        //                                {
        //                                    while (creationModel.Contacts.Count > 0)
        //                                    {
        //                                        for (int i = 0; i < specifiedPoolUsers.Count; i++)
        //                                        {
        //                                            var currentRation = creationModel.Contacts.Take(1).FirstOrDefault();
        //                                            if (currentRation != null)
        //                                            {
        //                                                Contact recordCreationModel = new Contact
        //                                                {
        //                                                    PoolID = pool.ID,
        //                                                    FullNameEN = currentRation.FullNameEN,
        //                                                    FullNameAR = currentRation.FullNameAR,
        //                                                    Address = currentRation.Address,
        //                                                    AssignedUserID = specifiedPoolUsers[i].UserID,
        //                                                    Email = currentRation.Email,
        //                                                    LeadSourceName = currentRation.LeadSourceName,
        //                                                    LeadSourceType = currentRation.LeadSourceType,
        //                                                    Occupation = currentRation.Occupation,
        //                                                    PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
        //                                                    StatusID = currentRation.StatusID,
        //                                                    State = (int)CustomerStageState.Initial,
        //                                                    IsFinalized = false,
        //                                                };

        //                                                var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);

        //                                                //Remove assigned rations from contact creation list
        //                                                creationModel.Contacts = creationModel.Contacts.Skip(1).ToList();
        //                                            }
        //                                            else //No more records to assign
        //                                            {
        //                                                break;
        //                                            }
        //                                        }
        //                                    }

        //                                    //Commit record creation
        //                                    await unitOfWork.SaveChangesAsync();
        //                                    result.Succeeded = true;
        //                                    return result;
        //                                }

        //                            }
        //                            else //Assign to existing pool prioritized users
        //                            {
        //                                var assignedUsersCount = poolUsers.Count;
        //                                var contactsCount = creationModel.Contacts.Count;

        //                                int rationedRecordsCount = contactsCount / assignedUsersCount;  //calculated rationed records to be assigned to each user
        //                                                                                                //Calculate remaining records if any
        //                                int multipliedRecords = rationedRecordsCount * assignedUsersCount;
        //                                int remainingRecordsCount = contactsCount - multipliedRecords; //Remaining rationed records

        //                                //If number of records are divisable by pool users count
        //                                if (rationedRecordsCount > 0)
        //                                {
        //                                    //rationalize records for each user
        //                                    for (int i = 0; i < poolUsers.Count; i++)
        //                                    {
        //                                        var rations = creationModel.Contacts.Take(rationedRecordsCount).ToList();

        //                                        //Create record
        //                                        for (int j = 0; j < rations.Count; j++)
        //                                        {
        //                                            var currentRation = rations[j];
        //                                            Contact recordCreationModel = new Contact
        //                                            {
        //                                                PoolID = pool.ID,
        //                                                FullNameEN = currentRation.FullNameEN,
        //                                                FullNameAR = currentRation.FullNameAR,
        //                                                Address = currentRation.Address,
        //                                                AssignedUserID = poolUsers[i].UserID,
        //                                                Email = currentRation.Email,
        //                                                LeadSourceName = currentRation.LeadSourceName,
        //                                                LeadSourceType = currentRation.LeadSourceType,
        //                                                Occupation = currentRation.Occupation,
        //                                                PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
        //                                                StatusID = currentRation.StatusID,
        //                                                State = (int)CustomerStageState.Initial,
        //                                                IsFinalized = false,
        //                                            };

        //                                            var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);
        //                                        }

        //                                        //Remove assigned rations from contact creation list
        //                                        creationModel.Contacts = creationModel.Contacts.Skip(rationedRecordsCount).ToList();
        //                                    }

        //                                    //Re-iterate selected users and assign remaining records if they exist (round robin)

        //                                    if (remainingRecordsCount > 0 && creationModel.Contacts.Count > 0)
        //                                    {
        //                                        while (creationModel.Contacts.Count > 0)
        //                                        {
        //                                            for (int i = 0; i < poolUsers.Count; i++)
        //                                            {
        //                                                var currentRation = creationModel.Contacts.Take(1).FirstOrDefault();
        //                                                if (currentRation != null)
        //                                                {
        //                                                    Contact recordCreationModel = new Contact
        //                                                    {
        //                                                        PoolID = pool.ID,
        //                                                        FullNameEN = currentRation.FullNameEN,
        //                                                        FullNameAR = currentRation.FullNameAR,
        //                                                        Address = currentRation.Address,
        //                                                        AssignedUserID = poolUsers[i].UserID,
        //                                                        Email = currentRation.Email,
        //                                                        LeadSourceName = currentRation.LeadSourceName,
        //                                                        LeadSourceType = currentRation.LeadSourceType,
        //                                                        Occupation = currentRation.Occupation,
        //                                                        PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
        //                                                        StatusID = currentRation.StatusID,
        //                                                        State = (int)CustomerStageState.Initial,
        //                                                        IsFinalized = false,
        //                                                    };

        //                                                    var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);

        //                                                    //Remove assigned rations from contact creation list
        //                                                    creationModel.Contacts = creationModel.Contacts.Skip(1).ToList();
        //                                                }
        //                                                else //No more records to assign
        //                                                {
        //                                                    break;
        //                                                }
        //                                            }
        //                                        }


        //                                        //Commit record creation
        //                                        await unitOfWork.SaveChangesAsync();
        //                                        result.Succeeded = true;
        //                                        return result;
        //                                    }
        //                                    else //Commit record creation
        //                                    {
        //                                        await unitOfWork.SaveChangesAsync();
        //                                        result.Succeeded = true;
        //                                        return result;
        //                                    }

        //                                }
        //                                else //Round robin process (1 record per user till all records are assigned)
        //                                {
        //                                    while (creationModel.Contacts.Count > 0)
        //                                    {
        //                                        for (int i = 0; i < poolUsers.Count; i++)
        //                                        {
        //                                            var currentRation = creationModel.Contacts.Take(1).FirstOrDefault();
        //                                            if (currentRation != null)
        //                                            {
        //                                                Contact recordCreationModel = new Contact
        //                                                {
        //                                                    PoolID = pool.ID,
        //                                                    FullNameEN = currentRation.FullNameEN,
        //                                                    FullNameAR = currentRation.FullNameAR,
        //                                                    Address = currentRation.Address,
        //                                                    AssignedUserID = poolUsers[i].UserID,
        //                                                    Email = currentRation.Email,
        //                                                    LeadSourceName = currentRation.LeadSourceName,
        //                                                    LeadSourceType = currentRation.LeadSourceType,
        //                                                    Occupation = currentRation.Occupation,
        //                                                    PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
        //                                                    StatusID = currentRation.StatusID,
        //                                                    State = (int)CustomerStageState.Initial,
        //                                                    IsFinalized = false,
        //                                                };

        //                                                var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);

        //                                                //Remove assigned rations from contact creation list
        //                                                creationModel.Contacts = creationModel.Contacts.Skip(1).ToList();
        //                                            }
        //                                            else //No more records to assign
        //                                            {
        //                                                break;
        //                                            }
        //                                        }
        //                                    }

        //                                    //Commit record creation
        //                                    await unitOfWork.SaveChangesAsync();
        //                                    result.Succeeded = true;
        //                                    return result;
        //                                }
        //                            }
        //                        }
        //                        else if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.Agent.ToString()) && (creationModel.AssignedUsers == null || creationModel.AssignedUsers.Count == 0)) //Can assign to self
        //                        {
        //                            for (int i = 0; i < creationModel.Contacts.Count; i++)
        //                            {
        //                                var contact = creationModel.Contacts[i];

        //                                var modelToCreate = new Contact
        //                                {
        //                                    PoolID = creationModel.PoolID,
        //                                    FullNameEN = contact.FullNameEN,
        //                                    FullNameAR = contact.FullNameAR,
        //                                    Address = contact.Address,
        //                                    AssignedUserID = userID,
        //                                    Email = contact.Email,
        //                                    LeadSourceName = contact.LeadSourceName,
        //                                    LeadSourceType = contact.LeadSourceType,
        //                                    Occupation = contact.Occupation,
        //                                    PrimaryPhoneNumber = contact.PrimaryPhoneNumber,
        //                                    StatusID = contact.StatusID,
        //                                    State = (int)CustomerStageState.Initial,
        //                                    IsFinalized = false
        //                                };

        //                                modelToCreate.State = (int)CustomerStageState.Initial;

        //                                var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
        //                                if (creationModelResult == null)
        //                                {
        //                                    result.Errors.Add("" + (i + 1));
        //                                }
        //                            }

        //                            //Commit record creation
        //                            await unitOfWork.SaveChangesAsync();
        //                            result.Succeeded = true;
        //                            return result;
        //                        }
        //                        else
        //                        {
        //                            result.Succeeded = false;
        //                            result.ErrorCode = ErrorCode.A500;
        //                            result.Errors.Add("Unauthorized");
        //                            return result;
        //                        }
        //                    }//END OF Auto Assignment flow
        //                    //else if (pool.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignmentCapacity)
        //                    //{

        //                    //    if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.Administrator.ToString()))//Can assign to others
        //                    //    {
        //                    //        //Get all pool users
        //                    //        var poolUsersQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == pool.ID);
        //                    //        var poolUsers = poolUsersQuery.ToList();


        //                    //        //Assign to specified users
        //                    //        if (creationModel.AssignedUsers != null && creationModel.AssignedUsers.Count > 0)
        //                    //        {
        //                    //            int totalCapacity = 0;

        //                    //            //Order specified users by priority
        //                    //            List<SpecifiedPoolUser> specifiedPoolUsers = new List<SpecifiedPoolUser>();

        //                    //            for (int i = 0; i < creationModel.AssignedUsers.Count; i++)
        //                    //            {
        //                    //                var specifiedUserID = creationModel.AssignedUsers[i];

        //                    //                var matchedUser = poolUsers.Where(t => t.UserID == specifiedUserID).FirstOrDefault();

        //                    //                if (matchedUser != null)
        //                    //                {
        //                    //                    var assignedPoolContactsQuery = await unitOfWork.ContactManager.GetAsync(t => t.PoolID == pool.ID && t.AssignedUserID == specifiedUserID
        //                    //                    && t.IsFinalized == false);
        //                    //                    var assignedPoolContactsCount = assignedPoolContactsQuery.Count();
        //                    //                    specifiedPoolUsers.Add(new SpecifiedPoolUser
        //                    //                    {
        //                    //                        UserID = specifiedUserID,
        //                    //                        Index = poolUsers.IndexOf(matchedUser),
        //                    //                        UserPoolCapacity = matchedUser.Capacity,
        //                    //                        AssignedRecordsCount = assignedPoolContactsCount
        //                    //                    });

        //                    //                    int userAvailableCapacity = matchedUser.Capacity.Value - assignedPoolContactsCount;
        //                    //                    totalCapacity += userAvailableCapacity;

        //                    //                }
        //                    //            }

        //                    //            specifiedPoolUsers = specifiedPoolUsers.OrderBy(t => t.Index).ToList();

        //                    //            //Iterate users and assign contacts (round robin process)
        //                    //            while (creationModel.Contacts.Count > 0 && totalCapacity > 0)
        //                    //            {
        //                    //                for (int i = 0; i < specifiedPoolUsers.Count; i++)
        //                    //                {
        //                    //                    var specifiedUser = specifiedPoolUsers[i];

        //                    //                    var currentRation = creationModel.Contacts.Take(1).FirstOrDefault();
        //                    //                    if (currentRation != null)
        //                    //                    {
        //                    //                        //Verify current capacity
        //                    //                        int userAvailableCapacity = specifiedUser.UserPoolCapacity.Value - specifiedUser.AssignedRecordsCount.Value;
        //                    //                        if (userAvailableCapacity > 0) //Available slots found, assign contact
        //                    //                        {
        //                    //                            Contact recordCreationModel = new Contact
        //                    //                            {
        //                    //                                PoolID = pool.ID,
        //                    //                                FullNameEN = currentRation.FullNameEN,
        //                    //                                FullNameAR = currentRation.FullNameAR,
        //                    //                                Address = currentRation.Address,
        //                    //                                AssignedUserID = specifiedUser.UserID,
        //                    //                                Email = currentRation.Email,
        //                    //                                LeadSourceName = currentRation.LeadSourceName,
        //                    //                                LeadSourceType = currentRation.LeadSourceType,
        //                    //                                Occupation = currentRation.Occupation,
        //                    //                                PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
        //                    //                                StatusID = currentRation.StatusID,
        //                    //                                State = (int)CustomerStageState.Initial,
        //                    //                                IsFinalized = false,
        //                    //                            };

        //                    //                            var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);

        //                    //                            //Remove assigned rations from contact creation list
        //                    //                            creationModel.Contacts = creationModel.Contacts.Skip(1).ToList();

        //                    //                            //Adjust current user capacity record & total capacity 
        //                    //                            if (creationRes != null)
        //                    //                            {
        //                    //                                specifiedUser.AssignedRecordsCount += 1;
        //                    //                                totalCapacity -= 1;
        //                    //                            }
        //                    //                        }
        //                    //                    }
        //                    //                    else //No more records to assign
        //                    //                    {
        //                    //                        break;
        //                    //                    }
        //                    //                }
        //                    //            }

        //                    //            //Verify whether any records are still remaining

        //                    //            if (creationModel.Contacts.Count > 0)
        //                    //            {
        //                    //                //Return with capacity re-evaluation response if user is a pool administrator
        //                    //                var poolAdminQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == pool.ID && t.UserID == userID && t.IsAdmin == true);
        //                    //                var isPoolAdmin = poolAdminQuery.FirstOrDefault();
        //                    //                if (isPoolAdmin == null)
        //                    //                {
        //                    //                    result.Succeeded = false;
        //                    //                    result.Data = new BulkAssignmentResponse
        //                    //                    {
        //                    //                        RemainingSlots = creationModel.Contacts.Count,
        //                    //                        Success = false
        //                    //                    };
        //                    //                    result.ErrorType = ErrorType.IncreaseCapacity;
        //                    //                    return result;
        //                    //                }
        //                    //                else //Create remaining records as unassigned if creator is not a pool administrator
        //                    //                {
        //                    //                    for (int i = 0; i < creationModel.Contacts.Count; i++)
        //                    //                    {
        //                    //                        var contact = creationModel.Contacts[i];
        //                    //                        Contact recordCreationModel = new Contact
        //                    //                        {
        //                    //                            PoolID = pool.ID,
        //                    //                            FullNameEN = contact.FullNameEN,
        //                    //                            FullNameAR = contact.FullNameAR,
        //                    //                            Address = contact.Address,
        //                    //                            Email = contact.Email,
        //                    //                            LeadSourceName = contact.LeadSourceName,
        //                    //                            LeadSourceType = contact.LeadSourceType,
        //                    //                            Occupation = contact.Occupation,
        //                    //                            PrimaryPhoneNumber = contact.PrimaryPhoneNumber,
        //                    //                            StatusID = contact.StatusID,
        //                    //                            State = (int)CustomerStageState.Unassigned,
        //                    //                            IsFinalized = false,
        //                    //                        };

        //                    //                        var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);
        //                    //                    }

        //                    //                    //Commit record creation
        //                    //                    await unitOfWork.SaveChangesAsync();
        //                    //                    result.Succeeded = true;
        //                    //                    return result;
        //                    //                }
        //                    //            }
        //                    //            else //No more records remaining, commit record creation
        //                    //            {
        //                    //                await unitOfWork.SaveChangesAsync();
        //                    //                result.Succeeded = true;
        //                    //                return result;
        //                    //            }
        //                    //        }
        //                    //        else //Assign to existing pool prioritized users
        //                    //        {
        //                    //            int totalCapacity = 0;

        //                    //            List<SpecifiedPoolUser> prioritizedPoolUsers = new List<SpecifiedPoolUser>();

        //                    //            //calculate each user's capacity and assigned records
        //                    //            for (int i = 0; i < poolUsers.Count; i++)
        //                    //            {
        //                    //                string prioritizedUserID = poolUsers[i].UserID;

        //                    //                var matchedUser = poolUsers.Where(t => t.UserID == prioritizedUserID).FirstOrDefault();

        //                    //                if (matchedUser != null)
        //                    //                {
        //                    //                    var assignedPoolContactsQuery = await unitOfWork.ContactManager.GetAsync(t => t.PoolID == pool.ID && t.AssignedUserID == prioritizedUserID
        //                    //                    && t.IsFinalized == false);
        //                    //                    var assignedPoolContactsCount = assignedPoolContactsQuery.Count();
        //                    //                    prioritizedPoolUsers.Add(new SpecifiedPoolUser
        //                    //                    {
        //                    //                        UserID = prioritizedUserID,
        //                    //                        Index = poolUsers.IndexOf(matchedUser),
        //                    //                        UserPoolCapacity = matchedUser.Capacity,
        //                    //                        AssignedRecordsCount = assignedPoolContactsCount
        //                    //                    });

        //                    //                    int userAvailableCapacity = matchedUser.Capacity.Value - assignedPoolContactsCount;
        //                    //                    totalCapacity += userAvailableCapacity;

        //                    //                }
        //                    //            }

        //                    //            //Iterate users and assign contacts (round robin process)
        //                    //            while (creationModel.Contacts.Count > 0 && totalCapacity > 0)
        //                    //            {
        //                    //                for (int i = 0; i < prioritizedPoolUsers.Count; i++)
        //                    //                {
        //                    //                    var prioritizedUser = prioritizedPoolUsers[i];

        //                    //                    var currentRation = creationModel.Contacts.Take(1).FirstOrDefault();
        //                    //                    if (currentRation != null)
        //                    //                    {
        //                    //                        //Verify current capacity
        //                    //                        int userAvailableCapacity = prioritizedUser.UserPoolCapacity.Value - prioritizedUser.AssignedRecordsCount.Value;
        //                    //                        if (userAvailableCapacity > 0) //Available slots found, assign contact
        //                    //                        {
        //                    //                            Contact recordCreationModel = new Contact
        //                    //                            {
        //                    //                                PoolID = pool.ID,
        //                    //                                FullNameEN = currentRation.FullNameEN,
        //                    //                                FullNameAR = currentRation.FullNameAR,
        //                    //                                Address = currentRation.Address,
        //                    //                                AssignedUserID = prioritizedUser.UserID,
        //                    //                                Email = currentRation.Email,
        //                    //                                LeadSourceName = currentRation.LeadSourceName,
        //                    //                                LeadSourceType = currentRation.LeadSourceType,
        //                    //                                Occupation = currentRation.Occupation,
        //                    //                                PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
        //                    //                                StatusID = currentRation.StatusID,
        //                    //                                State = (int)CustomerStageState.Initial,
        //                    //                                IsFinalized = false,
        //                    //                            };

        //                    //                            var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);

        //                    //                            //Remove assigned rations from contact creation list
        //                    //                            creationModel.Contacts = creationModel.Contacts.Skip(1).ToList();

        //                    //                            //Adjust current user capacity record & total capacity 
        //                    //                            if (creationRes != null)
        //                    //                            {
        //                    //                                prioritizedUser.AssignedRecordsCount += 1;
        //                    //                                totalCapacity -= 1;
        //                    //                            }
        //                    //                        }
        //                    //                    }
        //                    //                    else //No more records to assign
        //                    //                    {
        //                    //                        break;
        //                    //                    }
        //                    //                }
        //                    //            }

        //                    //            //Verify whether any records are still remaining

        //                    //            if (creationModel.Contacts.Count > 0)
        //                    //            {
        //                    //                //Return with capacity re-evaluation response if user is a pool administrator
        //                    //                var poolAdminQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == pool.ID && t.UserID == userID && t.IsAdmin == true);
        //                    //                var isPoolAdmin = poolAdminQuery.FirstOrDefault();
        //                    //                if (isPoolAdmin != null)
        //                    //                {
        //                    //                    result.Succeeded = false;
        //                    //                    result.Data = new BulkAssignmentResponse
        //                    //                    {
        //                    //                        RemainingSlots = creationModel.Contacts.Count,
        //                    //                        Success = false
        //                    //                    };
        //                    //                    result.ErrorType = ErrorType.IncreaseCapacity;
        //                    //                    return result;
        //                    //                }
        //                    //                else //Create remaining records as unassigned if creator is not a pool administrator
        //                    //                {
        //                    //                    for (int i = 0; i < creationModel.Contacts.Count; i++)
        //                    //                    {
        //                    //                        var contact = creationModel.Contacts[i];
        //                    //                        Contact recordCreationModel = new Contact
        //                    //                        {
        //                    //                            PoolID = pool.ID,
        //                    //                            FullNameEN = contact.FullNameEN,
        //                    //                            FullNameAR = contact.FullNameAR,
        //                    //                            Address = contact.Address,
        //                    //                            Email = contact.Email,
        //                    //                            LeadSourceName = contact.LeadSourceName,
        //                    //                            LeadSourceType = contact.LeadSourceType,
        //                    //                            Occupation = contact.Occupation,
        //                    //                            PrimaryPhoneNumber = contact.PrimaryPhoneNumber,
        //                    //                            StatusID = contact.StatusID,
        //                    //                            State = (int)CustomerStageState.Unassigned,
        //                    //                            IsFinalized = false,
        //                    //                        };

        //                    //                        var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);
        //                    //                    }

        //                    //                    //Commit record creation
        //                    //                    await unitOfWork.SaveChangesAsync();
        //                    //                    result.Succeeded = true;
        //                    //                    return result;
        //                    //                }
        //                    //            }
        //                    //            else //No more records remaining, commit record creation
        //                    //            {
        //                    //                await unitOfWork.SaveChangesAsync();
        //                    //                result.Succeeded = true;
        //                    //                return result;
        //                    //            }
        //                    //        }
        //                    //    }
        //                    //    else //Can assign to self
        //                    //    {
        //                    //        for (int i = 0; i < creationModel.Contacts.Count; i++)
        //                    //        {
        //                    //            var contact = creationModel.Contacts[i];

        //                    //            var modelToCreate = new Contact
        //                    //            {
        //                    //                PoolID = creationModel.PoolID,
        //                    //                FullNameEN = contact.FullNameEN,
        //                    //                FullNameAR = contact.FullNameAR,
        //                    //                Address = contact.Address,
        //                    //                AssignedUserID = userID,
        //                    //                Email = contact.Email,
        //                    //                LeadSourceName = contact.LeadSourceName,
        //                    //                LeadSourceType = contact.LeadSourceType,
        //                    //                Occupation = contact.Occupation,
        //                    //                PrimaryPhoneNumber = contact.PrimaryPhoneNumber,
        //                    //                StatusID = contact.StatusID,
        //                    //                State = (int)CustomerStageState.Initial,
        //                    //                IsFinalized = false
        //                    //            };

        //                    //            modelToCreate.State = (int)CustomerStageState.Initial;

        //                    //            var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
        //                    //            if (creationModelResult == null)
        //                    //            {
        //                    //                result.Errors.Add("" + (i + 1));
        //                    //            }
        //                    //        }

        //                    //        //Commit record creation
        //                    //        await unitOfWork.SaveChangesAsync();
        //                    //        result.Succeeded = true;
        //                    //        return result;
        //                    //    }
        //                    //} //END OF Auto Assignment W/Capacity flow
        //                    //else if (pool.ConfigurationType == (int)PoolConfigurationTypes.Capacity)
        //                    //{
        //                    //    //Can assign to others
        //                    //    if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.Administrator.ToString()) && (creationModel.AssignedUsers != null && creationModel.AssignedUsers.Count > 0))
        //                    //    {
        //                    //        //Get all pool users
        //                    //        var poolUsersQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == pool.ID);
        //                    //        var poolUsers = poolUsersQuery.ToList();

        //                    //        int totalCapacity = 0;

        //                    //        //Calculate current user's capacity
        //                    //        List<SpecifiedPoolUser> specifiedPoolUsers = new List<SpecifiedPoolUser>();

        //                    //        for (int i = 0; i < creationModel.AssignedUsers.Count; i++)
        //                    //        {
        //                    //            var specifiedUserID = creationModel.AssignedUsers[i];

        //                    //            var matchedUser = poolUsers.Where(t => t.UserID == specifiedUserID).FirstOrDefault();

        //                    //            if (matchedUser != null)
        //                    //            {
        //                    //                var assignedPoolContactsQuery = await unitOfWork.ContactManager.GetAsync(t => t.PoolID == pool.ID && t.AssignedUserID == specifiedUserID
        //                    //                && t.IsFinalized == false);
        //                    //                var assignedPoolContactsCount = assignedPoolContactsQuery.Count();
        //                    //                specifiedPoolUsers.Add(new SpecifiedPoolUser
        //                    //                {
        //                    //                    UserID = specifiedUserID,
        //                    //                    UserPoolCapacity = matchedUser.Capacity,
        //                    //                    AssignedRecordsCount = assignedPoolContactsCount
        //                    //                });

        //                    //                int userAvailableCapacity = matchedUser.Capacity.Value - assignedPoolContactsCount;
        //                    //                totalCapacity += userAvailableCapacity;

        //                    //            }
        //                    //        }

        //                    //        //Iterate users and assign contacts (round robin process)
        //                    //        while (creationModel.Contacts.Count > 0 && totalCapacity > 0)
        //                    //        {
        //                    //            for (int i = 0; i < specifiedPoolUsers.Count; i++)
        //                    //            {
        //                    //                var specifiedUser = specifiedPoolUsers[i];

        //                    //                var currentRation = creationModel.Contacts.Take(1).FirstOrDefault();
        //                    //                if (currentRation != null)
        //                    //                {
        //                    //                    //Verify current capacity
        //                    //                    int userAvailableCapacity = specifiedUser.UserPoolCapacity.Value - specifiedUser.AssignedRecordsCount.Value;
        //                    //                    if (userAvailableCapacity > 0) //Available slots found, assign contact
        //                    //                    {
        //                    //                        Contact recordCreationModel = new Contact
        //                    //                        {
        //                    //                            PoolID = pool.ID,
        //                    //                            FullNameEN = currentRation.FullNameEN,
        //                    //                            FullNameAR = currentRation.FullNameAR,
        //                    //                            Address = currentRation.Address,
        //                    //                            AssignedUserID = specifiedUser.UserID,
        //                    //                            Email = currentRation.Email,
        //                    //                            LeadSourceName = currentRation.LeadSourceName,
        //                    //                            LeadSourceType = currentRation.LeadSourceType,
        //                    //                            Occupation = currentRation.Occupation,
        //                    //                            PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
        //                    //                            StatusID = currentRation.StatusID,
        //                    //                            State = (int)CustomerStageState.Initial,
        //                    //                            IsFinalized = false,
        //                    //                        };

        //                    //                        var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);

        //                    //                        //Remove assigned rations from contact creation list
        //                    //                        creationModel.Contacts = creationModel.Contacts.Skip(1).ToList();

        //                    //                        //Adjust current user capacity record & total capacity 
        //                    //                        if (creationRes != null)
        //                    //                        {
        //                    //                            specifiedUser.AssignedRecordsCount += 1;
        //                    //                            totalCapacity -= 1;
        //                    //                        }
        //                    //                    }
        //                    //                }
        //                    //                else //No more records to assign
        //                    //                {
        //                    //                    break;
        //                    //                }
        //                    //            }
        //                    //        }

        //                    //        //Create remaining records as unassigned if found

        //                    //        if (creationModel.Contacts.Count > 0)
        //                    //        {
        //                    //            for (int i = 0; i < creationModel.Contacts.Count; i++)
        //                    //            {
        //                    //                var contact = creationModel.Contacts[i];
        //                    //                Contact recordCreationModel = new Contact
        //                    //                {
        //                    //                    PoolID = pool.ID,
        //                    //                    FullNameEN = contact.FullNameEN,
        //                    //                    FullNameAR = contact.FullNameAR,
        //                    //                    Address = contact.Address,
        //                    //                    Email = contact.Email,
        //                    //                    LeadSourceName = contact.LeadSourceName,
        //                    //                    LeadSourceType = contact.LeadSourceType,
        //                    //                    Occupation = contact.Occupation,
        //                    //                    PrimaryPhoneNumber = contact.PrimaryPhoneNumber,
        //                    //                    StatusID = contact.StatusID,
        //                    //                    State = (int)CustomerStageState.Unassigned,
        //                    //                    IsFinalized = false,
        //                    //                };

        //                    //                var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);
        //                    //            }

        //                    //            //Commit record creation
        //                    //            await unitOfWork.SaveChangesAsync();
        //                    //            result.Succeeded = true;
        //                    //            return result;
        //                    //        }
        //                    //        else //No more records remaining, commit record creation
        //                    //        {
        //                    //            await unitOfWork.SaveChangesAsync();
        //                    //            result.Succeeded = true;
        //                    //            return result;
        //                    //        }
        //                    //    }
        //                    //    else if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.Agent.ToString()))//Can assign to self
        //                    //    {
        //                    //        for (int i = 0; i < creationModel.Contacts.Count; i++)
        //                    //        {
        //                    //            var contact = creationModel.Contacts[i];

        //                    //            var modelToCreate = new Contact
        //                    //            {
        //                    //                PoolID = creationModel.PoolID,
        //                    //                FullNameEN = contact.FullNameEN,
        //                    //                FullNameAR = contact.FullNameAR,
        //                    //                Address = contact.Address,
        //                    //                AssignedUserID = userID,
        //                    //                Email = contact.Email,
        //                    //                LeadSourceName = contact.LeadSourceName,
        //                    //                LeadSourceType = contact.LeadSourceType,
        //                    //                Occupation = contact.Occupation,
        //                    //                PrimaryPhoneNumber = contact.PrimaryPhoneNumber,
        //                    //                StatusID = contact.StatusID,
        //                    //                State = (int)CustomerStageState.Initial,
        //                    //                IsFinalized = false
        //                    //            };

        //                    //            var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
        //                    //            if (creationModelResult == null)
        //                    //            {
        //                    //                result.Errors.Add("" + (i + 1));
        //                    //            }
        //                    //        }

        //                    //        //Commit record creation
        //                    //        await unitOfWork.SaveChangesAsync();
        //                    //        result.Succeeded = true;
        //                    //        return result;
        //                    //    }
        //                    //    else //Create all records as unassigned
        //                    //    {
        //                    //        for (int i = 0; i < creationModel.Contacts.Count; i++)
        //                    //        {
        //                    //            var contact = creationModel.Contacts[i];

        //                    //            var modelToCreate = new Contact
        //                    //            {
        //                    //                PoolID = creationModel.PoolID,
        //                    //                FullNameEN = contact.FullNameEN,
        //                    //                FullNameAR = contact.FullNameAR,
        //                    //                Address = contact.Address,
        //                    //                Email = contact.Email,
        //                    //                LeadSourceName = contact.LeadSourceName,
        //                    //                LeadSourceType = contact.LeadSourceType,
        //                    //                Occupation = contact.Occupation,
        //                    //                PrimaryPhoneNumber = contact.PrimaryPhoneNumber,
        //                    //                StatusID = contact.StatusID,
        //                    //                State = (int)CustomerStageState.Unassigned,
        //                    //                IsFinalized = false
        //                    //            };


        //                    //            var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
        //                    //            if (creationModelResult == null)
        //                    //            {
        //                    //                result.Errors.Add("" + (i + 1));
        //                    //            }
        //                    //        }

        //                    //        //Commit record creation
        //                    //        await unitOfWork.SaveChangesAsync();
        //                    //        result.Succeeded = true;
        //                    //        return result;
        //                    //    }
        //                    //} //END OF Capacity flow
        //                    else //Default pool configuration flow
        //                    {
        //                        //Can assign to others
        //                        if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.Administrator.ToString()) && (creationModel.AssignedUsers != null && creationModel.AssignedUsers.Count > 0))
        //                        {
        //                            while (creationModel.Contacts.Count > 0)
        //                            {
        //                                for (int i = 0; i < creationModel.AssignedUsers.Count; i++)
        //                                {
        //                                    var assignedUserID = creationModel.AssignedUsers[i];

        //                                    var currentRation = creationModel.Contacts.Take(1).FirstOrDefault();
        //                                    if (currentRation != null)
        //                                    {
        //                                        Contact recordCreationModel = new Contact
        //                                        {
        //                                            PoolID = pool.ID,
        //                                            FullNameEN = currentRation.FullNameEN,
        //                                            FullNameAR = currentRation.FullNameAR,
        //                                            Address = currentRation.Address,
        //                                            AssignedUserID = assignedUserID,
        //                                            Email = currentRation.Email,
        //                                            LeadSourceName = currentRation.LeadSourceName,
        //                                            LeadSourceType = currentRation.LeadSourceType,
        //                                            Occupation = currentRation.Occupation,
        //                                            PrimaryPhoneNumber = currentRation.PrimaryPhoneNumber,
        //                                            StatusID = currentRation.StatusID,
        //                                            State = (int)CustomerStageState.Initial,
        //                                            IsFinalized = false,
        //                                        };

        //                                        var creationRes = await unitOfWork.ContactManager.CreateAsync(recordCreationModel);

        //                                        //Remove assigned rations from contact creation list
        //                                        creationModel.Contacts = creationModel.Contacts.Skip(1).ToList();
        //                                    }
        //                                    else //No more records to assign
        //                                    {
        //                                        break;
        //                                    }
        //                                }
        //                            }

        //                            //Commit record creation
        //                            await unitOfWork.SaveChangesAsync();
        //                            result.Succeeded = true;
        //                            return result;
        //                        }
        //                        //Can assign to self
        //                        else if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.Administrator.ToString()) && creationModel.AssignedUsers == null)
        //                        {
        //                            for (int i = 0; i < creationModel.Contacts.Count; i++)
        //                            {
        //                                var contact = creationModel.Contacts[i];

        //                                var modelToCreate = new Contact
        //                                {
        //                                    PoolID = creationModel.PoolID,
        //                                    FullNameEN = contact.FullNameEN,
        //                                    FullNameAR = contact.FullNameAR,
        //                                    Address = contact.Address,
        //                                    AssignedUserID = userID,
        //                                    Email = contact.Email,
        //                                    LeadSourceName = contact.LeadSourceName,
        //                                    LeadSourceType = contact.LeadSourceType,
        //                                    Occupation = contact.Occupation,
        //                                    PrimaryPhoneNumber = contact.PrimaryPhoneNumber,
        //                                    StatusID = contact.StatusID,
        //                                    State = (int)CustomerStageState.Initial,
        //                                    IsFinalized = false
        //                                };

        //                                modelToCreate.State = (int)CustomerStageState.Initial;

        //                                var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
        //                                if (creationModelResult == null)
        //                                {
        //                                    result.Errors.Add("" + (i + 1));
        //                                }
        //                            }

        //                            //Commit record creation
        //                            await unitOfWork.SaveChangesAsync();
        //                            result.Succeeded = true;
        //                            return result;
        //                        }
        //                        //Create all records as unassigned
        //                        else
        //                        {
        //                            for (int i = 0; i < creationModel.Contacts.Count; i++)
        //                            {
        //                                var contact = creationModel.Contacts[i];

        //                                var modelToCreate = new Contact
        //                                {
        //                                    PoolID = creationModel.PoolID,
        //                                    FullNameEN = contact.FullNameEN,
        //                                    FullNameAR = contact.FullNameAR,
        //                                    Address = contact.Address,
        //                                    Email = contact.Email,
        //                                    LeadSourceName = contact.LeadSourceName,
        //                                    LeadSourceType = contact.LeadSourceType,
        //                                    Occupation = contact.Occupation,
        //                                    PrimaryPhoneNumber = contact.PrimaryPhoneNumber,
        //                                    StatusID = contact.StatusID,
        //                                    State = (int)CustomerStageState.Unassigned,
        //                                    IsFinalized = false
        //                                };

        //                                var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
        //                                if (creationModelResult == null)
        //                                {
        //                                    result.Errors.Add("" + (i + 1));
        //                                }
        //                            }

        //                            //Commit record creation
        //                            await unitOfWork.SaveChangesAsync();
        //                            result.Succeeded = true;
        //                            return result;
        //                        }
        //                    } //END OF Default pool configuration flow
        //                }
        //                else //Pool not found
        //                {
        //                    result.Succeeded = false;
        //                    result.Errors.Add("Pool not found or does not exist");
        //                    return result;
        //                }
        //            }
        //            else //Invalid user token
        //            {
        //                result.Succeeded = false;
        //                result.ErrorCode = ErrorCode.A500;
        //                result.Errors.Add("Unauthorized");
        //                return result;
        //            }
        //        }
        //        else //Invalid user token
        //        {
        //            result.Succeeded = false;
        //            result.ErrorCode = ErrorCode.A500;
        //            result.Errors.Add("Unauthorized");
        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Succeeded = false;
        //        result.Errors.Add(ex.Message);
        //        result.ErrorType = ErrorType.SystemError;
        //        return result;
        //    }
        //}

        //Create Contact 
        //public async Task<ApiResponse<bool>> CreateContact(ContactCreationModel creationModel)
        //{
        //    ApiResponse<bool> result = new ApiResponse<bool>();
        //    try
        //    {
        //        var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

        //        if (userID != null)
        //        {
        //            var modelQuery = await unitOfWork.ContactManager.GetAsync(t => t.PrimaryPhoneNumber == creationModel.PrimaryPhoneNumber, includeProperties: "PhoneNumbers,Status,Pool");
        //            var model = modelQuery.FirstOrDefault();

        //            //Phone number not duplicated
        //            if (model == null)
        //            {
        //                //Get user role
        //                var user = await unitOfWork.UserManager.GetUserById(userID);
        //                if (user != null)
        //                {
        //                    //Get Designated Pool
        //                    var poolQuery = await unitOfWork.PoolManager.GetAsync(t => t.ID == creationModel.PoolID);
        //                    var pool = poolQuery.FirstOrDefault();

        //                    if (pool != null)
        //                    {
        //                        //Identify pool configuration type
        //                        if (pool.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignment || pool.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignmentCapacity) //Auto assignemnt config.
        //                        {
        //                            //Can assign to others
        //                            if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.TeamLeader.ToString()) && creationModel.AssigneeID != null)
        //                            {
        //                                var modelToCreate = new Contact
        //                                {
        //                                    PoolID = creationModel.PoolID,
        //                                    FullNameEN = creationModel.FullNameEN,
        //                                    FullNameAR = creationModel.FullNameAR,
        //                                    Address = creationModel.Address,
        //                                    AssignedUserID = creationModel.AssigneeID,
        //                                    Email = creationModel.Email,
        //                                    LeadSourceName = creationModel.LeadSourceName,
        //                                    LeadSourceType = creationModel.LeadSourceType,
        //                                    Occupation = creationModel.Occupation,
        //                                    PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
        //                                    StatusID = creationModel.StatusID,
        //                                    IsFinalized = false
        //                                };

        //                                modelToCreate.State = (int)CustomerStageState.Initial;
        //                                var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
        //                                if (creationModelResult != null)
        //                                {
        //                                    await unitOfWork.SaveChangesAsync();
        //                                    result.Succeeded = true;
        //                                    result.Data = true;
        //                                    return result;
        //                                }
        //                                else
        //                                {
        //                                    result.Succeeded = false;
        //                                    result.Errors.Add("Unable to create contact, please try again later ");
        //                                    return result;
        //                                }
        //                            }
        //                            //Can assign to self
        //                            else if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.Administrator.ToString()))
        //                            {
        //                                var modelToCreate = new Contact
        //                                {
        //                                    PoolID = creationModel.PoolID,
        //                                    FullNameEN = creationModel.FullNameEN,
        //                                    FullNameAR = creationModel.FullNameAR,
        //                                    Address = creationModel.Address,
        //                                    AssignedUserID = userID,
        //                                    Email = creationModel.Email,
        //                                    LeadSourceName = creationModel.LeadSourceName,
        //                                    LeadSourceType = creationModel.LeadSourceType,
        //                                    Occupation = creationModel.Occupation,
        //                                    PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
        //                                    StatusID = creationModel.StatusID,
        //                                    IsFinalized = false
        //                                };

        //                                modelToCreate.State = (int)CustomerStageState.Initial;

        //                                var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
        //                                if (creationModelResult != null)
        //                                {
        //                                    await unitOfWork.SaveChangesAsync();
        //                                    result.Succeeded = true;
        //                                    result.Data = true;
        //                                    return result;
        //                                }
        //                                else
        //                                {
        //                                    result.Succeeded = false;
        //                                    result.Errors.Add("Unable to create contact, please try again later ");
        //                                    return result;
        //                                }
        //                            }
        //                            //Error
        //                            else
        //                            {
        //                                result.Succeeded = false;
        //                                result.Errors.Add("Not authorized");
        //                                return result;
        //                            }
        //                        }
        //                        else if (pool.ConfigurationType == (int)PoolConfigurationTypes.AutoAssignmentCapacity) //Auto assignment w/ capacity config.
        //                        {
        //                            //Can assign to others
        //                            if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.TeamLeader.ToString()) && creationModel.AssigneeID != null)
        //                            {
        //                                //Verify selected user's pool capacity
        //                                var selectedAssigneeQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == pool.ID && t.UserID == creationModel.AssigneeID);
        //                                var selectedAssignee = selectedAssigneeQuery.FirstOrDefault();

        //                                if (selectedAssignee != null)
        //                                {
        //                                    //Get assignee's pool contacts and verify with current user's capacity
        //                                    var assignedContactsQuery = await unitOfWork.ContactManager.GetAsync(t => t.AssignedUserID == selectedAssignee.UserID && t.PoolID == pool.ID && t.IsFinalized == false);
        //                                    var assignedContacts = assignedContactsQuery.ToList();

        //                                    if (assignedContacts != null)
        //                                    {
        //                                        if (assignedContacts.Count < selectedAssignee.Capacity.Value) //assign contact to user
        //                                        {
        //                                            var modelToCreate = new Contact
        //                                            {
        //                                                PoolID = creationModel.PoolID,
        //                                                FullNameEN = creationModel.FullNameEN,
        //                                                FullNameAR = creationModel.FullNameAR,
        //                                                Address = creationModel.Address,
        //                                                AssignedUserID = creationModel.AssigneeID,
        //                                                Email = creationModel.Email,
        //                                                LeadSourceName = creationModel.LeadSourceName,
        //                                                LeadSourceType = creationModel.LeadSourceType,
        //                                                Occupation = creationModel.Occupation,
        //                                                PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
        //                                                StatusID = creationModel.StatusID,
        //                                                IsFinalized = false
        //                                            };

        //                                            modelToCreate.State = (int)CustomerStageState.Initial;
        //                                            var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
        //                                            if (creationModelResult != null)
        //                                            {
        //                                                await unitOfWork.SaveChangesAsync();
        //                                                result.Succeeded = true;
        //                                                result.Data = true;
        //                                                return result;
        //                                            }
        //                                            else
        //                                            {
        //                                                result.Succeeded = false;
        //                                                result.Errors.Add("Unable to create contact, please try again later ");
        //                                                return result;
        //                                            }
        //                                        }
        //                                        else //Capacity exceeded, Verify creator pool priviliges
        //                                        {
        //                                            var poolAdminQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == pool.ID && t.UserID == userID && t.IsAdmin == true);
        //                                            var poolAdmin = poolAdminQuery.FirstOrDefault();

        //                                            if (poolAdmin != null) //Selected agent capacity increase response
        //                                            {
        //                                                result.Succeeded = false;
        //                                                result.Errors.Add("Selected user's capacity is full, would you like to increase this user's capacity?");
        //                                                result.Errors.Add("Selected user's capacity is full, would you like to increase this user's capacity?");
        //                                                result.ErrorType = ErrorType.IncreaseCapacity;
        //                                                return result;
        //                                            }
        //                                            else //Re-selection error
        //                                            {
        //                                                result.Succeeded = false;
        //                                                result.Errors.Add("Selected user's capacity is full");
        //                                                result.Errors.Add("Selected user's capacity is full");
        //                                                result.ErrorType = ErrorType.ReselectUser;
        //                                                return result;
        //                                            }
        //                                        }
        //                                    }
        //                                    else //assign contact to user
        //                                    {
        //                                        var modelToCreate = new Contact
        //                                        {
        //                                            PoolID = creationModel.PoolID,
        //                                            FullNameEN = creationModel.FullNameEN,
        //                                            FullNameAR = creationModel.FullNameAR,
        //                                            Address = creationModel.Address,
        //                                            AssignedUserID = creationModel.AssigneeID,
        //                                            Email = creationModel.Email,
        //                                            LeadSourceName = creationModel.LeadSourceName,
        //                                            LeadSourceType = creationModel.LeadSourceType,
        //                                            Occupation = creationModel.Occupation,
        //                                            PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
        //                                            StatusID = creationModel.StatusID,
        //                                            IsFinalized = false
        //                                        };

        //                                        modelToCreate.State = (int)CustomerStageState.Initial;
        //                                        var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
        //                                        if (creationModelResult != null)
        //                                        {
        //                                            await unitOfWork.SaveChangesAsync();
        //                                            result.Succeeded = true;
        //                                            result.Data = true;
        //                                            return result;
        //                                        }
        //                                        else
        //                                        {
        //                                            result.Succeeded = false;
        //                                            result.Errors.Add("Unable to create contact, please try again later ");
        //                                            return result;
        //                                        }
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    result.Succeeded = false;
        //                                    result.Errors.Add("Selected assignee does not exist");
        //                                    result.Errors.Add("Selected assignee does not exist");
        //                                    return result;
        //                                }
        //                            }
        //                            //Can assign to self
        //                            else if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.Administrator.ToString()))
        //                            {
        //                                var modelToCreate = new Contact
        //                                {
        //                                    PoolID = creationModel.PoolID,
        //                                    FullNameEN = creationModel.FullNameEN,
        //                                    FullNameAR = creationModel.FullNameAR,
        //                                    Address = creationModel.Address,
        //                                    AssignedUserID = userID,
        //                                    Email = creationModel.Email,
        //                                    LeadSourceName = creationModel.LeadSourceName,
        //                                    LeadSourceType = creationModel.LeadSourceType,
        //                                    Occupation = creationModel.Occupation,
        //                                    PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
        //                                    StatusID = creationModel.StatusID,
        //                                    IsFinalized = false
        //                                };

        //                                modelToCreate.State = (int)CustomerStageState.Initial;

        //                                var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
        //                                if (creationModelResult != null)
        //                                {
        //                                    await unitOfWork.SaveChangesAsync();
        //                                    result.Succeeded = true;
        //                                    result.Data = true;
        //                                    return result;
        //                                }
        //                                else
        //                                {
        //                                    result.Succeeded = false;
        //                                    result.Errors.Add("Unable to create contact, please try again later ");
        //                                    result.Errors.Add("Unable to create contact, please try again later ");
        //                                    return result;
        //                                }
        //                            }
        //                            else
        //                            {
        //                                result.Succeeded = false;
        //                                result.Errors.Add("Not authorized");
        //                                return result;
        //                            }
        //                        }
        //                        //else if (pool.ConfigurationType == (int)PoolConfigurationTypes.Capacity)
        //                        //{
        //                        //    //Can assign to others
        //                        //    if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.TeamLeader.ToString()) && creationModel.AssigneeID != null)
        //                        //    {
        //                        //        //Verify selected user's pool capacity
        //                        //        var selectedAssigneeQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == pool.ID && t.UserID == creationModel.AssigneeID);
        //                        //        var selectedAssignee = selectedAssigneeQuery.FirstOrDefault();

        //                        //        if (selectedAssignee != null)
        //                        //        {
        //                        //            //Get assignee's pool contacts and verify with current user's capacity
        //                        //            var assignedContactsQuery = await unitOfWork.ContactManager.GetAsync(t => t.AssignedUserID == selectedAssignee.UserID && t.PoolID == pool.ID && t.IsFinalized == false);
        //                        //            var assignedContacts = assignedContactsQuery.ToList();


        //                        //            if (assignedContacts != null)
        //                        //            {
        //                        //                if (assignedContacts.Count < selectedAssignee.Capacity.Value) //assign contact to user
        //                        //                {
        //                        //                    var modelToCreate = new Contact
        //                        //                    {
        //                        //                        PoolID = creationModel.PoolID,
        //                        //                        FullNameEN = creationModel.FullNameEN,
        //                        //                        FullNameAR = creationModel.FullNameAR,
        //                        //                        Address = creationModel.Address,
        //                        //                        AssignedUserID = creationModel.AssigneeID,
        //                        //                        Email = creationModel.Email,
        //                        //                        LeadSourceName = creationModel.LeadSourceName,
        //                        //                        LeadSourceType = creationModel.LeadSourceType,
        //                        //                        Occupation = creationModel.Occupation,
        //                        //                        PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
        //                        //                        StatusID = creationModel.StatusID,
        //                        //                        IsFinalized = false
        //                        //                    };

        //                        //                    modelToCreate.State = (int)CustomerStageState.Initial;
        //                        //                    var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
        //                        //                    if (creationModelResult != null)
        //                        //                    {
        //                        //                        await unitOfWork.SaveChangesAsync();
        //                        //                        result.Succeeded = true;
        //                        //                        result.Data = true;
        //                        //                        return result;
        //                        //                    }
        //                        //                    else
        //                        //                    {
        //                        //                        result.Succeeded = false;
        //                        //                        result.Errors.Add("Unable to create contact, please try again later ");
        //                        //                        return result;
        //                        //                    }
        //                        //                }
        //                        //                else //Capacity exceeded, Verify creator pool priviliges
        //                        //                {
        //                        //                    var poolAdminQuery = await unitOfWork.PoolUserManager.GetAsync(t => t.PoolID == pool.ID && t.UserID == userID && t.IsAdmin == true);
        //                        //                    var poolAdmin = poolAdminQuery.FirstOrDefault();

        //                        //                    if (poolAdmin != null) //Selected agent capacity increase response
        //                        //                    {
        //                        //                        result.Succeeded = false;
        //                        //                        result.Errors.Add("Selected user's capacity is full, would you like to increase this user's capacity?");
        //                        //                        result.Errors.Add("Selected user's capacity is full, would you like to increase this user's capacity?");
        //                        //                        result.ErrorType = ErrorType.IncreaseCapacity;
        //                        //                        return result;
        //                        //                    }
        //                        //                    else //Re-selection error
        //                        //                    {
        //                        //                        result.Succeeded = false;
        //                        //                        result.Errors.Add("Selected user's capacity is full");
        //                        //                        result.Errors.Add("Selected user's capacity is full");
        //                        //                        result.ErrorType = ErrorType.ReselectUser;
        //                        //                        return result;
        //                        //                    }
        //                        //                }
        //                        //            }
        //                        //            else //assign contact to user
        //                        //            {
        //                        //                var modelToCreate = new Contact
        //                        //                {
        //                        //                    PoolID = creationModel.PoolID,
        //                        //                    FullNameEN = creationModel.FullNameEN,
        //                        //                    FullNameAR = creationModel.FullNameAR,
        //                        //                    Address = creationModel.Address,
        //                        //                    AssignedUserID = creationModel.AssigneeID,
        //                        //                    Email = creationModel.Email,
        //                        //                    LeadSourceName = creationModel.LeadSourceName,
        //                        //                    LeadSourceType = creationModel.LeadSourceType,
        //                        //                    Occupation = creationModel.Occupation,
        //                        //                    PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
        //                        //                    StatusID = creationModel.StatusID,
        //                        //                    IsFinalized = false
        //                        //                };

        //                        //                modelToCreate.State = (int)CustomerStageState.Initial;
        //                        //                var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
        //                        //                if (creationModelResult != null)
        //                        //                {
        //                        //                    await unitOfWork.SaveChangesAsync();
        //                        //                    result.Succeeded = true;
        //                        //                    result.Data = true;
        //                        //                    return result;
        //                        //                }
        //                        //                else
        //                        //                {
        //                        //                    result.Succeeded = false;
        //                        //                    result.Errors.Add("Unable to create contact, please try again later ");
        //                        //                    return result;
        //                        //                }
        //                        //            }
        //                        //        }
        //                        //        else
        //                        //        {
        //                        //            result.Succeeded = false;
        //                        //            result.Errors.Add("Selected assignee does not exist");
        //                        //            result.Errors.Add("Selected assignee does not exist");
        //                        //            return result;
        //                        //        }
        //                        //    }
        //                        //    //Can assign to self
        //                        //    else if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.Administrator.ToString()))
        //                        //    {
        //                        //        var modelToCreate = new Contact
        //                        //        {
        //                        //            PoolID = creationModel.PoolID,
        //                        //            FullNameEN = creationModel.FullNameEN,
        //                        //            FullNameAR = creationModel.FullNameAR,
        //                        //            Address = creationModel.Address,
        //                        //            AssignedUserID = userID,
        //                        //            Email = creationModel.Email,
        //                        //            LeadSourceName = creationModel.LeadSourceName,
        //                        //            LeadSourceType = creationModel.LeadSourceType,
        //                        //            Occupation = creationModel.Occupation,
        //                        //            PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
        //                        //            StatusID = creationModel.StatusID,
        //                        //            IsFinalized = false
        //                        //        };

        //                        //        modelToCreate.State = (int)CustomerStageState.Initial;

        //                        //        var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
        //                        //        if (creationModelResult != null)
        //                        //        {
        //                        //            await unitOfWork.SaveChangesAsync();
        //                        //            result.Succeeded = true;
        //                        //            result.Data = true;
        //                        //            return result;
        //                        //        }
        //                        //        else
        //                        //        {
        //                        //            result.Succeeded = false;
        //                        //            result.Errors.Add("Unable to create contact, please try again later ");
        //                        //            result.Errors.Add("Unable to create contact, please try again later ");
        //                        //            return result;
        //                        //        }
        //                        //    }
        //                        //    //Create contact as unassigned
        //                        //    else
        //                        //    {
        //                        //        var modelToCreate = new Contact
        //                        //        {
        //                        //            PoolID = creationModel.PoolID,
        //                        //            FullNameEN = creationModel.FullNameEN,
        //                        //            FullNameAR = creationModel.FullNameAR,
        //                        //            Address = creationModel.Address,
        //                        //            Email = creationModel.Email,
        //                        //            LeadSourceName = creationModel.LeadSourceName,
        //                        //            LeadSourceType = creationModel.LeadSourceType,
        //                        //            Occupation = creationModel.Occupation,
        //                        //            PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
        //                        //            StatusID = creationModel.StatusID
        //                        //        };

        //                        //        modelToCreate.State = (int)CustomerStageState.Unassigned;

        //                        //        var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
        //                        //        if (creationModelResult != null)
        //                        //        {
        //                        //            await unitOfWork.SaveChangesAsync();
        //                        //            result.Succeeded = true;
        //                        //            result.Data = true;
        //                        //            return result;
        //                        //        }
        //                        //        else
        //                        //        {
        //                        //            result.Succeeded = false;
        //                        //            result.Errors.Add("Unable to create contact, please try again later ");
        //                        //            return result;
        //                        //        }
        //                        //    }
        //                        //}
        //                        else //Default config type
        //                        {
        //                            //Can assign to others
        //                            if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.TeamLeader.ToString()) && creationModel.AssigneeID != null)
        //                            {
        //                                var modelToCreate = new Contact
        //                                {
        //                                    PoolID = creationModel.PoolID,
        //                                    FullNameEN = creationModel.FullNameEN,
        //                                    FullNameAR = creationModel.FullNameAR,
        //                                    Address = creationModel.Address,
        //                                    AssignedUserID = creationModel.AssigneeID,
        //                                    Email = creationModel.Email,
        //                                    LeadSourceName = creationModel.LeadSourceName,
        //                                    LeadSourceType = creationModel.LeadSourceType,
        //                                    Occupation = creationModel.Occupation,
        //                                    PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
        //                                    StatusID = creationModel.StatusID,
        //                                    IsFinalized = false
        //                                };

        //                                modelToCreate.State = (int)CustomerStageState.Initial;
        //                                var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
        //                                if (creationModelResult != null)
        //                                {
        //                                    await unitOfWork.SaveChangesAsync();
        //                                    result.Succeeded = true;
        //                                    result.Data = true;
        //                                    return result;
        //                                }
        //                                else
        //                                {
        //                                    result.Succeeded = false;
        //                                    result.Errors.Add("Unable to create contact, please try again later ");
        //                                    return result;
        //                                }
        //                            }
        //                            //Can assign to self
        //                            else if (await unitOfWork.UserManager.IsInRoleAsync(user, UserRoles.Administrator.ToString()))
        //                            {
        //                                var modelToCreate = new Contact
        //                                {
        //                                    PoolID = creationModel.PoolID,
        //                                    FullNameEN = creationModel.FullNameEN,
        //                                    FullNameAR = creationModel.FullNameAR,
        //                                    Address = creationModel.Address,
        //                                    AssignedUserID = userID,
        //                                    Email = creationModel.Email,
        //                                    LeadSourceName = creationModel.LeadSourceName,
        //                                    LeadSourceType = creationModel.LeadSourceType,
        //                                    Occupation = creationModel.Occupation,
        //                                    PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
        //                                    StatusID = creationModel.StatusID,
        //                                    State = (int)CustomerStageState.Initial,
        //                                    IsFinalized = false
        //                                };

        //                                var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
        //                                if (creationModelResult != null)
        //                                {
        //                                    await unitOfWork.SaveChangesAsync();
        //                                    result.Succeeded = true;
        //                                    result.Data = true;
        //                                    return result;
        //                                }
        //                                else
        //                                {
        //                                    result.Succeeded = false;
        //                                    result.Errors.Add("Unable to create contact, please try again later ");
        //                                    result.Errors.Add("Unable to create contact, please try again later ");
        //                                    return result;
        //                                }
        //                            }
        //                            else //Create contact as unassigned
        //                            {
        //                                var modelToCreate = new Contact
        //                                {
        //                                    PoolID = creationModel.PoolID,
        //                                    FullNameEN = creationModel.FullNameEN,
        //                                    FullNameAR = creationModel.FullNameAR,
        //                                    Address = creationModel.Address,
        //                                    Email = creationModel.Email,
        //                                    LeadSourceName = creationModel.LeadSourceName,
        //                                    LeadSourceType = creationModel.LeadSourceType,
        //                                    Occupation = creationModel.Occupation,
        //                                    PrimaryPhoneNumber = creationModel.PrimaryPhoneNumber,
        //                                    State = (int)CustomerStageState.Unassigned,
        //                                    StatusID = creationModel.StatusID
        //                                };

        //                                var creationModelResult = await unitOfWork.ContactManager.CreateAsync(modelToCreate);
        //                                if (creationModelResult != null)
        //                                {
        //                                    await unitOfWork.SaveChangesAsync();
        //                                    result.Succeeded = true;
        //                                    result.Data = true;
        //                                    return result;
        //                                }
        //                                else
        //                                {
        //                                    result.Succeeded = false;
        //                                    result.Errors.Add("Unable to create contact, please try again later ");
        //                                    return result;
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        result.Succeeded = false;
        //                        result.Errors.Add("Pool does not exist");
        //                        result.Errors.Add("Pool does not exist");
        //                        return result;
        //                    }
        //                }
        //                else // user not found
        //                {
        //                    result.Succeeded = false;
        //                    result.Errors.Add("Unauthorized");
        //                    return result;
        //                }
        //            }
        //            else //Duplicate Contact
        //            {
        //                //duplicated contact is unassigned
        //                if (model.AssignedUserID == null && model.Status.Status == CustomerStageState.Unassigned.ToString())
        //                {
        //                    //Transfer contact to current user's pool
        //                    if (model.PoolID != creationModel.PoolID)
        //                    {
        //                        model.PoolID = creationModel.PoolID;
        //                    }
        //                    //assign contact to user
        //                    model.AssignedUserID = userID;

        //                    model.State = (int)CustomerStageState.Initial;

        //                    var assignModelResult = await unitOfWork.ContactManager.UpdateAsync(model);
        //                    if (assignModelResult)
        //                    {
        //                        await unitOfWork.SaveChangesAsync();
        //                        result.Succeeded = true;
        //                        result.Data = true;
        //                        return result;
        //                    }
        //                    else
        //                    {
        //                        result.Succeeded = false;
        //                        result.Errors.Add("Unable to assign existing contact, please try again later ");
        //                        return result;
        //                    }

        //                }
        //                else //duplicate contact is assigned
        //                {
        //                    result.Succeeded = false;
        //                    result.Errors.Add("Phone number cannot be duplicated");
        //                    return result;
        //                }
        //            }
        //        }
        //        else //Invalid user token
        //        {
        //            result.Succeeded = false;
        //            result.ErrorCode = ErrorCode.A500;
        //            result.Errors.Add("Unauthorized");
        //            return result;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        result.Succeeded = false;
        //        result.Errors.Add(ex.Message);
        //        result.ErrorType = ErrorType.SystemError;
        //        return result;
        //    }
        //}

    }

}


