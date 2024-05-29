using ErrorOr;
using Linde.Core.Coaching.Common.Models.Security.User;
using MediatR;

namespace Linde.Core.Coaching.Security.User.Commands.Authenticate;

public record AuthenticateCommand(string UserName, string Password) : IRequest<ErrorOr<AuthenticateResponse>>;
