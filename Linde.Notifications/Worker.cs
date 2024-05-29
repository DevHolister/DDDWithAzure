using ErrorOr;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Notifications.Specifications;
using Linde.Domain.Coaching.Common.Enum;
using Linde.Domain.Coaching.Notifications;
using Linde.Notifications.Coaching.Notifications.TestNotification;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Notifications.Coaching
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly Timer _timer;
        private IMediator _mediator;
        private readonly IRepository<TblNotification> _repository;

        public Worker(ILogger<Worker> logger,
            IMapper mapper,
            IServiceProvider serviceProvider,
            IConfiguration _configuration,
            IRepository<TblNotification> repository)
        {
            _logger = logger;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
            _repository = repository;
            if (!int.TryParse(_configuration["minutesTask"], out var time))
                _logger.LogError("La configuracion del tiempo de ejecución es incorrecta.");
            else
                _timer = new Timer(ReadNotificationsAsync, null, TimeSpan.Zero, TimeSpan.FromMinutes(time));
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Inicia Proceso Recurrente notificaciones.");
                    WaitHandle.WaitAny(new[] { stoppingToken.WaitHandle });
                }
            }
            finally
            {
                _logger.LogInformation("ShutDown Worker: {time}", DateTimeOffset.Now);
            }
        }
        private async void ReadNotificationsAsync(object state)
        {
            _logger.LogInformation("Inicia Proceso Recurrente notificaciones.");
            using var scope = _serviceProvider.CreateScope();
            var notificationsWaiting = await _repository.ListAsync(new NotificationSpecifictions());
            if (notificationsWaiting?.Any() ?? false)
            {
                _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                foreach (var item in notificationsWaiting.OrderBy(x => x.CreatedAt))
                {   
                    await ExecuteNotification(item);
                    item.Sent = true;
                    await _repository.UpdateAsync(item);
                }
            }
            _logger.LogInformation("Termina Proceso Recurrente notificaciones.");
        }

        private async Task<ErrorOr<Unit>> ExecuteNotification(TblNotification queue)
        {
            try
            {
                var request = (dynamic)null;
                switch (queue.TypeNotification)
                {
                    case TypeNotifications.TestNotification:
                        request = _mapper.Map<TestNotification>(queue);
                        break;
                    default:
                        break;
                }
                await _mediator.Publish(request);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }

        }
    }
}
