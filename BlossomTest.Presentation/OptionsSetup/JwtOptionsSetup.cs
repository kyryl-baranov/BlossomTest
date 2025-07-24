using BlossomTest.Infrastructure.Security;
using Microsoft.Extensions.Options;

namespace BlossomTest.Presentation.OptionsSetup;

internal class JwtOptionsSetup(IConfiguration configuration) : IConfigureOptions<JwtOptions>
{
    private const string JwtSection = "Jwt";
    
    public void Configure(JwtOptions options) => configuration.GetSection(JwtSection).Bind(options);
}