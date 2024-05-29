using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Questions.Commands.Edit
{
    public class EditQuestionCommand : IRequest<ErrorOr<Guid>>
    {
        public Guid QuestionId { get; set; }
        public string Question { get; set; }
        public bool IsCritical { get; set; }
        public List<Guid>? ActivitiesIds { get; set; }
    }
}
