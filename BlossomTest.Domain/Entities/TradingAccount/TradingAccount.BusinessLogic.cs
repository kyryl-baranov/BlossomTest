namespace BlossomTest.Domain.Entities;

public partial class TradingAccount
{
    private TradingAccount() { }

    public static Result<TradingAccount> Create(string accountNumber, string platform, int clientAccountId, decimal balance, ClientAccount clientAccount)
    {
        TradingAccount tradingAccount = new()
        {
            AccountNumber = accountNumber,
            Platform = platform,
            ClientAccountId = clientAccountId,
            Balance = balance,
            ClientAccount = clientAccount
        };

        return Result<TradingAccount>.Success(tradingAccount);
    }
}