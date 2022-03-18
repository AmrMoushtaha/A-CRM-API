using AutoMapper;
using Stack.DTOs.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Activities;

namespace Stack.API.AutoMapperConfig
{
    public class ActivitiesMapperProfile : Profile
    {
        public ActivitiesMapperProfile()
        {
            CreateMap<ActivityType, ActivityTypeMainViewDTO>()
            .ReverseMap();
        }

    }

}
