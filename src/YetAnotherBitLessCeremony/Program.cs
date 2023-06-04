using YetAnotherBitLessABitLessABitLessCeremony;
using YetAnotherBitLessABitLessABitLessCeremony.Features.Carts;
using YetAnotherBitLessABitLessABitLessCeremony.Features.Orders;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app
    .Map<CartRoutes>()
    .Map<OrderRoutes>();

app.Run();