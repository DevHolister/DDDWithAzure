using Linde.Domain.Coaching.ChecklistAggregate.ValueObjects;
using Linde.Domain.Coaching.Common;
using Linde.Domain.Coaching.Questions;
using Linde.Domain.Coaching.Questionsaggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Domain.Coaching.ChecklistAggregate.Entities
{
    public class ChecklistsQuestions : Entity<Guid>
    {
        public ChecklistId CatChecklistId { get; private set; }
        public QuestionId QuestionId { get; private set; }
        public CatChecklist Checklists { get; private set; }
        public CatQuestions Question { get; private set; }
        private ChecklistsQuestions(ChecklistId checklistId, QuestionId questionId)
        {
            CatChecklistId = checklistId;
            QuestionId = questionId;
        }
        public static ChecklistsQuestions Create(ChecklistId checklistId, QuestionId questionId)
        {
            return new(checklistId, questionId);
        }
        private ChecklistsQuestions() { }
    }
}
