using ErrorOr;
using Linde.Core.Coaching.Catalogs.Country.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Security.User.Specifications;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace Linde.Core.Coaching.Catalogs.Country.Commands.Edit;

internal class EditCountryCommandHandler : IRequestHandler<EditCountryCommand, ErrorOr<Unit>>
{
    private readonly IRepository<Domain.Coaching.CountryAggregate.Country> _countryRepository;
    private readonly ILogger<EditCountryCommandHandler> _logger;
    public EditCountryCommandHandler(IRepository<Domain.Coaching.CountryAggregate.Country> countryRepository, ILogger<EditCountryCommandHandler> logger)
    {
        _countryRepository = countryRepository;
        _logger = logger;
    }

    public async Task<ErrorOr<Unit>> Handle(EditCountryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            bool hasChanges = false;
            var country = await _countryRepository.FirstOrDefaultAsync(new CountryWhereSpecification(CountryId.Create(request.countryId)));
            if (country is null)
                return Default.NotFound;

            if (await _countryRepository.AnyAsync(new CountryWhereSpecification(request.Name, CountryId.Create(request.countryId))))
                return Errors.Country.DuplicateCountryName;
            if (await _countryRepository.AnyAsync(new CountryWhereSpecification(true, request.Code, CountryId.Create(request.countryId))))
                return Errors.Country.DuplicateCountryCode;

            if (country.Name != request.Name)
            {
                country.UpdateName(request.Name);
                hasChanges = true;
            }
            if (country.Code != request.Code)
            {
                country.UpdateCode(request.Code);
                hasChanges = true;
            }

            if (hasChanges)
                await _countryRepository.SaveChangesAsync();

            //return country.Id.Value;
            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
        throw new NotImplementedException();
    }
}
