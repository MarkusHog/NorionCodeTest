using Domain.Abstractions;

namespace Domain.Models;

public class Motorbike : IVehicle
{
    public string GetVehicleType()
    {
        return "Motorbike";
    }
}
