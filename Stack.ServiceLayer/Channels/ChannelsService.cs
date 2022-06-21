
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using Stack.DTOs;
using Stack.DTOs.Enums;
using Stack.DTOs.Requests.Modules.Channel;
using Stack.DTOs.Requests.Modules.Channels;
using Stack.DTOs.Requests.Modules.CustomerStage;
using Stack.Entities.Enums.Modules.Pool;
using Stack.Entities.Models.Modules.Channel;
using Stack.Entities.Models.Modules.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Stack.ServiceLayer.Modules.Channels
{
    public class ChannelsService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private static readonly HttpClient client = new HttpClient();

        public ChannelsService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<ChannelViewModel>>> GetAllChannels()
        {
            ApiResponse<List<ChannelViewModel>> result = new ApiResponse<List<ChannelViewModel>>();
            try
            {
                var channelsQ = await unitOfWork.ChannelManager.GetAsync();
                List<Channel> channels = channelsQ.ToList();
                if (channels != null && channels.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<ChannelViewModel>>(channels);
                    return result;
                }

                result.Errors.Add("No channels found!");
                result.Errors.Add("No channels found!");
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

        public async Task<ApiResponse<List<LeadSourceTypeViewModel>>> GetAllLeadSourceTypes()
        {
            ApiResponse<List<LeadSourceTypeViewModel>> result = new ApiResponse<List<LeadSourceTypeViewModel>>();
            try
            {
                var channelsQ = await unitOfWork.LeadSourceTypeManager.GetAsync();
                List<LeadSourceType> channels = channelsQ.ToList();
                if (channels != null && channels.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<LeadSourceTypeViewModel>>(channels);
                    return result;
                }

                result.Errors.Add("No channels found!");
                result.Errors.Add("No channels found!");
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

        public async Task<ApiResponse<List<LeadSourceNameViewModel>>> GetAllLeadSourceNames()
        {
            ApiResponse<List<LeadSourceNameViewModel>> result = new ApiResponse<List<LeadSourceNameViewModel>>();
            try
            {
                var channelsQ = await unitOfWork.LeadSourceNameManager.GetAsync();
                List<LeadSourceName> channels = channelsQ.ToList();
                if (channels != null && channels.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<LeadSourceNameViewModel>>(channels);
                    return result;
                }

                result.Errors.Add("No channels found!");
                result.Errors.Add("No channels found!");
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

        public async Task<ApiResponse<ChannelViewModel>> GetChannelByID(long channelID)
        {
            ApiResponse<ChannelViewModel> result = new ApiResponse<ChannelViewModel>();
            try
            {
                var channelsQ = await unitOfWork.ChannelManager.GetAsync(t => t.ID == channelID);
                Channel channel = channelsQ.FirstOrDefault();
                if (channel != null)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<ChannelViewModel>(channel);
                    return result;
                }

                result.Errors.Add("No channels found!");
                result.Errors.Add("No channels found!");
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

        public async Task<ApiResponse<LeadSourceTypeViewModel>> GetLeadSourceTypeByID(long channelID)
        {
            ApiResponse<LeadSourceTypeViewModel> result = new ApiResponse<LeadSourceTypeViewModel>();
            try
            {
                var channelsQ = await unitOfWork.LeadSourceTypeManager.GetAsync(t => t.ID == channelID);
                LeadSourceType channel = channelsQ.FirstOrDefault();
                if (channel != null)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<LeadSourceTypeViewModel>(channel);
                    return result;
                }

                result.Errors.Add("No channels found!");
                result.Errors.Add("No channels found!");
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

        public async Task<ApiResponse<LeadSourceNameViewModel>> GetLeadSourceNameByID(long channelID)
        {
            ApiResponse<LeadSourceNameViewModel> result = new ApiResponse<LeadSourceNameViewModel>();
            try
            {
                var channelsQ = await unitOfWork.LeadSourceNameManager.GetAsync(t => t.ID == channelID);
                LeadSourceName channel = channelsQ.FirstOrDefault();
                if (channel != null)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<LeadSourceNameViewModel>(channel);
                    return result;
                }

                result.Errors.Add("No channels found!");
                result.Errors.Add("No channels found!");
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

        public async Task<ApiResponse<List<LeadSourceTypeViewModel>>> GetLeadSourceTypesByChannelID(long channelID)
        {
            ApiResponse<List<LeadSourceTypeViewModel>> result = new ApiResponse<List<LeadSourceTypeViewModel>>();
            try
            {
                var channelsQ = await unitOfWork.LeadSourceTypeManager.GetAsync(t => t.ChannelID == channelID);
                List<LeadSourceType> channels = channelsQ.ToList();
                if (channels != null && channels.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<LeadSourceTypeViewModel>>(channels);
                    return result;
                }

                result.Errors.Add("No channels found!");
                result.Errors.Add("No channels found!");
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

        public async Task<ApiResponse<List<LeadSourceTypeViewModel>>> GetLeadSourceNamesByChannelID(long channelID)
        {
            ApiResponse<List<LeadSourceTypeViewModel>> result = new ApiResponse<List<LeadSourceTypeViewModel>>();
            try
            {
                var channelsQ = await unitOfWork.LeadSourceNameManager.GetAsync(t => t.LeadSourceTypeID == channelID);
                List<LeadSourceName> channels = channelsQ.ToList();
                if (channels != null && channels.Count != 0)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<LeadSourceTypeViewModel>>(channels);
                    return result;
                }

                result.Errors.Add("No channels found!");
                result.Errors.Add("No channels found!");
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


        public async Task<ApiResponse<bool>> CreateChannel(ChannelCreationModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                Channel channel = new Channel
                {
                    Status = (int)ChannelStatuses.Active,
                    TitleEN = model.TitleEN,
                    TitleAR = model.TitleAR,
                    DescriptionEN = model.DescriptionEN,
                    DescriptionAR = model.DescriptionAR,
                };

                var creationRes = await unitOfWork.ChannelManager.CreateAsync(channel);

                if (creationRes != null)
                {
                    await unitOfWork.SaveChangesAsync();
                    result.Succeeded = true;
                    return result;
                }


                result.Errors.Add("Error creating channel!");
                result.Errors.Add("Error creating channel!");
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

        public async Task<ApiResponse<bool>> CreateLeadType(ChannelCreationModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                LeadSourceType channelChild = new LeadSourceType
                {
                    Status = (int)ChannelStatuses.Active,
                    TitleEN = model.TitleEN,
                    TitleAR = model.TitleAR,
                    DescriptionEN = model.DescriptionEN,
                    DescriptionAR = model.DescriptionAR,
                    ChannelID = model.parentID.Value
                };

                var creationRes = await unitOfWork.LeadSourceTypeManager.CreateAsync(channelChild);

                if (creationRes != null)
                {
                    await unitOfWork.SaveChangesAsync();
                    result.Succeeded = true;
                    return result;
                }


                result.Errors.Add("Error creating lead source type!");
                result.Errors.Add("Error creating lead source type!");
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

        public async Task<ApiResponse<bool>> CreateLeadName(ChannelCreationModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                LeadSourceName channelChild = new LeadSourceName
                {
                    Status = (int)ChannelStatuses.Active,
                    TitleEN = model.TitleEN,
                    TitleAR = model.TitleAR,
                    DescriptionEN = model.DescriptionEN,
                    DescriptionAR = model.DescriptionAR,
                    LeadSourceTypeID = model.parentID.Value
                };

                var creationRes = await unitOfWork.LeadSourceNameManager.CreateAsync(channelChild);

                if (creationRes != null)
                {
                    await unitOfWork.SaveChangesAsync();
                    result.Succeeded = true;
                    return result;
                }


                result.Errors.Add("Error creating lead source name!");
                result.Errors.Add("Error creating lead source name!");
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



        public async Task<ApiResponse<bool>> UpdateChannel(ChannelCreationModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var channelQ = await unitOfWork.ChannelManager.GetAsync(t => t.ID == model.parentID.Value);
                var channel = channelQ.FirstOrDefault();

                if (channel != null)
                {

                    channel.TitleAR = model.TitleAR;
                    channel.TitleEN = model.TitleEN;
                    channel.DescriptionAR = model.DescriptionAR;
                    channel.DescriptionEN = model.DescriptionEN;

                    var updateRes = await unitOfWork.ChannelManager.UpdateAsync(channel);

                    if (updateRes)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        return result;
                    }
                    else
                    {
                        result.Errors.Add("Error updating channel!");
                        result.Errors.Add("Error updating channel!");
                        result.Succeeded = false;
                        return result;
                    }
                }
                else
                {
                    result.Errors.Add("Error updating channel!");
                    result.Errors.Add("Error updating channel!");
                    result.Succeeded = false;
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

        public async Task<ApiResponse<bool>> UpdateLeadSourceType(ChannelCreationModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var channelQ = await unitOfWork.LeadSourceTypeManager.GetAsync(t => t.ID == model.parentID.Value);
                var channel = channelQ.FirstOrDefault();

                if (channel != null)
                {

                    channel.TitleAR = model.TitleAR;
                    channel.TitleEN = model.TitleEN;
                    channel.DescriptionAR = model.DescriptionAR;
                    channel.DescriptionEN = model.DescriptionEN;

                    var updateRes = await unitOfWork.LeadSourceTypeManager.UpdateAsync(channel);

                    if (updateRes)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        return result;
                    }
                    else
                    {
                        result.Errors.Add("Error updating channel!");
                        result.Errors.Add("Error updating channel!");
                        result.Succeeded = false;
                        return result;
                    }
                }
                else
                {
                    result.Errors.Add("Error updating channel!");
                    result.Errors.Add("Error updating channel!");
                    result.Succeeded = false;
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

        public async Task<ApiResponse<bool>> UpdateLeadSourceName(ChannelCreationModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var channelQ = await unitOfWork.LeadSourceNameManager.GetAsync(t => t.ID == model.parentID.Value);
                var channel = channelQ.FirstOrDefault();

                if (channel != null)
                {

                    channel.TitleAR = model.TitleAR;
                    channel.TitleEN = model.TitleEN;
                    channel.DescriptionAR = model.DescriptionAR;
                    channel.DescriptionEN = model.DescriptionEN;

                    var updateRes = await unitOfWork.LeadSourceNameManager.UpdateAsync(channel);

                    if (updateRes)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        return result;
                    }
                    else
                    {
                        result.Errors.Add("Error updating channel!");
                        result.Errors.Add("Error updating channel!");
                        result.Succeeded = false;
                        return result;
                    }
                }
                else
                {
                    result.Errors.Add("Error updating channel!");
                    result.Errors.Add("Error updating channel!");
                    result.Succeeded = false;
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


