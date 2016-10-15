using System.Collections.Generic;

namespace TSP.Solver
{
    public class Target
    {
        public string Name { get; set; }
        public Coordinates Location { get; set; }
        public IDictionary<Target,double> Distances { get; set; }
    }
}