namespace BlossomTest.Infrastructure.Persistence.Data.Configurations;

internal sealed class ClientAccountConfiguration : BaseAggregateRootAuditableConfiguration<ClientAccount>
{    
    public override void Configure(EntityTypeBuilder<ClientAccount> builder)
    {
        base.Configure(builder);

        builder.Property(b => b.FirstName).HasMaxLength(200).IsRequired();
        builder.Property(b => b.LastName).HasMaxLength(200).IsRequired();
        builder.Property(b => b.Email).HasMaxLength(200).IsRequired();
        builder.Property(b => b.DateOfBirth).HasColumnType("datetime").IsRequired();
    }
}
