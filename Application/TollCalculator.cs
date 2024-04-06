using Domain.Abstractions;
using Domain.Utility;
using Microsoft.Extensions.Logging;

namespace Application;
public class TollCalculator(ITollFreeChecker tollFeeChecker, ILogger<TollCalculator> logger) : ITollCalculator
{
    private readonly PassageCost _passageCost = new();
    private readonly ITollFreeChecker _tollFeeChecker = tollFeeChecker;
    private readonly ILogger<TollCalculator> _logger = logger;

    public int GetTollFee(IVehicle vehicle, DateTime[] dates)
    {
        try
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
        catch (Exception e)
        {
            _logger.LogError(e, "Error in TollCalculator");
            return 0;
        }
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