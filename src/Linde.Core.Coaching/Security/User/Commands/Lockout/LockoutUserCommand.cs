using ErrorOr;
using MediatR;

namespace Linde.Core.Coaching.Security.User.Commands.Lockout;

public record LockoutUserCommand(Guid Id, bool Lockout) : IRequest<ErrorOr<Unit>>;
