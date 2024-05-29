using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Plant.Delete
{
    public record DeletePlantCommand(Guid Id) : IRequest<ErrorOr<Guid>>;
}
