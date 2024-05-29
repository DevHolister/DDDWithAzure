using Ardalis.Specification;
using Linde.Core.Coaching.Common.Models.Catalogs.Country;

namespace Linde.Core.Coaching.Catalogs.Country.Specifications;

internal class CountryMapSpecification : Specification<Domain.Coaching.CountryAggregate.Country, CountryDto>
{
    public CountryMapSpecification(string name, string code, int page = 1, int pageSize = 20, bool pagination = false)
    {
        if (pagination)
        {
            Query
                .Select(x => new CountryDto(
                    x.Id.Value,
                    x.Name,
                    x.Code))
                .AsNoTracking()
                .Skip((--page) * pageSize)
                .Take(pageSize);
        }

        Query.Where(x => x.Visible);

        if (!string.IsNullOrEmpty(name?.Trim()))
        {
            Query.Where(x => x.Name.ToUpper().Contains(name.Trim().ToUpper()));
        }

        if (!string.IsNullOrEmpty(code?.Trim()))
        {
            Query.Where(x => x.Code.ToUpper().Contains(code.Trim().ToUpper()));
        }
    }
}
