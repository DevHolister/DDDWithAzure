using ErrorOr;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Security.Permission.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Security.Permission.Queries.GetAll;

internal class GetAllPermissionQueryHandler : IRequestHandler<GetAllPermissionQuery, ErrorOr<IEnumerable<ItemDto>>>
{
    private readonly IRepository<Domain.Coaching.PermissionAggregate.Permission> _repository;
    private readonly ILogger<GetAllPermissionQueryHandler> _logger;

    public GetAllPermissionQueryHandler(
        IRepository<Domain.Coaching.PermissionAggregate.Permission> repository,
        ILogger<GetAllPermissionQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ErrorOr<IEnumerable<ItemDto>>> Handle(GetAllPermissionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var permissions = await _repository.ListAsync(new PermissionMapSpecification());
            return permissions;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}
