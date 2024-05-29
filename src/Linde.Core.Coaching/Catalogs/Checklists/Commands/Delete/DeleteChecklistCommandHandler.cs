using ErrorOr;
using Linde.Core.Coaching.Catalogs.Checklists.Specifications;
using Linde.Core.Coaching.Catalogs.Questions.Commands.Delete;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Domain.Coaching.ChecklistAggregate;
using Linde.Domain.Coaching.ChecklistAggregate.Entities;
using Linde.Domain.Coaching.ChecklistAggregate.ValueObjects;
using Linde.Domain.Coaching.Questions;
using Linde.Domain.Coaching.Questionsaggregate.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Checklists.Commands.Delete
{
    internal class DeleteChecklistCommandHandler : IRequestHandler<DeleteChecklistCommand, ErrorOr<Unit>>
    {
        private readonly ILogger<DeleteChecklistCommandHandler> _logger;
        private readonly IRepository<CatChecklist> _repository;
        private readonly IRepository<ChecklistsQuestions> _checkListQuestionsRepository;

        public DeleteChecklistCommandHandler(ILogger<DeleteChecklistCommandHandler> logger,
            IRepository<CatChecklist> repository,
             IRepository<ChecklistsQuestions> questionActivityrepository)
        {
            _logger = logger;
            _repository = repository;
            _checkListQuestionsRepository = questionActivityrepository;
        }
        public async Task<ErrorOr<Unit>> Handle(DeleteChecklistCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var checklist = await _repository.FirstOrDefaultAsync(new ChecklistSpecification(ChecklistId.Create(request.ChecklistId), false));
                if (checklist is null)
                    return Default.NotFound;

                checklist.DeleteChecklist();
                await _repository.UpdateTranAsync(checklist);
                await _checkListQuestionsRepository.DeleteRangeAsync(checklist.ChecklistsQuestions);
                await _repository.SaveChangesAsync();
                return Unit.Value;
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
        }
    }
}
