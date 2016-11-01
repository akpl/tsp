using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TSP.Solver;

namespace TSP.Tests
{
    [TestFixture]
    public class SolverTests
    {
        private ISolver _solver;
        [SetUp]
        public void SetUp()
        {
            _solver = new GeneticSolver();
        }

        [Test]
        public void SimpleTest()
        {
            var targets = new TargetsCollection();
            var a = new Target { Location = new Coordinates(0, 0) };
            var b = new Target { Location = new Coordinates(1, 1) };
            var c = new Target { Location = new Coordinates(2, 2) };
            a.Distances = new Dictionary<Target, double>
            {
                {b, 3},
                {c, 9}
            };
            b.Distances = new Dictionary<Target, double>
            {
                {a, 3},
                {c, 7}
            };
            c.Distances = new Dictionary<Target, double>
            {
                {a, 9},
                {b, 7}
            };
            targets.Add(a);
            targets.Add(b);
            targets.Add(c);

            Route shortestRoute = _solver.Solve(targets);

            Assert.AreEqual(3, shortestRoute.Stops);
        }

        [Test]
        public void TspNewCrossoverTest()
        {
            GeneticSolver g = new GeneticSolver();
            var r1 = new Route();
            r1.Add(new Target { Name = "10", Location = new Coordinates(1, 10) });
            r1.Add(new Target { Name = "8", Location = new Coordinates(1, 8) });
            r1.Add(new Target { Name = "4", Location = new Coordinates(1, 4) });
            r1.Add(new Target { Name = "5", Location = new Coordinates(1, 5) });
            r1.Add(new Target { Name = "6", Location = new Coordinates(1, 6) });
            r1.Add(new Target { Name = "7", Location = new Coordinates(1, 7) });
            r1.Add(new Target { Name = "1", Location = new Coordinates(1, 1) });
            r1.Add(new Target { Name = "3", Location = new Coordinates(1, 3) });
            r1.Add(new Target { Name = "2", Location = new Coordinates(1, 2) });
            r1.Add(new Target { Name = "9", Location = new Coordinates(1, 9) });

            var r2 = new Route();
            r2.Add(new Target { Name = "8", Location = new Coordinates(1, 8) });
            r2.Add(new Target { Name = "7", Location = new Coordinates(1, 7) });
            r2.Add(new Target { Name = "1", Location = new Coordinates(1, 1) });
            r2.Add(new Target { Name = "2", Location = new Coordinates(1, 2) });
            r2.Add(new Target { Name = "3", Location = new Coordinates(1, 3) });
            r2.Add(new Target { Name = "10", Location = new Coordinates(1, 10) });
            r2.Add(new Target { Name = "9", Location = new Coordinates(1, 9) });
            r2.Add(new Target { Name = "5", Location = new Coordinates(1, 5) });
            r2.Add(new Target { Name = "4", Location = new Coordinates(1, 4) });
            r2.Add(new Target { Name = "6", Location = new Coordinates(1, 6) });

            var c = g.PMXCrossover(r1, r2);
        }

        [Test]
        public void CrossoverTest()
        {
            GeneticSolver g = new GeneticSolver();
            var r1 = new Route();
            for (int i = 1; i < 10; i++)
            {
                r1.Add(new Target() { Name = i.ToString(), Location = new Coordinates(i,i)});
            }
            var r2 = new Route();
            r2.Add(new Target { Name = "9", Location = new Coordinates(9,9) });
            r2.Add(new Target { Name = "3", Location = new Coordinates(3,3) });
            r2.Add(new Target { Name = "7", Location = new Coordinates(7,7) });
            r2.Add(new Target { Name = "8", Location = new Coordinates(8,8) });
            r2.Add(new Target { Name = "2", Location = new Coordinates(2,2) });
            r2.Add(new Target { Name = "6", Location = new Coordinates(6,6) });
            r2.Add(new Target { Name = "5", Location = new Coordinates(5,5) });
            r2.Add(new Target { Name = "1", Location = new Coordinates(1,1) });
            r2.Add(new Target { Name = "4", Location = new Coordinates(4,4) });
            var c = g.PMXCrossover(r2, r1);
        }
    }
}
