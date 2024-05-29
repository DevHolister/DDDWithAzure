using ErrorOr;
using Linde.Domain.Coaching.Common.Enum;
using MediatR;

namespace Linde.Core.Coaching.Security.User.Commands.Edit;

public record EditUserCommand(
    Guid Id,
    //string Name,
    //string FirstSurname,
    //string SecondSurname,
    string FullName,
    string AccessType,
    string TypeUser,
    IEnumerable<Guid> Plants,
    IEnumerable<Guid> Roles,
    IEnumerable<Guid> Countries) : IRequest<ErrorOr<Unit>>;
