using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Solver
{
    public class GeneticSolver : ISolver
    {
        private const int PopulationSize = 50;
        private const int GenerationsCount = 100;
        private const int TournamentSize = 5;
        private readonly Random _random = new Random();
        public Route Solve(TargetsCollection targets)
        {
            var initialPopulation = new Population(PopulationSize);
            initialPopulation.InitialiseRandomRoutes(targets);

            for (int generation = 0; generation < GenerationsCount; generation++)
            {
                Evolve(initialPopulation);
            }

            return null;
        }

        private void Evolve(Population population)
        {
            for (int i = 0; i < population.Size; i++)
            {
                Route parent1 = SelectFittest(population);
                Route parent2 = SelectFittest(population);

                Route child = PMXCrossover(parent1, parent2);
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
         * Algorytm PMX - Partially Mapped Crossover
         * Goldberg, Lingle (1985)
         */
        public Route PMXCrossover(Route parent1, Route parent2)
        {
            if (parent1.Stops < 2 || parent1.Stops != parent2.Stops)
            {
                throw new ArgumentException("Invalid parent's size");
            }

            int parentSize = parent1.Stops;
            int firstCrossover = 2; //_random.Next(1, parentSize - 2);
            int secondCrossover = 6; //_random.Next(1, parentSize - 2);
            Trace.WriteLine($"PMX range: <{firstCrossover},{secondCrossover}>");
            if (firstCrossover > secondCrossover)
            {
                Swap(ref firstCrossover, ref secondCrossover);
            }

            Target[] child = new Target[parentSize];
            var replacements = new Dictionary<Target, Target>();

            for (int point = firstCrossover; point <= secondCrossover; point++)
            {
                child[point] = parent2[point];
                replacements[parent2[point]] = parent1[point];
            }

            for (int point = 0; point < parentSize; point++)
            {
                if (point >= firstCrossover && point <= secondCrossover)
                {
                    continue;
                }

                if (replacements.ContainsKey(parent1[point]))
                {
                    child[point] = replacements[parent1[point]];
                }
                else
                {
                    child[point] = parent1[point];
                }
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
