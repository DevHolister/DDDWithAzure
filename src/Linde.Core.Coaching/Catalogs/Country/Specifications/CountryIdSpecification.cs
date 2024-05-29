using Ardalis.Specification;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;

namespace Linde.Core.Coaching.Catalogs.Country.Specifications;

internal class CountryIdSpecification : Specification<Domain.Coaching.CountryAggregate.Country, CountryId>
{
    public CountryIdSpecification(IEnumerable<CountryId> ids)
    {
        Query
            .Select(c => c.Id)
            .Where(c => ids.Contains(c.Id));
    }
}
