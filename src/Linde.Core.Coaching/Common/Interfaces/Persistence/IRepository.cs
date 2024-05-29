using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Common.Interfaces.Persistence;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{
    Task<T> AddTranAsync(T entity, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> AddRangeTranAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task UpdateTranAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateRangeTranAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task DeleteTranAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteRangeTranAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
}
