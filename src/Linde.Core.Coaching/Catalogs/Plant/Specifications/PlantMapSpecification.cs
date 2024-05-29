using Ardalis.Specification;
using Linde.Core.Coaching.Common.Models.Catalog.Operador;
using Linde.Core.Coaching.Common.Models.Catalog.Plant;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Linde.Core.Coaching.Catalogs.Plant.Specifications
{
    internal class PlantMapSpecification : Specification<Domain.Coaching.Entities.Catalogs.Plant, PlantDTO>
    {
        public PlantMapSpecification(string? name,
    string? bu,
    string? CountryId,
    string? Division,
    string? State, int page = 1, int pageSize = 20, bool pagination = false)
        {

            Query
                .Select(x => new PlantDTO(
                      x.PlantId.ToString(),
                      x.Name,
                      x.Bu,
                      x.CountryId.Value.ToString(),
                      x.Country.Name.ToString(),
                      x.Division.ToString(),
                      x.State.ToString(),
                      x.City.ToString(),
                      x.Municipality.ToString(),
                      x.SuperintendentId.Value.ToString(),
                      x.UserManager.FullName.ToString(),
                      x.PlantManagerId.Value.ToString(),
                      x.UserManager.FullName.ToString()
                    ))
                .AsNoTracking()
                .Skip((--page) * pageSize)
            .Take(pageSize);


        }
        public PlantMapSpecification(Guid id)
        {
            Query
                .Select(x => new PlantDTO(
                      x.PlantId.ToString(),
                      x.Name,
                      x.Bu,
                      x.CountryId.Value.ToString(),
                      x.Country.Name.ToString(),
                      x.Division.ToString(),
                      x.State.ToString(),
                      x.City.ToString(),
                      x.Municipality.ToString(),
                      x.SuperintendentId.Value.ToString(),
                      x.UserManager.FullName.ToString(),
                      x.PlantManagerId.Value.ToString(),
                      x.UserManager.FullName.ToString()
                    ));

            Query.Where(x => x.PlantId == id);

        }
    }
}
