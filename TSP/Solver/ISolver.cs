using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Solver
{
    public interface ISolver
    {
        Route Solve(TargetsCollection targets);
    }
}
