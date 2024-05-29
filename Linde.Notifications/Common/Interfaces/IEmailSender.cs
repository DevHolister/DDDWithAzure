using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Notifications.Coaching.Common.Interfaces
{
    public interface IEmailSender
    {
        Task Send(string subject, string to, string body);
        Task<string> GetTemplateByName(string template);
    }
}
