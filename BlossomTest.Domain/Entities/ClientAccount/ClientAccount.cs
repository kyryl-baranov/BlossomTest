namespace BlossomTest.Domain.Entities
{
    public sealed partial class ClientAccount: AggregateRootAuditable
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateTime DateOfBirth { get; private set; }

        public ICollection<UserApplication> Applications { get; private set; } = new List<UserApplication>();
        public ICollection<TradingAccount> TradingAccounts { get; private set; } = new List<TradingAccount>();
        public ICollection<BankAccount> BankAccounts { get; private set; } = new List<BankAccount>();
    }
}
