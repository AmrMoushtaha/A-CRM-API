
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Stack.DTOs;
using System.Threading.Tasks;
using Stack.DTOs.Requests.Modules.Activities;
using Stack.Entities.Models.Modules.Activities;
using System.Collections.Generic;
using System.Linq;
using Stack.DTOs.Enums;
using System;
using Stack.DTOs.Models.Shared;

namespace Stack.ServiceLayer.Modules.Activities
{
    public class SectionsService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private static readonly HttpClient client = new HttpClient();

        public SectionsService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;

        }

        public async Task<ApiResponse<bool>> CreateActivityTypeSection(CreateActivityTypeSectionModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var duplicateSectionResult = await unitOfWork.SectionsManager.GetAsync(a => a.ActivityTypeID == model.ActivityTypeID && a.NameAR == model.NameAR || a.NameEN == model.NameEN);

                List<Section> duplicateSectionList = duplicateSectionResult.ToList();

                if (duplicateSectionList.Count > 0)
                {
                    result.Succeeded = false;
                    result.Data = false;
                    result.Errors.Add("Failed to create the new section, an section with a duplicate name arlready exists !");
                    return result;
                }


                Section newSection = new Section();

                newSection.NameAR = model.NameAR;

                newSection.NameEN = model.NameEN;

                newSection.ActivityTypeID = model.ActivityTypeID;   

                newSection.Order = model.Order;

                newSection.HasCreateInterest = model.HasCreateInterest;

                newSection.HasCreateRequest = model.HasCreateRequest;

                newSection.HasCreateResale = model.HasCreateResale;

                var createSectionResult = await unitOfWork.SectionsManager.CreateAsync(newSection);

                await unitOfWork.SaveChangesAsync();

                if (createSectionResult != null)
                {

                    result.Succeeded = true;

                    result.Data = true;

                    return result;

                }
                else
                {

                    result.Succeeded = false;

                    result.Data = false;

                    result.ErrorType = ErrorType.SystemError;

                    result.Errors.Add("Failed to create a section !");

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

        public async Task<ApiResponse<bool>> CreateSectionQuestion(CreateSectionQuestionModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var duplicateSectionQuestionsResult = await unitOfWork.SectionQuestionsManager.GetAsync(a => a.SectionID == model.SectionID && a.DescriptionAR == model.DescriptionAR || a.DescriptionEN == model.DescriptionEN);

                List<SectionQuestion> duplicateQuestionList = duplicateSectionQuestionsResult.ToList();

                if (duplicateQuestionList.Count > 0)
                {
                    result.Succeeded = false;
                    result.Data = false;
                    result.Errors.Add("Failed to create the new question, an question with a duplicate name already exists for this section !");
                    return result;
                }


                SectionQuestion newSectionQuestion = new SectionQuestion();

                newSectionQuestion.DescriptionEN = model.DescriptionEN;

                newSectionQuestion.DescriptionAR = model.DescriptionAR;

                newSectionQuestion.Type  = model.Type;

                newSectionQuestion.SectionID = model.SectionID;

                newSectionQuestion.isRequired = model.IsRequired;

                newSectionQuestion.IsDecisional = model.IsDecisional;

                var createSectionQuestionResult = await unitOfWork.SectionQuestionsManager.CreateAsync(newSectionQuestion);

                await unitOfWork.SaveChangesAsync();

                if (createSectionQuestionResult != null)
                {

                    result.Succeeded = true;

                    result.Data = true;

                    return result;

                }
                else
                {

                    result.Succeeded = false;

                    result.Data = false;

                    result.ErrorType = ErrorType.SystemError;

                    result.Errors.Add("Failed to create the new section question !");

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

        public async Task<ApiResponse<bool>> CreateSectionQuestionOption(CreateSectionQuestionOptionModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var duplicateSectionQuestionOptionResult = await unitOfWork.SectionQuestionOptionsManager.GetAsync(a => a.QuestionID == model.QuestionID && a.ValueAR == model.ValueAR || a.ValueEN == model.ValueEN);

                List<SectionQuestionOption> duplicateQuestionList = duplicateSectionQuestionOptionResult.ToList();

                if (duplicateQuestionList.Count > 0)
                {
                    result.Succeeded = false;
                    result.Data = false;
                    result.Errors.Add("Failed to create the new section question option, an option with a similar value arlready exists for this question !");
                    return result;
                }


                SectionQuestionOption newSectionQuestionOption = new SectionQuestionOption();

                newSectionQuestionOption.ValueAR = model.ValueAR;

                newSectionQuestionOption.ValueEN = model.ValueEN;

                newSectionQuestionOption.RoutesTo = model.RoutesTo; // Routes to would be equal to 'Submit' if the question routes to the submit section .

                newSectionQuestionOption.QuestionID = model.QuestionID;

                var CreateSectionQuestionOptionResult = await unitOfWork.SectionQuestionOptionsManager.CreateAsync(newSectionQuestionOption);

                await unitOfWork.SaveChangesAsync();

                if (CreateSectionQuestionOptionResult != null)
                {

                    result.Succeeded = true;

                    result.Data = true;

                    return result;

                }
                else
                {

                    result.Succeeded = false;

                    result.Data = false;

                    result.ErrorType = ErrorType.SystemError;

                    result.Errors.Add("Failed to create the new question option !");

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

        public async Task<ApiResponse<bool>> DeleteSection(DeletionModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var sectionsResult = await unitOfWork.SectionsManager.GetAsync(a => a.ID == model.ID);

                Section SectionToDelete = sectionsResult.ToList().FirstOrDefault();

                if (SectionToDelete != null)
                {

                    var deleteSectionResult = await unitOfWork.SectionsManager.RemoveAsync(SectionToDelete);

                    if (deleteSectionResult == true)
                    {

                        await unitOfWork.SaveChangesAsync();

                        result.Succeeded = true;
                        result.Data = true;
                        return result;

                    }
                    else

                    {

                        result.Succeeded = false;
                        result.Data = false;
                        result.Errors.Add("Failed to delete activity type section, Please try again !");
                        result.ErrorCode = ErrorCode.A500;
                        return result;

                    }

                }
                else
                {

                    result.Succeeded = false;
                    result.Data = false;
                    result.Errors.Add("Failed to  delete activity type section, Please try again !");
                    result.ErrorCode = ErrorCode.A500;
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

        public async Task<ApiResponse<bool>> DeleteSectionQuestion(DeletionModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var sectionQuestionResult = await unitOfWork.SectionQuestionsManager.GetAsync(a => a.ID == model.ID);

                SectionQuestion sectionQuestionToDelete = sectionQuestionResult.ToList().FirstOrDefault();

                if (sectionQuestionToDelete != null)
                {

                    var deleteSectionQuestionResult = await unitOfWork.SectionQuestionsManager.RemoveAsync(sectionQuestionToDelete);

                    if (deleteSectionQuestionResult == true)
                    {

                        await unitOfWork.SaveChangesAsync();

                        result.Succeeded = true;
                        result.Data = true;
                        return result;

                    }
                    else

                    {

                        result.Succeeded = false;
                        result.Data = false;
                        result.Errors.Add("Failed to delete section question , Please try again !");
                        result.ErrorCode = ErrorCode.A500;
                        return result;

                    }

                }
                else
                {

                    result.Succeeded = false;
                    result.Data = false;
                    result.Errors.Add("Failed to  delete section question , Please try again !");
                    result.ErrorCode = ErrorCode.A500;
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

        public async Task<ApiResponse<bool>> DeleteSectionQuestionOption(DeletionModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var sectionQuestionOptionsResult = await unitOfWork.SectionQuestionOptionsManager.GetAsync(a => a.ID == model.ID);

                SectionQuestionOption sectionQuestionOptionToDelete = sectionQuestionOptionsResult.ToList().FirstOrDefault();

                if (sectionQuestionOptionToDelete != null)
                {

                    var deleteSectionQuestionOptionResult = await unitOfWork.SectionQuestionOptionsManager.RemoveAsync(sectionQuestionOptionToDelete);

                    if (deleteSectionQuestionOptionResult == true)
                    {

                        await unitOfWork.SaveChangesAsync();

                        result.Succeeded = true;
                        result.Data = true;
                        return result;

                    }
                    else

                    {

                        result.Succeeded = false;
                        result.Data = false;
                        result.Errors.Add("Failed to delete section question option , Please try again !");
                        result.ErrorCode = ErrorCode.A500;
                        return result;

                    }

                }
                else
                {

                    result.Succeeded = false;
                    result.Data = false;
                    result.Errors.Add("Failed to  delete section question option , Please try again !");
                    result.ErrorCode = ErrorCode.A500;
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


