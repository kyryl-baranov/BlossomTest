namespace BlossomTest.Domain.Entities;

public partial class ClientAccount
{
    private ClientAccount() { }

    public static Result<ClientAccount> Create(string firstName, string lastName, string email, DateTime dob)
    {
        if (string.IsNullOrWhiteSpace(firstName)) return Result<ClientAccount>.Failure(ClientAccountErrors.ClientAccountIsRequired);
        if (!email.Contains("@")) return Result<ClientAccount>.Failure(ClientAccountErrors.ClientAccountInvalidEmail);
        
        ClientAccount application = new()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            DateOfBirth = dob
        };

        return Result<ClientAccount>.Success(application);
    }
}