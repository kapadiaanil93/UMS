using Microsoft.Extensions.DependencyInjection;
using UMS.Infrastructure.Interface;
using UMS.Infrastructure.Repositories;

namespace UMS.Infrastructure
{
    public static class InfrastructureServicesExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            // Register application services here
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
