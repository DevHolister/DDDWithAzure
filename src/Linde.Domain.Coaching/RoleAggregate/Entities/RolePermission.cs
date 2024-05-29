using Linde.Domain.Coaching.Common;
using Linde.Domain.Coaching.PermissionAggregate;
using Linde.Domain.Coaching.PermissionAggregate.ValueObjects;

namespace Linde.Domain.Coaching.RoleAggregate.Entities;

public sealed class RolePermission : Entity<Guid>
{
    public PermissionId PermissionId { get; private set; }
    public Permission Permission { get; private set; }
    private RolePermission(Guid id, PermissionId permissionId)
        : base(id)
    {
        PermissionId = permissionId;
    }

    public static RolePermission Create(PermissionId permissionId)
    {
        return new(Guid.NewGuid(), permissionId);
    }

    private RolePermission() { }
}
