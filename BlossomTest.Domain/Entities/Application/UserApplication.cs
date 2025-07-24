namespace BlossomTest.Domain.Entities
{
    public sealed partial class UserApplication: AggregateRootAuditable
    {
        public string Name { get; set; }
        public int ClientAccountId { get; set; }

        public ClientAccount ClientAccount { get; set; }
    }
}
