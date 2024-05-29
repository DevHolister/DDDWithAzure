using ErrorOr;
using Linde.Core.Coaching.Catalogs.Questions.Queries.GetAll;
using Linde.Core.Coaching.Common.Models.Catalogs.Questions;
using Linde.Core.Coaching.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linde.Core.Coaching.Common.Models.Catalogs.Checklist;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Domain.Coaching.Questions;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Linde.Domain.Coaching.ChecklistAggregate;
using Linde.Core.Coaching.Catalogs.Questions.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Catalogs.Checklists.Specifications;

namespace Linde.Core.Coaching.Catalogs.Checklists.Queries.GetAll
{
    public class GetAllChecklistsQueryHandler : IRequestHandler<GetAllCheklistsQuery, ErrorOr<PaginatedListDto<ChecklistDto>>>
    {
        private readonly ILogger<GetAllChecklistsQueryHandler> _logger;
        private readonly IRepository<CatChecklist> _repository;
        private readonly IMapper _mapper;

        public GetAllChecklistsQueryHandler(
            ILogger<GetAllChecklistsQueryHandler> logger,
            IRepository<CatChecklist> repository,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ErrorOr<PaginatedListDto<ChecklistDto>>> Handle(GetAllCheklistsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var total = await _repository.CountAsync(new ChecklistSpecification(
                    request.Name!,
                    request.Question!), cancellationToken);

                var pageSize = request.PageSize < 0 ? total : request.PageSize;

                var checklists = await _repository.ListAsync(new ChecklistSpecification(
                    request.Name!,
                    request.Question!,
                    request.Page,
                    pageSize,
                    true), cancellationToken);

                List<ChecklistDto> response = _mapper.Map<List<ChecklistDto>>(checklists);

                return new PaginatedListDto<ChecklistDto>(total, pageSize, request.Page, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
        }
    }
}
