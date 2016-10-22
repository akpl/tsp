using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP.Solver;

namespace TSP
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            Targets.Add(new Target { Name = "A" });
        }

        public TargetsCollection Targets { get; set; } = new TargetsCollection();
        public Target SelectedTarget { get; set; }
    }
}
