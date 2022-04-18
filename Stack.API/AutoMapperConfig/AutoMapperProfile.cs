using AutoMapper;
using Stack.DTOs.Models.Modules.AreaInterest;
using Stack.DTOs.Models.Modules.Areas;
using Stack.DTOs.Models.Modules.Auth;
using Stack.DTOs.Models.Modules.CustomerStage;
using Stack.DTOs.Models.Modules.Pool;
using Stack.DTOs.Requests.Modules.AreaInterest;
using Stack.DTOs.Requests.Modules.CustomerStage;
using Stack.DTOs.Requests.Modules.Hierarchy;
using Stack.DTOs.Requests.Modules.Interest;
using Stack.Entities.Models.Modules.Areas;
using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.CustomerStage;
using Stack.Entities.Models.Modules.Hierarchy;
using Stack.Entities.Models.Modules.Interest;

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
           .ForMember(dist => dist.StatusEN, opt => opt.MapFrom(t => t.Status.EN))
           .ForMember(dist => dist.StatusAR, opt => opt.MapFrom(t => t.Status.AR))
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

            CreateMap<ContactStatus, ContactStatusViewModel>()
           .ReverseMap();


            //Interest.
            CreateMap<LInterest, LInterestToAdd>()

             .ReverseMap();

            CreateMap<LInterest, LInterestModel>()
            .ForMember(dist => dist.ParentLInterest, opt => opt.Ignore())
            .ForMember(dist => dist.Location, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<LInterestInput, LInterestInputModel>()
            .ReverseMap();

            CreateMap<LInterestInput, InputToAdd>()
            .ReverseMap();
            CreateMap<LInterestInput, InputToEdit>()
            .ReverseMap();

            CreateMap<Level, LevelToAdd>() 
            .ReverseMap();
            CreateMap<Level, LevelToEdit>()
            .ReverseMap();

            CreateMap<LSection, SectionToAdd>()
            .ReverseMap();
            CreateMap<LSection, SectionToEdit>()
            .ReverseMap();


            CreateMap<Input, InputToAdd>()
            .ReverseMap();
            CreateMap<Input, InputToEdit>()
            .ReverseMap();

            CreateMap<LAttribute, AttributeToAdd>()
            .ReverseMap();
            CreateMap<LAttribute, AttributeToEdit>()
            .ReverseMap();

            CreateMap<Location, LocationModel>()
            .ForMember(dist => dist.ParentLocation, opt => opt.Ignore())
            .ReverseMap();
            CreateMap<Location, LocationToAdd>()
            .ReverseMap();
            CreateMap<Location, LocationToEdit>()
            .ReverseMap();

            CreateMap<Input, InputToAdd>()
           .ReverseMap();
            CreateMap<Input, InputToEdit>()
           .ReverseMap();


        }

    }

}
