using BlossomTest.Domain.Entities.Security;

namespace BlossomTest.Domain.Entities;

public partial class User : AggregateRoot
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? Email { get; init; }
    public string? HashedPassword { get; init; }
    public Address? Address { get; init; }
    public Gender? Gender { get; init; }
    public ICollection<Role> Roles { get; init; } = [];
}
