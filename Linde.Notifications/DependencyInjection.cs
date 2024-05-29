using Linde.Notifications.Coaching.Common.Interfaces;
using Linde.Notifications.Coaching.Notifications.Helpers;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Notifications.Coaching
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAutoMapperNotification(this IServiceCollection services)
        {
            services.AddScoped<IMapper, ServiceMapper>();
            services.AddTransient<IEmailSender, EmailSender>();
            //services.AddScoped(provider => new MapperConfiguration(x =>
            //{
            //    x.AddProfile(new GeneralProfile(provider.GetService<ICurrentUserService>()));
            //    x.AddProfile(new NotificationProfile());
            //}).CreateMapper());

            //services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
