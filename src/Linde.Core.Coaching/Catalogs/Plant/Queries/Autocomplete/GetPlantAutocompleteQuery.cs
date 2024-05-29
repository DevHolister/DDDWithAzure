using ErrorOr;
using Linde.Core.Coaching.Common.Models.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Plant.Queries.Autocomplete
{
    public record GetPlantAutocompleteQuery(
    string? FullName,
    string? CountryId) : IRequest<ErrorOr<List<PlantAutocompleteDto>>>;
}
