using ErrorOr;
using Linde.Core.Coaching.Common.Models.Security.User;
using Linde.Core.Coaching.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linde.Core.Coaching.Common.Models.Common;

namespace Linde.Core.Coaching.Security.User.Queries.Autocomplete
{
    public record GetUserAutocompleteQuery(
    string? FullName) : IRequest<ErrorOr<List<UserAutocompleteDto>>>;
}
