﻿using BlossomTest.Domain.Common;

namespace BlossomTest.Infrastructure.Persistence.Data.Configurations;

internal abstract class BaseAggregateRootAuditableConfiguration<T> : IEntityTypeConfiguration<T> where T : AggregateRootAuditable
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.Property(x => x.UpdatedDate)
            .IsRequired();

        builder.Ignore(x => x.DomainEvents);
    }
}
