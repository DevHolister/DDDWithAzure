using Linde.Domain.Coaching.Common;
using Linde.Domain.Coaching.DivisionAggregate.ValueObjects;
using Linde.Domain.Coaching.DivisionAggregate;
using Linde.Domain.Coaching.Questionsaggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linde.Domain.Coaching.Questionsaggregate.Entities;
using Linde.Domain.Coaching.UserAggreagate.ValueObjects;
using Linde.Domain.Coaching.UserAggreagate.Entities;

namespace Linde.Domain.Coaching.Questions
{
    public sealed class CatQuestions : Entity<QuestionId>, IAggregateRoot
    {
        private readonly List<QuestionsActivities> _QuestionsActivities = new();
        public IReadOnlyList<QuestionsActivities> QuestionsActivities => _QuestionsActivities.AsReadOnly();
        public string Name { get; private set; }
        public bool IsCritical { get; private set; }
        private CatQuestions(QuestionId questionId, string name, bool isCritical)
        {
            Id = questionId;
            Name = name;
            IsCritical = isCritical;
            Visible = true;
        }

        public static CatQuestions Create(string name, bool isCritical)
        {
            return new(
                QuestionId.CreateUnique(),
                name,
                isCritical);
        }
        private CatQuestions()
        {
            Visible = true;
        }

        public void UpdateQuestion(string name)
        {
            Name = name;
        }
        public void UpdateCritical(bool isCritical)
        {
            IsCritical = isCritical;
        }
        public void AddActivity(QuestionsActivities activity)
        {
            if (!_QuestionsActivities.Contains(activity))
                _QuestionsActivities.Add(activity);
        }
        public void UpdateActivities(List<QuestionsActivities> activities)
        {
            _QuestionsActivities.RemoveAll(x => !activities.Contains(x));
            activities.ForEach(x => AddActivity(x));
        }

        public void DeleteQuestion()
        {
            Visible = false;
        }
        public void DeleteActivity(List<QuestionsActivities> activities)
        {
            _QuestionsActivities.RemoveAll(x => activities.Contains(x));
        }
    }
}
