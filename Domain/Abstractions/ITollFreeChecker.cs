namespace Domain.Abstractions
{
    public interface ITollFreeChecker
    {
        bool IsTollFree(IVehicle vehicle, DateTime date);
    }
}
