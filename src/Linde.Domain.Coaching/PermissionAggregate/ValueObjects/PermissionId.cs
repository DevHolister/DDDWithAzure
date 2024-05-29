using Linde.Domain.Coaching.Common;

namespace Linde.Domain.Coaching.PermissionAggregate.ValueObjects;

public sealed class PermissionId : ValueObject
{
    public Guid Value { get; }

    private PermissionId(Guid value)
    {
        Value = value;
    }
    public static PermissionId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    public static PermissionId Create(Guid value)
    {
        return new(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
