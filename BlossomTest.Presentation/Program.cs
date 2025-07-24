using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using BlossomTest.Helpers;
using Serilog;
using BlossomTest.Presentation.Endpoints;
using BlossomTest.Presentation.Configuration;

var builder = WebApplication.CreateBuilder(args);

var clientId = builder.Configuration["AppSettings:MicrosoftClientId"];
var keyVaultUri = builder.Configuration["AppSettings:MicrosoftKeyVaultUri"];
var tenantId = builder.Configuration["AppSettings:MicrosoftTenantId"];
var thumbprint = builder.Configuration["AppSettings:MicrosoftThumbprint"] ?? "";

if (builder.Environment.IsProduction())
{
    builder.Configuration.AddAzureKeyVault(
        new SecretClient(
            new Uri(keyVaultUri),
            //new DefaultAzureCredential(includeInteractiveCredentials: true)
            new ClientCertificateCredential(tenantId, clientId, X509CertificateHelper.GetCertificate(thumbprint))
        ),
        new AzureKeyVaultConfigurationOptions()
        {
            ReloadInterval = TimeSpan.FromSeconds(1000)
        }
    );
}

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services    
    .AddApplicationServices()
    .AddInfrastructureServices()
    .AddInfrastructurePersistenceServices(builder.Configuration)
    .AddPresentationServices();

builder.Logging.ClearProviders();
builder.Host.UseSerilog(((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration)));

WebApplication app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseHealthChecks("/health");

// API Endpoints
app.MapApplicationEndpoints();
app.MapUserEndpoints();
app.MapMemberEndpoints();

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

await app.RunAsync();