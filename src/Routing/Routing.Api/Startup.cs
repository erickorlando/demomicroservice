using Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Provider;
using Service;

namespace Routing.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("Configuration/swagger.json", optional: false, reloadOnChange: true)
                .AddJsonFile("Configuration/appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"Configuration/appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInjectionDependency()
                .AddSwaggerDoc(Configuration, 
                    System.Reflection.Assembly.GetExecutingAssembly().GetName().Name)
                .Configure<Utility.Appsettings>(Configuration)
                .AddCors(options => 
                    options.AddPolicy("AllowAll", 
                        p => p.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()))
                .AddApiVersions(Configuration)
                .AddDbContext<RoutingDbContext>()
                .AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<MiddlewareInterceptor>()
                .UseCors("AllowAll")
                .UseMvc()
                .UseSwaggerDoc(Configuration, provider);
        }
    }
}
