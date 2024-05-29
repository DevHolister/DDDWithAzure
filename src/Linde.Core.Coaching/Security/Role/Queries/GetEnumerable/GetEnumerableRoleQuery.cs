using ErrorOr;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Security.Role;
using MediatR;

namespace Linde.Core.Coaching.Security.Role.Queries.GetEnumerable;

public record GetEnumerableRoleQuery() : IRequest<ErrorOr<IEnumerable<ItemDto>>>;