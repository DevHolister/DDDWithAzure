using ErrorOr;
using Linde.Core.Coaching.Catalogs.Country.Specifications;
using Linde.Core.Coaching.Catalogs.Divisions.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Domain.Coaching.DivisionAggregate.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Catalogs.Divisions.Commands.Edit;

internal class EditDivisionCommandHandler : IRequestHandler<EditDivisionCommand, ErrorOr<Unit>>
{
    private readonly IRepository<Domain.Coaching.DivisionAggregate.Division> _repository;
    private readonly ILogger<EditDivisionCommandHandler> _logger;

    public EditDivisionCommandHandler(
        IRepository<Domain.Coaching.DivisionAggregate.Division> repository,
        ILogger<EditDivisionCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ErrorOr<Unit>> Handle(EditDivisionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var division = await _repository.FirstOrDefaultAsync(new DivisionWhereSpecification(DivisionId.Create(request.Id)));
            
            if (division is null)
                return Default.NotFound;

            division.UpdateName(request.name.ToUpper());

            await _repository.UpdateAsync(division);

            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}