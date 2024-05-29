using Ardalis.Specification;
using Linde.Domain.Coaching.PermissionAggregate.ValueObjects;

namespace Linde.Core.Coaching.Security.Permission.Specifications;

public class PermissionIdSpecification : Specification<Domain.Coaching.PermissionAggregate.Permission, PermissionId>
{
    public PermissionIdSpecification(IEnumerable<PermissionId> ids)
    {
        Query
            .Select(x => x.Id)
            .Where(x => ids.Contains(x.Id));
    }
}
