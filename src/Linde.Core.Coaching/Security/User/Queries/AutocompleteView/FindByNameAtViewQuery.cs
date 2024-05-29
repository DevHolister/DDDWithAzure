using ErrorOr;
using Linde.Core.Coaching.Common.Models.Security.User;
using MediatR;

namespace Linde.Core.Coaching.Security.User.Queries.AutocompleteView;

public record FindByNameAtViewQuery(string? fullName) : IRequest<ErrorOr<List<UserQueryDto>>>;