using Domain;
using TollFeeCalculator;
using Application;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ITollCalculator, TollCalculator>(); 

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
