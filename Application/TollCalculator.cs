using Domain.Abstractions;
using Domain.Utility;

namespace Application;
public class TollCalculator(ITollFreeChecker tollFeeChecker) : ITollCalculator
{
    private readonly PassageCost _passageCost = new();
    private readonly ITollFreeChecker _tollFeeChecker = tollFeeChecker;

    public int GetTollFee(IVehicle vehicle, DateTime[] dates)
    {
        DateTime intervalStart = dates[0];
        int totalFee = 0;
        foreach (DateTime date in dates ?? [])
        {
            int nextFee = GetTollFee(date, vehicle);

            totalFee += nextFee;

            if (IsWithinOneHour(intervalStart, date) && !date.Equals(intervalStart))
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

    private static bool IsWithinOneHour(DateTime intervalStart, DateTime date)
    {
        return date < intervalStart.AddMinutes(60);
    }
    private int GetTollFee(DateTime date, IVehicle vehicle)
    {
        if (IsTollFree(vehicle, date)) return 0;

        return _passageCost.GetPassageCost(date);
    }

    private bool IsTollFree(IVehicle vehicle, DateTime date)
    {
        return _tollFeeChecker.IsTollFree(vehicle, date);
    }
}