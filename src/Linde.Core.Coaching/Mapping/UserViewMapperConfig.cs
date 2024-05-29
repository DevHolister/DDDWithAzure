using Linde.Core.Coaching.Common.Models.Security.User;
using Linde.Domain.Coaching.Views;
using Mapster;

namespace Linde.Core.Coaching.Mapping;

internal class UserViewMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<VwEmpleado, UserQueryDto>()
            .Map(x => x.Name, src => src.NameComplete)
            .Map(x => x.EmployeeNumber, src => src.NoEmPloyee)
            .Map(x => x.UserName, src => src.User)
            .Map(x => x.Email, src => src.Email)
            .Map(x => x.Country, src => src.CodeCountry)
            ;
    }
}
