using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using BlossomTest.Application.Common.Behaviours;

namespace BlossomTest.Application;

public static class DependencyInjection
{   
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Adds validators from the executing assembly to the service collection.
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Adds MediatR services and registers behaviors from the executing assembly.
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        });

        return services;
    }
}