using ErrorOr;
using Linde.Core.Coaching.Common.Models.Security.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Security.User.Queries.FindAtUsersView;

public record FindAtUsersViewQuery(string? UserName) : IRequest<ErrorOr<UserQueryDto>>;