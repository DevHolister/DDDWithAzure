using Ardalis.Specification;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;
using System.Xml.Linq;

namespace Linde.Core.Coaching.Catalogs.Country.Specifications;

internal class CountryWhereSpecification : Specification<Domain.Coaching.CountryAggregate.Country>
{
    public CountryWhereSpecification(CountryId Id)
    {
        Query.Where(x => x.Id == Id);
    }
    public CountryWhereSpecification(string name)
    {
        Query.Where(x => x.Name.ToUpper().Contains(name.Trim().ToUpper()));
    }
    public CountryWhereSpecification(bool OnlyCode, string Code)
    {
        if (OnlyCode)
        {
            Query.Where(x => x.Code == Code);
        }
    }
    public CountryWhereSpecification(string Name, CountryId Id)
    {
        Query.Where(x => x.Name == Name && x.Id != Id);
    }
    public CountryWhereSpecification(bool OnlyCode, string Code, CountryId Id)
    {
        if (OnlyCode)
        {
            Query.Where(x => x.Code == Code && x.Id != Id);
        }
    }

}
