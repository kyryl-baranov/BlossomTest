using DomainValidation;
using Microsoft.Extensions.Logging;

namespace BlossomTest.Infrastructure.Persistence.Data.UnitOfWork;

public sealed partial class ApplicationUnitOfWork(ApplicationDbContext context, ILogger<ApplicationUnitOfWork> logger) : IApplicationUnitOfWork
{   
    public async Task<Result> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return Result.Success();
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "An error occurred while saving changes.");

            return Result.Failure(ex.Message);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }

    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this);
    }
}
