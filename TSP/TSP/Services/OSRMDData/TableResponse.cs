using System.Collections.Generic;

namespace TSP.Services.OSRMDData
{
    public class Destination
    {
        public string hint { get; set; }
        public string name { get; set; }
        public List<double> location { get; set; }
    }

    public class Source
    {
        public string hint { get; set; }
        public string name { get; set; }
        public List<double> location { get; set; }
    }

    public class TableResponse
    {
        public string code { get; set; }
        public List<List<double>> durations { get; set; }
        public List<Destination> destinations { get; set; }
        public List<Source> sources { get; set; }
    }
}