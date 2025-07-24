namespace BlossomTest.Infrastructure.Persistence.Data.Configurations;

internal sealed class BankAccountConfiguration : BaseAggregateRootAuditableConfiguration<BankAccount>
{    
    public override void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        base.Configure(builder);

        builder.Property(b => b.BankName)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(b => b.AccountNumber)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(b => b.SwiftCode)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasOne(b => b.ClientAccount).WithMany(b => b.BankAccounts)
                    .HasForeignKey(d => d.ClientAccountId)
                    .HasConstraintName("FK_BankAccounts_ClientAccount");
    }
}
