namespace BlossomTest.Domain.Entities
{
    public sealed partial class BankAccount : AggregateRootAuditable
    {
        public string BankName { get; private set; }
        public string AccountNumber { get; private set; }
        public string SwiftCode { get; private set; }
        public int ClientAccountId { get; private set; }
        public ClientAccount ClientAccount { get; private set; } = default!;
    }
}
