using ErrorOr;
using Linde.Core.Coaching.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Security.Role.Queries.Autocomplete;

public record class GetRoleAutocompleteQuery(string name) : IRequest<ErrorOr<List<ItemDto>>>;
