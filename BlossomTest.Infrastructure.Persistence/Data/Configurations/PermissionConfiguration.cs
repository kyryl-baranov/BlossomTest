﻿using BlossomTest.Domain.Enums;
using BlossomTest.Domain.Entities.Security;

namespace BlossomTest.Infrastructure.Persistence.Data.Configurations;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(x => x.Id);

        IEnumerable<Permission> permissions = Enum.GetValues<Permissions>()
            .Select(x => new Permission { Id = (int)x, Name = x.ToString() });

        builder.HasData(permissions);
    }
}
