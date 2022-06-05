using AutoMapper;
using Stack.DTOs.Models.Modules.Auth;
using Stack.Entities.Models.Modules.Auth;

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
        }

    }

}
