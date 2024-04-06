using TollFeeCalculator;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

var car = new Car();
var fee = new TollCalculator().GetTollFee(car, new DateTime[] { DateTime.Now });

app.Run();
