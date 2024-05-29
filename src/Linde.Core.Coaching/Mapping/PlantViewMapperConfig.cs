using Linde.Core.Coaching.Common.Models.Catalog.Plant;
using Linde.Domain.Coaching.Views;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Mapping;

internal class PlantViewMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<VwPlantas, PlantDTO>()
             .Map(x => x.PlantId, src => src.NumSucursal)
             .Map(x => x.Name, src => src.Sucursal)
             .Map(x => x.CountryId, src => src.Pais)
             .Map(x => x.Division, src => src.Division)
             .Map(x => x.State, src => src.Estado)
             .Map(x => x.City, src => src.Ciudad)
             .Map(x => x.Municipality, src => src.Municipio);
    }
}
