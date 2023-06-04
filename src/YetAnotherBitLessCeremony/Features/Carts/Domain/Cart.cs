using LanguageExt;

namespace YetAnotherBitLessABitLessABitLessCeremony.Features.Carts.Domain;

public record CartItem(ItemId ItemId, ushort Quantity);

public abstract record CartEvent : IDomainEvent
{
    private CartEvent(CartId cartId, AggregateVersion version)
    {
        CartId = cartId;
        Version = version;
    }

    public CartId CartId { get; }
    public AggregateVersion Version { get; }

    public sealed record CartOpened(CartId CartId, AggregateVersion Version) : CartEvent(CartId, Version);

    public sealed record ItemAddedToCart(CartId CartId, ItemId ItemId, ushort Quantity, AggregateVersion Version)
        : CartEvent(CartId, Version);
}

public abstract record AddItemToCartError
{
    private AddItemToCartError()
    {
    }
    
    public sealed record ItemAlreadyInCart(ItemId ItemId) : AddItemToCartError;
    
    public sealed record CannotAddItemWithZeroQuantity(ItemId ItemId) : AddItemToCartError;
}

public class Cart : IAggregateRoot<CartId>
{
    private readonly Dictionary<ItemId, CartItem> _items = new();

    public CartId Id { get; }
    
    public AggregateVersion Version { get; private set; }

    public IReadOnlyCollection<CartItem> Items => _items.Values;

    public Either<AddItemToCartError, CartEvent.ItemAddedToCart> AddItemToCart(ItemId itemId, ushort quantity)
    {
        if (_items.ContainsKey(itemId)) return new AddItemToCartError.ItemAlreadyInCart(itemId);

        if (quantity == 0) return new AddItemToCartError.CannotAddItemWithZeroQuantity(itemId);

        var @event = new CartEvent.ItemAddedToCart(Id, itemId, quantity, Version.Next());
        Apply(@event);
        return @event;
    }

    // given we're not using event sourcing, Apply methods aren't exactly needed
    // in this case, they're basically an optimization, so we have the latest state in case we want to use it
    // when publishing events, in which we might want to include more information than just the change
    
    private void Apply(CartEvent.ItemAddedToCart @event)
    {
        _items.Add(@event.ItemId, new CartItem(@event.ItemId, @event.Quantity));
        Version = @event.Version;
    }
}