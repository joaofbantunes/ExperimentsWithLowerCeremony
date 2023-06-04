using LanguageExt;

namespace ABitLessCeremony.Features.Carts.Infrastructure;

public static class GetCartById
{
    public record Query(CartId Id) : IQuery<Option<Result>>;

    public record Result(CartId Id, IReadOnlyCollection<Result.CartItem> Items)
    {
        public record CartItem(ItemId ItemId, ushort Quantity);
    }

    public class Handler : IQueryHandler<Query, Option<Result>>
    {
        public Task<Option<Result>> HandleAsync(Query query, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}