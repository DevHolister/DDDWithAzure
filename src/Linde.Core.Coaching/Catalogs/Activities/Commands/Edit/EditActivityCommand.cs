﻿using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Activities.Commands.Edit;

public record EditActivityCommand(Guid id, string name, string description
    ): IRequest<ErrorOr<Unit>>;
