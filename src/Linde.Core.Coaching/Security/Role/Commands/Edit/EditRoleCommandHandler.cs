using ErrorOr;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Security.Permission.Specifications;
using Linde.Core.Coaching.Security.Role.Specifications;
using Linde.Domain.Coaching.PermissionAggregate.ValueObjects;
using Linde.Domain.Coaching.RoleAggregate.Entities;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Security.Role.Commands.Edit;

internal class EditRoleCommandHandler : IRequestHandler<EditRoleCommand, ErrorOr<Unit>>
{
    private readonly IRepository<Domain.Coaching.RoleAggregate.Role> _repository;
    private readonly IRepository<Domain.Coaching.PermissionAggregate.Permission> _permissionRepository;
    private readonly ILogger<EditRoleCommandHandler> _logger;

    public EditRoleCommandHandler(
        IRepository<Domain.Coaching.RoleAggregate.Role> repository,
        IRepository<Domain.Coaching.PermissionAggregate.Permission> permissionRepository,
        ILogger<EditRoleCommandHandler> logger)
    {
        _repository = repository;
        _permissionRepository = permissionRepository;
        _logger = logger;
    }

    public async Task<ErrorOr<Unit>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            bool hasChanges = false;

            var role = await _repository.FirstOrDefaultAsync(new RoleWhereSpecification(RoleId.Create(request.Id)));

            if (role is null)
                return Default.NotFound;

            if (role.Name != request.Name.Trim().ToUpper() || role.Description != request.Description.Trim())
            {
                role.UpdateData(request.Name, request.Description);

                hasChanges = true;
            }

            var permissionRequestIds = request.Permissions
                .Select(x => PermissionId.Create(x));

            if (!permissionRequestIds.All(x => role.PermissionItems.Any(y => y.PermissionId == x)) || !role.PermissionItems.All(x => permissionRequestIds.Contains(x.PermissionId)))
            {
                var permissionIds = await _permissionRepository.ListAsync(new PermissionIdSpecification(permissionRequestIds));

                if (!permissionRequestIds.All(x => permissionIds.Contains(x)))
                    return Errors.Role.InvalidPermission;

                var permissions = permissionRequestIds
                    .Select(x => RolePermission.Create(x))
                    .ToList();

                role.UpdatePermissions(permissions);

                hasChanges = true;
            }

            if (hasChanges)
                await _repository.SaveChangesAsync();

            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}
