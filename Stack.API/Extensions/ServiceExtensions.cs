
using Microsoft.Extensions.DependencyInjection;
using Stack.ServiceLayer.Modules.Auth;
using Stack.ServiceLayer.Modules.Employees;
using Stack.ServiceLayer.Modules.Organizations;
using Stack.Repository.Common;
using Stack.ServiceLayer.Modules.Materials;

namespace Stack.API.Extensions
{
    public static class ServiceExtensions
    {

        public static void AddBusinessServices(this IServiceCollection caller)
        {

            caller.AddScoped<UsersService>();
            caller.AddScoped<RolesService>();
            caller.AddScoped<EmployeesService>();
            caller.AddScoped<CompanyCodesService>();
            caller.AddScoped<PurchasingGroupsService>();
            caller.AddScoped<OrgUnitsService>();
            caller.AddScoped<CostCentersService>();
            caller.AddScoped<PlantsService>();
            caller.AddScoped<MaterialGroupsService>();
            caller.AddScoped<UOMsService>();
            caller.AddScoped<PositionsService>();
            caller.AddScoped<MaterialTypesService>();
            caller.AddScoped<MaterialService>();

        }

    }

}
