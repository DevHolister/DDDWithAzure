using ErrorOr;
using MediatR;

namespace Linde.Core.Coaching.Security.User.Commands.Create;

public record class CreateUserCommand(
    //string Name,
    //string FirstSurname,
    //string SecondSurname,
    string FullName,
    string UserName,
    string Email,
    string AccessType,
    string TypeUser,
    string ZoneId,
    string EmployeeNumber,
    //IEnumerable<Guid> Roles,
    Guid Role,
    Guid PlantId,
    Guid CountryId) : IRequest<ErrorOr<Guid>>;
