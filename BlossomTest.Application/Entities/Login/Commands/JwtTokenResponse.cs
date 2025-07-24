namespace BlossomTest.Application.Entities.Login.Commands;

public record JwtTokenResponse(string AccessToken, string RefreshToken, DateTimeOffset ExpiresIn);
