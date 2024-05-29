using ErrorOr;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;
using Linde.Domain.Coaching.UserAggreagate.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Operador.Commands.Delete
{
    public record DeleteOperadorCommand(UserId UserId) : IRequest<ErrorOr<Unit>>;    
}
