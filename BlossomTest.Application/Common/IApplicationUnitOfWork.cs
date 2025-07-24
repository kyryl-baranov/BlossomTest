using BlossomTest.Domain.Entities;
using BlossomTest.Domain.Entities.Security;

namespace BlossomTest.Application.Common;

/// <summary>
/// Represents a unit of work that manages the persistence of changes.
/// </summary>
public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Saves all changes made in this unit of work asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains a <see cref="Result"/>.</returns>
    public Task<Result> SaveChangesAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// Represents an application-specific unit of work that includes specific DbSets.
/// </summary>
public interface IApplicationUnitOfWork : IUnitOfWork
{    
    public DbSet<User> Users { get; }

    public DbSet<UserApplication> Applications { get; }

    public DbSet<BankAccount> BankAccounts { get; }

    public DbSet<ClientAccount> ClientAccounts { get; }

    public DbSet<TradingAccount> TradingAccounts { get; }

    public DbSet<Role> Roles { get; }

    public DbSet<Permission> Permissions { get; }

    public DbSet<RefreshToken> RefreshTokens { get; }
}
