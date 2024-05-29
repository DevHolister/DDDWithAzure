using ErrorOr;
using Linde.Core.Coaching.Catalogs.Country.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Security.User.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace Linde.Core.Coaching.Catalogs.Country.Commands.Create;

internal class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, ErrorOr<Guid>>
{
    private readonly IRepository<Domain.Coaching.CountryAggregate.Country> _countryRepository;
    private readonly ILogger<CreateCountryCommandHandler> _logger;
    public CreateCountryCommandHandler(IRepository<Domain.Coaching.CountryAggregate.Country> countryRepository, ILogger<CreateCountryCommandHandler> logger)
    {
        _countryRepository = countryRepository;
        _logger = logger;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (await _countryRepository.AnyAsync(new CountryWhereSpecification(request.Name)))
                return Errors.Country.DuplicateCountryName;
            if (await _countryRepository.AnyAsync(new CountryWhereSpecification(true, request.Code)))
                return Errors.Country.DuplicateCountryCode;

            var country = Domain.Coaching.CountryAggregate.Country.Create(
                request.Name,
                request.Code);
            country.Visible = true;
            await _countryRepository.AddAsync(country);
            await _countryRepository.SaveChangesAsync();
            return country.Id.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
        throw new NotImplementedException();
    }
}
