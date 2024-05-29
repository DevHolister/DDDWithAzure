namespace Linde.Core.Coaching.Common.Models.Security.Role;

public record PermissionAccessDto(string Module, string Actions)
{
    public IEnumerable<string> Access => Actions.Split(",")
        .Where(x => !string.IsNullOrEmpty(x))
        .Select(x => x.Trim());
}
