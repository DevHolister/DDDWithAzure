using Linde.Core.Coaching.Common.Interfaces.Services;
using Linde.Domain.Coaching.Settings;
using Linde.Infrastructure.Coaching.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace Linde.Infrastructure.Coaching;

public static class ServiceExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
            };
            options.Events = new JwtBearerEvents()
            {
                OnAuthenticationFailed = context =>
                {
                    context.NoResult();
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";
                    ProblemDetails responseModel = new()
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Type = "Unauthorized",
                        Title = "Usuario no autenticado.",
                    };
                    var response = JsonConvert.SerializeObject(responseModel);
                    return context.Response.WriteAsync(response);
                },
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    if (!(context?.HttpContext?.User?.Identity?.IsAuthenticated ?? false) && !context.Response.HasStarted)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                        ProblemDetails responseModel = new()
                        {
                            Status = StatusCodes.Status401Unauthorized,
                            Type = "Unauthorized",
                            Title = "Permisos insuficientes.",
                        };
                        var response = JsonConvert.SerializeObject(responseModel);
                        return context.Response.WriteAsync(response);
                    }

                    return Task.CompletedTask;
                },
                OnForbidden = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    context.Response.ContentType = "application/json";
                    ProblemDetails responseModel = new()
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Type = "Unauthorized",
                        Title = "Permisos insuficientes.",
                    };
                    var response = JsonConvert.SerializeObject(responseModel);
                    return context.Response.WriteAsync(response);
                }
            };
        });

        services.AddHttpContextAccessor();

        services.AddTransient<ICurrentUserService, CurrentUserService>();

        services.AddTransient<IAccountService, AccountService>();
    }
}
