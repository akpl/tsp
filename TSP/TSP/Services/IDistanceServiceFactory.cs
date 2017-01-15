namespace TSP.Services
{
    public interface IDistanceServiceFactory
    {
        IDistanceService Build();
    }
}