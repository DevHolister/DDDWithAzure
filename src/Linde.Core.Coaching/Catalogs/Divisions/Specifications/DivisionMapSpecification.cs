using Ardalis.Specification;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Catalog.Division;
using Linde.Core.Coaching.Common.Models.Security.Role;
using Linde.Domain.Coaching.DivisionAggregate.ValueObjects;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Divisions.Specifications;

internal class DivisionMapSpecification : Specification<Domain.Coaching.DivisionAggregate.Division, DivisionDto>
{
    public DivisionMapSpecification(string name, int page = 1, int pageSize = 20, bool pagination = false)
    {
        if (pagination)
        {
            Query
                .Select(x => new DivisionDto(
                    x.Id.Value,
                    x.Name
                   ))
                .AsNoTracking()
                .Skip((--page) * pageSize)
                .Take(pageSize);
        }

        //Query.Where(x => x.Visible);
    }

    public DivisionMapSpecification(DivisionId id)
    {
        Query
            .Select(x => new DivisionDto(
                x.Id.Value,
                x.Name
                ))
            .AsNoTracking();
        Query.Where(x => x.Id == id && x.Visible);
    }
}
