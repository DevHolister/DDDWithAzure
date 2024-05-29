using ErrorOr;
using Linde.Core.Coaching.Common.Models.Security.Role;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Security.Role.Queries.GetAll;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Security.Role.Specifications;
using Microsoft.Extensions.Logging;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;

namespace Linde.Core.Coaching.Security.Role.Queries.GetById
{
    internal class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, ErrorOr<RoleDto>>
    {
        private readonly IRepository<Domain.Coaching.RoleAggregate.Role> _repository;
        private readonly ILogger<GetRoleByIdQueryHandler> _logger;

        public GetRoleByIdQueryHandler(
            IRepository<Domain.Coaching.RoleAggregate.Role> repository,
            ILogger<GetRoleByIdQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ErrorOr<RoleDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _repository.FirstOrDefaultAsync(new RoleMapSpecification(RoleId.Create(request.RoleId)), cancellationToken);
                if (role is null)
                    return Default.NotFound;

                return role;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
        }
    }
}
