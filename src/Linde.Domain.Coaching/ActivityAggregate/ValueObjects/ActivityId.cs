using Linde.Domain.Coaching.DivisionAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Domain.Coaching.ActivityAggregate.ValueObjects
{
    public class ActivityId
    {
        public Guid Value { get; }

        private ActivityId(Guid value)
        {
            Value = value;
        }
        public static ActivityId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
        public static ActivityId Create(Guid value)
        {
            return new(value);
        }
    }
}
