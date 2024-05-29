using ErrorOr;
using Linde.Core.Coaching.Common.Models.Catalog.Activity;
using Linde.Core.Coaching.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Activities.Queries.Autocomplete
{
    public record ActivityAutocompleteQuery(string name) : IRequest<ErrorOr<List<ItemDto>>>;
}
