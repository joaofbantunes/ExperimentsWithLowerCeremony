namespace YetAnotherBitLessABitLessABitLessCeremony.Features.Orders;

public class OrderRoutes : IRouteMapper
{
    public static void Map(IEndpointRouteBuilder routes)
    {
        routes
            .MapGroup("orders");
    }
}