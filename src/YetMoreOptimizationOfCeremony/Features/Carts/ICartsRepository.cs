using YetMoreOptimizationOfCeremony.Features.Carts.Domain;

namespace YetMoreOptimizationOfCeremony.Features.Carts;

public interface ICartsRepository
{
    Task<CartForAddItem?> LoadForAddItemAsync(CartId id, ItemId itemId, CancellationToken ct);
    
    Task SaveAsync(CartEvent @event, CancellationToken ct);
}