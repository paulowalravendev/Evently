namespace Evently.Common.Domain;

public abstract class DomainEvent : IDomainEvent
{
    protected DomainEvent()
    {
        Id = Guid.NewGuid();
        OccurredOnUtc = DateTime.UtcNow;
    }

    protected DomainEvent(Guid id, DateTime occurredOnutc)
    {
        Id = id;
        OccurredOnUtc = occurredOnutc;
    }

    public Guid Id { get; init; }

    public DateTime OccurredOnUtc { get; init; }
}
