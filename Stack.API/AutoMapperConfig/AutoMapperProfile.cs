using AutoMapper;
using Stack.DTOs.Models.Modules.AreaInterest;
using Stack.DTOs.Models.Modules.Areas;
using Stack.DTOs.Models.Modules.Auth;
using Stack.DTOs.Models.Modules.CustomerStage;
using Stack.DTOs.Models.Modules.Pool;
using Stack.DTOs.Requests.Modules.AreaInterest;
using Stack.DTOs.Requests.Modules.CustomerStage;
using Stack.DTOs.Requests.Modules.Pool;
using Stack.Entities.Models.Modules.AreaInterest;
using Stack.Entities.Models.Modules.Areas;
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

            CreateMap<ApplicationRole, ApplicationRoleDTO>()
            .ReverseMap();

            CreateMap<Pool_User, PoolSidebarViewModel>()
            .ForMember(dist => dist.NameEN, opt => opt.MapFrom(t => t.Pool.NameEN))
            .ForMember(dist => dist.NameAR, opt => opt.MapFrom(t => t.Pool.NameAR))
            .ReverseMap();

            CreateMap<Pool_User, PoolAssignedUsersModel>()
            .ForMember(dist => dist.UserID, opt => opt.MapFrom(t => t.UserID))
            .ForMember(dist => dist.NameEN, opt => opt.MapFrom(t => t.User.FirstName + " " + t.User.LastName))
            .ForMember(dist => dist.NameAR, opt => opt.MapFrom(t => t.User.FirstName + " " + t.User.LastName))
            .ReverseMap();

            CreateMap<Pool_User, PoolAssignedUserCapacityModel>()
            .ForMember(dist => dist.FullName, opt => opt.MapFrom(t => t.User.FirstName + " " + t.User.LastName))
            .ForMember(dist => dist.Capacity, opt => opt.MapFrom(t => t.Capacity))
            .ReverseMap();

            CreateMap<Pool, PoolConfigurationModel>()
            .ForMember(dist => dist.PoolID, opt => opt.MapFrom(t => t.ID))
            .ReverseMap();

            //Contact
            CreateMap<Contact, ContactViewModel>()
           .ForMember(dist => dist.ContactPhoneNumbers, opt => opt.MapFrom(t => t.PhoneNumbers))
           .ForMember(dist => dist.AssignedUserName, opt => opt.MapFrom(t => t.AssignedUser.FirstName + " " + t.AssignedUser.LastName))
           .ForMember(dist => dist.Comments, opt => opt.MapFrom(t => t.Comments))
           .ForMember(dist => dist.StatusEN, opt => opt.MapFrom(t => t.Status.EN))
           .ForMember(dist => dist.StatusAR, opt => opt.MapFrom(t => t.Status.AR))
           .ForMember(dist => dist.StatusID, opt => opt.MapFrom(t => t.Status.ID))
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

            CreateMap<LInterest_InterestAttribute, LInterest_InterestAttributeModel>()
           .ForMember(dist => dist.LInterest, opt => opt.Ignore())
           .ForMember(dist => dist.InterestAttribute, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<Location, LocationModel>()
            .ForMember(dist => dist.ParentLocation, opt => opt.Ignore())
            .ReverseMap();
            CreateMap<Location, LocationToAdd>()
            .ReverseMap();
            CreateMap<Location, LocationToEdit>()
            .ReverseMap();

            CreateMap<LInterest_LInterestInput, LInterestInputToAdd>()
           .ReverseMap();
            CreateMap<LInterest_LInterestInput, LInterestInputToEdit>()
           .ReverseMap();
            CreateMap<LInterest_InterestAttribute, InterestAttributeToAdd>()
           .ReverseMap();

        }

    }

}
