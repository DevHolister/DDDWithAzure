using ErrorOr;
using Linde.Core.Coaching.Catalogs.Questions.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Catalogs.Questions;
using Linde.Domain.Coaching.Questions;
using Linde.Domain.Coaching.Questionsaggregate.ValueObjects;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Catalogs.Questions.Queries.GetById
{
    internal class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, ErrorOr<QuestionDto>>
    {
        private readonly ILogger<GetQuestionByIdQueryHandler> _logger;
        private readonly IRepository<CatQuestions> _repository;
        private readonly IMapper _mapper;

        public GetQuestionByIdQueryHandler(
            ILogger<GetQuestionByIdQueryHandler> logger,
            IRepository<CatQuestions> repository,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ErrorOr<QuestionDto>> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var question = await _repository.FirstOrDefaultAsync(new QuestionSpecification(QuestionId.Create(request.QuestionId), true), cancellationToken);

                if (question is null)
                {
                    return Default.NotFound;
                }

                var response = _mapper.Map<QuestionDto>(question);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
        }
    }
}
