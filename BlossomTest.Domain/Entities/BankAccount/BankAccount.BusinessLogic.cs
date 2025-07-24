namespace BlossomTest.Domain.Entities;

public partial class BankAccount
{
    private BankAccount() { }

    public static Result<BankAccount> Create(string bankName, string accountNumber, string swiftCode, int clientAccountId, ClientAccount clientAccount)
    {
        if (string.IsNullOrWhiteSpace(accountNumber)) return Result<BankAccount>.Failure(BankAccountErrors.AccountNumberIsRequired);
        if (swiftCode.Length < 8) return Result<BankAccount>.Failure(BankAccountErrors.SWIFTCodeInvalid);

        BankAccount bank = new()
        {
            BankName = bankName,
            AccountNumber = accountNumber,
            SwiftCode = swiftCode,
            ClientAccountId = clientAccountId,
            ClientAccount = clientAccount
        };

        return Result<BankAccount>.Success(bank);
    }
}