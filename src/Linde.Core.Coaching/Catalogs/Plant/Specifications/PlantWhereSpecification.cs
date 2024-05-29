using Ardalis.Specification;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;
using Linde.Domain.Coaching.Entities.Catalogs;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Plant.Specifications
{
    public class PlantWhereSpecification : Specification<Linde.Domain.Coaching.Entities.Catalogs.Plant>
    {
        public PlantWhereSpecification(string name)
        {
            Query
                .Where(x => x.Name.ToUpper().Contains(name.Trim().ToUpper()));
        }

        public PlantWhereSpecification(Guid id)
        {
            Query
                .Where(x => x.PlantId == id);
        }

        public PlantWhereSpecification(string name, string? countryid = null)
        {
            if (countryid != null)
            {
                Query
                .Where(x => x.Name.ToUpper().Contains(name.Trim().ToUpper()) && x.CountryId == CountryId.Create(Guid.Parse(countryid)));

            }
            else {
                Query
                .Where(x => x.Name.ToUpper().Contains(name.Trim().ToUpper()));
            }
            
        }
    }
}
