using System;
using System.Collections.Generic;
using System.Linq;

namespace TSP.Solver
{
    internal class Population
    {
        private readonly Route[] _routes;
        public Population(int populationSize)
        {
            _routes = new Route[populationSize];
        }

        public void InitialiseRandomRoutes(IEnumerable<Target> targets, Target first, Target last)
        {
            var targetList = targets.ToList();

            for (int routeIndex = 0; routeIndex < Size; routeIndex++)
            {
                targetList.Shuffle();
                var randomRoute = new Route(targetList, first, last);
                _routes[routeIndex] = randomRoute;
            }
        }

        public Route this[int key]
        {
            get { return _routes[key]; }
            set { _routes[key] = value; }
        }

        public int Size => _routes.Length;

        public Route FindFittest()
        {
            return _routes.Min();
        }
    }
}