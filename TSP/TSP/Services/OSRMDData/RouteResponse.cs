using System.Collections.Generic;

namespace TSP.Services.OSRMDData
{
    public class Leg
    {
        public List<object> steps { get; set; }
        public string summary { get; set; }
        public double duration { get; set; }
        public double distance { get; set; }
    }

    public class Route
    {
        public List<Leg> legs { get; set; }
        public double duration { get; set; }
        public double distance { get; set; }
    }

    public class Waypoint
    {
        public string hint { get; set; }
        public string name { get; set; }
        public List<double> location { get; set; }
    }

    public class RouteResponse
    {
        public string code { get; set; }
        public List<Route> routes { get; set; }
        public List<Waypoint> waypoints { get; set; }
    }
}