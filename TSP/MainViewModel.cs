using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MapControl;
using TSP.Solver;

namespace TSP
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            AddTargetCommand = new RelayCommand(AddTarget);
            Targets.Add(new Target { Name = "Rynek Główny" });
        }

        public TargetsCollection Targets { get; } = new TargetsCollection();
        public Target SelectedTarget { get; set; }
        public ObservableCollection<Marker> Markers { get; } = new ObservableCollection<Marker>();


        public ICommand AddTargetCommand { get; private set; }
        private void AddTarget()
        {
            Markers.Add(new Marker { Latitude = 50.061820, Longitude = 19.936709, Title = "Rynek" });

            SelectedTarget = new Target();
            Targets.Add(SelectedTarget);
        }
    }
}
