using Ardalis.Specification;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Security.User;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;

namespace Linde.Core.Coaching.Security.User.Specifications;

internal class UserMapSpecification : Specification<Domain.Coaching.UserAggreagate.User, UserDto>
{
    public UserMapSpecification(string name, string role, string userName, CountryId countryId, int page = 1, int pageSize = 20, bool pagination = false)
    {
        if (pagination)
        {
            Query
                .Select(x => new UserDto(
                    x.Id.Value,
                    x.FullName,
                    //x.FullName.Value,
                    //x.FullName.Name,
                    //x.FullName.FirstSurname,
                    //x.FullName.SecondSurname,
                    x.Email,
                    x.UserName,
                    x.Lockout,
                    x.AccessType,
                    x.TypeUser,
                    x.ZoneId,
                    x.EmployeeNumber,
                    x.RoleItems.Select(y => new ItemDto(
                        y.RoleId.Value,
                        y.Role.Name)),
                    x.CountryItems.Select(y => new ItemDto(
                        y.CountryId.Value,
                        y.Country.Name)),
                    x.UserPlants.Select(y => new ItemDto(
                        y.Plant.PlantId,
                        y.Plant.Name))))
                .AsNoTracking()
                .Include(r => r.RoleItems)
                .ThenInclude(x => x.Role)
                .Include(ab => ab.CountryItems)
                .ThenInclude(a => a.Country)
                .Skip((--page) * pageSize)
                .Take(pageSize);
        }

        Query.Where(x => x.Visible && x.CountryItems.Any(y => y.CountryId == countryId));

        if (!string.IsNullOrEmpty(name?.Trim()))
        {
            Query.Where(x => x.FullName.Trim().ToUpper().Contains(name.Trim().ToUpper()));
            //Query.Where(x => (x.FullName.Name + " " + x.FullName.FirstSurname + " " + x.FullName.SecondSurname).ToUpper().Contains(name.Trim().ToUpper()));
        }

        if (!string.IsNullOrEmpty(role?.Trim()))
        {
            Query.Where(x => x.RoleItems.Any(y => y.Role.Name.Contains(role.Trim().ToUpper())));
        }

        if (!string.IsNullOrEmpty(userName?.Trim()))
        {
            Query.Where(x => x.UserName.Trim().ToUpper().Contains(userName.Trim().ToUpper()));
        }
    }
}
