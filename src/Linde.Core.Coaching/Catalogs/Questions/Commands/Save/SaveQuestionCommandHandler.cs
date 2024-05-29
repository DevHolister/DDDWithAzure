using ErrorOr;
using Linde.Core.Coaching.Catalogs.Activities.Specifications;
using Linde.Core.Coaching.Catalogs.Questions.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Domain.Coaching.ActivityAggregate.ValueObjects;
using Linde.Domain.Coaching.Questions;
using Linde.Domain.Coaching.Questionsaggregate.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Catalogs.Questions.Commands.Save
{
    public class SaveQuestionCommandHandler : IRequestHandler<SaveQuestionCommand, ErrorOr<Guid>>
    {
        private readonly IRepository<CatQuestions> _repository;
        private readonly IRepository<QuestionsActivities> _asocRepository;
        private readonly ILogger<SaveQuestionCommandHandler> _logger;
        private readonly IRepository<Domain.Coaching.ActivityAggregate.Activity> _activityRepository;

        public SaveQuestionCommandHandler(
            IRepository<CatQuestions> repository,
            IRepository<QuestionsActivities> ascocRepository,
            ILogger<SaveQuestionCommandHandler> logger,
            IRepository<Domain.Coaching.ActivityAggregate.Activity> activityRepository)
        {
            _repository = repository;
            _asocRepository = ascocRepository;
            _logger = logger;
            _activityRepository = activityRepository;
        }

        public async Task<ErrorOr<Guid>> Handle(SaveQuestionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existQuestion = await _repository.AnyAsync(new QuestionSpecification(request.Question));
                if (existQuestion)
                    return Errors.ErrorQuestions.DuplicatedQuestion;


                var acitvitiesRequestIds = request.ActivitiesIds.Distinct()
                .Select(x => ActivityId.Create(x));
                var activtyIds = await _activityRepository.CountAsync(new ActivityToIdSpecification(acitvitiesRequestIds));
                if (activtyIds != request.ActivitiesIds.Count)
                    return Errors.ErrorQuestions.InvalidActivity;

                var question = CatQuestions.Create(
                request.Question,
                request.IsCritical);

                var activities = acitvitiesRequestIds
                       .Select(x => QuestionsActivities.Create(question.Id, x))
                       //.Select(x => QuestionsActivities.Create( x))
                       .ToList();

                foreach (var activity in activities)
                {
                    question.AddActivity(activity);
                }
                await _repository.AddAsync(question);
                return question.Id.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
        }
    }
}
