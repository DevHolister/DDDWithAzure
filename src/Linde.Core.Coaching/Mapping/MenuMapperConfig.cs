using Linde.Core.Coaching.Common.Models.Catalogs.Country;
using Linde.Core.Coaching.Common.Models.Menu;
using Linde.Core.Coaching.Common.Models.Security.User;
using Linde.Domain.Coaching.MenuAggregate;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Mapping
{
    internal class MenuMapperConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Domain.Coaching.MenuAggregate.Menu, MenuDTO>()
                .Map(x => x.MenuName, src => src.Name)
                .Map(x => x.MenuId, src => src.Id.Value)
                .Map(x => x.ParentId, src => src.Id.Value);
        }
    }
}
