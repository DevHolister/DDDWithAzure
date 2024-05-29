using Linde.Domain.Coaching.Common;

namespace Linde.Domain.Coaching.UserAggreagate.ValueObjects;

public sealed class Password : ValueObject
{
    public string Value { get; private set; }

    private Password(string password)
    {
        Value = password;
    }

    public static Password Create(string password)
    {
        return new(password);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
