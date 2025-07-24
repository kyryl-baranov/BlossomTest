﻿using BlossomTest.Domain.Enums;
using BlossomTest.Domain.Entities.Security;

namespace BlossomTest.Infrastructure.Persistence.Data.Configurations;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();

        builder.HasMany(x => x.Users)
            .WithMany()
            .UsingEntity<RoleUser>(u =>
                    u.HasOne<User>().WithMany().HasForeignKey(ru => ru.UserId),
                r =>
                    r.HasOne<Role>().WithMany().HasForeignKey(ru => ru.RoleId));

        IEnumerable<Role> roles = Enum.GetValues<Roles>().Select(x => new Role { Id = (int)x, Name = x.ToString() });

        builder.HasData(roles);
    }
}
