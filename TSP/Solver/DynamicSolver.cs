using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Solver
{
    /**
     * Algorytm Helda-Karpa
     * O(n^2*2^n)
     * */
    public class DynamicSolver : ISolver
    {
        private double[,] _distanceMatrix;
        public Route Solve(TargetsCollection targets)
        {
            _distanceMatrix = targets.CreateDistanceMatrix();
            throw new NotImplementedException();
        }

        private static double[,] CreateDistanceMatrix(ICollection<Target> targets)
        {
            int targetsCount = targets.Count;
            double[,] matrix = new double[targetsCount,targetsCount];

            int x = 0;
            foreach (Target target in targets)
            {
                int y = 0;
                foreach (KeyValuePair<Target, double> pair in target.Distances)
                {
                    matrix[x, y] = pair.Value;
                    ++y;
                }
                ++x;
            }

            return matrix;
        }
    }
}
