namespace BlossomTest.Infrastructure.Persistence.Data.Configurations;

internal sealed class UserApplicationConfiguration : BaseAggregateRootAuditableConfiguration<UserApplication>
{
    public override void Configure(EntityTypeBuilder<UserApplication> builder)
    {
        base.Configure(builder);

        builder.Property(b => b.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasOne(b => b.ClientAccount).WithMany(b => b.Applications)
                    .HasForeignKey(d => d.ClientAccountId)
                    .HasConstraintName("FK_Applications_ClientAccount");
    }
}