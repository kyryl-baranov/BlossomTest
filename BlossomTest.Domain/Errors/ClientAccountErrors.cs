namespace BlossomTest.Domain.Errors;

public static class ClientAccountErrors
{
    public static readonly Error ClientAccountIsRequired = new("Name is required.", "ClientAccountInvalid");
    public static readonly Error ClientAccountInvalidEmail = new ("Invalid email format.", "ClientAccountInvalidEmail");
}
