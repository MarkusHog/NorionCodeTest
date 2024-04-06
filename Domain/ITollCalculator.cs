using TollFeeCalculator;

namespace Domain
{
    public interface ITollCalculator
    {
        int GetTollFee(Vehicle vehicle, DateTime[] dates);
    }
}
