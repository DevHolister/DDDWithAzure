using ErrorOr;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Catalog.Operador;
using Linde.Core.Coaching.Common.Models.Common;
using Linde.Core.Coaching.Common.Models.Security.User;
using Linde.Domain.Coaching.Entities.Catalogs;
using Linde.Domain.Coaching.UserAggreagate;
using Linde.Domain.Coaching.UserAggreagate.Entities;
using Mapster;
using System.Reflection;

namespace Linde.Core.Coaching.Mapping;

internal class UserMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserRole, ItemDto>()
            .Map(x => x.Name, src => src.Role.Name)
            .Map(x => x.Id, src => src.RoleId.Value);

        config.NewConfig<UserCountry, ItemDto>()
            .Map(x => x.Name, src => src.Country.Name)
            .Map(x => x.Id, src => src.CountryId.Value);
        
        config.NewConfig<UserPlant, ItemDto>()
            .Map(x => x.Name, src => src.Plant.Name)
            .Map(x => x.Id, src => src.Plant.PlantId);

        config.NewConfig<User, UserDto>()
            .Map(x => x.Id, src => src.Id.Value)
            .Map(x => x.FullName, src => src.FullName)
            .Map(x => x.Email, src => src.Email)
            .Map(x => x.UserName, src => src.UserName)
            .Map(x => x.Lockout, src => src.Lockout)
            .Map(x => x.AccessType, src => src.AccessType)
            .Map(x => x.TypeUser, src => src.TypeUser)
            .Map(x => x.EmployeeNumber, src => src.EmployeeNumber)
            .Map(x => x.ZoneId, src => src.ZoneId)
            .Map(x => x.Roles, src => src.RoleItems)
            .Map(x => x.Countries, src => src.CountryItems)
            .Map(x => x.Plants, src => src.UserPlants);

        config.NewConfig<User, UserAutocompleteDto>()
            .Map(x => x.Name, src => src.FullName)
            .Map(x => x.Id, src => src.Id.Value);

        config.NewConfig<User, UserAutocompleteByFiltersDto>()
            .Map(x => x.Name, src => src.FullName)
            .Map(x => x.Id, src => src.Id.Value)
            .Map(x => x.LindeId, src => src.UserName)
            .Map(x => x.NoEmployee, src => src.EmployeeNumber)
            .Map(x => x.Countries, src => getCountries(src.CountryItems.ToList()))
            .Map(x => x.Plants, src => getPlants(src.UserPlants.ToList()));

        config.NewConfig<User, OperadorDto>()
                .Map(x => x.UserId, src => src.Id.Value)
                .Map(x => x.Name, src => src.FullName)
                .Map(x => x.LindeId, src => src.UserName)
                .Map(x => x.NoEmployee, src => src.EmployeeNumber)
                .Map(x => x.Countries, src => getCountries(src.CountryItems.ToList()))
                .Map(x => x.Plants, src => getPlants(src.UserPlants.ToList()));
    }

    private List<ItemDto> getCountries(List<UserCountry> list)
    {
        var items = list
                .Select(x => new ItemDto(x.Country.Id.Value, x.Country.Name));
        return items.ToList();
    }
    private List<ItemDto> getPlants(List<UserPlant> list)
    {
        var items = list
               .Select(x => new ItemDto(x.Plant.PlantId, x.Plant.Name));
        return items.ToList();
    }
}
