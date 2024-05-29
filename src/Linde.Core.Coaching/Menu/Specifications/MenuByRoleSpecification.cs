using Ardalis.Specification;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;

namespace Linde.Core.Coaching.Menu.Specifications
{
    internal class MenuByRoleSpecification : Specification<Domain.Coaching.RoleAggregate.Role, Domain.Coaching.MenuAggregate.Menu>
    {
        public MenuByRoleSpecification(List<RoleId> roles)
        {
            Query
            .SelectMany(t => t.PermissionItems.Select(t => t.Permission.Menu))
            .Where(c => roles.Contains(c.Id) && c.PermissionItems.Any(t => t.Permission.Menu.PermissionId != null));

        }
    }
}
