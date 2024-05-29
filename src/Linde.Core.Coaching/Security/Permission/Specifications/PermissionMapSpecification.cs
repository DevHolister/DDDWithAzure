using Ardalis.Specification;
using Linde.Core.Coaching.Common.Models;

namespace Linde.Core.Coaching.Security.Permission.Specifications;

internal class PermissionMapSpecification : Specification<Domain.Coaching.PermissionAggregate.Permission, ItemDto>
{
    public PermissionMapSpecification()
    {
        Query
            .Select(x => new ItemDto(
                x.Id.Value,
                x.Name));
        Query.OrderBy(t => t.Name);
    }
}
