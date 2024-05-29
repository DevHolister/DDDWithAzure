using ErrorOr;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Models.Security.User;
using Linde.Core.Coaching.Security.User.Specifications;
using Linde.Domain.Coaching.Views;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Catalogs.Country.Queries.GetAllUsersInView;
internal class GetAllUsersInViewQueryHandler : IRequestHandler<GetAllUsersInViewQuery, ErrorOr<IEnumerable<UserQueryDto>>>
{
    private readonly ILogger<GetAllUsersInViewQueryHandler> _logger;
    private readonly IRepository<VwEmpleado> _repository;
    private readonly IMapper _mapper;

    public GetAllUsersInViewQueryHandler(ILogger<GetAllUsersInViewQueryHandler> logger,
        IRepository<VwEmpleado> repository,
        IMapper mapper)
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<IEnumerable<UserQueryDto>>> Handle(GetAllUsersInViewQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var users = await _repository.ListAsync();
            var items = _mapper.Map<List<UserQueryDto>>(users!);

            return items;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}
