namespace YetAnotherBitLessABitLessABitLessCeremony.Features;

public interface IDomainEvent
{
}

public interface IAggregateRoot<out TAggregateRootId> 
{
    public TAggregateRootId Id { get; }
    
    public AggregateVersion Version { get; }
}

public readonly record struct AggregateVersion
{
    private AggregateVersion(uint value)
    {
        Value = value;
    }
    
    public uint Value { get; }

    public static AggregateVersion New() => new(0);

    public AggregateVersion Next() => new(Value + 1);
    
    public AggregateVersion Previous() => Value > 0 ? new(Value - 1) : throw new InvalidOperationException();
}

