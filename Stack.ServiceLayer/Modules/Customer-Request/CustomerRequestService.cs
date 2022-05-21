
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
using Stack.Entities.Models.Modules.CR;
using Stack.DTOs.Models.Modules.CR;

namespace Stack.ServiceLayer.Modules.CR
{
    public class CustomerRequestService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private static readonly HttpClient client = new HttpClient();

        public CustomerRequestService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;

        }


        #region Phase
        //public async Task<ApiResponse<List<ContactListViewModel>>> GetAssignedContacts()
        //{
        //    ApiResponse<List<ContactListViewModel>> result = new ApiResponse<List<ContactListViewModel>>();
        //    try
        //    {
        //        var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

        //        if (userID != null)
        //        {
        //            var assignedRecords = await unitOfWork.ContactManager.GetAssignedContacts(userID);

        //            if (assignedRecords != null && assignedRecords.Count > 0)
        //            {
        //                result.Succeeded = true;
        //                result.Data = assignedRecords;
        //                return result;
        //            }
        //            else
        //            {
        //                result.Succeeded = false;
        //                result.Errors.Add("No contacts found");
        //                result.Errors.Add("لم يتم العثور على جهات اتصال");
        //                return result;
        //            }
        //        }
        //        else
        //        {
        //            result.Succeeded = false;
        //            result.Errors.Add("Unauthorized");
        //            result.Errors.Add("غير مصرح");
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

        public async Task<ApiResponse<bool>> CreatePhase(PhaseCreationModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                CRPhase creationModel = new CRPhase
                {
                    CreationDate = await HelperFunctions.GetEgyptsCurrentLocalTime(),
                    TitleAR = model.TitleAR,
                    TitleEN = model.TitleEN,
                    Status = (int)PhaseStatuses.Active,
                };

                var phaseCreationRes = await unitOfWork.CRPhaseManager.CreateAsync(creationModel);


                if (phaseCreationRes != null)
                {

                    await unitOfWork.SaveChangesAsync();

                    //Create inputs
                    for (int i = 0; i < model.PhaseInputs.Count; i++)
                    {
                        var input = model.PhaseInputs[i];

                        CRPhaseInput inputCreationModel = new CRPhaseInput
                        {
                            PhaseID = phaseCreationRes.ID,
                            TitleAR = input.TitleAR,
                            TitleEN = input.TitleEN,
                            Type = input.Type,
                        };

                        var inputCreationRes = await unitOfWork.CRPhaseInputManager.CreateAsync(inputCreationModel);

                        if (inputCreationModel != null)
                        {
                            if (input.Attributes != null && input.Attributes.Count > 0)
                            {
                                await unitOfWork.SaveChangesAsync();

                                for (int j = 0; j < input.Attributes.Count; j++)
                                {
                                    var option = input.Attributes[j];
                                    CRPhaseInputOption inputOptionCreationModel = new CRPhaseInputOption
                                    {
                                        InputID = inputCreationModel.ID,
                                        TitleAR = option.LabelAR,
                                        TitleEN = option.LabelEN,
                                    };

                                    var optionCreationRes = await unitOfWork.CRPhaseInputOptionManager.CreateAsync(inputOptionCreationModel);

                                    if (optionCreationRes == null)
                                    {
                                        result.Errors.Add("Error adding option");
                                    }
                                }

                            }
                        }
                    }

                    if (result.Errors.Count > 0)
                    {
                        result.Succeeded = false;
                        return result;
                    }
                    else
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        return result;
                    }
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
    }

}


