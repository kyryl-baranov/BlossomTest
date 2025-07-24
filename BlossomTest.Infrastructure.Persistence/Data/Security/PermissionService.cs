using BlossomTest.Domain.Entities.Security;
using BlossomTest.Application.Common.Interfaces;

namespace BlossomTest.Infrastructure.Persistence.Data.Security;

internal sealed class PermissionService(IApplicationUnitOfWork applicationUnitOfWork) : IPermissionService
{
    public async Task<HashSet<string>> GetPermissionsAsync(int memberId)
    {
        List<Role> roles = await applicationUnitOfWork.Users
            .Include(x => x.Roles)!
            .ThenInclude(x => x.Permissions)
            .Where(x => x.Id == memberId)
            .SelectMany(x => x.Roles!)
            .ToListAsync().ConfigureAwait(false);

        return roles
            .SelectMany(x => x.Permissions)
            .Select(x => x.Name)
            .ToHashSet();
    }
}
