using Linde.Domain.Coaching.Common.Enum;

namespace Linde.Core.Coaching.Common.Models.Security.User;

public record UserDto(
    Guid Id,
    string FullName,
    //string Name,
    //string FirstSurname,
    //string SecondSurname,
    string Email,
    string UserName,
    bool Lockout,
    AccessType AccessType,
    TypeUser TypeUser,
    int ZoneId,
    string EmployeeNumber,
    IEnumerable<ItemDto> Roles,
    IEnumerable<ItemDto> Countries,
    IEnumerable<ItemDto> Plants);
