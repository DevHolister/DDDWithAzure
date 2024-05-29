using ErrorOr;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Services;
using Linde.Core.Coaching.Common.Models.Security.User;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Linde.Core.Coaching.Security.User.Queries.FindAtAD;

internal class FindUserAtADQueryHandler : IRequestHandler<FindUserAtADQuery, ErrorOr<UserQueryDto>>
{
    private readonly IAccountService _accountService;
    private readonly ILogger<FindUserAtADQueryHandler> _logger;

    public FindUserAtADQueryHandler(
        IAccountService accountService,
        ILogger<FindUserAtADQueryHandler> logger)
    {
        _accountService = accountService;
        _logger = logger;
    }
    public async Task<ErrorOr<UserQueryDto>> Handle(FindUserAtADQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(request.EmployeeNumber?.Trim()) && string.IsNullOrEmpty(request.UserName?.Trim()))
                return Default.ValidationError;

            var user = await _accountService.FindUserAsync(userName: request.UserName, employeeNumber: request.EmployeeNumber);

            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error.");
            return Default.ServerError;
        }
    }
}
