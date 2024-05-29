using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Persistence.Coaching.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Linde.Persistence.Coaching;

public static class ServiceExtensions
{
    public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CoachingDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("CoachingConnection")!,
                x => x.MigrationsAssembly(typeof(CoachingDbContext).Assembly.FullName).EnableRetryOnFailure()));

        //services.AddIdentity<ApplicationUser, ApplicationRole>()
        //    .AddEntityFrameworkStores<ChatDbContext>()
        //    .AddDefaultTokenProviders();

        //services.Configure<IdentityOptions>(options =>
        //{
        //    //PasswordSetings
        //    options.Password.RequiredLength = 6;
        //    options.Password.RequireDigit = true;
        //    options.Password.RequireNonAlphanumeric = false;
        //    options.Password.RequireUppercase = false;
        //    options.Password.RequireLowercase = false;

        //    //Lockout Settings
        //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(730);
        //    options.Lockout.MaxFailedAccessAttempts = int.TryParse(configuration["accessFailedCount"], out int result) ? result : 5; // parametrizable
        //    options.User.RequireUniqueEmail = true;
        //});

        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
    }
}
