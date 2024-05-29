using ErrorOr;
using Linde.Core.Coaching.Catalogs.Activities.Specifications;
using Linde.Core.Coaching.Catalogs.Checklists.Errors;
using Linde.Core.Coaching.Catalogs.Checklists.Specifications;
using Linde.Core.Coaching.Catalogs.Questions.Commands.Save;
using Linde.Core.Coaching.Catalogs.Questions.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Domain.Coaching.ActivityAggregate.ValueObjects;
using Linde.Domain.Coaching.ChecklistAggregate;
using Linde.Domain.Coaching.ChecklistAggregate.Entities;
using Linde.Domain.Coaching.Questions;
using Linde.Domain.Coaching.Questionsaggregate.Entities;
using Linde.Domain.Coaching.Questionsaggregate.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Checklists.Commands.Save
{
    public class SaveChecklistCommandHandler : IRequestHandler<SaveChecklistCommand, ErrorOr<Guid>>
    {
        private readonly IRepository<CatChecklist> _repository;
        private readonly IRepository<ChecklistsQuestions> _asocRepository;
        private readonly ILogger<SaveChecklistCommandHandler> _logger;
        private readonly IRepository<CatQuestions> _questionRepository;

        public SaveChecklistCommandHandler(
            IRepository<CatChecklist> repository,
            IRepository<ChecklistsQuestions> ascocRepository,
            ILogger<SaveChecklistCommandHandler> logger,
            IRepository<CatQuestions> questionRepository)
        {
            _repository = repository;
            _asocRepository = ascocRepository;
            _logger = logger;
            _questionRepository = questionRepository;
        }
        public async Task<ErrorOr<Guid>> Handle(SaveChecklistCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existChecklist= await _repository.AnyAsync(new ChecklistSpecification(request.Name));
                if (existChecklist)
                    return ErrorChecklists.DuplicatedChecklist;


                var questionsRequestIds = request.QuestionsIds.Distinct()
                .Select(x => QuestionId.Create(x));
                var activtyIds = await _questionRepository.CountAsync(new QuestionToIdSpecification(questionsRequestIds));
                if (activtyIds != request.QuestionsIds.Count)
                    return ErrorChecklists.InvalidQuestion;

                var checklist = CatChecklist.Create(
                request.Name,
                request.Description);

                var questions = questionsRequestIds
                       .Select(x => ChecklistsQuestions.Create(checklist.Id, x))
                       .ToList();

                foreach (var question in questions)
                {
                    checklist.AddQuestion(question);
                }
                await _repository.AddAsync(checklist);
                return checklist .Id.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
        }
    }
}
