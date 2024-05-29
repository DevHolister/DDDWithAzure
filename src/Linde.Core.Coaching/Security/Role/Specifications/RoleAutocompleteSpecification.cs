using Ardalis.Specification;
using Linde.Core.Coaching.Common.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Security.Role.Specifications;

internal class RoleAutocompleteSpecification : Specification<Domain.Coaching.RoleAggregate.Role>
{
    public RoleAutocompleteSpecification(string name)
    {
        Query.Where(x => x.Name.ToUpper().Contains(name.ToUpper()) && x.Visible);
    }
}
