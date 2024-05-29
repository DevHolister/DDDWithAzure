using Ardalis.Specification;

namespace Linde.Core.Coaching.Common.Interfaces.Persistence;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
{ }
