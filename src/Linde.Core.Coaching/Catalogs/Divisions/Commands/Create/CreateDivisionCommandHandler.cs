
using ErrorOr;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Security.Permission.Specifications;
using Linde.Core.Coaching.Security.Role.Commands.Create;
using Linde.Core.Coaching.Security.Role.Specifications;
using Linde.Domain.Coaching.DivisionAggregate.ValueObjects;
using Linde.Domain.Coaching.PermissionAggregate.ValueObjects;
using Linde.Domain.Coaching.RoleAggregate.Entities;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Catalogs.Divisions.Commands.Create;

internal class CreateDivisionCommandHandler : IRequestHandler<CreateDivisionCommand, ErrorOr<Guid>>
{
    private readonly IRepository<Domain.Coaching.DivisionAggregate.Division> _repository;
    private readonly ILogger<CreateRoleCommandHandler> _logger;

    public CreateDivisionCommandHandler(
        IRepository<Domain.Coaching.DivisionAggregate.Division> repository,
        ILogger<CreateRoleCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateDivisionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var divisionId = DivisionId.Create(Guid.NewGuid());

            var division = Domain.Coaching.DivisionAggregate.Division.Create(divisionId: divisionId, name: request.Name.ToUpper());

            await _repository.AddAsync(division);

            return division.Id.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}