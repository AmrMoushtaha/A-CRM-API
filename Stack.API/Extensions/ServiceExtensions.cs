
using Microsoft.Extensions.DependencyInjection;
using Stack.ServiceLayer.Modules.Activities;
using Stack.ServiceLayer.Modules.Auth;
using Stack.ServiceLayer.Modules.Channels;
using Stack.ServiceLayer.Modules.CR;
using Stack.ServiceLayer.Modules.CustomerStage;
using Stack.ServiceLayer.Modules.Hierarchy;
using Stack.ServiceLayer.Modules.Interest;
using Stack.ServiceLayer.Modules.pool;
using Stack.ServiceLayer.Modules.SystemInitialization;
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
            caller.AddScoped<PoolService>();
            caller.AddScoped<ActivitiesService>();
            caller.AddScoped<SectionsService>();
            caller.AddScoped<ProcessFlowService>();
            caller.AddScoped<ZoomService>();
            caller.AddScoped<SystemInitializationService>();
            caller.AddScoped<LocationService>();
            caller.AddScoped<InterestService>();
            caller.AddScoped<HierarchyService>();
            caller.AddScoped<GeneralCustomersService>();
            caller.AddScoped<ChannelsService>();
            caller.AddScoped<CustomerRequestService>();
            caller.AddScoped<ChatService>();
        }

    }

}
