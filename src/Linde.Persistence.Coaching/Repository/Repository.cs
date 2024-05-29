using Ardalis.Specification.EntityFrameworkCore;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Linde.Persistence.Coaching.Repository;

public class Repository<T> : RepositoryBase<T>, IRepository<T> where T : class
{
    private readonly CoachingDbContext dbContext;
    public Repository(CoachingDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<T> AddTranAsync(T entity, CancellationToken cancellationToken = default)
    {
        dbContext.Set<T>().Add(entity);

        return entity;
    }
    public async Task<IEnumerable<T>> AddRangeTranAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        dbContext.Set<T>().AddRange(entities);
        return entities;
    }

    /// <inheritdoc/>
    public async Task UpdateTranAsync(T entity, CancellationToken cancellationToken = default)
    {
        dbContext.Set<T>().Update(entity);
    }

    /// <inheritdoc/>
    public async Task UpdateRangeTranAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        dbContext.Set<T>().UpdateRange(entities);
    }

    /// <inheritdoc/>
    public async Task DeleteTranAsync(T entity, CancellationToken cancellationToken = default)
    {
        dbContext.Set<T>().Remove(entity);
    }

    /// <inheritdoc/>
    public async Task DeleteRangeTranAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        dbContext.Set<T>().RemoveRange(entities);
    }
}
