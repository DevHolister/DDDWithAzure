using ErrorOr;
using Linde.Core.Coaching.Catalogs.Questions.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Catalogs.Questions;
using Linde.Domain.Coaching.Questions;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Catalogs.Questions.Queries.GetAll
{
    public class GetAllQuestionsQueryHandler : IRequestHandler<GetAllQuestionsQuery, ErrorOr<PaginatedListDto<QuestionDto>>>
    {
        private readonly ILogger<GetAllQuestionsQueryHandler> _logger;
        private readonly IRepository<CatQuestions> _repository;
        private readonly IMapper _mapper;

        public GetAllQuestionsQueryHandler(
            ILogger<GetAllQuestionsQueryHandler> logger,
            IRepository<CatQuestions> repository,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ErrorOr<PaginatedListDto<QuestionDto>>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var total = await _repository.CountAsync(new QuestionSpecification(
                    request.question!,
                    request.Activity!,
                    request.IsCritical), cancellationToken);

                var pageSize = request.PageSize < 0 ? total : request.PageSize;

                var questions = await _repository.ListAsync(new QuestionSpecification(
                    request.question!,
                    request.Activity!,
                    request.IsCritical,
                    request.Page,
                    pageSize,
                    true), cancellationToken);

                List<QuestionDto> response = _mapper.Map<List<QuestionDto>>(questions);

                return new PaginatedListDto<QuestionDto>(total, pageSize, request.Page, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
        }
    }
}
