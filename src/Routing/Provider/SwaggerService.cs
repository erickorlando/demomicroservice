using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Provider
{
    public static class SwaggerService
    {
        private static string project;

        public static IServiceCollection AddSwaggerDoc(this IServiceCollection services, IConfiguration configuration, string name)
        {
            project = name;

            services.AddSwaggerGen(o =>
            {
                var security = new Dictionary<string, IEnumerable<string>>();

                configuration.GetSection($"Swagger:{name}:Gen:Security").GetChildren().ToList().ForEach( s => 
                {
                    security.Add(s.GetSection("Name").Value, new string[] { });

                    o.AddSecurityDefinition(s.GetSection("Name").Value, new ApiKeyScheme
                    {
                        Name = s.GetSection("Name").Value,
                        Description = s.GetSection("Description").Value,
                        In = s.GetSection("In").Value,
                        Type = s.GetSection("Type").Value
                    });
                });

                services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>().ApiVersionDescriptions.ToList().ForEach( P =>
                {
                    configuration.GetSection($"Swagger:{name}:Gen:Doc").GetChildren().Where(D => D.GetSection("Version").Value.Equals(P.GroupName)).ToList().ForEach(D =>
                    {
                        o.SwaggerDoc(P.GroupName, new Info()
                        {
                            Title = D.GetSection("Title").Value,
                            Version = P.GroupName,
                            Description = D.GetSection("Description").Value,
                            Contact = new Contact()
                            {
                                Name = D.GetSection("Contact:Name").Value,
                                Email = D.GetSection("Contact:Email").Value
                            },
                            License = new License()
                            {
                                Name = D.GetSection("License:Name").Value,
                                Url = D.GetSection("License:Url").Value
                            }
                        });
                    });
                });

                o.AddSecurityRequirement(security);
                o.DescribeAllEnumsAsStrings();
                o.OperationFilter<SwaggerDefaultValues>();
                o.EnableAnnotations();
                o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, Path.GetFileName($"{name}.xml")));
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app, IConfiguration configuration, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                provider.ApiVersionDescriptions.ToList().ForEach( EP =>
                {
                    o.SwaggerEndpoint($"/swagger/{EP.GroupName}/swagger.json", EP.GroupName.ToUpperInvariant());
                });
                o.RoutePrefix = configuration.GetSection($"Swagger:{project}:Use:Route").Value;
                o.DocumentTitle = configuration.GetSection($"Swagger:{project}:Use:DocumentTitle").Value;
                //o.DisplayOperationId();
                o.DocExpansion(DocExpansion.List);
                o.DisplayRequestDuration();
                o.EnableFilter();
                o.EnableValidator();
                //o.EnableDeepLinking();  // Habilita la url directo al tag
            });

            return app;
        }
    }

    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            operation.Deprecated = apiDescription.IsDeprecated();

            if (operation.Parameters == null)
            {
                return;
            }

            foreach (var parameter in operation.Parameters.OfType<NonBodyParameter>())
            {
                var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                if (parameter.Default == null)
                {
                    parameter.Default = description.DefaultValue;
                }

                parameter.Required |= description.IsRequired;
            }
        }
    }
}
