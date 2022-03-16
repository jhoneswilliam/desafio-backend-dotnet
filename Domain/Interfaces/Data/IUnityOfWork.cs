using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Interfaces.Data;

public interface IUnityOfWork
{
    IExecutionStrategy CreateExecutionStrategy();
    IDbContextTransaction BeginTransaction();
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}