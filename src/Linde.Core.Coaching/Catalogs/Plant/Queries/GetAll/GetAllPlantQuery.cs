using ErrorOr;
using Linde.Core.Coaching.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linde.Core.Coaching.Common.Models.Catalog.Plant;

namespace Linde.Core.Coaching.Catalogs.Plant.Queries.GetAll
{
    public record GetAllPlantQuery(int Page,
    int PageSize,
    string? name,
    string? bu,
    string? CountryId,
    string? Division,
    string? State,
    string? PlantId) : IRequest<ErrorOr<PaginatedListDto<PlantDTO>>>;
}
