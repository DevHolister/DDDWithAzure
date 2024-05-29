using Ardalis.Specification;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;

namespace Linde.Core.Coaching.Catalogs.Plant.Specifications;

internal class RolePlantIdSpecification : Specification<Domain.Coaching.Entities.Catalogs.UserPlant>
{
    public RolePlantIdSpecification(IEnumerable<Guid> ids)
    {
        Query
            //.Select(c => c.Id)
            .Where(c => ids.Contains(c.PlantId));
    }
}
