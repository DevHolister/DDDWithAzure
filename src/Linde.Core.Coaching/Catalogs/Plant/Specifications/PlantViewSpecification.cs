using Ardalis.Specification;
using Linde.Domain.Coaching.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Plant.Specifications
{
    internal class PlantViewSpecification : Specification<VwPlantas>
    {
        public PlantViewSpecification()
        {
            Query
                .AsNoTracking();
        }
        public PlantViewSpecification(string buName)
        {
            Query
                .Where(x => x.Sucursal!.ToUpper().Contains(buName.ToUpper()) || x.NumSucursal!.ToString().ToUpper().Contains(buName.ToUpper()))
                .AsNoTracking();
        }
    }
}
