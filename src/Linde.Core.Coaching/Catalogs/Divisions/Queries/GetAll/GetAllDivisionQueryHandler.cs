using ErrorOr;
using Linde.Core.Coaching.Catalogs.Divisions.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Interfaces.Services;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Catalog.Division;
using Linde.Core.Coaching.Common.Models.Security.Role;
using Linde.Core.Coaching.Security.Role.Queries.GetAll;
using Linde.Core.Coaching.Security.Role.Specifications;
using Linde.Core.Coaching.Security.User.Queries.GetAll;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Divisions.Queries.GetAll
{
    internal class GetAllDivisionQueryHandler : IRequestHandler<GetAllDivisionsQuery, ErrorOr<PaginatedListDto<DivisionDto>>>
    {
        private readonly IRepository<Domain.Coaching.DivisionAggregate.Division> _repository;
        private readonly ILogger<GetAllDivisionQueryHandler> _logger;

        public GetAllDivisionQueryHandler(
        IRepository<Domain.Coaching.DivisionAggregate.Division> repository,
        ILogger<GetAllDivisionQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ErrorOr<PaginatedListDto<DivisionDto>>> Handle(GetAllDivisionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var divisions = await _repository.ListAsync(new DivisionMapSpecification(
                    request.name!,
                    request.Page,
                    request.PageSize,
                    true), cancellationToken);

                var total = await _repository.CountAsync(new DivisionMapSpecification(
                    request.name!), cancellationToken);

                return new PaginatedListDto<DivisionDto>(total, request.PageSize, request.Page, divisions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
        }
    }
}
