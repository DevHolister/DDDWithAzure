using Linde.Domain.Coaching.RoleAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Domain.Coaching.DivisionAggregate.ValueObjects
{
    public class DivisionId
    {
        public Guid Value { get; }

        private DivisionId(Guid value)
        {
            Value = value;
        }
        public static DivisionId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
        public static DivisionId Create(Guid value)
        {
            return new(value);
        }
    }
}
