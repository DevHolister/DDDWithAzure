using ErrorOr;
using Linde.Core.Coaching.Common.Models.Security.User;
using MediatR;

namespace Linde.Core.Coaching.Security.User.Queries.FindAtAD;

public record FindUserAtADQuery(string? UserName, string? EmployeeNumber) : IRequest<ErrorOr<UserQueryDto>>;
