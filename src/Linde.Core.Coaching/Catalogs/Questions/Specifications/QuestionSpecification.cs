using Ardalis.Specification;
using Linde.Domain.Coaching.PermissionAggregate;
using Linde.Domain.Coaching.Questions;
using Linde.Domain.Coaching.Questionsaggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Questions.Specifications
{
    public class QuestionSpecification : Specification<CatQuestions>
    {
        public QuestionSpecification(string question, string activity, bool? isCritical, int page = 1, int pageSize = 20, bool pagination = false)
        {
            if (pagination)
            {
                Query
                    .AsNoTracking()
                    .Include(r => r.QuestionsActivities)
                    .ThenInclude(x => x.Activity)
                    .Skip((--page) * pageSize)
                    .Take(pageSize);
            }

            Query.Where(x => x.Visible);

            if (!string.IsNullOrEmpty(question?.Trim()))
            {
                Query.Where(x => x.Name!.ToUpper().Contains(question.Trim().ToUpper()));
            }

            if (!string.IsNullOrEmpty(activity?.Trim()))
            {
                Query.Where(x => x.QuestionsActivities.Any(t => t.Activity.Name!.ToUpper().Contains(activity.ToUpper())));
            }

            if (isCritical is not null)
            {
                Query.Where(x => x.IsCritical == isCritical);
            }
        }
        public QuestionSpecification(string question)
        {
            Query
                .AsNoTracking()
                .Include(r => r.QuestionsActivities)
                .ThenInclude(x => x.Activity);
            Query.Where(x => x.Name == question && x.Visible);

        }
        public QuestionSpecification(QuestionId id, bool isQuery)
        {
            if(isQuery)
            {
                Query
                .AsNoTracking()
                .Include(t => t.QuestionsActivities)
                .ThenInclude(t => t.Activity);
            }
            else
            {
                Query
                .AsNoTracking()
                .Include(t => t.QuestionsActivities);
            }
            
            Query.Where(x => x.Id == id);

        }
    }
}
