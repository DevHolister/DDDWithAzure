using Linde.Domain.Coaching.Common;

namespace Linde.Domain.Coaching.UserAggreagate.ValueObjects;

public sealed class FullName : ValueObject
{
    public string Name { get; private set; }
    public string FirstSurname { get; private set; }
    public string SecondSurname { get; private set; }

    public string Value => $"{Name} {FirstSurname} {SecondSurname}";

    private FullName(string name,
        string firstSurname,
        string secondSurname)
    {
        Name = name;
        FirstSurname = firstSurname;
        SecondSurname = secondSurname;
    }

    public static FullName? Create(string name,
        string firstSurname,
        string secondSurname)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(firstSurname) || string.IsNullOrEmpty(secondSurname))
            return null;

        return new(name, firstSurname, secondSurname);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return FirstSurname;
        yield return SecondSurname;
    }
}
