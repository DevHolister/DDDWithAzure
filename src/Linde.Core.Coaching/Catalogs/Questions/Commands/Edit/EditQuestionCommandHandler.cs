using ErrorOr;
using Linde.Core.Coaching.Catalogs.Activities.Specifications;
using Linde.Core.Coaching.Catalogs.Questions.Commands.Save;
using Linde.Core.Coaching.Catalogs.Questions.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Security.Role.Specifications;
using Linde.Domain.Coaching.ActivityAggregate.ValueObjects;
using Linde.Domain.Coaching.Questions;
using Linde.Domain.Coaching.Questionsaggregate.Entities;
using Linde.Domain.Coaching.Questionsaggregate.ValueObjects;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;
using Linde.Domain.Coaching.UserAggreagate.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Questions.Commands.Edit
{
    public class EditQuestionCommandHandler : IRequestHandler<EditQuestionCommand, ErrorOr<Guid>>
    {
        private readonly IRepository<CatQuestions> _repository;
        private readonly IRepository<QuestionsActivities> _asocRepository;
        private readonly ILogger<EditQuestionCommandHandler> _logger;
        private readonly IRepository<Domain.Coaching.ActivityAggregate.Activity> _activityRepository;

        public EditQuestionCommandHandler(
            IRepository<CatQuestions> repository,
            IRepository<QuestionsActivities> ascocRepository,
            ILogger<EditQuestionCommandHandler> logger,
            IRepository<Domain.Coaching.ActivityAggregate.Activity> activityRepository)
        {
            _repository = repository;
            _asocRepository = ascocRepository;
            _logger = logger;
            _activityRepository = activityRepository;
        }

        public async Task<ErrorOr<Guid>> Handle(EditQuestionCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var questionDd = await _repository.FirstOrDefaultAsync(new QuestionSpecification(QuestionId.Create(request.QuestionId), false));
                if (questionDd is null)
                    return Errors.ErrorQuestions.DuplicatedQuestion;

                if (questionDd.Name != request.Question)
                {
                    questionDd.UpdateQuestion(request.Question);
                }
                if (questionDd.IsCritical != request.IsCritical)
                {
                    questionDd.UpdateCritical(request.IsCritical);
                }

                var activitiesRequestIds = request.ActivitiesIds
                .Select(x => ActivityId.Create(x));

                var activityIds = questionDd.QuestionsActivities.Select(t => t.ActivityId.Value);
                var activitiesNew = request.ActivitiesIds.Except(activityIds);
                var activitiesDelete = activityIds.Where(t => !request.ActivitiesIds.Contains(t));

                if (activitiesDelete?.Any() ?? false)
                {
                    var delete = questionDd.QuestionsActivities.Where(t => activitiesDelete.Contains(t.ActivityId.Value));
                    await _asocRepository.DeleteRangeTranAsync(delete);
                }

                if (activitiesNew?.Any() ?? false)
                {
                    var news = activitiesRequestIds.Where(t => activitiesNew.Contains(t.Value));
                    var activities = news
                        .Select(x => QuestionsActivities.Create(questionDd.Id, x))
                        .ToList();
                    await _asocRepository.AddRangeTranAsync(activities);
                }
                
                await _repository.UpdateAsync(questionDd);
                await _repository.SaveChangesAsync();
                return questionDd.Id.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
        }
    }
}
