using BlossomTest.Domain.Entities;

namespace BlossomTest.Application.Common.Interfaces;

/// <summary>
/// Interface for generating JWT tokens.
/// </summary>
public interface IJwtTokenProvider
{

    Task<(string Token, DateTimeOffset ExpirationDate)> GenerateJwtTokenAsync(User user);
    (string Token, DateTimeOffset ExpirationDate) GenerateRefreshToken();
}
