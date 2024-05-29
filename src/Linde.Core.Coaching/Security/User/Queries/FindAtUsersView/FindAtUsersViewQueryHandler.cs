using ErrorOr;
using Linde.Core.Coaching.Catalogs.Country.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Interfaces.Services;
using Linde.Core.Coaching.Common.Models.Security.User;
using Linde.Core.Coaching.Security.User.Specifications;
using Linde.Domain.Coaching.Views;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Security.User.Queries.FindAtUsersView;

internal class FindAtUsersViewQueryHandler : IRequestHandler<FindAtUsersViewQuery, ErrorOr<UserQueryDto>>
{
    private readonly ILogger<FindAtUsersViewQueryHandler> _logger;
    private readonly IRepository<VwEmpleado> _repository;
    private readonly IMapper _mapper;
    private readonly IRepository<Domain.Coaching.CountryAggregate.Country> _countryRepository;

    public FindAtUsersViewQueryHandler(
        ILogger<FindAtUsersViewQueryHandler> logger,
        IRepository<VwEmpleado> repository,
        IMapper mapper,
        IRepository<Domain.Coaching.CountryAggregate.Country> countryRepository)
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
        _countryRepository = countryRepository;
    }
    public async Task<ErrorOr<UserQueryDto>> Handle(FindAtUsersViewQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(request.UserName?.Trim()))
                return Default.ValidationError;

            var user = await _repository.FirstOrDefaultAsync(new UserViewEspecification(request.UserName));
            if (user.CodeCountry != null)
            {
                var country = await _countryRepository.FirstOrDefaultAsync(new CountryWhereSpecification(true, user.CodeCountry));
                if (country != null)
                {
                    user.CodeCountry = country.Id.ToString();
                }
            }

            var item = _mapper.Map<UserQueryDto>(user!);

            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}
