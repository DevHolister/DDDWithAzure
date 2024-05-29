using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Questions.Commands.Delete
{
    public class DeleteQuestionCommand : IRequest<ErrorOr<Unit>>
    {
        public Guid QuestionId { get; set; }
    }
}
