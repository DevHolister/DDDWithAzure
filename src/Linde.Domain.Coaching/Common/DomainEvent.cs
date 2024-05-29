using MediatR;

namespace Linde.Domain.Coaching.Common;

public record DomainEvent() : INotification
{
    public DateTime DateOcurred { get; protected set; } = DateTime.Now;
}
