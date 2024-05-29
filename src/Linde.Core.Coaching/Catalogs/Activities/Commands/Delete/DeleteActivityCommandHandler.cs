using ErrorOr;
using Linde.Core.Coaching.Catalogs.Activities.Specifications;
using Linde.Core.Coaching.Catalogs.Divisions.Commands.Delete;
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

namespace Linde.Core.Coaching.Catalogs.Activities.Commands.Delete;

internal class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, ErrorOr<Unit>>
{
    private readonly IRepository<Domain.Coaching.ActivityAggregate.Activity> _repository;
    private readonly ILogger<DeleteActivityCommandHandler> _logger;

    public DeleteActivityCommandHandler(
        IRepository<Domain.Coaching.ActivityAggregate.Activity> repository,
        ILogger<DeleteActivityCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    public async Task<ErrorOr<Unit>> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var activity = await _repository.FirstOrDefaultAsync(new ActivityWhereSpecification(ActivityId.Create(request.id)));

            if (activity is null)
                return Default.NotFound;

            await _repository.DeleteAsync(activity!);

            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}