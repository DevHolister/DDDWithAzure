using ErrorOr;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Security.Role;
using Linde.Core.Coaching.Security.Role.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Security.Role.Queries.GetAll;

internal class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, ErrorOr<PaginatedListDto<RoleDto>>>
{
    private readonly IRepository<Domain.Coaching.RoleAggregate.Role> _repository;
    private readonly ILogger<GetAllRolesQueryHandler> _logger;

    public GetAllRolesQueryHandler(
        IRepository<Domain.Coaching.RoleAggregate.Role> repository,
        ILogger<GetAllRolesQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ErrorOr<PaginatedListDto<RoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var total = await _repository.CountAsync(new RoleMapSpecification(
                request.Name!,
                request.Description!,
                request.Permission!), cancellationToken);

            var pageSize = request.PageSize < 0 ? total : request.PageSize;

            var roles = await _repository.ListAsync(new RoleMapSpecification(
                request.Name!,
                request.Description!,
                request.Permission!,
                request.Page,
                pageSize,
                true), cancellationToken);            

            return new PaginatedListDto<RoleDto>(total, pageSize, request.Page, roles);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}
