using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Diagnostics;
using BlossomTest.Application.Common.Interfaces;
using BlossomTest.Infrastructure.Persistence.Data;
using BlossomTest.Infrastructure.Persistence.Data.Security;
using BlossomTest.Infrastructure.Persistence.Data.UnitOfWork;
using BlossomTest.Infrastructure.Persistence.Data.Interceptors;

namespace BlossomTest.Infrastructure.Persistence;

public static class DependencyInjection
{    
    public static IServiceCollection AddInfrastructurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Retrieves the connection string from the configuration.
        string? connectionString = configuration.GetConnectionString("AllocationSystem");

        // Throws an exception if the connection string is null or empty.
        ArgumentException.ThrowIfNullOrEmpty(connectionString);

        // Adds the AuditableEntityInterceptor as a scoped service.
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        // Check if the environment is testing
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Testing")
        {
            // Configures the ApplicationDbContext with the connection string and interceptors.
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());               
                options.UseSqlServer(connectionString, builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });
        }

        // Adds the ApplicationUnitOfWork as a scoped service.
        services.AddScoped<IApplicationUnitOfWork, ApplicationUnitOfWork>();

        // Adds the system time provider as a singleton service.
        services.AddSingleton(TimeProvider.System);

        services.AddScoped<IPermissionService, PermissionService>();

        return services;
    }
}
