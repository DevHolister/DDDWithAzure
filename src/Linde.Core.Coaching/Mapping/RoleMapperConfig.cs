using Linde.Core.Coaching.Common.Models;
using Linde.Domain.Coaching.RoleAggregate;
using Mapster;

namespace Linde.Core.Coaching.Mapping;

internal class RoleMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Role, ItemDto>()
            .Map(x => x.Name, src => src.Name)
            .Map(x => x.Id, src => src.Id.Value);
    }
}
