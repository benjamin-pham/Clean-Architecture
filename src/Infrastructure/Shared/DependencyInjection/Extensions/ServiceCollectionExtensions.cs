using Application.Libraries;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddShared(this IServiceCollection services)
    {
        services.AddScoped<IDateTime, AppDateTime>();
        services.AddScoped<IJsonConverter, JsonConverter>();
        services.AddScoped<IJwtHandler, JwtHandler>();
        services.AddScoped<IHash, Hash>();
    }
}
