namespace BlossomTest.Domain.Errors;

public static class UserApplicationErrors
{
    public static readonly Error ApplicationNameIsRequired = new("Name is required.", "ApplicationNameIsRequired");
}
