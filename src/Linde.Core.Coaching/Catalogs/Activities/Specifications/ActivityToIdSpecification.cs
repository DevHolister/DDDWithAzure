using Ardalis.Specification;
using Linde.Domain.Coaching.ActivityAggregate.ValueObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Activities.Specifications
{
    internal class ActivityToIdSpecification : Specification<Domain.Coaching.ActivityAggregate.Activity, ActivityId>
    {

        public ActivityToIdSpecification(IEnumerable<ActivityId> ids)
        {
            Query
                .Select(x => x.Id)
                .Where(x => ids.Contains(x.Id));
        }
    }
}
