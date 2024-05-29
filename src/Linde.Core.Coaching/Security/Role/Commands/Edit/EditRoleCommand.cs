using ErrorOr;
using MediatR;

namespace Linde.Core.Coaching.Security.Role.Commands.Edit;

public record EditRoleCommand(
    Guid Id,
    string Name,
    string Description,
    IEnumerable<Guid> Permissions) : IRequest<ErrorOr<Unit>>;
