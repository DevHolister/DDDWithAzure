using ErrorOr;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Models.Security.User;
using Linde.Core.Coaching.Security.User.Specifications;
using Linde.Domain.Coaching.UserAggreagate.ValueObjects;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Security.User.Queries.FindById;

internal class FindByIdQueryHandler : IRequestHandler<FindByIdQuery, ErrorOr<UserDto>>
{
    private readonly IRepository<Domain.Coaching.UserAggreagate.User> _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<FindByIdQueryHandler> _logger;

    public FindByIdQueryHandler(IRepository<Domain.Coaching.UserAggreagate.User> repository, IMapper mapper, ILogger<FindByIdQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<ErrorOr<UserDto>> Handle(FindByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _repository.FirstOrDefaultAsync(new UserWhereSpecification(UserId.Create(request.UserId!), true));
            if (user is null)
                return Default.NotFound;
            var item = _mapper.Map<UserDto>(user);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}
