using ErrorOr;
using Linde.Core.Coaching.Catalogs.Checklists.Errors;
using Linde.Core.Coaching.Catalogs.Checklists.Specifications;
using Linde.Core.Coaching.Catalogs.Questions.Commands.Edit;
using Linde.Core.Coaching.Catalogs.Questions.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Domain.Coaching.ActivityAggregate.ValueObjects;
using Linde.Domain.Coaching.ChecklistAggregate;
using Linde.Domain.Coaching.ChecklistAggregate.Entities;
using Linde.Domain.Coaching.ChecklistAggregate.ValueObjects;
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

namespace Linde.Core.Coaching.Catalogs.Checklists.Commands.Edit
{
    public class EditChecklistCommandHandler : IRequestHandler<EditChecklistCommand, ErrorOr<Guid>>
    {
        private readonly IRepository<CatChecklist> _repository;
        private readonly IRepository<ChecklistsQuestions> _asocRepository;
        private readonly ILogger<EditChecklistCommandHandler> _logger;
        private readonly IRepository<CatQuestions> _questionRepository;

        public EditChecklistCommandHandler(
            IRepository<CatChecklist> repository,
            IRepository<ChecklistsQuestions> ascocRepository,
            ILogger<EditChecklistCommandHandler> logger,
            IRepository<CatQuestions> questionRepository)
        {
            _repository = repository;
            _asocRepository = ascocRepository;
            _logger = logger;
            _questionRepository = questionRepository;
        }

        public async Task<ErrorOr<Guid>> Handle(EditChecklistCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var checklistDd = await _repository.FirstOrDefaultAsync(new ChecklistSpecification(ChecklistId.Create(request.ChecklistId), false));
                if (checklistDd is null)
                    return ErrorChecklists.DuplicatedChecklist;

                if (checklistDd.Name != request.Name)
                {
                    checklistDd.UpdateChecklist(request.Name, request.Description);
                }


                var questionssRequestIds = request.QuestionsIds.Select(x => QuestionId.Create(x));

                var questionIds = checklistDd.ChecklistsQuestions.Select(t => t.QuestionId.Value);
                var questionsNew = request.QuestionsIds.Except(questionIds);
                var questionsDelete = questionIds.Where(t => !request.QuestionsIds.Contains(t));

                if (questionsDelete?.Any() ?? false)
                {
                    var delete = checklistDd.ChecklistsQuestions.Where(t => questionsDelete.Contains(t.QuestionId.Value));
                    await _asocRepository.DeleteRangeTranAsync(delete);
                }

                if (questionsNew?.Any() ?? false)
                {
                    var news = questionssRequestIds.Where(t => questionsNew.Contains(t.Value));
                    var activities = news
                        .Select(x => ChecklistsQuestions.Create(checklistDd.Id, x))
                        .ToList();
                    await _asocRepository.AddRangeTranAsync(activities);
                }

                await _repository.UpdateAsync(checklistDd);
                await _repository.SaveChangesAsync();
                return checklistDd.Id.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
        }
    }
}
