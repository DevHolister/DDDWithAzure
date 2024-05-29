using ErrorOr;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Catalog.Operador;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Operador.Queries.GetAll
{
    public record GetAllOperadorQuery(int Page,
    int PageSize,
    string? name,
    string? lindeId,
    string? noEmployee,
    string? country,
    string? plant): IRequest<ErrorOr<PaginatedListDto<OperadorDto>>>;
}
