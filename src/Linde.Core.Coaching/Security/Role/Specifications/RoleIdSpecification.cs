using Ardalis.Specification;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;

namespace Linde.Core.Coaching.Security.Role.Specifications;

internal class RoleIdSpecification : Specification<Domain.Coaching.RoleAggregate.Role, RoleId>
{
    public RoleIdSpecification(IEnumerable<RoleId> ids)
    {
        Query
            .Select(x => x.Id)
            .Where(x => ids.Contains(x.Id));
    }
}
