using ErrorOr;
using Linde.Core.Coaching.Catalogs.Country.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Models.Catalogs.Country;
using Linde.Core.Coaching.Security.User.Specifications;
using Linde.Domain.Coaching.Views;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Catalogs.Country.Queries.FindAtUsersView;

internal class FindAtUsersViewQueryHandler : IRequestHandler<FindAtUsersViewQuery, ErrorOr<CountryDto>>
{
    private readonly ILogger<FindAtUsersViewQueryHandler> _logger;
    private readonly IRepository<VwEmpleado> _repositoryView;
    private readonly IMapper _mapper;
    private readonly IRepository<Domain.Coaching.CountryAggregate.Country> _countryRepository;

    public FindAtUsersViewQueryHandler(
        ILogger<FindAtUsersViewQueryHandler> logger,
        IRepository<VwEmpleado> repositoryView,
        IMapper mapper,
        IRepository<Domain.Coaching.CountryAggregate.Country> countryRepository)
    {
        _logger = logger;
        _repositoryView = repositoryView;
        _mapper = mapper;
        _countryRepository = countryRepository;
    }
    public async Task<ErrorOr<CountryDto>> Handle(FindAtUsersViewQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Code?.Trim()))
                return Default.ValidationError;

            var findCodeCountry = await _repositoryView.FirstOrDefaultAsync(new UserViewEspecification(new CountryDto(Guid.NewGuid(), string.Empty, request.Code)));
            if (findCodeCountry == null)
            {
                return Default.NotFound;
            }

            var country = await _countryRepository.FirstOrDefaultAsync(new CountryWhereSpecification(true, findCodeCountry.CodeCountry!));
            if (country != null)
            {
                return Errors.Country.CountryCodePreviousAsigned;
            }

            CountryDto item = new(Guid.Empty, string.Empty, request.Code);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}
