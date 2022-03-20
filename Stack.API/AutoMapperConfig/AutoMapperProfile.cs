using AutoMapper;
using Stack.DTOs.Models.Modules.Auth;
using Stack.DTOs.Models.Modules.CustomerStage;
using Stack.DTOs.Models.Modules.Pool;
using Stack.DTOs.Requests.Modules.CustomerStage;
using Stack.DTOs.Requests.Modules.Pool;
using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.CustomerStage;

namespace Stack.API.AutoMapperConfig
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDTO>()
            .ReverseMap();

            CreateMap<Pool_Users, PoolSidebarViewModel>()
            .ForMember(dist => dist.NameEN, opt => opt.MapFrom(t => t.Pool.NameEN))
            .ForMember(dist => dist.NameAR, opt => opt.MapFrom(t => t.Pool.NameAR))
            .ReverseMap();

            CreateMap<Pool_Admin, PoolSidebarViewModel>()
           .ForMember(dist => dist.NameEN, opt => opt.MapFrom(t => t.Pool.NameEN))
           .ForMember(dist => dist.NameAR, opt => opt.MapFrom(t => t.Pool.NameAR))
           .ReverseMap();



            //Contact
            CreateMap<Contact, ContactViewModel>()
           .ForMember(dist => dist.ContactPhoneNumbers, opt => opt.MapFrom(t => t.PhoneNumbers))
           .ForMember(dist => dist.AssignedUserName, opt => opt.MapFrom(t => t.AssignedUser.FirstName + " " + t.AssignedUser.LastName))
           .ForMember(dist => dist.Comments, opt => opt.MapFrom(t => t.Comments))
           .ForMember(dist => dist.Status, opt => opt.MapFrom(t => t.Status.Status))
           .ForMember(dist => dist.Tags, opt => opt.MapFrom(t => t.Tags))
           .ReverseMap();

            CreateMap<ContactPhoneNumber, ContactPhoneNumberDTO>()
           .ReverseMap();

            CreateMap<ContactComment, ContactCommentDTO>()
           .ReverseMap();

            CreateMap<Contact_Tag, ContactTagDTO>()
           .ReverseMap();

            
            CreateMap<Tag, TagsDTO>()
           .ReverseMap();


        }

    }

}
