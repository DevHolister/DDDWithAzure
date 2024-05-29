using Linde.Core.Coaching;
using Linde.Infrastructure.Coaching;
using Linde.Notifications.Coaching;
using Linde.Persistence.Coaching;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices(services =>
    {
        services.AddAutoMapperNotification();
        services.AddApplicationLayer();
        services.AddInfrastructureLayer(configuration);
        services.AddPersistenceLayer(configuration);

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Worker)));
        services.AddHostedService<Worker>();
        services.AddSignalR(hubOptions =>
        {
            hubOptions.EnableDetailedErrors = true;
            hubOptions.KeepAliveInterval = TimeSpan.FromMinutes(2);
        });
    })
    .ConfigureHostConfiguration(hostConfig =>
    {
        hostConfig.SetBasePath(Directory.GetCurrentDirectory());
        hostConfig.AddJsonFile("appsettings.json", optional: true);
    })
    .Build();


await host.RunAsync();

