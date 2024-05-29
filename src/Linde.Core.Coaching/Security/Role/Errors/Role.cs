using ErrorOr;

namespace Linde.Core.Coaching.Security.Role.Errors;

public static class Role
{
    public static Error DuplicateRoleName => Error.Conflict(
        code: "Role.DuplicateRoleName",
        description: "Ya existe un rol con el mismo nombre.");

    public static Error InvalidPermission => Error.Conflict(
        code: "Role.InvalidPermission",
        description: "Se encontró un permiso no válido.");
    public static Error HasUser => Error.Conflict(
        code: "Role.HasUser",
        description: "El rol cuenta con usuario relacionados.");
}
