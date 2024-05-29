using ErrorOr;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Security.Role;
using MediatR;

namespace Linde.Core.Coaching.Security.Role.Queries.GetAll;

public record GetAllRolesQuery(
    int Page,
    int PageSize,
    string? Name,
    string? Description,
    string? Permission) : IRequest<ErrorOr<PaginatedListDto<RoleDto>>>;
