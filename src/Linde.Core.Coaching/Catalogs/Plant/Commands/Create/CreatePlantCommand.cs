using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Plant.Commands.Create
{
    public record class CreatePlantCommand(
        string Name,
        string Bu,
        string CountryId,
        string Division,
        string State,
        string City,
        string Municipality,
        string SuperintendentId,
        string PlantManagerId
        ) : IRequest<ErrorOr<Guid>>;
}
