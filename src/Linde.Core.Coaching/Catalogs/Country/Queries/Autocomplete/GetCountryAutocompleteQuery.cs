using ErrorOr;
using Linde.Core.Coaching.Common.Models.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Country.Queries.Autocomplete
{
    public record GetCountryAutocompleteQuery(
    string? FullName) : IRequest<ErrorOr<List<CountryAutocompleteDto>>>;
}
