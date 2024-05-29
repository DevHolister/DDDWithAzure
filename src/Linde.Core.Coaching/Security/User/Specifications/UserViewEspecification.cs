using Ardalis.Specification;
using Linde.Core.Coaching.Common.Models.Catalogs.Country;
using Linde.Domain.Coaching.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Security.User.Specifications
{
    internal class UserViewEspecification : Specification<VwEmpleado>
    {
        public UserViewEspecification()
        {
            Query
                .AsNoTracking();
        }

        public UserViewEspecification(string userName, bool fullName = true)
        {
            if (fullName)
            {
                Query
                    .Where(x => x.NameComplete!.ToUpper().Contains(userName.ToUpper()))
                    .AsNoTracking();
            }
            else
            {
                Query
                    .Where(x => x.Name!.ToUpper().Contains(userName.ToUpper()))
                    .AsNoTracking();
            }
        }

        public UserViewEspecification(string userName)
        {
            Query
                .Where(x => x.User!.ToUpper().Equals(userName.ToUpper()))
                .AsNoTracking();
        }

        public UserViewEspecification(CountryDto country)
        {
            if (country.Code != null)
            {
                Query
                .Where(x => x.CodeCountry!.ToUpper().Equals(country.Code.ToUpper()))
                .AsNoTracking();
            }
        }
    }
}
