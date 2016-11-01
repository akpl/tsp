using System;
using System.Collections.Generic;

namespace TSP.Solver
{
    [Serializable]
    public class Target
    {
        public string Name { get; set; }
        public Coordinates Location { get; set; }
        public IDictionary<Target,double> Distances { get; set; }

        protected bool Equals(Target other)
        {
            return Location.Equals(other.Location);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Target)obj);
        }

        public override int GetHashCode()
        {
            return Location.GetHashCode();
        }
    }
}