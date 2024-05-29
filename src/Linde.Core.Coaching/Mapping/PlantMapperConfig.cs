using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Common;
using Linde.Domain.Coaching.Entities.Catalogs;
using Linde.Domain.Coaching.UserAggreagate.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Mapping
{
    internal class PlantMapperConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Plant, PlantAutocompleteDto>()
            .Map(x => x.Name, src => src.Name)
            .Map(x => x.Id, src => src.PlantId);

            config.NewConfig<Plant, ItemDto>()
            .Map(x => x.Name, src => src.Name)
            .Map(x => x.Id, src => src.PlantId);
        }
    }
}
