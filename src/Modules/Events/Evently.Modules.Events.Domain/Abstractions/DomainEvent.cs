namespace Evently.Modules.Events.Domain.Abstractions;

public abstract class DomainEvent : IDomainEvent
{
    protected DomainEvent()
    {
        Id = Guid.NewGuid();
        OccurredOnutc = DateTime.UtcNow;
    }

    protected DomainEvent(Guid id, DateTime occurredOnutc)
    {
        Id = id;
        OccurredOnutc = occurredOnutc;
    }

    public Guid Id { get; init; }

    public DateTime OccurredOnutc { get; init; }
}
