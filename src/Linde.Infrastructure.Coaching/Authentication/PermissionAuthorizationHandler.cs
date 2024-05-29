using Linde.Core.Coaching.Common.Interfaces.Services;
using Linde.Core.Coaching.Common.Models.Security.Role;
using Microsoft.AspNetCore.Authorization;

namespace Linde.Infrastructure.Coaching.Authentication;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirenment>
{
    private readonly ICurrentUserService _currentUser;

    public PermissionAuthorizationHandler(ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
    }
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirenment requirement)
    {
        try
        {
            var permissionsString = _currentUser.Permissions;
            var permissions = permissionsString.Select(x => new PermissionAccessDto(x.Split(":")[0], x.Split(":")[1]));
            var requiredPermission = requirement.Permission.Split(":");
            if (!(permissions?.Any() ?? false))
            {
                context.Fail();
                return Task.CompletedTask;
            }
            string module = requiredPermission!.First();
            string access = requiredPermission!.Length > 1 ? requiredPermission![1] : "";
            var accessArray = access.Split(",");
            if (permissions!.Any(x => x.Module == module && x.Access.Any(y => accessArray.Contains(y))))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
        catch (Exception)
        {

            context.Fail();
        }
        return Task.CompletedTask;
    }
}
