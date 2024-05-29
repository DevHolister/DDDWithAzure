using ErrorOr;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Interfaces.Services;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Security.User;
using Linde.Core.Coaching.Security.User.Specifications;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Security.User.Queries.GetAll;

internal class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, ErrorOr<PaginatedListDto<UserDto>>>
{
    private readonly IRepository<Domain.Coaching.UserAggreagate.User> _repository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<GetAllUserQuery> _logger;

    public GetAllUserQueryHandler(
        IRepository<Domain.Coaching.UserAggreagate.User> repository,
        IMapper mapper,
        ICurrentUserService currentUserService,
        ILogger<GetAllUserQuery> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _logger = logger;
    }

    public async Task<ErrorOr<PaginatedListDto<UserDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (!Guid.TryParse(_currentUserService.CountryId, out var countryId))
                return Default.UnauthorizedError;

            var users = await _repository.ListAsync(new UserMapSpecification(
                request.FullName!,
                request.Role!,
                request.userName!,
                CountryId.Create(countryId),
                request.Page,
                request.PageSize,
                true), cancellationToken);

            var total = await _repository.CountAsync(new UserMapSpecification(
                request.FullName!,
                request.Role!,
                request.userName!,
                CountryId.Create(countryId)), cancellationToken);

            return new PaginatedListDto<UserDto>(total, request.PageSize, request.Page, users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}
