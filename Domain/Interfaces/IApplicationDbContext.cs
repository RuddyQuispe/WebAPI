using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces;

public interface IApplicationDbContext
{
    DbContext dbContext { get; }
    bool HasActiveTransaction { get; }
    Task<IDbContextTransaction> BeginTransactionAsync(System.Data.IsolationLevel eTipoTransaccion = System.Data.IsolationLevel.ReadCommitted);
    Task CommitTransactionAsync(IDbContextTransaction transaction);
    Task RollbackTransactionAsync();
}
