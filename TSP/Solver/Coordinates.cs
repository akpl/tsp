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
            return $"{Latitude}, {Longitude}";
        }

        #region Equality members
        public bool Equals(Coordinates other)
        {
            return Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Coordinates && Equals((Coordinates)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Latitude.GetHashCode() * 397) ^ Longitude.GetHashCode();
            }
        }
        #endregion

    }
}