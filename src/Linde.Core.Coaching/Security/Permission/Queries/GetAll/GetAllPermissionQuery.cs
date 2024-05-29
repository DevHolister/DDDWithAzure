using ErrorOr;
using Linde.Core.Coaching.Common.Models;
using MediatR;

namespace Linde.Core.Coaching.Security.Permission.Queries.GetAll;

public record GetAllPermissionQuery() : IRequest<ErrorOr<IEnumerable<ItemDto>>>;
