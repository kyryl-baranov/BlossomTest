using BlossomTest.Domain.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BlossomTest.Infrastructure.Persistence.Data.Interceptors;

public class AuditableEntityInterceptor(TimeProvider timeProvider) : SaveChangesInterceptor
{
    
    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        ArgumentNullException.ThrowIfNull(eventData);

        UpdateEntities(eventData.Context);

        return base.SavedChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = new())
    {
        ArgumentNullException.ThrowIfNull(eventData);

        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context is null)
        {
            return;
        }

        foreach (EntityEntry<EntityAuditable> entry in context.ChangeTracker.Entries<EntityAuditable>())
        {
            if (entry.State is not (EntityState.Added or EntityState.Modified) && !HasChangedOwnedEntities(entry))
            {
                continue;
            }

            DateTime utcNow = timeProvider.GetUtcNow().DateTime;

            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = utcNow;
            }

            entry.Entity.UpdatedDate = utcNow;
        }
    }

    private static bool HasChangedOwnedEntities(EntityEntry entry)
    {
        return entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            r.TargetEntry.State is EntityState.Added or EntityState.Modified);
    }
}
