using System.Collections.Generic;
using TSP.Solver;

namespace TSP.Services
{
    public interface IDistanceService
    {
        void FetchDistances(IList<Target> targets);
        string Unit { get; }
    }
}