using ErrorOr;
using Linde.Core.Coaching.Catalogs.Activities.Specifications;
using Linde.Core.Coaching.Catalogs.Divisions.Commands.Edit;
using Linde.Core.Coaching.Catalogs.Divisions.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Domain.Coaching.ActivityAggregate.ValueObjects;
using Linde.Domain.Coaching.DivisionAggregate.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Activities.Commands.Edit;

internal class EditActivityCommandHandler : IRequestHandler<EditActivityCommand, ErrorOr<Unit>>
{
    private readonly IRepository<Domain.Coaching.ActivityAggregate.Activity> _repository;
    private readonly ILogger<EditActivityCommandHandler> _logger;

    public EditActivityCommandHandler(
        IRepository<Domain.Coaching.ActivityAggregate.Activity> repository,
        ILogger<EditActivityCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ErrorOr<Unit>> Handle(EditActivityCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var activity = await _repository.FirstOrDefaultAsync(new ActivityWhereSpecification(ActivityId.Create(request.id)));

            if (activity is null)
                return Default.NotFound;

            activity.UpdateActivity(request.name.ToUpper(), request.description.ToUpper());

            await _repository.UpdateAsync(activity);

            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}
