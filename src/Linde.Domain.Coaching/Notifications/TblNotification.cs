using Linde.Domain.Coaching.Common;
using Linde.Domain.Coaching.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Domain.Coaching.Notifications
{
    public class TblNotification : Entity
    {
        public TblNotification()
        {
            Visible = true;
            Sent = false;
        }
        public int NotificationId { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
        public TypeNotifications TypeNotification { get; set; }
        public bool Sent { get; set; }
    }
}
