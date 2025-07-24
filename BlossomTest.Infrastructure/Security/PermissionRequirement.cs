using Microsoft.AspNetCore.Authorization;

namespace BlossomTest.Infrastructure.Security;

public sealed class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission => permission;
}
