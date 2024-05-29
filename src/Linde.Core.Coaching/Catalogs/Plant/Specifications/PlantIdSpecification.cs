using Ardalis.Specification;

namespace Linde.Core.Coaching.Catalogs.Plant.Specifications;

internal class PlantIdSpecification : Specification<Domain.Coaching.Entities.Catalogs.Plant>
{
    public PlantIdSpecification(IEnumerable<Guid> ids)
    {
        Query
            //.Select(c => c.Id)
            .Where(c => ids.Contains(c.PlantId));
    }
}
