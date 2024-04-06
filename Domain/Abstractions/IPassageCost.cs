namespace Domain.Abstractions;
//Interface for PassageCost abstracts the logic and allows for different implementations.
public interface IPassageCost
{
    int GetPassageCost(DateTime passageTime);
}
