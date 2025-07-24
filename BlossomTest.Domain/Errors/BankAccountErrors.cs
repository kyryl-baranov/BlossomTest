namespace BlossomTest.Domain.Errors;

public static class BankAccountErrors
{
    public static readonly Error AccountNumberIsRequired = new ("Account number required.", "AccountNumberIsRequired");

    public static readonly Error SWIFTCodeInvalid = new ("SWIFT code must be at least 8 characters.", "SWIFTCodeInvalid");
}
