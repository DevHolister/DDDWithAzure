using ErrorOr;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Models.Catalogs.Country;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Catalogs.Country.Queries.GetEnumerable;

internal class GetEnumerableCountryQueryHandler : IRequestHandler<GetEnumerableCountryQuery, ErrorOr<IEnumerable<CountryDto>>>
{
    private readonly IRepository<Domain.Coaching.CountryAggregate.Country> _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetEnumerableCountryQueryHandler> _logger;

    public GetEnumerableCountryQueryHandler(IRepository<Domain.Coaching.CountryAggregate.Country> repository,
        IMapper mapper,
        ILogger<GetEnumerableCountryQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<ErrorOr<IEnumerable<CountryDto>>> Handle(GetEnumerableCountryQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var userCountries = await _repository.ListAsync();
            var items = _mapper.Map<List<CountryDto>>(userCountries.Where(x => x.Visible == true));
            return items;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}