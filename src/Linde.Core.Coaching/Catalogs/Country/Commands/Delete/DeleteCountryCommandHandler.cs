using ErrorOr;
using Linde.Core.Coaching.Catalogs.Country.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Security.User.Specifications;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Catalogs.Country.Commands.Delete;

internal class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, ErrorOr<Unit>>
{
    private readonly IRepository<Domain.Coaching.CountryAggregate.Country> _countryRepository;
    private readonly ILogger<DeleteCountryCommandHandler> _logger;
    private readonly IRepository<Domain.Coaching.UserAggreagate.User> _usersRepository;

    public DeleteCountryCommandHandler(IRepository<Domain.Coaching.CountryAggregate.Country> countryRepository, ILogger<DeleteCountryCommandHandler> logger, IRepository<Domain.Coaching.UserAggreagate.User> usersRepository)
    {
        _countryRepository = countryRepository;
        _logger = logger;
        _usersRepository = usersRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            bool usersAsigned = false;
            var country = await _countryRepository.FirstOrDefaultAsync(new CountryWhereSpecification(CountryId.Create(request.CountryId)));
            if (country is null)
                return Default.NotFound;

            var usersWithCountry = await _usersRepository.ListAsync(new UserWhereSpecification(string.Empty, string.Empty, country.Id, 1, 20, true));
            if (usersWithCountry.Count > 0)
            {
                usersAsigned = true;
                return Errors.Country.UsersWithCountryAsigned;
            }

            await _countryRepository.DeleteAsync(country);
            await _countryRepository.SaveChangesAsync();

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
