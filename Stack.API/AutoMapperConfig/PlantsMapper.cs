using Stack.Entities.Models.Modules.Materials;
using Stack.DTOs.Models.Modules.Materials.Plants;

namespace Stack.API.AutoMapperConfig
{
    public class PlantsMapper : AutoMapperProfile
    {
        public PlantsMapper()
        {

           CreateMap<Plant, PlantMainViewDTO>()
          .ForMember(dest => dest.CompanyCodeDescriptionAR, opt => opt.MapFrom(e => e.CompanyCode.DescriptionAR))
          .ForMember(dest => dest.CompanyCodeDescriptionEN, opt => opt.MapFrom(e => e.CompanyCode.DescriptionEN))
          .ForMember(dest => dest.CompanyCode, opt => opt.MapFrom(e => e.CompanyCode.Code))
          .ReverseMap();

        }

    }

}
