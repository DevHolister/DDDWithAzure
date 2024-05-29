using Linde.Core.Coaching.Common.Interfaces.Services;
using Linde.Core.Coaching.Common.Models.Security.Role;
using Linde.Core.Coaching.Common.Models.Security.User;
using Linde.Domain.Coaching.Settings;
using Linde.Domain.Coaching.UserAggreagate;
using Linde.Infrastructure.Coaching.Authentication;
using Linde.Infrastructure.Coaching.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServiceAD;
using System.DirectoryServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Serialization;

namespace Linde.Infrastructure.Coaching.Services;

internal class AccountService : IAccountService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AccountService> _logger;
    private readonly JwtSettings _jwtSettings;

    public AccountService(
        IConfiguration configuration,
        IOptions<JwtSettings> options,
        ILogger<AccountService> logger)
    {
        _configuration = configuration;
        _logger = logger;
        _jwtSettings = options.Value;
    }

    public async Task<bool> AuthenticateWcf(string userName, string password)
    {
        PrxMxServicesSoapClient service = new PrxMxServicesSoapClient(
            PrxMxServicesSoapClient.EndpointConfiguration.PrxMxServicesSoap,
            _configuration["prxMxServicesUrl"]);

        var authenticated = await service.loginAsync(userName.Trim(), password.Trim());

        return authenticated;
    }

    public async Task<UserQueryDto> FindUserAsync(string userName = "", string employeeNumber = "")
    {
        var _service = new PrxMxServicesSoapClient(
            PrxMxServicesSoapClient.EndpointConfiguration.PrxMxServicesSoap,
            _configuration["prxMxServicesUrl"]);
        
        var user = await _service.userDataAsync(userName, employeeNumber);

        return DeserializeUserData(user);
    }

    private UserQueryDto DeserializeUserData(userDataResponseUserDataResult user)
    {
        XmlSerializer serializer = user.Any1.InnerXml.Contains("NewDataSet ") ? new(typeof(NewDataSet)) : new(typeof(DocumentElement));

        using var stringReader = new StringReader(user.Any1.InnerXml);
        if (user.Any1.InnerXml.Contains("NewDataSet "))
        {
            var userData = (NewDataSet)serializer.Deserialize(stringReader)!;
            return new UserQueryDto(
                Name: userData.UserData.NOMBRE,
                UserName: userData.UserData.USUARIO,
                JobTitle: userData.UserData.EMP_PUESTO,
                Email: userData.UserData.EMAIL,
                EmployeeNumber: string.Empty,
                Country: string.Empty,
                Branch: string.Empty);
        }
        else
        {
            var userData = (DocumentElement)serializer.Deserialize(stringReader)!;
            return new UserQueryDto(
                Name: userData.UserData.NOMBRE,
                UserName: userData.UserData.USUARIO,
                JobTitle: userData.UserData.EMP_PUESTO,
                Email: userData.UserData.EMAIL,
                EmployeeNumber: string.Empty,
                Country: string.Empty,
                Branch: string.Empty);
        }
    }

    public AuthenticateResponse GenerateAccessToken(User user)
    {
        /*
         * TODO:
         * Implementar funcionalidad para información de paises.
         */
        string completename = user.FullName;
        //string completename = user.FullName.Value;
        var countryId = user.CountryItems
            .OrderBy(x => x.CreatedAt)
            .Select(x => x.CountryId)
            .FirstOrDefault();
        List<Claim> claims = new()
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                //new Claim("CountryCode",  countryCode),
                new Claim("CountryId", countryId!.Value.ToString()),
                new Claim("UserId", user.Id.Value.ToString()),
        };

        var permissions = GetPermissions(user);

        claims.AddRange(permissions.Select(x => new Claim(CustomClaims.Permissions, x)).ToList());

        var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

        var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var expired = DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expired,
            signingCredentials: signingCredentials);

        var response = new AuthenticateResponse(
            IsVerified: true,
            AccessToken: new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            UserName: user.UserName,
            ExpiresIn: this._jwtSettings.DurationInMinutes,
            ExpiresAt: expired.ToString(),
            Permissions: permissions,
            TokenType: "bearer",
            //CountryCode: countryCode,
            CountryId: countryId.Value.ToString(),
            UserId: user.Id.Value.ToString(),
            FullName: completename);

        return response;
    }

    private IEnumerable<string> GetPermissions(User user)
    {
        var permissions = user.RoleItems
            .Select(x => x.Role)
            .SelectMany(x => x.PermissionItems)
            .Select(x => x.Permission)
            .Select(x => new PermissionAccessDto(x.Path, x.Actions));
        return permissions.GroupBy(x => x.Module)
            .Select(x => $"{x.Key}:{string.Join(",", x.Where(y => y.Access?.Any() ?? false).SelectMany(y => y.Access).Distinct())}");
    }

    public async Task<bool> AuthenticateLdap(string userName, string password)
    {
        return await Task.Run(() =>
        {
            try
            {
                //string ldap = $"LDAP://{_configuration["pathLdap"]}";
                //using DirectoryEntry directoryEntry = new(ldap, userName, password);
                //var nativeObject = directoryEntry.NativeObject!;
                return true;
            }
            catch (DirectoryServicesCOMException ex)
            {
                _logger.LogError(ex, "Error.");
                return false;
            }
        });
    }
}
