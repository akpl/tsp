using System;
using System.Diagnostics;
using System.Linq;

namespace TSP.Solver
{
    public class GeneticSolver : ISolver
    {
        public int PopulationSize = 50;
        public int GenerationsCount = 100;
        public int TournamentSize = 5;
        public double MutationRate = 0.15;

        private readonly Random _random = new Random();

        public Route Solve(TargetsCollection targets)
        {
            var population = new Population(PopulationSize);
            population.InitialiseRandomRoutes(targets);
            double bestRandom = population.FindFittest().CalculateDistance();

            for (int generation = 0; generation < GenerationsCount; generation++)
            {
                population = Evolve(population);
            }
            double bestEvolved = population.FindFittest().CalculateDistance();

            return population.FindFittest();
        }

        private Population Evolve(Population population)
        {
            var evolvedPopulation = new Population(PopulationSize);

            for (int i = 0; i < evolvedPopulation.Size; i++)
            {
                Route parent1 = SelectFittest(population);
                Route parent2 = SelectFittest(population);

                Route child = OrderedCrossover(parent1, parent2);
                Mutate(child);
                evolvedPopulation[i] = child;
            }

            return evolvedPopulation;
        }

        private void Mutate(Route route)
        {
            for (int firstTarget = 0; firstTarget < route.Stops; ++firstTarget)
            {
                if (_random.NextDouble() < MutationRate)
                {
                    int secondTarget = _random.Next(0, route.Stops);
                    Target tempTarget = route[firstTarget];
                    route[firstTarget] = route[secondTarget];
                    route[secondTarget] = tempTarget;
                }
            }
        }

        /**
         * Tournament selection
         */
        private Route SelectFittest(Population population)
        {
            var tournamentPopulation = new Population(TournamentSize);
            for (int individualIndex = 0; individualIndex < tournamentPopulation.Size; individualIndex++)
            {
                int randomIndividualIndex = _random.Next(population.Size);
                tournamentPopulation[individualIndex] = population[randomIndividualIndex];
            }

            return tournamentPopulation.FindFittest();
        }

        /* 
         * Algorytm OX - Ordered Crossover
         */
        public Route OrderedCrossover(Route parent1, Route parent2)
        {
            if (parent1.Stops < 2 || parent1.Stops != parent2.Stops)
            {
                throw new ArgumentException("Invalid parent's size");
            }

            int parentSize = parent1.Stops;
            int crossoverStart = _random.Next(0, parentSize - 1);
            int crossoverEnd = _random.Next(0, parentSize - 1);
            Trace.WriteLine($"Crossover range: <{crossoverStart},{crossoverEnd}>");
            if (crossoverStart > crossoverEnd)
            {
                Swap(ref crossoverStart, ref crossoverEnd);
            }

            Target[] child = new Target[parentSize];
            var orderedList = parent2.ToList();
            for (int point = crossoverStart; point <= crossoverEnd; point++)
            {
                child[point] = parent1[point];
                orderedList.Remove(child[point]);
            }

            int orderedListPoint = 0;
            for (int childPoint = 0; childPoint < parentSize; childPoint++)
            {
                if (childPoint >= crossoverStart && childPoint <= crossoverEnd)
                {
                    continue;
                }

                child[childPoint] = orderedList[orderedListPoint];
                orderedListPoint++;
            }
            
            return new Route(child);
        }

        private static void Swap(ref int first, ref int second)
        {
            int temp = first;
            first = second;
            second = temp;
        }
    }
}
