using ErrorOr;
using Linde.Core.Coaching.Common.Models.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Security.User.Queries.AutoCompleteFilters
{
    public class GetUserAutoCompleteFiltersQuery : IRequest<ErrorOr<List<UserAutocompleteByFiltersDto>>>
    {
        public string? Name { get; set; }
        public string? UserLindeId { get; set; }
        public string? NoEmployee { get; set; }
       
    }
}
