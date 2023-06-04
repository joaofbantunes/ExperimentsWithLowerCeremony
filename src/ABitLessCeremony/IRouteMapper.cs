namespace ABitLessCeremony;

public interface IRouteMapper
{
    static abstract void Map(IEndpointRouteBuilder routes);
}

public static class RouteMapperExtensions
{
    public static IEndpointRouteBuilder Map<TRouteMapper>(this IEndpointRouteBuilder routes)
        where TRouteMapper : IRouteMapper
    {
        TRouteMapper.Map(routes);
        return routes;
    }
}