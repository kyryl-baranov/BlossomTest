namespace BlossomTest.Domain.Entities.Security;

public sealed class RoleUser
{
    public required int UserId { get; init; }

    public required int RoleId { get; init; }
}