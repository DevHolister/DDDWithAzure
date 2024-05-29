using ErrorOr;
using Linde.Core.Coaching.Catalogs.Questions.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Models.Catalogs.Questions;
using Linde.Core.Coaching.Security.User.Commands.Delete;
using Linde.Core.Coaching.Security.User.Specifications;
using Linde.Domain.Coaching.Questions;
using Linde.Domain.Coaching.Questionsaggregate.Entities;
using Linde.Domain.Coaching.Questionsaggregate.ValueObjects;
using Linde.Domain.Coaching.UserAggreagate.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Questions.Commands.Delete
{
    internal class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, ErrorOr<Unit>>
    {
        private readonly ILogger<DeleteQuestionCommandHandler> _logger;
        private readonly IRepository<CatQuestions> _repository;
        private readonly IRepository<QuestionsActivities> _questionActivityrepository;

        public DeleteQuestionCommandHandler(ILogger<DeleteQuestionCommandHandler> logger, 
            IRepository<CatQuestions> repository,
             IRepository<QuestionsActivities> questionActivityrepository)
        {
            _logger = logger;
            _repository = repository;
            _questionActivityrepository = questionActivityrepository;
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var question = await _repository.FirstOrDefaultAsync(new QuestionSpecification(QuestionId.Create(request.QuestionId), false));
                if (question is null)
                    return Default.NotFound;

                question.DeleteQuestion();
                await _repository.UpdateTranAsync(question);
                await _questionActivityrepository.DeleteRangeAsync(question.QuestionsActivities);
                await _repository.SaveChangesAsync();
                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
            throw new NotImplementedException();
        }
    }
}
