using Linde.Domain.Coaching.Common;
using Linde.Domain.Coaching.Entities.Catalogs;
using Linde.Domain.Coaching.RoleAggregate.Entities;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;

namespace Linde.Domain.Coaching.RoleAggregate;

public sealed class Role : Entity<RoleId>
{
    private readonly List<RolePermission> _permissionItems = new();
    public IReadOnlyList<RolePermission> PermissionItems => _permissionItems.AsReadOnly();
    public string Name { get; private set; }
    public string Description { get; private set; }

    private Role(RoleId userId,
        string name,
        string description) : base(userId)
    {
        Name = name;
        Description = description;
        Visible = true;
    }

    public static Role Create(
        string name,
        string description)
    {
        return new(
            RoleId.CreateUnique(),
            name,
            description);
    }

    private Role() { }

    public void AddPermission(RolePermission permission)
    {
        if (!_permissionItems.Contains(permission))
            _permissionItems.Add(permission);
    }

    public void UpdatePermissions(List<RolePermission> permissions)
    {
        _permissionItems.RemoveAll(x => !permissions.Contains(x));
        permissions.ForEach(x => AddPermission(x));
    }

    public void UpdateData(string name, string description)
    {
        Name = name.Trim().ToUpper();
        Description = description.Trim();
    }
}
