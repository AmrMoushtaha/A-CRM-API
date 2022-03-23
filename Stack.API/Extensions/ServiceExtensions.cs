
using Microsoft.Extensions.DependencyInjection;
using Stack.ServiceLayer.Modules.Auth;
using Stack.ServiceLayer.Modules.CustomerStage;
using Stack.ServiceLayer.Modules.Interest;
using Stack.ServiceLayer.Modules.pool;
using Stack.ServiceLayer.Modules.Region;
using Stack.ServiceLayer.Modules.Areas;
using Stack.ServiceLayer.Modules.Activities;
using Stack.ServiceLayer.Modules.Zoom;

namespace Stack.API.Extensions
{
    public static class ServiceExtensions
    {

        public static void AddBusinessServices(this IServiceCollection caller)
        {
            caller.AddScoped<UsersService>();
            caller.AddScoped<RolesService>();
            caller.AddScoped<AuthService>();
            caller.AddScoped<ContactService>();
            caller.AddScoped<CustomerService>();
            caller.AddScoped<DealService>();
            caller.AddScoped<LeadService>();
            caller.AddScoped<OpportunityService>();
            caller.AddScoped<ProspectService>();
            caller.AddScoped<InterestService>();
            caller.AddScoped<PoolService>();
            caller.AddScoped<ActivitiesService>();
            caller.AddScoped<SectionsService>();
            caller.AddScoped<ProcessFlowService>();
            caller.AddScoped<RegionService>();
            caller.AddScoped<AreaService>();
            caller.AddScoped<ZoomService>();
        }

    }

}
