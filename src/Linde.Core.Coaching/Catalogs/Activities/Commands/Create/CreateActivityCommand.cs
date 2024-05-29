using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Activities.Commands.Create;

public record CreateActivityCommand
(
    string Name,
    string Description
    ): IRequest<ErrorOr<Guid>>;
