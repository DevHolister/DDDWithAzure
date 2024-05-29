using Linde.Core.Coaching.Common.Models.Menu;
using Mapster;
using Newtonsoft.Json;

namespace Linde.Notifications.Coaching.Mappings
{
    public class NotificationProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //config.NewConfig<Domain.Coaching.MenuAggregate.Menu, MenuDTO>()
            //    .Map(x => x.MenuName, src => src.Name);

            //config.NewConfig<Domain.Coaching.MenuAggregate.Menu, MenuDTO>()
            //    .Map(x => x.MenuName, x => JsonConvert.DeserializeObject<MenuDTO>(x.Content)));
        }
    }
}
