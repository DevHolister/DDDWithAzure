using Linde.Domain.Coaching.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Domain.Coaching.ChecklistAggregate.ValueObjects
{
    public class ChecklistId : ValueObject
    {
        public Guid Value { get; }
        private ChecklistId(Guid value)
        {
            Value = value;
        }
        public static ChecklistId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
        public static ChecklistId Create(Guid value)
        {
            return new(value);
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
