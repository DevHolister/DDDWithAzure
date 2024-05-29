using ErrorOr;
using Linde.Core.Coaching.Common.Models.Catalogs.Questions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Questions.Queries.GetById
{
    public class GetQuestionByIdQuery : IRequest<ErrorOr<QuestionDto>>
    {
        public Guid QuestionId { get; set; }
    }
}
