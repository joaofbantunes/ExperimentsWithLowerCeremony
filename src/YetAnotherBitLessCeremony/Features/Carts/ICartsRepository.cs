using YetAnotherBitLessABitLessABitLessCeremony.Features.Carts.Domain;

namespace YetAnotherBitLessABitLessABitLessCeremony.Features.Carts;

public interface ICartsRepository : IRepository<Cart, CartId, CartEvent>
{
    // Task<Cart?> LoadAsync(CartId id, CancellationToken ct);
    //
    // Task SaveAsync(Cart aggregate, CartEvent @event, CancellationToken ct);
}