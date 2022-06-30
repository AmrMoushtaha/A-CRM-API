using AutoMapper;
using Stack.DTOs.Models.Modules.Activities;
using Stack.DTOs.Models.Modules.Teams;
using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Teams;

namespace Stack.API.AutoMapperConfig
{
    public class TeamMapperProfile : Profile
    {
        public TeamMapperProfile()
        {

            CreateMap<Team, TeamSidebarViewModel>()
            .ReverseMap();

        }

    }

}
