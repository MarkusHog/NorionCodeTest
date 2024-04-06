using Domain.Abstractions;

namespace Domain.Models;

public class Car : IVehicle
{
    public string GetVehicleType()
    {
        return "Car";
    }
}