using Linde.Domain.Coaching.Common;
using Linde.Domain.Coaching.RoleAggregate;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;

namespace Linde.Domain.Coaching.UserAggreagate.Entities;

public sealed class UserRole : Entity<Guid>
{
    public RoleId RoleId { get; private set; }
    public Role Role { get; private set; }

    private UserRole(Guid id, RoleId roleId)
        : base(id)
    {
        RoleId = roleId;
    }

    public static UserRole Create(RoleId roleId)
    {
        return new(Guid.NewGuid(), roleId);
    }

    private UserRole() { }
}
