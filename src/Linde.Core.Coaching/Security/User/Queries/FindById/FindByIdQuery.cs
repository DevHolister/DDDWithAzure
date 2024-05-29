using ErrorOr;
using Linde.Core.Coaching.Common.Models.Security.User;
using MediatR;

namespace Linde.Core.Coaching.Security.User.Queries.FindById;

public class FindByIdQuery : IRequest<ErrorOr<UserDto>>
{
    public Guid UserId { get; set; }
}