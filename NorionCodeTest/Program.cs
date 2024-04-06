using Application;
using Domain.Abstractions;
using Domain.Models;
using Domain.Utility;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//Using dependency injection to inject the services make the code easier to decouple and build more modular.
builder.Services.AddScoped<ILogger, Logger<TollCalculator>>();
builder.Services.AddScoped<ITollCalculator, TollCalculator>(); 
builder.Services.AddScoped<ITollFreeChecker, TollFreeChecker>();
builder.Services.AddScoped<IVehicle, Car>();
builder.Services.AddScoped<IVehicle, Motorbike>();
builder.Services.AddScoped<IPassageCost, PassageCost>();

var app = builder.Build();

//Added a simple API endpoint to test the toll calculation
//Test with query parameters
// https://localhost:7121/api/toll?vehicle=car&dates=2024-04-04T08:15:00&dates=2024-04-04T17:30:00
app.MapGet("/api/toll", (ITollCalculator tollCalculator, string vehicle, DateTime[] dates) =>
    {
        if (!string.Equals(vehicle, "car", StringComparison.OrdinalIgnoreCase))
        {
            return Results.Ok(0);
        }
        if (dates is null)
        {
            return Results.BadRequest("Dates are required");
        }
        var car = new Car();
        var type = car.GetVehicleType();
        var result = tollCalculator.GetTollFee(car, dates);
        return Results.Ok(result);
    }       
);

app.Run();




