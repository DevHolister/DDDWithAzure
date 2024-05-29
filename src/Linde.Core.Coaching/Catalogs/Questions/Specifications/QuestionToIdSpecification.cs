using Ardalis.Specification;
using Linde.Domain.Coaching.Questions;
using Linde.Domain.Coaching.Questionsaggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Questions.Specifications
{
    internal class QuestionToIdSpecification : Specification<CatQuestions, QuestionId>
    {
        public QuestionToIdSpecification(IEnumerable<QuestionId> ids)
        {
            Query
                .Select(x => x.Id)
                .Where(x => ids.Contains(x.Id));
        }
    }
}
