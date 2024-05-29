using Ardalis.Specification;
using Linde.Domain.Coaching.Notifications;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Notifications.Specifications
{
    public class NotificationSpecifictions : Specification<TblNotification>
    {
        public NotificationSpecifictions()
        {
            Query.AsNoTracking()
            .Where(c => c.Visible && !c.Sent);
        }
    }
}
