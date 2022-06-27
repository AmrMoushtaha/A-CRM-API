using AutoMapper;
using Stack.DTOs.Models.Modules.AreaInterest;
using Stack.DTOs.Requests.Modules.Chat;
using Stack.DTOs.Requests.Modules.Hierarchy;
using Stack.DTOs.Requests.Modules.Interest;
using Stack.DTOs.Requests.Shared;
using Stack.Entities.Models.Modules.Chat;
using Stack.Entities.Models.Modules.Hierarchy;
using Stack.Entities.Models.Modules.Interest;

namespace Stack.API.AutoMapperConfig
{
    public class ChatMapperProfile : Profile
    {
        public ChatMapperProfile()
        {
            //Interest.
            CreateMap<Conversation, ConversationToCreate>()
             .ReverseMap();

            CreateMap<Conversation, ConversationDto>()
             .ReverseMap();

            CreateMap<Message, MessageDto>()
             .ReverseMap();

            CreateMap<UsersConversations, UsersConversationsDto>()
             .ReverseMap();

            CreateMap<Message, AddMsg>()
             .ReverseMap();

            CreateMap<LInterest, LInterestModel>()
            .ForMember(dist => dist.ParentLInterest, opt => opt.Ignore())
            .ForMember(dist => dist.Location, opt => opt.Ignore())
            .ReverseMap();

        }

    }

}
