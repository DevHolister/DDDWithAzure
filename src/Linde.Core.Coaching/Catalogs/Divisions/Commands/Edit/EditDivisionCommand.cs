using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Divisions.Commands.Edit;

public record EditDivisionCommand(
    Guid Id,
    string name
    ) : IRequest<ErrorOr<Unit>>;