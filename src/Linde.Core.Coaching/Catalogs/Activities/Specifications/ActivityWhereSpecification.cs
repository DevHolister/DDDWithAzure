using Ardalis.Specification;
using Linde.Domain.Coaching.ActivityAggregate.ValueObjects;
using Linde.Domain.Coaching.DivisionAggregate.ValueObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Activities.Specifications;

internal class ActivityWhereSpecification : Specification<Domain.Coaching.ActivityAggregate.Activity>
{
    public ActivityWhereSpecification(ActivityId Id)
    {

        Query.Where(x => x.Id == Id);
    }

}