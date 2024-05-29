using ErrorOr;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Catalogs.Checklist;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Checklists.Queries.GetAll
{
    public class GetAllCheklistsQuery : IRequest<ErrorOr<PaginatedListDto<ChecklistDto>>>
    {
        public string? Name { get; set; }
        public string? Question { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
