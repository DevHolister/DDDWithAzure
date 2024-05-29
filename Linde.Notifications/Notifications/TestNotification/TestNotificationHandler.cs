using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Domain.Coaching.Common.Enum;
using Linde.Domain.Coaching.Notifications;
using Linde.Notifications.Coaching.Common.Interfaces;
using Linde.Notifications.Coaching.Notifications.Helpers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Linde.Notifications.Coaching.Notifications.TestNotification
{
    public class TestNotificationHandler : INotificationHandler<TestNotification>
    {
        private readonly ILogger<TestNotificationHandler> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly IRepository<TblNotification> _repositoryNotification;
        public TestNotificationHandler(ILogger<TestNotificationHandler> logger,
            IEmailSender emailSender,
            IConfiguration configuration)
        {
            _logger = logger;
            _emailSender = emailSender;
            _configuration = configuration;
            IRepository<TblNotification> repositoryNotification;
        }
        public async Task Handle(TestNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Inicia envio de notificación de prueba");
                string email = "daniel.basconselos@imfostware.pro";
                string applicationpath = _configuration["endpoints:webapp"];
                string body = (await _emailSender.GetTemplateByName("ModelTemplate"))
                 .Replace("@link", applicationpath);
                _logger.LogInformation("Email: [" + email.Trim() + "],  Tipo: " + TypeNotifications.TestNotification.ToString());
                await _emailSender.Send(notification.Subject, email, body);
                _logger.LogInformation("Termina envio de notificación de prueba");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
            }
        }
    }
}
