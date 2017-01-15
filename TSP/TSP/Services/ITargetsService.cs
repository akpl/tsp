using System.Collections.Generic;
using TSP.Solver;

namespace TSP.Services
{
    public interface ITargetsService
    {
        List<Target> Read();
        void Save(IList<Target> targets);
    }
}