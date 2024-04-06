using Domain.Abstractions;

namespace Domain.Utility;

//Using a separate class for passage costs makes it more readable and easier to maintain and update.
public class PassageCost : IPassageCost
{
    Dictionary<TimeSpan, int> _costs = new Dictionary<TimeSpan, int>
    {
        { new TimeSpan(6, 0, 0), 8 },
        { new TimeSpan(6, 30, 0), 13 },
        { new TimeSpan(7, 0, 0), 18 },
        { new TimeSpan(8, 0, 0), 13 },
        { new TimeSpan(8, 30, 0), 8 },
        { new TimeSpan(15, 0, 0), 13 },
        { new TimeSpan(15, 30, 0), 18 },
        { new TimeSpan(17, 0, 0), 13 },
        { new TimeSpan(18, 0, 0), 8 },
        { new TimeSpan(18, 30, 0), 0 },
    };

    public int GetPassageCost(DateTime passageTime)
    {
        TimeSpan passageTimeOfDay = passageTime.TimeOfDay;
        DateTime previousDateTime = DateTime.MinValue;

        foreach (var passageCost in _costs)
        {
            if (passageTimeOfDay < passageCost.Key)
            {
                if (previousDateTime == DateTime.MinValue)
                    return 0;
                else
                    return _costs[previousDateTime.TimeOfDay];
            }

            previousDateTime = passageTime.Date + passageCost.Key;
        }

        return _costs[previousDateTime.TimeOfDay];
    }
}

