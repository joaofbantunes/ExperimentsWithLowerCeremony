using LanguageExt;
using MySql.Data.MySqlClient;
using YetMoreOptimizationOfCeremony.Features.Carts.Domain;
using static YetMoreOptimizationOfCeremony.Features.Carts.Domain.CartEvent;

namespace YetMoreOptimizationOfCeremony.Features.Carts.Infrastructure;

public class CartsRepository : ICartsRepository
{
    private readonly Func<MySqlConnection> _connectionFactory;

    public CartsRepository(Func<MySqlConnection> connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public Task<CartForAddItem?> LoadForAddItemAsync(CartId id, ItemId itemId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync(CartEvent @event, CancellationToken ct)
    {
        // with the event, instead of persisting the whole aggregate, we can just change what needs to be changed
        // potentially reducing some infrastructure waste

        await Handle(this, @event, ct);

        static Task Handle(CartsRepository @this, CartEvent @event, CancellationToken ct)
            => @event switch
            {
                CartEvent.CartOpened cartOpened => @this.CreateOpenCartAsync(cartOpened, ct),
                CartEvent.ItemAddedToCart itemAddedToCart => @this.AddItemToCartAsync(itemAddedToCart, ct),
                _ => throw new NotImplementedException()
            };
    }

    private Task CreateOpenCartAsync(CartEvent.CartOpened cartOpened, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    private Task AddItemToCartAsync(CartEvent.ItemAddedToCart itemAddedToCart, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}