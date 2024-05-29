using ErrorOr;
using Linde.Core.Coaching.Catalogs.Country.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Security.User.Specifications;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;
using Linde.Domain.Coaching.UserAggreagate.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Security.User.Commands.Delete;

internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ErrorOr<Unit>>
{
    private readonly ILogger<DeleteUserCommandHandler> _logger;
    private readonly IRepository<Domain.Coaching.UserAggreagate.User> _usersRepository;

    public DeleteUserCommandHandler(ILogger<DeleteUserCommandHandler> logger, IRepository<Domain.Coaching.UserAggreagate.User> usersRepository)
    {
        _logger = logger;
        _usersRepository = usersRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _usersRepository.FirstOrDefaultAsync(new UserWhereSpecification(UserId.Create(request.UserId)));
            if (user is null)
                return Default.NotFound;

            if (user.UserName.ToUpper().Contains("ADMIN"))
            {
                return Errors.User.UnableRemoveAdmin;
            }

            //Solo sera eliminacion logica --> Israel indica 24 Nov 2023
            //await _usersRepository.DeleteAsync(user);
            user.Visible = false;
            await _usersRepository.UpdateAsync(user);
            await _usersRepository.SaveChangesAsync();

            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
        throw new NotImplementedException();
    }
}
