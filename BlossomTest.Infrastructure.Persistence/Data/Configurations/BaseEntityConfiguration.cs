using BlossomTest.Domain.Common;

namespace BlossomTest.Infrastructure.Persistence.Data.Configurations;

internal abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Ignore(x => x.DomainEvents);
    }
}
