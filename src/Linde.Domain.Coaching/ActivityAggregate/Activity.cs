using Linde.Domain.Coaching.ActivityAggregate.ValueObjects;
using Linde.Domain.Coaching.Common;
using Linde.Domain.Coaching.DivisionAggregate.ValueObjects;
using Linde.Domain.Coaching.DivisionAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linde.Domain.Coaching.Questionsaggregate.Entities;

namespace Linde.Domain.Coaching.ActivityAggregate;

public sealed class Activity : Entity<ActivityId>
{
    private readonly List<QuestionsActivities> _QuestionsActivities = new();
    public IReadOnlyList<QuestionsActivities> QuestionsActivities => _QuestionsActivities.AsReadOnly();
    public string Name { get; private set; }
    public string Description { get; private set; }

    private Activity(ActivityId activityId, string name, string description)
    {
        Id = activityId;
        Name = name;
        Description = description;
    }

    public static Activity Create(ActivityId activityId, string name, string description)
    {
        return new(
            activityId,
            name,
            description);
    }
    private Activity() { }

    public void UpdateActivity(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
