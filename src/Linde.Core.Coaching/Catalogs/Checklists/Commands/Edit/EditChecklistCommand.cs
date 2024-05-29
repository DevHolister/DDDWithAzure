using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Checklists.Commands.Edit
{
    public class EditChecklistCommand : IRequest<ErrorOr<Guid>>
    {
        public Guid ChecklistId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Guid> QuestionsIds { get; set; }
    }
}
