using ErrorOr;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Security.Role.Specifications;
using Linde.Core.Coaching.Security.User.Specifications;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Security.Role.Commands.Delete;

internal class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ErrorOr<Unit>>
{
    private readonly IRepository<Domain.Coaching.RoleAggregate.Role> _repository;
    private readonly ILogger<DeleteRoleCommandHandler> _logger;
    private readonly IRepository<Domain.Coaching.UserAggreagate.User> _repositoryUser;

    public DeleteRoleCommandHandler(
        IRepository<Domain.Coaching.RoleAggregate.Role> repository,
        ILogger<DeleteRoleCommandHandler> logger,
        IRepository<Domain.Coaching.UserAggreagate.User> repositoryUser)
    {
        _repository = repository;
        _logger = logger;
        _repositoryUser = repositoryUser;
    }
    public async Task<ErrorOr<Unit>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var role = await _repository.FirstOrDefaultAsync(new RoleWhereSpecification(RoleId.Create(request.Id)));

            if (role is null)
                return Default.NotFound;

            var userRole = await _repositoryUser.ListAsync(new UserWhereSpecification(RoleId.Create(request.Id), true));
            if(userRole is not null && userRole.Count > 0)
            {
                return Errors.Role.HasUser;
            }

            await _repository.DeleteAsync(role!);

            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}
