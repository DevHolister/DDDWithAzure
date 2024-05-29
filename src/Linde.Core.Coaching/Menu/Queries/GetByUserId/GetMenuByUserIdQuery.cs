using ErrorOr;
using Linde.Core.Coaching.Common.Models.Menu;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Menu.Queries.GetByUserId
{
    public class GetMenuByUserIdQuery : IRequest<ErrorOr<List<MenuDTO>>>
    {
    }
}
