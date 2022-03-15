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
using Stack.Entities.Models.Modules.Materials;
using Stack.DTOs.Models.Modules.Materials.MaterialGroups;

namespace Stack.API.AutoMapperConfig
{
    public class MaterialGroupsMapper : AutoMapperProfile
    {
        public MaterialGroupsMapper()
        {

           CreateMap<MaterialGroup, MaterialGroupMainViewDTO>()
          .ReverseMap();

        }

    }

}
