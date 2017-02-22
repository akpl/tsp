using TSP.Solver;

namespace TSP
{
    public class RoutingResult
    {
        public RoutingResult(Route route)
        {
            Route = route;
            Path = route.ToPolylinePath();
        }

        public Route Route { get; set; }
        public string Path { get; set; }
        public string Length { get; set; }
    }
}