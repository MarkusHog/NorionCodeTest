namespace Domain.Abstractions;

public interface ITollCalculator
{
    int GetTollFee(IVehicle vehicle, DateTime[] dates);
}
