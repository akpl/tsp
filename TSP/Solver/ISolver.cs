using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Solver
{
    interface ISolver
    {
        IList<Target> Solve(ICollection<Target> targets);
    }
}
