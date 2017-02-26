using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TSP.Solver
{
    [Serializable]
    public class Target
    {
        [NonSerialized]
        private IDictionary<Target, double> _distances;

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public Coordinates Location { get; set; }
        [JsonIgnore]
        public IDictionary<Target, double> Distances
        {
            get { return _distances; }
            set { _distances = value; }
        }

        public double FindDistanceTo(Target target)
        {
            return Distances[target];
        }

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