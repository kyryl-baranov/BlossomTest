namespace BlossomTest.Infrastructure.Persistence.Data.Configurations;

internal sealed class TradingAccountConfiguration : BaseAggregateRootAuditableConfiguration<TradingAccount>
{  
    public override void Configure(EntityTypeBuilder<TradingAccount> builder)
    {
        base.Configure(builder);

        builder.Property(b => b.AccountNumber)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(b => b.Platform)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(b => b.Balance)
            .HasColumnType("decimal(18, 5)");

        builder.HasOne(b => b.ClientAccount).WithMany(b => b.TradingAccounts)
                    .HasForeignKey(d => d.ClientAccountId)
                    .HasConstraintName("FK_TradingAccounts_ClientAccount");
    }
}