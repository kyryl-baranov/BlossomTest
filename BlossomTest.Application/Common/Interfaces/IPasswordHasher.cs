namespace BlossomTest.Application.Common.Interfaces;

/// <summary>
/// Interface for password hashing and verification.
/// </summary>
public interface IPasswordHasher
{   
    string HashPassword(string password);
    bool VerifyPassword(string password, string? hashedPassword);
}