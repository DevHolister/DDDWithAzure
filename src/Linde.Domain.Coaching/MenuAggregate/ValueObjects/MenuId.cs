using Linde.Domain.Coaching.DivisionAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Domain.Coaching.MenuAggregate.ValueObjects
{
    public class MenuId
    {
        public Guid Value { get; }

        private MenuId(Guid value)
        {
            Value = value;
        }
        public static MenuId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
        public static MenuId Create(Guid value)
        {
            return new(value);
        }
    }
}
