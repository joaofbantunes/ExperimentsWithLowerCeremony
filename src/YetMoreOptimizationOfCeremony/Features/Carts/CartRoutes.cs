using YetMoreOptimizationOfCeremony.Features.Carts.Domain;
using LanguageExt;
using YetMoreOptimizationOfCeremony.Features.Carts.Infrastructure;

namespace YetMoreOptimizationOfCeremony.Features.Carts;

// if this starts to get big, we can break each endpoint into its own file

public class CartRoutes : IRouteMapper
{
    public static void Map(IEndpointRouteBuilder routes)
    {
        var cartRoutes = routes.MapGroup("carts");
        cartRoutes.MapGet("{cartId}", GetCartByIdAsync);
        cartRoutes.MapPost("", OpenCartAsync);
        cartRoutes.MapPost("{cartId}", AddItemToCartAsync);
    }

    private static async Task<IResult> GetCartByIdAsync(
        Guid cartId,
        QueryHandler<GetCartById.Query, Option<GetCartById.Result>> handler,
        CancellationToken ct)
    {
        var maybeCart = await handler(new GetCartById.Query(new CartId(cartId)), ct);
        return maybeCart.Match(
            static cart => Results.Ok(cart),
            static () => Results.NotFound());
    }

    private static async Task<IResult> OpenCartAsync(CancellationToken ct)
        => throw new NotImplementedException();

    private static async Task<IResult> AddItemToCartAsync(
        Guid cartId,
        AddItemToCartDto command,
        ICartsRepository repository,
        CancellationToken ct)
    {
        // mapping should involve validation
        var domainCartId = new CartId(cartId);
        var domainItemId = new ItemId(command.ItemId);
        
        var cartForAdd = await repository.LoadForAddItemAsync(domainCartId, domainItemId, ct);
        
        if (cartForAdd is null) return Results.NotFound();

        var result = Cart.AddItemToCart(cartForAdd, domainItemId, command.Quantity);
        
        switch (result.Case)
        {
            case CartEvent @event:
                await repository.SaveAsync(@event, ct);
                return Results.NoContent();
            case AddItemToCartError error:
                return error switch
                {
                    // we should make use of problem details to provide more info about the error
                    AddItemToCartError.CannotAddItemWithZeroQuantity => Results.UnprocessableEntity(),
                    AddItemToCartError.ItemAlreadyInCart => Results.Conflict(),
                    _ => throw new NotImplementedException()
                };
            default:
                throw new InvalidOperationException();
        }
    }

    private record AddItemToCartDto(Guid ItemId, ushort Quantity);
}