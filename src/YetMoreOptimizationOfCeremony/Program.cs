using YetMoreOptimizationOfCeremony;
using YetMoreOptimizationOfCeremony.Features.Carts;
using YetMoreOptimizationOfCeremony.Features.Orders;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app
    .Map<CartRoutes>()
    .Map<OrderRoutes>();

app.Run();