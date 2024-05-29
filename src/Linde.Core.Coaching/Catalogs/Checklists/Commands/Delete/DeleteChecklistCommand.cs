using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Checklists.Commands.Delete
{
    public class DeleteChecklistCommand : IRequest<ErrorOr<Unit>>
    {
        public Guid ChecklistId { get; set; }
    }
}
