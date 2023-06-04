﻿using ABitLessCeremony.Features.Carts.Domain;
using LanguageExt;
using MySql.Data.MySqlClient;
using static ABitLessCeremony.Features.Carts.Domain.CartEvent;

namespace ABitLessCeremony.Features.Carts.Infrastructure;

public class CartsRepository : ICartsRepository
{
    private readonly Func<MySqlConnection> _connectionFactory;

    public CartsRepository(Func<MySqlConnection> connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Cart?> LoadAsync(CartId id, CancellationToken ct) => throw new NotImplementedException();

    public async Task SaveAsync(Cart aggregate, CartEvent @event, CancellationToken ct)
    {
        // with the event, instead of persisting the whole aggregate, we can just change what needs to be changed
        // potentially reducing some infrastructure waste
        
        // TODO: don't forget to map the event into something we can publish, and store in the outbox
        
        await Handle(this, aggregate, @event, ct);

        static Task Handle(CartsRepository @this, Cart currentState, CartEvent @event, CancellationToken ct)
            => @event switch
            {
                CartOpened cartOpened => @this.CreateOpenCartAsync(currentState, cartOpened, ct),
                ItemAddedToCart itemAddedToCart => @this.AddItemToCartAsync(currentState, itemAddedToCart, ct),
                _ => throw new NotImplementedException()
            };
    }
    
    private Task CreateOpenCartAsync(Cart aggregate, CartOpened cartOpened, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
    
    private Task AddItemToCartAsync(Cart aggregate, ItemAddedToCart itemAddedToCart, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}