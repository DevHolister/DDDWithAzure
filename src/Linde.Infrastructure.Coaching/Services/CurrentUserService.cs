using Linde.Core.Coaching.Common.Interfaces.Services;
using Linde.Infrastructure.Coaching.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Linde.Infrastructure.Coaching.Services;

internal class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public CurrentUserService(
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration
    )
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException();
        _configuration = configuration;
    }

    public bool IsAuthenticated => _httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    public string IpAddress => throw new NotImplementedException();

    public string UserName => _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "";

    public string UserId => !string.IsNullOrEmpty(_httpContextAccessor.HttpContext?.User?.Identity?.Name) ? GetClaimByType("UserId") : _configuration["AdministratorId"];

    public IEnumerable<string> Permissions => GetClaimsByType(CustomClaims.Permissions);

    public string CountryId => !string.IsNullOrEmpty(_httpContextAccessor.HttpContext?.User?.Identity?.Name) ? GetClaimByType("CountryId") : _configuration["CountryId"];

    public string GetClaimByType(string type)
    {
        var _Claim = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == type);
        return _Claim?.Value ?? string.Empty;
    }

    public IEnumerable<string> GetClaimsByType(string type)
    {
        var _Claims = _httpContextAccessor?.HttpContext?.User?.Claims?.Where(x => x.Type == type).Select(x => x.Value);
        return _Claims ?? new HashSet<string>();
    }
}
