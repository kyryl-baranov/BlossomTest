
namespace BlossomTest.Domain.Entities
{
    public sealed partial class TradingAccount : AggregateRootAuditable
    {
        public string AccountNumber { get; private set; }
        public string Platform { get; private set; }
        public decimal Balance { get; private set; }

        public int ClientAccountId { get; private set; }
        public ClientAccount ClientAccount { get; private set; } = default!;
    }
}
