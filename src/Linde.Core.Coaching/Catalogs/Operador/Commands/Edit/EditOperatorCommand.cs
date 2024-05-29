using ErrorOr;
using Linde.Domain.Coaching.Entities.Catalogs;
using Linde.Domain.Coaching.UserAggreagate.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Operador.Commands.Edit
{
    public record class EditOperatorCommand(
    Guid UserId,
    Guid PlantId,
    Guid CurrentPlantId
    ) : IRequest<ErrorOr<Guid>>;
}
