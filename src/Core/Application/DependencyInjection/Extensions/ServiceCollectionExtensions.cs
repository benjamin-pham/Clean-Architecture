using Application.Behaviors;
using Application.DependencyInjection.Options;
using Application.Libraries;
using Application.Libraries.AppMapper;
using Application.Middlewares;
using FluentValidation;
using MediatR;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(AssemblyReference.Assembly))
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
        //.AddTransient(typeof(IPipelineBehavior<,>), typeof(TracingPipelineBehavior<,>))
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformancePipelineBehavior<,>))
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionPipeLineBehavior<,>))
        .AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);
        


        ModelMapperConfig.MapperRegister();

        services.AddTransient<ExceptionHandlingMiddleware>();

        // configure strongly typed settings objects
        IConfiguration configuration = services.BuildServiceProvider().GetService<IConfiguration>();

        var appSettingsSection = configuration.GetSection("AppSettings");
        services.Configure<AppSettings>(appSettingsSection);
        var appSettings = appSettingsSection.Get<AppSettings>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                //ValidAudience = builder.Configuration["JWT:ValidAudience"],
                //ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Jwt.AccessToken.SecretKey))
            };
        });

      
    }
    public static void UseApplicationConfig(this IApplicationBuilder app)
    {
        var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
        ApplicationHttpContext.Configure(httpContextAccessor);
    }
    public static void UseExceptionHandlingWrapper(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
    public static void UseJwtMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<JwtMiddleware>();
    }
    public static void UseLanguage(this IApplicationBuilder app)
    {
        app.UseMiddleware<LanguageMiddleware>();
    }
}
