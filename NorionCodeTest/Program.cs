using Application;
using Domain.Abstractions;
using Domain.Models;
using Domain.Utility;

var builder = WebApplication.CreateBuilder(args);

//Using dependency injection to inject the services make the code easier to decouple and build more modular.
builder.Services.AddScoped<ITollCalculator, TollCalculator>(); 
builder.Services.AddScoped<ITollFreeChecker, TollFreeChecker>();
builder.Services.AddScoped<IVehicle, Car>();
builder.Services.AddScoped<IVehicle, Motorbike>();
builder.Services.AddScoped<IPassageCost, PassageCost>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
