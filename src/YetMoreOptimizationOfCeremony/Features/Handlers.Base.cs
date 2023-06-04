namespace YetMoreOptimizationOfCeremony.Features;

public interface IQuery<TResult>
{
}

public delegate Task<TResult> QueryHandler<in TQuery, TResult>(TQuery query, CancellationToken ct)
    where TQuery : IQuery<TResult>;

public interface IQueryHandler<in TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken ct);
}

public static class HandlersServiceCollectionExtensions
{
    public static IServiceCollection AddQueryHandler<TQueryHandler, TQuery, TResult>(
        this IServiceCollection services)
        where TQueryHandler : class, IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        services
            .AddTransient<IQueryHandler<TQuery, TResult>, TQueryHandler>()
            .AddTransient<QueryHandler<TQuery, TResult>>(s =>
                s.GetRequiredService<IQueryHandler<TQuery, TResult>>().HandleAsync);

        return services;
    }
}