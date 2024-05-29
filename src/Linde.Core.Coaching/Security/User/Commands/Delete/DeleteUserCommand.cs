using ErrorOr;
using MediatR;

namespace Linde.Core.Coaching.Security.User.Commands.Delete;

public class DeleteUserCommand : IRequest<ErrorOr<Unit>>
{
    public Guid UserId { get; set; }
}