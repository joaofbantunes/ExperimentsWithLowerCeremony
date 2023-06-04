using ABitLessCeremony.Features.Carts.Domain;
using ABitLessCeremony.Features.Carts.Infrastructure;
using LanguageExt;

namespace ABitLessCeremony.Features.Carts;

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
        CommandHandler<AddItemToCart.Command, Either<AddItemToCartError, Unit>> handler,
        CancellationToken ct)
    {
        // command mapping should involve validation
        
        var result = await handler(
            new AddItemToCart.Command(
                new CartId(cartId),
                new ItemId(command.ItemId),
                command.Quantity),
            ct);

        return result.Match(
            static _ => Results.NoContent(),
            static error => Results.Conflict()); // the error should be better mapped
    }

    private record AddItemToCartDto(Guid ItemId, ushort Quantity);
}