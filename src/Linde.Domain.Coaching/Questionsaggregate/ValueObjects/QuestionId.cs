using Linde.Domain.Coaching.Common;
using Linde.Domain.Coaching.DivisionAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Domain.Coaching.Questionsaggregate.ValueObjects
{
    public class QuestionId : ValueObject
    {
        public Guid Value { get; }
        private QuestionId(Guid value)
        {
            Value = value;
        }
        public static QuestionId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
        public static QuestionId Create(Guid value)
        {
            return new(value);
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
