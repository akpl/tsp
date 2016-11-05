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
        public void SimpleProblemTest()
        {
            var targets = new TargetsCollection();
            var a = new Target { Name = "A", Location = new Coordinates(0, 0) };
            var b = new Target { Name = "B", Location = new Coordinates(1, 1) };
            var c = new Target { Name = "C", Location = new Coordinates(2, 2) };
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
            Assert.AreEqual(10, shortestRoute.CalculateDistance());
        }

        [Test]
        public void RouteDistanceTest()
        {
            var targetE = new Target { Name = "E", Location = new Coordinates(5, 1) };
            var targetD = new Target { Name = "D", Location = new Coordinates(4, 1), Distances = new Dictionary<Target, double> { { targetE, 6 }} };
            var targetC = new Target { Name = "C", Location = new Coordinates(3, 1), Distances = new Dictionary<Target, double> { { targetD, 12 } } };
            var targetB = new Target { Name = "B", Location = new Coordinates(2, 1), Distances = new Dictionary<Target, double> { { targetC, 4 } } };
            var targetA = new Target { Name = "A", Location = new Coordinates(1, 1), Distances = new Dictionary<Target, double> { { targetB, 20 } } };

            var route = new Route {targetA, targetB, targetC, targetD, targetE};

            Assert.AreEqual(42, route.CalculateDistance());
        }

        [Test]
        public void CrossoverDoesntCreateDuplicatesTest()
        {
            GeneticSolver g = new GeneticSolver();
            var r1 = new Route
            {
                new Target {Name = "10", Location = new Coordinates(1, 10)},
                new Target {Name = "8", Location = new Coordinates(1, 8)},
                new Target {Name = "4", Location = new Coordinates(1, 4)},
                new Target {Name = "5", Location = new Coordinates(1, 5)},
                new Target {Name = "6", Location = new Coordinates(1, 6)},
                new Target {Name = "7", Location = new Coordinates(1, 7)},
                new Target {Name = "1", Location = new Coordinates(1, 1)},
                new Target {Name = "3", Location = new Coordinates(1, 3)},
                new Target {Name = "2", Location = new Coordinates(1, 2)},
                new Target {Name = "9", Location = new Coordinates(1, 9)}
            };

            var r2 = new Route
            {
                new Target {Name = "8", Location = new Coordinates(1, 8)},
                new Target {Name = "7", Location = new Coordinates(1, 7)},
                new Target {Name = "1", Location = new Coordinates(1, 1)},
                new Target {Name = "2", Location = new Coordinates(1, 2)},
                new Target {Name = "3", Location = new Coordinates(1, 3)},
                new Target {Name = "10", Location = new Coordinates(1, 10)},
                new Target {Name = "9", Location = new Coordinates(1, 9)},
                new Target {Name = "5", Location = new Coordinates(1, 5)},
                new Target {Name = "4", Location = new Coordinates(1, 4)},
                new Target {Name = "6", Location = new Coordinates(1, 6)}
            };

            var child = g.OrderedCrossover(r1, r2);

            bool hasDuplicate = child.GroupBy(target => target).Count(targetsGroup => targetsGroup.Count() > 1) != 0;
            Assert.IsFalse(hasDuplicate);
            Assert.AreEqual(10, child.Stops);
        }
    }
}
