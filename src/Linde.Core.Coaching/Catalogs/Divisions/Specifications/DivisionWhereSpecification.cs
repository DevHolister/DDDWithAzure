using Ardalis.Specification;
using Linde.Domain.Coaching.DivisionAggregate.ValueObjects;

namespace Linde.Core.Coaching.Catalogs.Divisions.Specifications;
internal class DivisionWhereSpecification : Specification<Domain.Coaching.DivisionAggregate.Division>
{
    public DivisionWhereSpecification(DivisionId Id)
    {
        
        Query.Where(x => x.Id == Id);
    }

}