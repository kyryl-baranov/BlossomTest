using BlossomTest.Domain.Entities.Security;

namespace BlossomTest.Infrastructure.Persistence.Data.UnitOfWork;

public sealed partial class ApplicationUnitOfWork
{
    public DbSet<User> Users => context.Set<User>();

    public DbSet<UserApplication> Applications => context.Set<UserApplication>();

    public DbSet<BankAccount> BankAccounts => context.Set<BankAccount>();

    public DbSet<ClientAccount> ClientAccounts => context.Set<ClientAccount>();

    public DbSet<TradingAccount> TradingAccounts => context.Set<TradingAccount>();

    public DbSet<Role> Roles => context.Set<Role>();

    public DbSet<Permission> Permissions => context.Set<Permission>();

    public DbSet<RefreshToken> RefreshTokens => context.Set<RefreshToken>();
}
