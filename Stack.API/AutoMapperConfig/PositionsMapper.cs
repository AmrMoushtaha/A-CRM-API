using Stack.Entities.Models.Modules.Materials;
using Stack.DTOs.Models.Modules.Materials.Plants;

using Stack.DTOs.Models.Modules.Employees.Positions;
﻿using AutoMapper;
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

namespace Stack.API.AutoMapperConfig
{
    public class PositionsMapper : AutoMapperProfile
    {
        public PositionsMapper()
        {

           CreateMap<Position, PositionMainViewDTO>()
          .ForMember(dest => dest.Code, opt => opt.MapFrom(e => e.Code))
          .ForMember(dest => dest.OrgUnitNameAR, opt => opt.MapFrom(e => e.OrgUnit.DescriptionAR))
          .ForMember(dest => dest.OrgUnitNameEN, opt => opt.MapFrom(e => e.OrgUnit.DescriptionEN))
          .ForMember(dest => dest.OrgUnitCode, opt => opt.MapFrom(e => e.OrgUnit.Code))
          .ReverseMap();
           CreateMap<Position, PositionDTO>()
          .ReverseMap();

        }

    }

}
