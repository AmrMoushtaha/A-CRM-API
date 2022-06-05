using AutoMapper;
using Stack.DTOs.Models.Modules.Areas;
using Stack.DTOs.Requests.Modules.AreaInterest;
using Stack.Entities.Models.Modules.Areas;

namespace Stack.API.AutoMapperConfig
{
    public class LocationMapperProfile : Profile
    {
        public LocationMapperProfile()
        {
            CreateMap<Location, LocationModel>()
         .ForMember(dist => dist.ParentLocation, opt => opt.Ignore())
         .ReverseMap();
            CreateMap<Location, LocationToAdd>()
            .ReverseMap();
            CreateMap<Location, LocationToEdit>()
            .ReverseMap();


        }

    }

}
