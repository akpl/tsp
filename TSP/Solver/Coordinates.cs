using System;

namespace TSP.Solver
{
    public struct Coordinates
    {
        public Coordinates(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public readonly double Latitude;
        public readonly double Longitude;

        public override string ToString()
        {
            return string.Format("{0}°{1} {2}°{3}", 
                Math.Abs(Latitude), 
                Latitude >= 0 ? "N" : "S", 
                Math.Abs(Longitude),
                Longitude >= 0 ? "E" : "W");
        }

        public string ToDecimalDegreesString()
        {
            return string.Format("{0}, {1}", Latitude, Longitude);
        }
    }
}