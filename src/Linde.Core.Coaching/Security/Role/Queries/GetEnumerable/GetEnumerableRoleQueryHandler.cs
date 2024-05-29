using ErrorOr;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Catalogs.Country;
using Linde.Core.Coaching.Common.Models.Security.Role;
using Linde.Core.Coaching.Security.Role.Specifications;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Security.Role.Queries.GetEnumerable;

internal class GetEnumerableRoleQueryHandler : IRequestHandler<GetEnumerableRoleQuery, ErrorOr<IEnumerable<ItemDto>>>
{
    private readonly IRepository<Domain.Coaching.RoleAggregate.Role> _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetEnumerableRoleQueryHandler> _logger;

    public GetEnumerableRoleQueryHandler(IRepository<Domain.Coaching.RoleAggregate.Role> repository,
        IMapper mapper,
        ILogger<GetEnumerableRoleQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<ErrorOr<IEnumerable<ItemDto>>> Handle(GetEnumerableRoleQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var userRoles = await _repository.ListAsync();
            var items = _mapper.Map<List<ItemDto>>(userRoles);
            return items;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}