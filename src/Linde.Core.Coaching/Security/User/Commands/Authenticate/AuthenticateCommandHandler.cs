using ErrorOr;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Interfaces.Services;
using Linde.Core.Coaching.Common.Models.Security.User;
using Linde.Core.Coaching.Security.User.Specifications;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Security.User.Commands.Authenticate;

internal class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, ErrorOr<AuthenticateResponse>>
{
    private readonly IRepository<Domain.Coaching.UserAggreagate.User> _repository;
    private readonly IAccountService _accountService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthenticateCommandHandler> _logger;

    public AuthenticateCommandHandler(
        IRepository<Domain.Coaching.UserAggreagate.User> repository,
        IAccountService accountService,
        IConfiguration configuration,
        ILogger<AuthenticateCommandHandler> logger)
    {
        _repository = repository;
        _accountService = accountService;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<ErrorOr<AuthenticateResponse>> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _repository.FirstOrDefaultAsync(new UserWhereSpecification(request.UserName?.Trim() ?? "", true));

            if (user is null)
                return Errors.User.UserNotFound;

            if (user.Lockout)
                return Errors.User.Lockout;

            var result = await _accountService.AuthenticateLdap(request.UserName!, request.Password);

            if (result)
            {
                user.ResetAccessFailed();

                await _repository.UpdateAsync(user);

                return _accountService.GenerateAccessToken(user);
            }

            var accessFailedAllowed = GetAccessFailedConfig();

            user.IncreaseAccessFailed(accessFailedAllowed);

            await _repository.UpdateAsync(user);

            if (user.AccessFailedCount < accessFailedAllowed)
                return Errors.User.InvalidCredentials(accessFailedAllowed, user.AccessFailedCount);

            return Errors.User.LockoutUser;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }

    private int GetAccessFailedConfig()
    {
        /* 
         TODO:
         Validar número de intentos fallidos de credenciales, se deja 3 por default.
        */
        var value = _configuration["accessFailedCount"];
        return int.TryParse(value, out int parseValue) ? parseValue : 3;
    }
}
