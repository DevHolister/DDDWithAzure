using ErrorOr;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Security.User;
using Linde.Core.Coaching.Security.Role.Specifications;
using Linde.Core.Coaching.Security.User.Queries.AutocompleteView;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Security.Role.Queries.Autocomplete;

internal class GetRoleAutocompleteQueryHandler : IRequestHandler<GetRoleAutocompleteQuery, ErrorOr<List<ItemDto>>>
{
    private readonly IRepository<Domain.Coaching.RoleAggregate.Role> _repository;
    private readonly ILogger<GetRoleAutocompleteQueryHandler> _logger;
    private readonly IMapper _mapper;
    public GetRoleAutocompleteQueryHandler(IRepository<Domain.Coaching.RoleAggregate.Role> repository,
        ILogger<GetRoleAutocompleteQueryHandler> logger,
        IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<ErrorOr<List<ItemDto>>> Handle(GetRoleAutocompleteQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var roles = await _repository.ListAsync(new RoleAutocompleteSpecification(
                request.name!), cancellationToken);
            var items = _mapper.Map<List<ItemDto>>(roles!);

            return items;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}
