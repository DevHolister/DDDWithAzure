namespace Linde.Core.Coaching.Common.Models.Security.User;

public record AuthenticateResponse(
    //string CountryCode,
    //string CountryId,
    string UserName,
    IEnumerable<string> Permissions,
    bool IsVerified,
    string AccessToken,
    string TokenType,
    double ExpiresIn,
    string ExpiresAt,
    string UserId,
    string FullName,
    string CountryId);