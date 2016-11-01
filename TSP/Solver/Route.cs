using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Solver
{
    public class Route : IComparable<Route>
    {
        private readonly IList<Target> _targets;

        public Route()
        {
            _targets = new List<Target>();
        }
        public Route(IEnumerable<Target> collection)
        {
            _targets = new List<Target>(collection);
        }

        public int Stops => _targets.Count;

        public Target this[int key]
        {
            get { return _targets[key]; }
            set { _targets[key] = value; }
        }

        public double CalculateDistance()
        {
            double total = 0;
            if (Stops < 2)
            {
                return 0;
            }

            int legs = Stops - 1;
            for (int targetIndex = 0; targetIndex < legs; targetIndex++)
            {
                total += _targets[targetIndex].Distances[_targets[targetIndex+1]];
            }

            return total;
        }

        public int CompareTo(Route other)
        {
            return CalculateDistance().CompareTo(other.CalculateDistance());
        }

        public override string ToString()
        {
            if (Stops == 0)
            {
                return string.Empty;
            }
            var route = new StringBuilder();
            foreach (Target target in _targets)
            {
                route.Append(target.Name ?? String.Empty);
                route.Append("->");
            }

            return route.ToString(0, route.Length - 2);
        }

        public void Add(Target target)
        {
            _targets.Add(target);
        }
    }
}
