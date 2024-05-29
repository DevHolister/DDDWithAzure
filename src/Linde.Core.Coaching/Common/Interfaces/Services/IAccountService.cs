using Linde.Core.Coaching.Common.Models.Security.User;
using Linde.Domain.Coaching.UserAggreagate;

namespace Linde.Core.Coaching.Common.Interfaces.Services;

public interface IAccountService
{
    Task<UserQueryDto> FindUserAsync(string userName = "", string employeeNumber = "");
    Task<bool> AuthenticateWcf(string userName, string password);
    Task<bool> AuthenticateLdap(string userName, string password);
    AuthenticateResponse GenerateAccessToken(User user);
}
