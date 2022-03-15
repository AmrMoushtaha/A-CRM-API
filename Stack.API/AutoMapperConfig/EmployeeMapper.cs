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

namespace Stack.API.AutoMapperConfig
{
    public class EmployeeMapper : AutoMapperProfile
    {
        public EmployeeMapper()
        {
            CreateMap<Employee, EmployeeDTO>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(e => e.ApplicationUser.UserName))
            .ForMember(dest => dest.NameEN, opt => opt.MapFrom(e => e.ApplicationUser.NameEN))
            .ForMember(dest => dest.NameAR, opt => opt.MapFrom(e => e.ApplicationUser.NameAR))
            .ReverseMap();

            CreateMap<Employee, EmployeeMasterDataTableViewModel>()
            .ForMember(dest => dest.CompanyCode, opt => opt.MapFrom(e => e.CostCenter.CompanyCode.Code))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(e => e.ApplicationUser.UserName))
            .ForMember(dest => dest.NameEN, opt => opt.MapFrom(e => e.ApplicationUser.NameEN))
            .ForMember(dest => dest.NameAR, opt => opt.MapFrom(e => e.ApplicationUser.NameAR))
            .ReverseMap();

            CreateMap<ApplicationUser, Employee_MasterData>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(e => e.UserName))
            .ForMember(dest => dest.NameAR, opt => opt.MapFrom(e => e.NameAR))
            .ForMember(dest => dest.NameEN, opt => opt.MapFrom(e => e.NameEN))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(e => e.Gender))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(e => e.Email))
            .ReverseMap();

            CreateMap<Employee, Employee_MasterData>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(e => e.StartDate))
            .ForMember(dest => dest.HiringDate, opt => opt.MapFrom(e => e.HiringDate))
            .ReverseMap();

            CreateMap<EmployeeCreation, Employee>().ReverseMap();

            CreateMap<ApplicationUser, EmployeeCreation_ApplicationUser>().ReverseMap();

            CreateMap<EmployeeAddress, Address_Creation>().ReverseMap();

            CreateMap<Employee_Position, Employee_OrgUnits>()
            .ForMember(dest => dest.PositionDescriptionEN, opt => opt.MapFrom(e => e.Position.DescriptionEN))
            .ForMember(dest => dest.PositionDescriptionAR, opt => opt.MapFrom(e => e.Position.DescriptionAR))
            .ForMember(dest => dest.OrgUnitDescriptionEN, opt => opt.MapFrom(e => e.Position.OrgUnit.DescriptionEN))
            .ForMember(dest => dest.OrgUnitDescriptionAR, opt => opt.MapFrom(e => e.Position.OrgUnit.DescriptionAR))
            .ForMember(dest => dest.PositionId, opt => opt.MapFrom(e => e.Position.ID))
            .ReverseMap();


            CreateMap<EmployeeAddress, Employee_Address>()
            .ReverseMap();

            CreateMap<Employee_PhoneNumber, Employee_PhoneNumberDTO>()
            .ReverseMap();

            //CreateMap<EmployeeCreationModel, Employee>()

            //Master Data
            //.ForMember(dest => dest.StartDate, opt => opt.MapFrom(e => e.MasterData.StartDate))
            ////.ForMember(dest => dest.ApplicationUser.UserName, opt => opt.MapFrom(e => e.MasterData.UserName))
            ////.ForMember(dest => dest.ApplicationUser.NameEN, opt => opt.MapFrom(e => e.MasterData.NameEN))
            ////.ForMember(dest => dest.ApplicationUser.NameAR, opt => opt.MapFrom(e => e.MasterData.NameAR))
            ////.ForMember(dest => dest.ApplicationUser.Gender, opt => opt.MapFrom(e => e.MasterData.Gender))
            ////.ForMember(dest => dest.ApplicationUser.Email, opt => opt.MapFrom(e => e.MasterData.Email))

            //////Communication
            ////.ForMember(dest => dest.ApplicationUser.PhoneNumber, opt => opt.MapFrom(e => e.Communication.PhoneNumbers.FirstOrDefault()))
            //.ForMember(dest => dest.Addresses, opt => opt.MapFrom(e => e.Communication.Addresses))
            ////.ForMember(dest => dest.PhoneNumbers, opt => opt.MapFrom(e => e.Communication.PhoneNumbers))

            ////Company Code
            //.ForMember(dest => dest.CostCenter.CompanyCodeID, opt => opt.MapFrom(e => e.CompanyCode.CodeID));

        }
    }

}
