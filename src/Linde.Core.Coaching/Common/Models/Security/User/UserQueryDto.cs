namespace Linde.Core.Coaching.Common.Models.Security.User;

public record UserQueryDto(
    string Name,
    string UserName,
    string JobTitle,
    string Email,
    string EmployeeNumber,
    string Country,
    string Branch);
