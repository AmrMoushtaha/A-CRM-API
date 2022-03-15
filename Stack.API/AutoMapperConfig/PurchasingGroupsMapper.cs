using AutoMapper;
using Stack.DTOs.Models;
using Stack.DTOs.Models.Modules.Auth;
using Stack.DTOs.Models.Modules.Employees;
using Stack.DTOs.Requests;
using Stack.Entities.Models;
using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stack.DTOs.Requests.Modules.Employees;
using Stack.Entities.Models.Modules.Organizations;
using Stack.DTOs.Models.Modules.Organizations.OrgUnits;
using Stack.DTOs.Models.Modules.Organizations.PurchasingGroups;

namespace Stack.API.AutoMapperConfig
{
    public class PurchasingGroupsMapper : AutoMapperProfile
    {
        public PurchasingGroupsMapper()
        {

            CreateMap<PurchasingGroup, PurchasingGroupMainViewDTO>()
           .ForMember(dest => dest.PlantNameAR, opt => opt.MapFrom(e => e.Plant.DescriptionAR))
           .ForMember(dest => dest.PlantNameEN, opt => opt.MapFrom(e => e.Plant.DescriptionEN))
           .ForMember(dest => dest.PlantCode, opt => opt.MapFrom(e => e.Plant.Code))
           .ForMember(dest => dest.PlantID, opt => opt.MapFrom(e => e.Plant.ID))
           .ReverseMap();

        }

    }

}
