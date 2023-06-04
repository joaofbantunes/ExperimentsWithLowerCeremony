namespace YetAnotherBitLessABitLessABitLessCeremony.Features;

public interface IRepository<TAggregateRoot, TAggregateRootId, in TAggregateRootEvent>
    where TAggregateRoot : IAggregateRoot<TAggregateRootId>
{
    Task<TAggregateRoot?> LoadAsync(CartId id, CancellationToken ct);

    // the aggregate isn't actually needed, as the event is enough to implement the persistence logic
    // the aggregate is just an optimization, since we already have it in memory,
    // so if we need to publish an external event with more info than the domain event, we can use it  
    Task SaveAsync(TAggregateRoot aggregate, TAggregateRootEvent @event, CancellationToken ct);
}