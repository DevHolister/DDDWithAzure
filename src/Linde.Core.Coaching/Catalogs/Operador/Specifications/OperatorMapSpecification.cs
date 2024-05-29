using Ardalis.Specification;
using Linde.Core.Coaching.Common.Models.Security.User;
using Linde.Core.Coaching.Common.Models;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linde.Core.Coaching.Common.Models.Catalog.Operador;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Catalogs.Country.Specifications;

namespace Linde.Core.Coaching.Catalogs.Operador.Specifications
{
    internal class OperatorMapSpecification : Specification<Domain.Coaching.UserAggreagate.User>
    {
        public OperatorMapSpecification(string name,
            string lindeId,
            string noEmployee,
            string country,
            string plant,
            int page = 1,
            int pageSize = 20,
            bool pagination = false)
        {
            if (pagination)
            {
                Query
                    .AsNoTracking()
                    .Include(r => r.CountryItems)
                    .ThenInclude(r => r.Country)
                    .Include(r => r.UserPlants)
                    .ThenInclude(r => r.Plant)
                    .Skip((--page) * pageSize)
                    .Take(pageSize);
            }

            Query.Where(x => x.Visible && x.UserPlants.Any(t => t.isOperator));

            if (!string.IsNullOrEmpty(name?.Trim()))
            {
                Query.Where(x => x.FullName!.ToUpper().Contains(name.Trim().ToUpper()));
            }
            if (!string.IsNullOrEmpty(noEmployee?.Trim()))
            {
                Query.Where(x => x.EmployeeNumber!.ToUpper().Contains(noEmployee.Trim().ToUpper()));
            }

            if (!string.IsNullOrEmpty(lindeId?.Trim()))
            {
                Query.Where(x => x.UserName!.ToUpper().Contains(lindeId.Trim().ToUpper()));
            }

            if (!string.IsNullOrEmpty(country?.Trim()))
            {
                Query.Where(x => x.CountryItems.Any(
                    t => t.Country.Name!.ToUpper().Contains(country.Trim().ToUpper())
                    ));
            }
            if (!string.IsNullOrEmpty(plant?.Trim()))
            {
                Query.Where(x => x.UserPlants.Any(
                   t => t.Plant.Name!.ToUpper().Contains(plant.Trim().ToUpper())
                   ));
            }
        }
    }
}
