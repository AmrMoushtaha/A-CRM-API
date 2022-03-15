using AutoMapper;
using Stack.DTOs.Models;
using Stack.DTOs.Models.Modules.Auth;
using Stack.DTOs.Models.Modules.Employees;
using Stack.DTOs.Models.Modules.Organizations.CompanyCodes;
using Stack.DTOs.Models.Modules.Organizations.CostCenters;
using Stack.DTOs.Models.Modules.Organizations.OrgUnits;
using Stack.DTOs.Models.Modules.Organizations.PurchasingGroups;
using Stack.DTOs.Requests;
using Stack.Entities.Models;
using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.Employees;
using Stack.Entities.Models.Modules.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stack.API.AutoMapperConfig
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<ApplicationUser, ApplicationUserDTO>()
            .ReverseMap();

            CreateMap<CompanyCode, CompanyCodeDTO>()
            .ReverseMap();

            CreateMap<CostCenter, CostCenterDTO>()
            .ReverseMap();

            CreateMap<OrgUnit, OrgUnitDTO>()
            .ReverseMap();

            CreateMap<PurchasingGroup, PurchasingGroupDTO>()
            .ReverseMap();

        }

    }

}
