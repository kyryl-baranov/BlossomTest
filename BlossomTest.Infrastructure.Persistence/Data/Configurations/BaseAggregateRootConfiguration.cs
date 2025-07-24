using BlossomTest.Domain.Common;

namespace BlossomTest.Infrastructure.Persistence.Data.Configurations;

internal abstract class BaseAggregateRootConfiguration<T> : IEntityTypeConfiguration<T> where T : AggregateRoot
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Ignore(x => x.DomainEvents);
    }
}
