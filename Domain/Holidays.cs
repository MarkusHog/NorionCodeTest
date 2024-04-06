namespace Domain;

//Easier to maintain if we have a class for holidays where we can add or delete holidays.
public class Holidays
{
    public static bool IsHolidayOrDayBefore(DateTime date)
    {
        if (IsTollFree(date))
        {
            return true;
        }
        if (IsTollFree(date.AddDays(-1)))
        {
            return true;
        }
        return false;
        
    }

    private static bool IsTollFree(DateTime date)
    {
        return date.Month == 1 && date.Day == 1 ||
               date.Month == 1 && date.Day == 6 ||
               date.Month == 4 && date.Day == 10 ||
               date.Month == 4 && date.Day == 13 ||
               date.Month == 5 && date.Day == 1 ||
               date.Month == 5 && date.Day == 21 ||
               date.Month == 6 && date.Day == 6 ||
               date.Month == 6 && date.Day == 26 ||
               date.Month == 11 && date.Day == 3 ||
               date.Month == 12 && date.Day == 24 ||
               date.Month == 12 && date.Day == 25 ||
               date.Month == 12 && date.Day == 26 ||
               date.Month == 12 && date.Day == 31;
    }
}
