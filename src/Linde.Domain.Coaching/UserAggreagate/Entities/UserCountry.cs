using Linde.Domain.Coaching.Common;
using Linde.Domain.Coaching.CountryAggregate;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;

namespace Linde.Domain.Coaching.UserAggreagate.Entities;

public sealed class UserCountry : Entity<Guid>
{
    public CountryId CountryId { get; private set; }
    public Country Country { get; private set; }

    private UserCountry(Guid id, CountryId countryId)
    : base(id)
    {
        CountryId = countryId;
    }

    public static UserCountry Create(CountryId countryId)
    {
        return new(Guid.NewGuid(), countryId);
    }

    private UserCountry() { }
}
