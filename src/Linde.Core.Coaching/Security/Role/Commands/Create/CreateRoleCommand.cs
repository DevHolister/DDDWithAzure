using ErrorOr;
using MediatR;

namespace Linde.Core.Coaching.Security.Role.Commands.Create;

public record CreateRoleCommand(
    string Name,
    string Description,
    IEnumerable<Guid> Permissions) : IRequest<ErrorOr<Guid>>;
