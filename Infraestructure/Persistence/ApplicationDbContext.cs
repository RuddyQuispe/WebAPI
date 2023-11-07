using Domain.Entities.UserModule;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Infraestructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private IDbContextTransaction _currentTransaction;
    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
    public bool HasActiveTransaction => _currentTransaction != null;

    public DbContext dbContext => this;
    private SqlConnectionStringBuilder _sqlConnectionStringBuilder { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Para buscar configuraciones en el ensamblado actual
        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        //configurationBuilder.Properties<DateOnly>()
        //  .HaveConversion<DateOnlyConverter, DateOnlyComparer>()
        //  .HaveColumnType("date");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    public SqlConnectionStringBuilder getConnectionStringBuilder()
    {
        return new SqlConnectionStringBuilder(this.dbContext.Database.GetConnectionString());
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel eTipoTransaccion = IsolationLevel.ReadCommitted)
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync(eTipoTransaccion);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            transaction.Commit();
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        try
        {
            await _currentTransaction?.RollbackAsync();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    #region ENTITIES
    public virtual DbSet<User> User { get; set; }
    public virtual DbSet<Role> Role { get; set; }
    public virtual DbSet<Permission> Permission { get; set; }
    public virtual DbSet<RolePermission> RolePermission { get; set; }
    #endregion ENTITIES
}
