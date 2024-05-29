using ErrorOr;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Security.Permission.Specifications;
using Linde.Core.Coaching.Security.Role.Specifications;
using Linde.Domain.Coaching.PermissionAggregate.ValueObjects;
using Linde.Domain.Coaching.RoleAggregate.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Security.Role.Commands.Create;

public record CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ErrorOr<Guid>>
{
    private readonly IRepository<Domain.Coaching.RoleAggregate.Role> _repository;
    private readonly IRepository<Domain.Coaching.PermissionAggregate.Permission> _permissionRepository;
    private readonly ILogger<CreateRoleCommandHandler> _logger;

    public CreateRoleCommandHandler(
        IRepository<Domain.Coaching.RoleAggregate.Role> repository,
        IRepository<Domain.Coaching.PermissionAggregate.Permission> permissionRepository,
        ILogger<CreateRoleCommandHandler> logger)
    {
        _repository = repository;
        _permissionRepository = permissionRepository;
        _logger = logger;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (await _repository.AnyAsync(new RoleWhereSpecification(request.Name)))
                return Errors.Role.DuplicateRoleName;

            var permissionsRequestIds = request.Permissions
                .Distinct()
                .Select(x => PermissionId.Create(x));

            if (permissionsRequestIds?.Any() ?? false)
            {
                var permissionIds = await _permissionRepository.ListAsync(new PermissionIdSpecification(permissionsRequestIds));

                if (!permissionsRequestIds.All(x => permissionIds.Contains(x)))
                    return Errors.Role.InvalidPermission;
            }

            var role = Domain.Coaching.RoleAggregate.Role.Create(
                name: request.Name.Trim().ToUpper(),
                description: request.Description.Trim());

            var rolePermission = permissionsRequestIds?.Select(x =>
                RolePermission.Create(x))
                .ToList();

            rolePermission?.ForEach(x => role.AddPermission(x));

            await _repository.AddAsync(role);

            return role.Id.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}
