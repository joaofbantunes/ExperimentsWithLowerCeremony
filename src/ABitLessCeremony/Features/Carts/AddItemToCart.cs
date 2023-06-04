using ABitLessCeremony.Features.Carts.Domain;
using LanguageExt;

namespace ABitLessCeremony.Features.Carts;

public class AddItemToCart
{
    public record Command(CartId CartId, ItemId ItemId, ushort Quantity) : ICommand<Either<AddItemToCartError, Unit>>;

    public class Handler : ICommandHandler<Command, Either<AddItemToCartError, Unit>>
    {
        private readonly ICartsRepository _repository;

        public Handler(ICartsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Either<AddItemToCartError, Unit>> HandleAsync(Command command, CancellationToken ct)
        {
            var cart = await _repository.LoadAsync(command.CartId, ct);

            if (cart is null) return new AddItemToCartError.CartDoesNotExist(command.CartId);

            var result = cart.AddItemToCart(command.ItemId, command.Quantity);

            switch (result.Case)
            {
                case CartEvent @event:
                    await _repository.SaveAsync(cart, @event, ct);
                    return Unit.Default;
                case AddItemToCartError error:
                    return error;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}