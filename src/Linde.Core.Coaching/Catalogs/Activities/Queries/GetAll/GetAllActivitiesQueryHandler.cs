using ErrorOr;
using Linde.Core.Coaching.Catalogs.Divisions.Queries.GetAll;
using Linde.Core.Coaching.Common.Models.Catalog.Division;
using Linde.Core.Coaching.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Microsoft.Extensions.Logging;
using Linde.Core.Coaching.Common.Models.Catalog.Activity;
using Linde.Core.Coaching.Catalogs.Divisions.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Catalogs.Activities.Specifications;

namespace Linde.Core.Coaching.Catalogs.Activities.Queries.GetAll
{
    internal class GetAllActivitiesQueryHandler : IRequestHandler<GetAllActivitiesQuery, ErrorOr<PaginatedListDto<ActivityDTO>>>
    {
        private readonly IRepository<Domain.Coaching.ActivityAggregate.Activity> _repository;
        private readonly ILogger<GetAllActivitiesQueryHandler> _logger;

        public GetAllActivitiesQueryHandler(
        IRepository<Domain.Coaching.ActivityAggregate.Activity> repository,
        ILogger<GetAllActivitiesQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ErrorOr<PaginatedListDto<ActivityDTO>>> Handle(GetAllActivitiesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var activities = await _repository.ListAsync(new ActivityMapSpecification(
                    request.name!,
                    request.description!,
                    request.Page,
                    request.PageSize,
                    true), cancellationToken);

                var total = await _repository.CountAsync(new ActivityMapSpecification(
                    request.name!, request.description!), cancellationToken);

                return new PaginatedListDto<ActivityDTO>(total, request.PageSize, request.Page, activities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
        }
    }
}
