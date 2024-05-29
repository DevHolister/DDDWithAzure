using Linde.Domain.Coaching.ActivityAggregate;
using Linde.Domain.Coaching.ActivityAggregate.ValueObjects;
using Linde.Domain.Coaching.Common;
using Linde.Domain.Coaching.Questions;
using Linde.Domain.Coaching.Questionsaggregate.ValueObjects;

namespace Linde.Domain.Coaching.Questionsaggregate.Entities
{
    public class QuestionsActivities : Entity<Guid>
    {
        public QuestionId CatQuestionsId { get; private set; }
        public ActivityId ActivityId { get; private set; }
        public CatQuestions Questions { get; private set; }
        public Activity Activity { get; private set; }
        private QuestionsActivities(QuestionId questionId, ActivityId activityId)
            : base()
        {
            CatQuestionsId = questionId;
            ActivityId = activityId;
        }
        public static QuestionsActivities Create(QuestionId questionId, ActivityId activityId)
        {
            return new(questionId, activityId);
        }

        private QuestionsActivities() { }
    }
}
