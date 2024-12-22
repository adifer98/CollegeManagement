namespace CollegeManagement.Domain.Common;

public class Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];
    
    public List<IDomainEvent> PopDomainEvents()
    {
        var domainEvents = _domainEvents.ToList();

        _domainEvents.Clear();

        return domainEvents;
    }
    
    public void AddDomainEvent(IDomainEvent domainEvent)
    {
       _domainEvents.Add(domainEvent);
    }

    protected Entity()
    {}
}