using ErrorOr;
using Linde.Core.Coaching.Catalogs.Country.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Interfaces.Services;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Catalogs.Country;
using Linde.Core.Coaching.Security.User.Specifications;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Catalogs.Country.Queries.GetAll;

internal class GetAllCountryQueryHandler : IRequestHandler<GetAllCountryQuery, ErrorOr<PaginatedListDto<CountryDto>>>
{
    private readonly IRepository<Domain.Coaching.CountryAggregate.Country> _repository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<GetAllCountryQuery> _logger;

    public GetAllCountryQueryHandler(
        IRepository<Domain.Coaching.CountryAggregate.Country> repository,
        IMapper mapper,
        ICurrentUserService currentUserService,
        ILogger<GetAllCountryQuery> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _logger = logger;
    }

    public async Task<ErrorOr<PaginatedListDto<CountryDto>>> Handle(GetAllCountryQuery request, CancellationToken cancellationToken)
    {
        try
        {
            //if (!Guid.TryParse(_currentUserService.CountryId, out var countryId))
            //    return Default.UnauthorizedError;

            var countries = await _repository.ListAsync(new CountryMapSpecification(
                request.Name!,
                request.Code!,
                request.Page,
                request.PageSize,
                true), cancellationToken);

            var total = await _repository.CountAsync(new CountryMapSpecification(
                request.Name!, request.Code!), cancellationToken);

            return new PaginatedListDto<CountryDto>(total, request.PageSize, request.Page, countries);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}
