using ErrorOr;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Security.User;
using MediatR;

namespace Linde.Core.Coaching.Security.User.Queries.GetAll;

public record GetAllUserQuery(
    int Page,
    int PageSize,
    string? FullName,
    string? userName,
    string? Role) : IRequest<ErrorOr<PaginatedListDto<UserDto>>>;
