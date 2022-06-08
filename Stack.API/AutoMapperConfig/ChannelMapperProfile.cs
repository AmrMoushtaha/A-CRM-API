using AutoMapper;
using Stack.DTOs.Requests.Modules.Channel;
using Stack.DTOs.Requests.Modules.Channels;
using Stack.Entities.Models.Modules.Channel;
using Stack.Entities.Models.Modules.Channels;

namespace Stack.API.AutoMapperConfig
{
    public class ChannelMapperProfile : Profile
    {
        public ChannelMapperProfile()
        {

            CreateMap<Channel, ChannelViewModel>()
            .ReverseMap();
            CreateMap<LeadSourceType, LeadSourceTypeViewModel>()
            .ReverseMap();
            CreateMap<LeadSourceName, LeadSourceNameViewModel>()
            .ReverseMap();


        }

    }

}
