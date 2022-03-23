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

            CreateMap<Activity, ActivityHistoryViewDTO>()
            .ForMember(dest => dest.ActivityTypeNameAR, m => m.MapFrom(d => d.ActivityType.NameAR))
            .ForMember(dest => dest.ActivityTypeNameEN, m => m.MapFrom(d => d.ActivityType.NameEN))
            .ReverseMap();

        }

    }

}
