
using Microsoft.Extensions.DependencyInjection;
using Stack.ServiceLayer.Modules.Auth;
using Stack.Repository.Common;


namespace Stack.API.Extensions
{
    public static class ServiceExtensions
    {

        public static void AddBusinessServices(this IServiceCollection caller)
        {

            caller.AddScoped<UsersService>();
            caller.AddScoped<RolesService>();

        }

    }

}
