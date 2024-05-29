using ErrorOr;
using Linde.Core.Coaching.Common.Models.Catalog.Operador;
using Linde.Core.Coaching.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Operador.Queries.GetOperador
{
    public record GetOperadorQuery(
    string? UserId,
    string? PlantId) : IRequest<ErrorOr<PaginatedListDto<OperadorDto>>>;
}
