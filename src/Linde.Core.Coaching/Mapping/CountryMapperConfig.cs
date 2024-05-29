using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Catalogs.Country;
using Linde.Core.Coaching.Common.Models.Common;
using Linde.Domain.Coaching.CountryAggregate;
using Linde.Domain.Coaching.Entities.Catalogs;
using Linde.Domain.Coaching.UserAggreagate.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Mapping;

internal class CountryMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Country, CountryDto>()
            .Map(x => x.Name, src => src.Name)
            .Map(x => x.Code, src => src.Code)
            //.Map(x => x.OriginalCode, src => src.OriginalCode)
            .Map(x => x.Id, src => src.Id.Value);

        config.NewConfig<Country, CountryAutocompleteDto>()
            .Map(x => x.Name, src => src.Name)
            .Map(x => x.Id, src => src.Id.Value);
    }
}
