﻿using Business;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Adapters;
using Repository.Factory;

namespace Service
{
    public static class InjectionDependency
    {
        public static IServiceCollection AddInjectionDependency(this IServiceCollection services)
        {
            services.AddScoped<IRouteBusiness, RouteBusiness>();

            services.AddScoped<IRouteService, RouteService>();

            // EF Core
            //services.AddScoped<IRepository<Entity.Route>, RouteRepository>();

            // SQL Server
            services.AddScoped<IRepository<Entity.Route>, RouteSqlRepository>();

            return services;
        }
    }
}