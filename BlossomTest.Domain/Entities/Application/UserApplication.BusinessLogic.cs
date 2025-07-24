namespace BlossomTest.Domain.Entities;

public partial class UserApplication
{
    private UserApplication() { }

    public static Result<UserApplication> Create(string name, int clientAccountId)
    {
        if (string.IsNullOrWhiteSpace(name)) return Result<UserApplication>.Failure(UserApplicationErrors.ApplicationNameIsRequired);
        
        UserApplication application = new()
        {
            Name = name,
            ClientAccountId = clientAccountId
        };

        return Result<UserApplication>.Success(application);
    }
}