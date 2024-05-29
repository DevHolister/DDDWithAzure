using Ardalis.Specification;
using Linde.Core.Coaching.Common.Models.Catalog.Activity;
using Linde.Core.Coaching.Common.Models.Catalog.Division;
using Linde.Domain.Coaching.ActivityAggregate.ValueObjects;
using Linde.Domain.Coaching.DivisionAggregate.ValueObjects;


namespace Linde.Core.Coaching.Catalogs.Activities.Specifications;

internal class ActivityMapSpecification : Specification<Domain.Coaching.ActivityAggregate.Activity, ActivityDTO>
{
    public ActivityMapSpecification(string name, string description, int page = 1, int pageSize = 20, bool pagination = false)
    {
        if (pagination)
        {
            Query
                .Select(x => new ActivityDTO(
                    x.Id.Value,
                    x.Name,
                    x.Description
                   ))
                .AsNoTracking()
                .Skip((--page) * pageSize)
                .Take(pageSize);
            Query.Where(x => name.Contains(name));
        }

        //Query.Where(x => x.Visible);
    }

    public ActivityMapSpecification(ActivityId id)
    {
        Query
            .Select(x => new ActivityDTO(
                x.Id.Value,
                x.Name,
                x.Description
                ))
            .AsNoTracking();
        Query.Where(x => x.Id == id && x.Visible);
    }
}
