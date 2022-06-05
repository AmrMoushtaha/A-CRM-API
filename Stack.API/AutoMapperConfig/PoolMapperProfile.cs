using AutoMapper;
using Stack.DTOs.Models.Modules.AreaInterest;
using Stack.DTOs.Models.Modules.Areas;
using Stack.DTOs.Models.Modules.Auth;
using Stack.DTOs.Models.Modules.CR;
using Stack.DTOs.Models.Modules.CustomerStage;
using Stack.DTOs.Models.Modules.Pool;
using Stack.DTOs.Requests.Modules.AreaInterest;
using Stack.DTOs.Requests.Modules.Channel;
using Stack.DTOs.Requests.Modules.Channels;
using Stack.DTOs.Requests.Modules.CustomerStage;
using Stack.DTOs.Requests.Modules.Hierarchy;
using Stack.DTOs.Requests.Modules.Interest;
using Stack.Entities.Enums.Modules.Pool;
using Stack.Entities.Models.Modules.Areas;
using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.Channel;
using Stack.Entities.Models.Modules.Channels;
using Stack.Entities.Models.Modules.CR;
using Stack.Entities.Models.Modules.CustomerStage;
using Stack.Entities.Models.Modules.Hierarchy;
using Stack.Entities.Models.Modules.Interest;
using System.Linq;

namespace Stack.API.AutoMapperConfig
{
    public class PoolMapperProfile : Profile
    {
        public PoolMapperProfile()
        {

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
            .ForMember(dist => dist.UsersCount, opt => opt.MapFrom(t => t.Pool_Users.Where(a => a.IsAdmin == false).Count()))
            .ForMember(dist => dist.AdminsCount, opt => opt.MapFrom(t => t.Pool_Users.Where(a => a.IsAdmin == true).Count()))
            .ForMember(dist => dist.RequestsCount, opt => opt.MapFrom(t => t.Requests.Where(a => a.Status == (int)PoolRequestStatuses.Pending).Count()))
            .ReverseMap();

            CreateMap<PoolRequest, PoolRequestModel>()
            .ForMember(dist => dist.PoolID, opt => opt.MapFrom(t => t.ID))
            .ForMember(dist => dist.PoolNameAR, opt => opt.MapFrom(t => t.Pool.NameAR))
            .ForMember(dist => dist.PoolNameEN, opt => opt.MapFrom(t => t.Pool.NameEN))
            .ReverseMap();

        }

    }

}
