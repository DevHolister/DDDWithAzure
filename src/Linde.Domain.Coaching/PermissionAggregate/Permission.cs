using Linde.Domain.Coaching.Common;
using Linde.Domain.Coaching.MenuAggregate;
using Linde.Domain.Coaching.PermissionAggregate.ValueObjects;

namespace Linde.Domain.Coaching.PermissionAggregate;

public sealed class Permission : Entity<PermissionId>
{
    public string Name { get; private set; }
    public string Path { get; private set; }
    public string Actions { get; private set; }
    public Menu Menu { get; set; }

    private Permission() { }

    private Permission(
        PermissionId permissionId,
        string name,
        string path,
        string actions) : base(permissionId)
    {
        Name = name;
        Path = path;
        Actions = actions;
    }

    public static Permission Create(
        string name,
        string path,
        string actions)
    {
        return new(
            PermissionId.CreateUnique(),
            name,
            path,
            actions);
    }
}
