using Linde.Domain.Coaching.DivisionAggregate.ValueObjects;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;
using Linde.Domain.Coaching.RoleAggregate;
using Linde.Domain.Coaching.Common;
using Linde.Domain.Coaching.UserAggreagate.ValueObjects;

namespace Linde.Domain.Coaching.DivisionAggregate;

public sealed class Division : Entity<DivisionId>
{
    public string Name { get; private set; }

    private Division(DivisionId divisionId, string name)
    {
        Id = divisionId;
        Name = name;
    }

    public static Division Create(DivisionId divisionId, string name)
    {
        return new(
            divisionId,
            name);
    }
    private Division() { }

    public void UpdateName(string name)
    {
        Name = name;
    }
}