using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Divisions.Commands.Create;

public record CreateDivisionCommand
    (
        string Name
    ) : IRequest<ErrorOr<Guid>>;