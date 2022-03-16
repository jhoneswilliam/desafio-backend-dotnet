using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces.Data;

public interface IRepository
{
    DbSet<T> GetSet<T>() where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<T?> GetAsync<T>(int id, CancellationToken cancellationToken = default) where T : class;    
    void Add<T>(T entity) where T : class;
    void Update<T>(T entity) where T : class;
    void Remove<T>(T entity) where T : class;
    IQueryable<T> GetAll<T>(bool @readonly) where T : class;
}