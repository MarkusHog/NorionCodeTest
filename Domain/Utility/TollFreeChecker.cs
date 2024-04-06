using Domain.Abstractions;

namespace Domain.Utility;

// Separated the logic for checking if a vehicle is toll free or not into a separate class for maintability.
public class TollFreeChecker : ITollFreeChecker
{
    public bool IsTollFree(IVehicle vehicle, DateTime date)
    {
        if (IsTollFreeVehicle(vehicle)) return true;
        if (IsTollFreeDate(date)) return true;
        return false;
    }

    private static bool IsTollFreeVehicle(IVehicle vehicle)
    {
        if (vehicle is null) return false;
        var vehicleType = vehicle.GetVehicleType();

        return vehicleType.Equals(TollFreeVehicles.Motorbike.ToString(), StringComparison.Ordinal) ||
               vehicleType.Equals(TollFreeVehicles.Tractor.ToString(), StringComparison.Ordinal) ||
               vehicleType.Equals(TollFreeVehicles.Emergency.ToString(), StringComparison.Ordinal) ||
               vehicleType.Equals(TollFreeVehicles.Diplomat.ToString(), StringComparison.Ordinal) ||
               vehicleType.Equals(TollFreeVehicles.Foreign.ToString(), StringComparison.Ordinal) ||
               vehicleType.Equals(TollFreeVehicles.Military.ToString(), StringComparison.Ordinal);
    }

    private static bool IsTollFreeDate(DateTime date) => date switch
    {
        var tollFreeDate when tollFreeDate.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday => true,
        var tollFreeDate when Holidays.IsHolidayOrDayBefore(tollFreeDate) => true,
        var tollFreeDate when IsMonthJuly(tollFreeDate) => true,
        _ => false
    };

    private static bool IsMonthJuly(DateTime date) => date.Month == 7;

    private enum TollFreeVehicles
    {
        Motorbike = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5
    }
}
