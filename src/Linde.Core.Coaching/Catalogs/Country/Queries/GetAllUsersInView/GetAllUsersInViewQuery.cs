using ErrorOr;
using Linde.Core.Coaching.Common.Models.Security.User;
using MediatR;

namespace Linde.Core.Coaching.Catalogs.Country.Queries.GetAllUsersInView;

public record class GetAllUsersInViewQuery : IRequest<ErrorOr<IEnumerable<UserQueryDto>>>;