using ErrorOr;
using Linde.Core.Coaching.Catalogs.Checklists.Specifications;
using Linde.Core.Coaching.Catalogs.Questions.Queries.GetById;
using Linde.Core.Coaching.Catalogs.Questions.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Models.Catalogs.Checklist;
using Linde.Core.Coaching.Common.Models.Catalogs.Questions;
using Linde.Domain.Coaching.ChecklistAggregate;
using Linde.Domain.Coaching.ChecklistAggregate.ValueObjects;
using Linde.Domain.Coaching.Questions;
using Linde.Domain.Coaching.Questionsaggregate.ValueObjects;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Checklists.Queries.GetById
{
    internal class GetChecklistByIdQueryHandler : IRequestHandler<GetChecklistByIdQuery, ErrorOr<ChecklistDto>>
    {
        private readonly ILogger<GetChecklistByIdQueryHandler> _logger;
        private readonly IRepository<CatChecklist> _repository;
        private readonly IMapper _mapper;

        public GetChecklistByIdQueryHandler(
            ILogger<GetChecklistByIdQueryHandler> logger,
            IRepository<CatChecklist> repository,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ErrorOr<ChecklistDto>> Handle(GetChecklistByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var checklist = await _repository.FirstOrDefaultAsync(new ChecklistSpecification(ChecklistId.Create(request.QuestionId), true), cancellationToken);

                if (checklist is null)
                {
                    return Default.NotFound;
                }

                var response = _mapper.Map<ChecklistDto>(checklist);

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
