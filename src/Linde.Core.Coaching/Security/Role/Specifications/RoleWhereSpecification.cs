using Ardalis.Specification;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;

namespace Linde.Core.Coaching.Security.Role.Specifications;

public class RoleWhereSpecification : Specification<Domain.Coaching.RoleAggregate.Role>
{
    public RoleWhereSpecification(string name)
    {
        Query
            .Where(x => x.Name == name.Trim().ToUpper());
    }

    public RoleWhereSpecification(RoleId id)
    {
        Query
            .Where(x => x.Id == id)
            .Include(x => x.PermissionItems);

    }

}
