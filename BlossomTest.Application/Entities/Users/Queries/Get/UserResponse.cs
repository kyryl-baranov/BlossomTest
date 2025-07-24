namespace BlossomTest.Application.Entities.Users.Queries.Get;

public record UserResponse(int Id, string FirstName, string LastName, AddressResponse? Address);

public record AddressResponse(string? City, string? Street, string? PostalCode);