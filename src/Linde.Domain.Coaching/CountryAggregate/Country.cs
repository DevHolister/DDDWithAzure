using Linde.Domain.Coaching.Common;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;
using Linde.Domain.Coaching.Entities.Catalogs;
using Linde.Domain.Coaching.UserAggreagate.ValueObjects;

namespace Linde.Domain.Coaching.CountryAggregate;

public sealed class Country : Entity<CountryId>, IAggregateRoot
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string OriginalCode { get; private set; }
    public List<Plant> Plants { get; private set; }
    private Country(
        CountryId countryId,
        string name,
        string code) : base(countryId)
    {
        Name = name;
        Code = code;
        OriginalCode = code;
    }

    public static Country Create(
        string name,
        string code)
    {
        return new(
            CountryId.CreateUnique(),
            name,
            code);
    }

    public void UpdateName(string name)
    {
        Name = name;
    }

    public void UpdateCode(string code)
    {
        Code = code;
    }

    private Country()
    {
        Visible = true;
    }
}
