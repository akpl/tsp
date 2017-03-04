using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using TSP.Solver;

namespace TSP.Tests
{
    [TestFixture]
    class PerformanceTests
    {
        private const int RepeatCount = 10;
        private IList<long> _elapsedTimes;
        private IList<double> _routeDistances;
        private IList<Target> _targets;
        private int _targetCount;
        private GeneticSolver _solver;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            double[][] distances;
            using (Stream stream = typeof(PerformanceTests).Assembly.GetManifestResourceStream("TSP.Tests.data.json"))
            using (StreamReader reader = new StreamReader(stream))
            {
                string tableText = reader.ReadToEnd();
                distances = JsonConvert.DeserializeObject<double[][]>(tableText);
            }
            _targetCount = distances.Length;
            _targets = new List<Target>(_targetCount);

            for (int i = 0; i < _targetCount; i++)
            {
                _targets.Add(new Target
                {
                    Location = new Coordinates(i, i),
                    Distances = new Dictionary<Target, double>(_targetCount),
                    Id = Guid.NewGuid(),
                    Name = $"Punkt {i}"
                });
            }

            for (int x = 0; x < _targetCount; x++)
            {
                for (int y = 0; y < _targetCount; y++)
                {
                    _targets[x].Distances[_targets[y]] = distances[x][y];
                }
            }
        }

        [SetUp]
        public void SetUp()
        {
            _solver = new GeneticSolver();
            _elapsedTimes = new List<long>(RepeatCount);
            _routeDistances = new List<double>(RepeatCount);
        }

        [Test]
        public void DefaultSettingsTest()
        {
            _solver.PopulationSize = 50;
            _solver.GenerationsCount = 100;

            RunSolver();
        }

        [Test]
        public void Population100Test()
        {
            _solver.PopulationSize = 100;
            _solver.GenerationsCount = 100;

            RunSolver();
        }

        [Test]
        public void Population1000Test()
        {
            _solver.PopulationSize = 1000;
            _solver.GenerationsCount = 100;

            RunSolver();
        }

        [Test]
        public void Population50Generations1000Test()
        {
            _solver.PopulationSize = 50;
            _solver.GenerationsCount = 1000;

            RunSolver();
        }

        [Test]
        public void Population100Generations1000Test()
        {
            _solver.PopulationSize = 100;
            _solver.GenerationsCount = 1000;

            RunSolver();
        }

        [Test]
        public void Population1000Generations1000Test()
        {
            _solver.PopulationSize = 1000;
            _solver.GenerationsCount = 1000;

            RunSolver();
        }

        [Test]
        public void ElitismOffPopulation50Generations100Test()
        {
            _solver.IsElitismEnabled = false;
            _solver.PopulationSize = 50;
            _solver.GenerationsCount = 100;

            RunSolver();
        }

        [Test]
        public void ElitismOffPopulation100Generations100Test()
        {
            _solver.IsElitismEnabled = false;
            _solver.PopulationSize = 100;
            _solver.GenerationsCount = 100;

            RunSolver();
        }

        [Test]
        public void ElitismOffPopulation1000Generations100Test()
        {
            _solver.IsElitismEnabled = false;
            _solver.PopulationSize = 1000;
            _solver.GenerationsCount = 100;

            RunSolver();
        }

        [Test]
        public void ElitismOffPopulation50Generations1000Test()
        {
            _solver.IsElitismEnabled = false;
            _solver.PopulationSize = 50;
            _solver.GenerationsCount = 1000;

            RunSolver();
        }

        [Test]
        public void ElitismOffPopulation100Generations1000Test()
        {
            _solver.IsElitismEnabled = false;
            _solver.PopulationSize = 100;
            _solver.GenerationsCount = 1000;

            RunSolver();
        }

        [Test]
        public void ElitismOffPopulation1000Generations1000Test()
        {
            _solver.IsElitismEnabled = false;
            _solver.PopulationSize = 1000;
            _solver.GenerationsCount = 1000;

            RunSolver();
        }

        private void RunSolver()
        {
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            for (int repeat = 0; repeat < RepeatCount; repeat++)
            {
                timer.Restart();
                var route = _solver.Solve(new TargetsCollection(_targets));
                timer.Stop();
                Console.WriteLine($"Próba #{repeat}: {timer.ElapsedMilliseconds} ms, wynik: {route.CalculateDistance()}");

                _elapsedTimes.Add(timer.ElapsedMilliseconds);
                _routeDistances.Add(route.CalculateDistance());
            }

            Console.WriteLine("----------------------------------");
            Console.WriteLine($"Łączny:   {_elapsedTimes.Sum()} ms, wynik: {_routeDistances.Sum()}");
            Console.WriteLine($"Średnia:  {_elapsedTimes.Average()} ms, wynik: {_routeDistances.Average()}");
            Console.WriteLine($"Maksimum: {_elapsedTimes.Max()} ms, wynik: {_routeDistances.Max()}");
            Console.WriteLine($"Minimum:  {_elapsedTimes.Min()} ms, wynik: {_routeDistances.Min()}");
        }
    }
}
