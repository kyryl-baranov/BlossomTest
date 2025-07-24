namespace BlossomTest.Domain.Entities;

public sealed partial class RefreshToken : EntityAuditable
{
    public required string Token { get; init; }

    public DateTimeOffset ExpiresDate { get; init; }

    public int UserId { get; init; }

    public required User User { get; init; }

    public bool IsExpired => DateTimeOffset.UtcNow >= ExpiresDate;
}
