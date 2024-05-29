using ErrorOr;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Security.User.Specifications;
using Linde.Domain.Coaching.UserAggreagate.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Security.User.Commands.Lockout;

internal class LockoutUserCommandHandler : IRequestHandler<LockoutUserCommand, ErrorOr<Unit>>
{
    private readonly IRepository<Domain.Coaching.UserAggreagate.User> _repository;
    private readonly ILogger<LockoutUserCommandHandler> _logger;

    public LockoutUserCommandHandler(
        IRepository<Domain.Coaching.UserAggreagate.User> repository,
        ILogger<LockoutUserCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ErrorOr<Unit>> Handle(LockoutUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _repository.FirstOrDefaultAsync(new UserWhereSpecification(UserId.Create(request.Id)));

            if (user is null)
                return Default.NotFound;

            user.SetLockout(request.Lockout);

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
