namespace BlossomTest.Application.Common.Interfaces;

/// <summary>
/// Defines the contract for a service that provides permissions for a member.
/// </summary>
public interface IPermissionService
{   
    Task<HashSet<string>> GetPermissionsAsync(int memberId);
}