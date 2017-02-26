using TSP.Solver;

namespace TSP
{
    public enum DistanceResolverType
    {
        GoogleMapsDistance,
        OSRMTime,
        OSRMDistance
    }

    public class Configuration
    {
        public DistanceResolverType Engine { get; set; } = DistanceResolverType.GoogleMapsDistance;
        public Target StartingPoint { get; set; } = null;
        public bool ShouldEndInStartingPoint { get; set; } = false;

        public int PopulationSize { get; set; } = 50;
        public int GenerationsCount { get; set; } = 100;
        public int TournamentSize { get; set; } = 5;
        public double MutationRate { get; set; } = 0.1;
        public bool IsElitismEnabled { get; set; } = true;
    }
}