using Ardalis.Specification;
using Linde.Domain.Coaching.ChecklistAggregate;
using Linde.Domain.Coaching.ChecklistAggregate.ValueObjects;
using Linde.Domain.Coaching.Questionsaggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Checklists.Specifications
{
    public class ChecklistSpecification: Specification<CatChecklist>
    {
        public ChecklistSpecification(string checklist, string question, int page = 1, int pageSize = 20, bool pagination = false)
        {
            if (pagination)
            {
                Query
                    .AsNoTracking()
                    .Include(r => r.ChecklistsQuestions)
                    .ThenInclude(x => x.Question)
                    .Skip((--page) * pageSize)
                    .Take(pageSize);
            }

            Query.Where(x => x.Visible);

            if (!string.IsNullOrEmpty(checklist?.Trim()))
            {
                Query.Where(x => x.Name!.ToUpper().Contains(question.Trim().ToUpper()));
            }

            if (!string.IsNullOrEmpty(checklist?.Trim()))
            {
                Query.Where(x => x.ChecklistsQuestions.Any(t => t.Question.Name!.ToUpper().Contains(question.ToUpper())));
            }
        }
        public ChecklistSpecification(string checklist)
        {
            Query
                .AsNoTracking()
                .Include(r => r.ChecklistsQuestions)
                .ThenInclude(x => x.Question);
            Query.Where(x => x.Name == checklist && x.Visible);

        }
        public ChecklistSpecification(ChecklistId id, bool isQuery)
        {
            if (isQuery)
            {
                Query
                .AsNoTracking()
                .Include(t => t.ChecklistsQuestions)
                .ThenInclude(t => t.Question);
            }
            else
            {
                Query
                .AsNoTracking()
                .Include(t => t.ChecklistsQuestions);
            }

            Query.Where(x => x.Id == id);

        }
    }
}
