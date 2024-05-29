using Ardalis.Specification;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;
using Linde.Domain.Coaching.UserAggreagate.ValueObjects;
using System.Xml.Linq;

namespace Linde.Core.Coaching.Security.User.Specifications;

internal class UserWhereSpecification : Specification<Domain.Coaching.UserAggreagate.User>
{
    public UserWhereSpecification(string UserName, bool IncludeData = false)
    {
        if (IncludeData)
        {
            Query
                .AsNoTracking()
                .Include(x => x.CountryItems)
                .ThenInclude(x => x.Country)
                .Include(x => x.RoleItems)
                .ThenInclude(x => x.Role)
                .ThenInclude(x => x.PermissionItems)
                .ThenInclude(x => x.Permission);
        }

        Query.Where(x => x.UserName == UserName.Trim().ToUpper());
    }

    public UserWhereSpecification(UserId Id, bool IncludeData = false)
    {
        if (IncludeData)
        {
            Query
            .Include(x => x.CountryItems)
            .ThenInclude(x => x.Country)
            .Include(x => x.RoleItems)
            .ThenInclude(x => x.Role)
            .Include(x => x.UserPlants)
            .ThenInclude(x => x.Plant);
        }

        Query
            .Where(x => x.Id == Id);
    }

    public UserWhereSpecification(string name, string role, CountryId countryId, int page = 1, int pageSize = 20, bool pagination = false)
    {
        Query.Where(x => x.Visible && x.CountryItems.Any(y => y.CountryId == countryId));

        if (!string.IsNullOrEmpty(name?.Trim()))
        {
            //Query.Where(x => x.FullName.Name!.ToUpper().Contains(name.Trim().ToUpper()));
            Query.Where(x => x.FullName!.ToUpper().Contains(name.Trim().ToUpper()));
        }

        if (!string.IsNullOrEmpty(role?.Trim()))
        {
            Query.Where(x => x.RoleItems.Any(y => y.Role.Name.Contains(role.Trim().ToUpper())));
        }

        if (pagination)
        {
            Query
                .Include(r => r.RoleItems)
                .ThenInclude(x => x.Role)
                .Include(ab => ab.CountryItems)
                .ThenInclude(a => a.Country)
                .Skip((--page) * pageSize)
                .Take(pageSize);
        }
    }
    public UserWhereSpecification(RoleId roleId, bool IncludeData = false)
    {
        if (IncludeData)
        {
            Query
            .Include(x => x.CountryItems)
            .Include(x => x.RoleItems);
        }

        Query
            .Where(x => x.RoleItems.Any(t => t.RoleId == roleId));
    }

    public UserWhereSpecification(string fullname)
    {
        Query
            .Where(x => x.FullName!.ToUpper().Contains(fullname.Trim().ToUpper()));
        //.Where(x => x.FullName.Name!.ToUpper().Contains(fullname.Trim().ToUpper()));
    }
    public UserWhereSpecification(string name, string userLindeId, string noEmployee)
    {
        Query.AsNoTracking()
            .Include(x => x.CountryItems)
            .ThenInclude(x => x.Country)
            .Include(x => x.UserPlants)
            .ThenInclude(x => x.Plant);

        if (!string.IsNullOrEmpty(name?.Trim()))
        {
            Query.Where(x => x.FullName!.ToUpper().Contains(name.Trim().ToUpper()));
        }
        if (!string.IsNullOrEmpty(userLindeId?.Trim()))
        {
            Query.Where(x => x.UserName!.ToUpper().Contains(userLindeId.Trim().ToUpper()));
        }
        if (!string.IsNullOrEmpty(noEmployee?.Trim()))
        {
            Query.Where(x => x.EmployeeNumber!.ToUpper().Contains(noEmployee.Trim().ToUpper()));
        }
        Query.OrderBy(t => t.FullName);
    }
}
