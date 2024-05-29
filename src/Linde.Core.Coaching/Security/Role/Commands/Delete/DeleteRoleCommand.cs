using ErrorOr;
using MediatR;

namespace Linde.Core.Coaching.Security.Role.Commands.Delete;

public record DeleteRoleCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
