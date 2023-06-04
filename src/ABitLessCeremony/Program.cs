using ABitLessCeremony;
using ABitLessCeremony.Features.Carts;
using ABitLessCeremony.Features.Orders;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app
    .Map<CartRoutes>()
    .Map<OrderRoutes>();

app.Run();