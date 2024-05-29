namespace Linde.Core.Coaching.Common.Interfaces.Services;

public interface ICurrentUserService
{
    string UserName { get; }
    bool IsAuthenticated { get; }
    string IpAddress { get; }
    string UserId { get; }
    IEnumerable<string> Permissions { get; }
    string CountryId { get; }
}
