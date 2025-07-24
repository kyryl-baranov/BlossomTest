using BlossomTest.Domain.Entities;

namespace BlossomTest.Application.Entities.Applications.Commands.Update;

public static class ApplicationMappingExtensions
{   
    public static void UpdateFromRequest(this UserApplication application, UpdateApplicationCommand request)
    {
        ArgumentNullException.ThrowIfNull(application);
        ArgumentNullException.ThrowIfNull(request);

        application.Name = request.Name;
        application.ClientAccountId = request.ClientAccountId;
    }
}
