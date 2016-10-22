using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapControl
{
    [Serializable]
    public class Marker
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Title { get; set; }
    }
}
