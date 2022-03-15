using Stack.Entities.Models.Modules.Materials;
using Stack.DTOs.Models.Modules.Materials.Plants;
using Stack.Entities.Models.Modules.Organizations;
using Stack.DTOs.Models.Modules.Organizations.CostCenters;

namespace Stack.API.AutoMapperConfig
{
    public class CostCentersMapper : AutoMapperProfile
    {
        public CostCentersMapper()
        {

           CreateMap<CostCenter, CostCenterMainViewDTO>()
          .ForMember(dest => dest.CompanyCode, opt => opt.MapFrom(e => e.CompanyCode.Code))   
          .ReverseMap();

        }

    }

}
