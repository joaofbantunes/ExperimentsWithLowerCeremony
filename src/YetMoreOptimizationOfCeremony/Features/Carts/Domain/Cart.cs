using LanguageExt;

namespace YetMoreOptimizationOfCeremony.Features.Carts.Domain;

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

// instead of loading the whole aggregate, load just what's needed for some given logic
public record CartForAddItem(CartId Id, CartItem? ExistingItem, AggregateVersion Version);

public static class Cart
{
    public static Either<AddItemToCartError, CartEvent.ItemAddedToCart> AddItemToCart(
        CartForAddItem cart,
        ItemId itemId,
        ushort quantity)
    {
        if (cart.ExistingItem is not null) return new AddItemToCartError.ItemAlreadyInCart(itemId);

        if (quantity == 0) return new AddItemToCartError.CannotAddItemWithZeroQuantity(itemId);

        return new CartEvent.ItemAddedToCart(cart.Id, itemId, quantity, cart.Version.Next());
    }
}