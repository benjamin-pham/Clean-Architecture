using Application.Constants;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Presentation.DependencyInjection.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.RegularExpressions;

namespace Presentation.DependencyInjection.Extensions;


public static class ServiceCollectionExtensions
{
    public static void AddPresentation(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Conventions.Add(new ControllerDocumentationConvention());
            //options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
        });
        services.AddFluentValidationRulesToSwagger();
        services.AddSwaggerGen(opt =>
        {
            //opt.ExampleFilters();

            //opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                     new string[] { }
                }
            });

            // Set the comments path for the Swagger JSON and UI.**
            //opt.IncludeXmlComments($@"{AppContext.BaseDirectory}\{AssemblyReference.Assembly.GetName().Name}.xml");
            //opt.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}\Application.xml");

            var dir = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory));
            foreach (var fi in dir.EnumerateFiles("*.xml"))
            {
                opt.IncludeXmlComments(fi.FullName);
            }

        });

        //services.AddSwaggerGen();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddApiVersioning(options => options.ReportApiVersions = true)
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
    }

    public static void UseConfigureSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var version in app.DescribeApiVersions().Select(version => version.GroupName))
                options.SwaggerEndpoint($"/swagger/{version}/swagger.json", version);

            options.DisplayRequestDuration();
            //options.EnableTryItOutByDefault();
            options.DocExpansion(DocExpansion.List);
            options.EnableFilter();
        });
        //app.MapGet("/", () => Results.Redirect("/swagger/index.html"))
        //    .WithTags(string.Empty);
    }

    private class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value)
        {
            // Slugify value
            return value == null ? null : Regex.Replace(Regex.Replace(value.ToString(), @"\s+", ""), "([a-z])([A-Z])", "$1-$2").ToLower();
        }
    }

    public class ControllerDocumentationConvention : IControllerModelConvention
    {
        void IControllerModelConvention.Apply(ControllerModel controller)
        {
            if (controller == null)
                return;
            controller.ControllerName = Regex.Replace(controller.ControllerName.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();
        }
    }
}