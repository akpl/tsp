using TSP.Solver;

namespace TSP.Services
{
    public interface IConfigurationService
    {
        Configuration Get();
        void Save(Configuration config);
    }
}
