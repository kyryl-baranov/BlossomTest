﻿using BlossomTest.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BlossomTest.Infrastructure.Persistence.Data.Interceptors;

public class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        ArgumentNullException.ThrowIfNull(eventData);

        DispatchDomainEvents(eventData.Context);

        return base.SavedChanges(eventData, result);
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = new())
    {
        ArgumentNullException.ThrowIfNull(eventData);

        await DispatchDomainEventsAsync(eventData.Context).ConfigureAwait(false);

        return await base.SavedChangesAsync(eventData, result, cancellationToken).ConfigureAwait(false);
    }

    private async Task DispatchDomainEventsAsync(DbContext? context)
    {
        if (context == null) return;

        IEnumerable<AggregateRoot> entities = context.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(e => e.Entity.DomainEvents.Count != 0)
            .Select(e => e.Entity)
            .ToList();

        List<INotification> domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (INotification domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent).ConfigureAwait(false);
        }
    }

    private void DispatchDomainEvents(DbContext? context)
    {
        if (context == null) return;

        IEnumerable<AggregateRoot> entities = context.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(e => e.Entity.DomainEvents.Count != 0)
            .Select(e => e.Entity)
            .ToList();

        List<INotification> domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (INotification domainEvent in domainEvents)
        {
            mediator.Publish(domainEvent);
        }
    }
}
