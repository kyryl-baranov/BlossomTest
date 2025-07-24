using BlossomTest.Domain.Entities;

namespace BlossomTest.Application.Entities.Applications.Queries.Get;

public static class ApplicationExtensions
{   
    public static ApplicationResponse ToResponse(this UserApplication application)
    {
        ArgumentNullException.ThrowIfNull(application);

        return new ApplicationResponse(application.Name, application.ClientAccountId);
    }
}