using ErrorOr;
using Linde.Core.Coaching.Catalogs.Divisions.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Domain.Coaching.DivisionAggregate.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Catalogs.Divisions.Commands.Delete;
internal class DeleteDivisionCommandHandler : IRequestHandler<DeleteDivisionCommand, ErrorOr<Unit>>
{
    private readonly IRepository<Domain.Coaching.DivisionAggregate.Division> _repository;
    private readonly ILogger<DeleteDivisionCommandHandler> _logger;

    public DeleteDivisionCommandHandler(
        IRepository<Domain.Coaching.DivisionAggregate.Division> repository,
        ILogger<DeleteDivisionCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteDivisionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var division = await _repository.FirstOrDefaultAsync(new DivisionWhereSpecification(DivisionId.Create(request.Id)));

            if (division is null)
                return Default.NotFound;

            await _repository.DeleteAsync(division!);

            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}