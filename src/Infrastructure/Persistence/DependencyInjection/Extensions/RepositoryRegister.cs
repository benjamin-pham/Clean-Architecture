using Domain.Abstractions.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence.DependencyInjection.Extensions;
public class RepositoryRegister
{
    public static void AddRepository(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
