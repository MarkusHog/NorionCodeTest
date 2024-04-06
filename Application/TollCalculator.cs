using Domain;
using TollFeeCalculator;

namespace Application;
public class TollCalculator : ITollCalculator
{
    public int GetTollFee(Vehicle vehicle, DateTime[] dates)
    {
        DateTime intervalStart = dates[0];
        int totalFee = 0;
        foreach (DateTime date in dates ?? [])
        {
            int nextFee = GetTollFee(date, vehicle);

            totalFee += nextFee;

            if (date < intervalStart.AddMinutes(60) && !date.Equals(intervalStart))
            {
                int tempFee = GetTollFee(intervalStart, vehicle);
                totalFee -= nextFee <= tempFee ? nextFee : tempFee;
            }
            else
            {
                intervalStart = date;
            }

        }
        totalFee = totalFee > 60 ? 60 : totalFee;
        return totalFee;
    }

    private static bool IsTollFreeVehicle(Vehicle vehicle)
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

    private int GetTollFee(DateTime date, Vehicle vehicle)
    {
        if (IsTollFreeDate(date)
            || IsTollFreeVehicle(vehicle)) return 0;

        return GetPassageCost(date);
    }

    private static int GetPassageCost(DateTime date) => date switch
    {
        var passageDate when passageDate.TimeOfDay >= new TimeSpan(06, 0, 0) && passageDate.TimeOfDay <= new TimeSpan(06, 29, 59) => 8,
        var passageDate when passageDate.TimeOfDay >= new TimeSpan(06, 30, 0) && passageDate.TimeOfDay <= new TimeSpan(06, 59, 59) => 13,
        var passageDate when passageDate.TimeOfDay >= new TimeSpan(07, 0, 0) && passageDate.TimeOfDay <= new TimeSpan(07, 59, 59) => 18,
        var passageDate when passageDate.TimeOfDay >= new TimeSpan(08, 0, 0) && passageDate.TimeOfDay <= new TimeSpan(08, 29, 59) => 13,
        var passageDate when passageDate.TimeOfDay >= new TimeSpan(08, 30, 0) && passageDate.TimeOfDay <= new TimeSpan(14, 59, 59) => 8,
        var passageDate when passageDate.TimeOfDay >= new TimeSpan(15, 0, 0) && passageDate.TimeOfDay <= new TimeSpan(15, 29, 59) => 13,
        var passageDate when passageDate.TimeOfDay >= new TimeSpan(15, 30, 0) && passageDate.TimeOfDay <= new TimeSpan(16, 59, 59) => 18,
        var passageDate when passageDate.TimeOfDay >= new TimeSpan(17, 0, 0) && passageDate.TimeOfDay <= new TimeSpan(17, 59, 59) => 13,
        var passageDate when passageDate.TimeOfDay >= new TimeSpan(18, 0, 0) && passageDate.TimeOfDay <= new TimeSpan(18, 29, 59) => 8,
        _ => 0
    };

    private static bool IsTollFreeDate(DateTime date)
    {
        if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday) return true;
        if (Holidays.IsHolidayOrDayBefore(date)) return true;
        if (IsMonthJuly(date)) return true;

        return false;
    }

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