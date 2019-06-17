using Business;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace Service
{
    public static class InjectionDependency
    {
        public static IServiceCollection AddInjectionDependency(this IServiceCollection services)
        {
            services.AddScoped<IRouteBusiness, RouteBusiness>();

            services.AddScoped<IRouteService, RouteService>();

            services.AddScoped<IRepository<Entity.Route>, RouteRepository>();

            return services;
        }
    }
}