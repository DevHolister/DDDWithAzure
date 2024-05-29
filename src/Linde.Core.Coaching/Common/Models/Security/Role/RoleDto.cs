namespace Linde.Core.Coaching.Common.Models.Security.Role;

public record RoleDto(
    Guid Id,
    string Name,
    string Description,
    IEnumerable<ItemDto> Permissions);