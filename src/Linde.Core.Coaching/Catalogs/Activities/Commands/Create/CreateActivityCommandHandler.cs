using ErrorOr;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Security.Role.Commands.Create;
using Linde.Domain.Coaching.ActivityAggregate.ValueObjects;
using Linde.Domain.Coaching.DivisionAggregate.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Catalogs.Activities.Commands.Create;

internal class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, ErrorOr<Guid>>
{
    private readonly IRepository<Domain.Coaching.ActivityAggregate.Activity> _repository;
    private readonly ILogger<CreateActivityCommandHandler> _logger;

    public CreateActivityCommandHandler(
        IRepository<Domain.Coaching.ActivityAggregate.Activity> repository,
        ILogger<CreateActivityCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    public async Task<ErrorOr<Guid>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var activityId = ActivityId.Create(Guid.NewGuid());

            var activity = Domain.Coaching.ActivityAggregate.Activity.Create(activityId: activityId, name: request.Name.ToUpper(), description: request.Description.ToUpper());

            await _repository.AddAsync(activity);

            return activity.Id.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}
