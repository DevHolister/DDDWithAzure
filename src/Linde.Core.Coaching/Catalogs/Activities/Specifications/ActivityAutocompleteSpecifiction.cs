using Ardalis.Specification;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Catalog.Activity;
using Linde.Domain.Coaching.ActivityAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Activities.Specifications
{
    public class ActivityAutocompleteSpecifiction : Specification<Domain.Coaching.ActivityAggregate.Activity, ItemDto>
    {
        public ActivityAutocompleteSpecifiction(string name)
        {
            Query
                .Select(x => new ItemDto(
                    x.Id.Value,
                    x.Name
                    ))
                .AsNoTracking();
            Query.Where(x => x.Name.Contains(name) && x.Visible);
        }
    }
}
