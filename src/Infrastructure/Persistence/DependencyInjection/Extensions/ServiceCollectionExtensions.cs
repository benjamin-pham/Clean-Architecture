using Application.Abstractions;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace Persistence.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPersistence(this IServiceCollection services, string migrationAssembly = default)
    {
        AddDbContext(services, migrationAssembly);

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();


        AddRepository(services);
    }

    private static void AddDbContext(IServiceCollection services, string migrationAssembly = default)
    {
        var migrateAssembly = migrationAssembly;
        if (string.IsNullOrEmpty(migrateAssembly))
        {
            migrateAssembly = AssemblyReference.Assembly.GetName().Name;
        }

        //services.AddDbContext<DbContext, ApplicationDbContext>((provider, builder) =>
        //{
        //    IConfiguration configuration = provider.GetRequiredService<IConfiguration>();
        //    builder
        //    //.EnableDetailedErrors(true)
        //    //.EnableSensitiveDataLogging(true)
        //    .UseLazyLoadingProxies(true)
        //    .UseSqlServer(connectionString: configuration.GetConnectionString("ApplicationDbContext"),
        //        sqlServerOptionsAction: optionsBuilder => optionsBuilder.ExecutionStrategy(
        //            dependencies => new SqlServerRetryingExecutionStrategy(
        //                dependencies: dependencies,
        //                maxRetryCount: 5,
        //                maxRetryDelay: TimeSpan.FromSeconds(5),
        //                errorNumbersToAdd: []
        //            )).MigrationsAssembly(migrateAssembly));
        //});

        services.AddDbContext<DbContext, ApplicationDbContext>((provider, builder) =>
        {
            IConfiguration configuration = provider.GetRequiredService<IConfiguration>();

            builder
            .UseLazyLoadingProxies()
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .UseNpgsql(connectionString: configuration.GetConnectionString("ApplicationDbContext"),
            npgsqlOptionsAction: optionsBuilder => optionsBuilder.ExecutionStrategy(
                dependencies => new NpgsqlRetryingExecutionStrategy(
                    dependencies: dependencies,
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorCodesToAdd: []
                )).MigrationsAssembly(migrateAssembly));
        });
    }

    private static void AddRepository(IServiceCollection services)
    {
        RepositoryRegister.AddRepository(services);
    }
}
