using Microsoft.Extensions.DependencyInjection;
using UMS.Application.Interfaces;
using UMS.Application.Services;

namespace UMS.Application
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Register application services here
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
        }
    }
}
