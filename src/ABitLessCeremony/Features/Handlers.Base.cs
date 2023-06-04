namespace ABitLessCeremony.Features;

public interface ICommand<TResult>
{
}

public delegate Task<TResult> CommandHandler<in TCommand, TResult>(TCommand command, CancellationToken ct)
    where TCommand : ICommand<TResult>;

public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand<TResult>
{
    Task<TResult> HandleAsync(TCommand command, CancellationToken ct);
}

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
    public static IServiceCollection AddCommandHandler<TCommandHandler, TCommand, TResult>(
        this IServiceCollection services)
        where TCommandHandler : class, ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        services
            .AddTransient<ICommandHandler<TCommand, TResult>, TCommandHandler>()
            .AddTransient<CommandHandler<TCommand, TResult>>(s =>
                s.GetRequiredService<ICommandHandler<TCommand, TResult>>().HandleAsync);

        return services;
    }

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