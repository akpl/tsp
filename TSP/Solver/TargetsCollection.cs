using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Solver
{
    public class TargetsCollection : IEnumerable<Target>
    {
        private readonly IList<Target> _targets = new List<Target>();

        public TargetsCollection()
        {
        }

        public TargetsCollection(IList<Target> targets)
        {
            _targets = targets;
        }

        public int Count => _targets.Count;

        public double[,] CreateDistanceMatrix()
        {
            var lookup = new Dictionary<Target,int>();
            for (int targetIndex = 0; targetIndex < Count; targetIndex++)
            {
                lookup[_targets[targetIndex]] = targetIndex;
            }

            var matrix = new double[Count, Count];

            foreach (Target target in _targets)
            {
                foreach (KeyValuePair<Target, double> pair in target.Distances)
                {
                    matrix[lookup[target], lookup[pair.Key]] = pair.Value;
                }
            }

            return matrix;
        }

        public void Add(Target target)
        {
            _targets.Add(target);
        }

        public IEnumerator<Target> GetEnumerator()
        {
            return _targets.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _targets).GetEnumerator();
        }
    }
}
