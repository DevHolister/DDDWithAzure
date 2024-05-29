using Microsoft.AspNetCore.Authorization;

namespace Linde.Infrastructure.Coaching.Authentication;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission)
        : base(policy: permission)
    {

    }
}
