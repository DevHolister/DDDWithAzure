using ErrorOr;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Catalog.Division;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Divisions.Queries.GetAll
{
    public record GetAllDivisionsQuery
    (
        int Page,
        int PageSize,
        string? name
    ): IRequest<ErrorOr<PaginatedListDto<DivisionDto>>>;
}
 