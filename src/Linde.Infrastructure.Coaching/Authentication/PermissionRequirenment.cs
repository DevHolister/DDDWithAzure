using Microsoft.AspNetCore.Authorization;

namespace Linde.Infrastructure.Coaching.Authentication;

public class PermissionRequirenment : IAuthorizationRequirement
{
    public PermissionRequirenment(string permission)
    {
        Permission = permission;
    }

    public string Permission { get; }
}
