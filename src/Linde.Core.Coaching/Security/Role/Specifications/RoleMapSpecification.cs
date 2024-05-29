using Ardalis.Specification;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Security.Role;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;

namespace Linde.Core.Coaching.Security.Role.Specifications;

internal class RoleMapSpecification : Specification<Domain.Coaching.RoleAggregate.Role, RoleDto>
{
    public RoleMapSpecification(string name, string description, string permission, int page = 1, int pageSize = 20, bool pagination = false)
    {
        if (pagination)
        {
            Query
                .Select(x => new RoleDto(
                    x.Id.Value,
                    x.Name,
                    x.Description,
                    x.PermissionItems.Select(y => new ItemDto(
                        y.PermissionId.Value,
                        y.Permission.Name))
                    ))
                .AsNoTracking()
                .Include(r => r.PermissionItems)
                .ThenInclude(x => x.Permission)
                .Skip((--page) * pageSize)
                .Take(pageSize);
        }

        Query.Where(x => x.Visible);

        if (!string.IsNullOrEmpty(name?.Trim()))
        {
            Query.Where(x => x.Name!.Contains(name.Trim().ToUpper()));
        }

        if (!string.IsNullOrEmpty(description?.Trim()))
        {
            Query.Where(x => x.Description.ToUpper().Contains(description.Trim().ToUpper()));
        }

        if (!string.IsNullOrEmpty(permission?.Trim()))
        {
            Query.Where(x => x.PermissionItems.Any(y => y.Permission.Name.Contains(permission.Trim().ToUpper())));
        }
        Query.OrderBy(t => t.Name);
    }
    public RoleMapSpecification(RoleId id)
    {
        Query
            .Select(x => new RoleDto(
                x.Id.Value,
                x.Name,
                x.Description,
                x.PermissionItems.Select(y => new ItemDto(
                    y.PermissionId.Value,
                    y.Permission.Name))
                ))
            .AsNoTracking()
            .Include(r => r.PermissionItems)
            .ThenInclude(x => x.Permission);
        Query.Where(x => x.Id == id && x.Visible);
    }
}
