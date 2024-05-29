using ErrorOr;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Catalogs.Questions;
using Linde.Core.Coaching.Common.Models.Menu;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Questions.Queries.GetAll
{
    public class GetAllQuestionsQuery : IRequest<ErrorOr<PaginatedListDto<QuestionDto>>>
    {
        public string? question { get; set; }
        public string? Activity { get; set; }
        public bool? IsCritical { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
