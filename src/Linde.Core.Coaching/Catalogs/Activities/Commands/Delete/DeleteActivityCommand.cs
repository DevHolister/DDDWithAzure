using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Activities.Commands.Delete;

public record DeleteActivityCommand(Guid id) : IRequest<ErrorOr<Unit>>;