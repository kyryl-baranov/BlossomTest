using BlossomTest.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

namespace BlossomTest.Infrastructure;

public static class DependencyInjection
{    
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Adds the EmailService to the service collection with a scoped lifetime.
        services.AddScoped<IViewRenderService, EmailServiceProvider.ViewRenderService>();
        services.AddScoped<IEmailService, EmailServiceProvider.EmailService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();

        return services;
    }
}