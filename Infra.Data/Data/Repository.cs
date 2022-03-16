using Domain.Enums;
using Domain.Interfaces.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data;

public class Repository : IRepository
{
    private readonly IUnityOfWork _unityOfWork;

    public Repository(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }

    public DbSet<T> GetSet<T>() where T : class
    {
        return _unityOfWork.Set<T>();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _unityOfWork.SaveChangesAsync();
    }

    public async Task<T?> GetAsync<T>(int id, CancellationToken cancellationToken = default) where T : class
    {
        return await GetSet<T>().FindAsync(new object[] { id }, cancellationToken);
    }  
    
    public void Add<T>(T entity) where T : class
    {
        GetSet<T>().Add(entity);
    }

    public void Update<T>(T entity) where T : class
    {
        GetSet<T>().Update(entity);
    }

    public void Remove<T>(T entity) where T : class
    {
        GetSet<T>().Remove(entity);
    }
    
    public IQueryable<T> GetAll<T>(bool @readonly) where T : class
    {
        return @readonly ? GetSet<T>().AsNoTracking() : GetSet<T>();
    }
}